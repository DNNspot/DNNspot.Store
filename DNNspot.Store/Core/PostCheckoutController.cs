/*
* This software is licensed under the GNU General Public License, version 2
* You may copy, distribute and modify the software as long as you track changes/dates of in source files and keep all modifications under GPL. You can distribute your application using a GPL library commercially, but you must also provide the source code.

* DNNspot Software (http://www.dnnspot.com)
* Copyright (C) 2013 Atriage Software LLC
* Authors: Kevin Southworth, Matthew Hall, Ryan Doom

* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; either version 2
* of the License, or (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program; if not, write to the Free Software
* Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

* Full license viewable here: http://www.gnu.org/licenses/gpl-2.0.txt
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using DNNspot.Store.DataModel;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Exceptions;

namespace DNNspot.Store
{
    public class PostCheckoutController
    {
        StoreContext storeContext;

        internal PostCheckoutController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        internal CheckoutResult DoPostCheckoutProcessing(Order submittedOrder, bool sendEmails)
        {
            CheckoutResult checkoutResult = new CheckoutResult();
            checkoutResult.SubmittedOrder = submittedOrder;

            if (submittedOrder != null)
            {
                bool paymentStatusOk = (submittedOrder.PaymentStatus == PaymentStatusName.Completed) || (submittedOrder.PaymentStatus == PaymentStatusName.Pending);
                bool orderStatusOk = (submittedOrder.OrderStatus == OrderStatusName.Completed) || (submittedOrder.OrderStatus == OrderStatusName.Processing);

                if (paymentStatusOk && orderStatusOk)
                {
                    //---- Order SUCCESS                        
                    DoPostCheckoutSuccess(checkoutResult, submittedOrder, sendEmails);
                }
                else if (!paymentStatusOk)
                {
                    checkoutResult.Errors.Add(string.Format(@"Payment Failed. Your order status is: {0}", submittedOrder.OrderStatus));
                    PaymentTransaction paymentTransaction = submittedOrder.GetMostRecentPaymentTransaction();
                    if (paymentTransaction != null && !string.IsNullOrEmpty(paymentTransaction.GatewayError))
                    {
                        checkoutResult.Errors.Add(string.Format(@" Payment Error: {0}.", paymentTransaction.GatewayError));
                    }
                }
                else if (!orderStatusOk)
                {
                    checkoutResult.Errors.Add(string.Format(@"Unable to complete your order. Your order status is: {0}", submittedOrder.OrderStatus));
                }
            }
            else
            {
                checkoutResult.Errors.Add("Submitted Order was NULL");
            }

            return checkoutResult;            
        }

        private void DoPostCheckoutSuccess(CheckoutResult checkoutResult, Order submittedOrder, bool sendEmails)
        {
            //--- Update Inventory
            foreach(var orderItem in submittedOrder.OrderItemCollectionByOrderId)
            {
                var product = orderItem.UpToProductByProductId;
                if (product.InventoryIsEnabled.GetValueOrDefault(false))
                {
                    product.InventoryQtyInStock = product.InventoryQtyInStock - orderItem.Quantity;
                    product.Save();
                }
            }

            //List<Product> productsOrdered = submittedOrder.GetOrderItemProducts();
            //foreach(Product p in productsOrdered)
            //{                
            //    if(p.InventoryIsEnabled.GetValueOrDefault(false))
            //    {
            //        Product temp = new Product();
            //        if (temp.LoadByPrimaryKey(p.Id.Value))
            //        {
            //            temp.InventoryQtyInStock--;
            //            temp.Save();
            //        }
            //    }
            //}            

            //--- Clear the Checkout SESSION
            HttpContext.Current.Session.Remove(storeContext.SessionKeys.CheckoutOrderInfo);
            
            //--- Delete the Cart
            CartController cartController = new CartController(storeContext);
            //cartController.DeleteCart();
            cartController.DeleteCartForOrder(submittedOrder);

            try
            {
                //--- Add DNN Roles to user if order is PAID
                if (submittedOrder.PaymentStatus == PaymentStatusName.Completed)
                {
                    AddUserToDnnRoles(submittedOrder);
                }

                TokenHelper tokenHelper = new TokenHelper(storeContext);
                Dictionary<string, string> orderTokens = tokenHelper.GetOrderTokens(submittedOrder, false);

                string urlToPostOrder = storeContext.CurrentStore.GetSetting(StoreSettingNames.UrlToPostCompletedOrder);
                PostOrderTokensToUrl(checkoutResult, orderTokens, urlToPostOrder);

                if (sendEmails)
                {
                    Dictionary<string, string> orderEmailTokens = tokenHelper.GetOrderTokens(submittedOrder, true);
                    SendOrderEmails(checkoutResult, submittedOrder, orderEmailTokens);
                }
            }
            catch(Exception ex)
            {
                checkoutResult.Warnings.Add(ex.Message);

                Exceptions.LogException(ex);
            }
        }

        internal static void AddUserToDnnRoles(Order submittedOrder)
        {
            try
            {
                if (submittedOrder.UserId.HasValue && submittedOrder.UpToStoreByStoreId.PortalId.HasValue)
                {
                    int userId = submittedOrder.UserId.Value;                    
                    int portalId = submittedOrder.UpToStoreByStoreId.PortalId.Value;

                    //--- get the RoleId's from the products the user purchased
                    List<Product> orderProducts = submittedOrder.GetOrderItemProducts();
                    List<CheckoutRoleInfo> checkoutRoleInfos = new List<CheckoutRoleInfo>();
                    foreach (Product p in orderProducts)
                    {
                        if(!string.IsNullOrEmpty(p.CheckoutAssignRoleInfoJson))
                        {
                            checkoutRoleInfos.AddRange(p.GetCheckoutRoleInfos());
                        }
                    }
                    RoleController roleController = new RoleController();
                    foreach (CheckoutRoleInfo checkoutRoleInfo in checkoutRoleInfos)
                    {
                        DateTime newEffectiveDate = Null.NullDate;
                        DateTime newExpireDate = Null.NullDate;
                        int addExpireDays = checkoutRoleInfo.ExpireDays.GetValueOrDefault(0);

                        // check if the user is already in this role...
                        UserRoleInfo userRoleInfo = roleController.GetUserRole(portalId, userId, checkoutRoleInfo.RoleId);
                        if (userRoleInfo != null)
                        {
                            // they have this role already

                            // grab the existing effective date
                            if (userRoleInfo.EffectiveDate != Null.NullDate)
                            {
                                newEffectiveDate = userRoleInfo.EffectiveDate;
                            }

                            // grab the current expire date
                            if (userRoleInfo.ExpiryDate != Null.NullDate)
                            {
                                newExpireDate = userRoleInfo.ExpiryDate.AddDays(addExpireDays);
                            }
                        }
                        else
                        {
                            // they don't have this role yet
                            newEffectiveDate = DateTime.Today;
                            newExpireDate = DateTime.Now.AddDays(addExpireDays);
                        }

                        roleController.AddUserRole(portalId, userId, checkoutRoleInfo.RoleId, newEffectiveDate, newExpireDate);                            
                    }

                    //// Clear the user's cached/stored role membership, will reload on next page cycle
                    //PortalSecurity.ClearRoles();
                    //DataCache.ClearUserCache(portalId, HttpContext.Current.User.Identity.Name);                    
                }
            }
            catch (Exception ex)
            {                
                Exceptions.LogException(ex);
            }
        }

        private void PostOrderTokensToUrl(CheckoutResult checkoutResult, Dictionary<string, string> orderTokens, string urlToPostOrder)
        {            
            if (!string.IsNullOrEmpty(urlToPostOrder))
            {
                try
                {
                    orderTokens["store.guid"] = storeContext.CurrentStore.StoreGuid.Value.ToString();

                    HttpWebResponse postResponse = HttpHelper.HttpPost(urlToPostOrder, orderTokens);
                    string responseString = HttpHelper.WebResponseToString(postResponse);

                    if(postResponse.StatusCode != HttpStatusCode.OK || (!string.IsNullOrEmpty(responseString) && !responseString.StartsWith("OK")))
                    {
                        checkoutResult.Warnings.Add(string.Format(@"Something went wrong when we tried to POST your order. Error: {0} {1}", responseString, postResponse.StatusDescription));
                    }
                }
                catch(WebException webEx)
                {
                    string response = "";
                    if (webEx.Response != null)
                    {
                        StreamReader reader = new StreamReader(webEx.Response.GetResponseStream());
                        response = reader.ReadToEnd();
                    }

                    checkoutResult.Warnings.Add(string.Format(@"Something went wrong when we tried to POST your order. Exception: {0}. Response: {1}", webEx.Message, response));
                }
                catch (Exception postEx)
                {
                    // NOTE - should a failure here be propagated up to the user?
                    //checkoutResult.Errors.Add(string.Format(@"Something went wrong when we tried to POST your order to ""{0}"". Error: {1}", urlToPostOrder, postEx.Message));

                    Exceptions.LogException(postEx);
                }
            }
        }

        private void SendOrderEmails(CheckoutResult checkoutResult, Order submittedOrder, Dictionary<string, string> orderTokens)
        {
            try
            {
                EmailController emailController = new EmailController();                

                string adminEmailResponse = emailController.SendEmailTemplate(EmailTemplateNames.OrderReceivedAdmin, orderTokens, storeContext.CurrentStore.GetSetting(StoreSettingNames.OrderCompletedEmailRecipient), storeContext.CurrentStore);
                if (WA.Parser.ToBool(storeContext.CurrentStore.GetSetting(StoreSettingNames.SendOrderReceivedEmail)).GetValueOrDefault(true))
                {
                    string emailResponse = emailController.SendEmailTemplate(EmailTemplateNames.OrderReceived, orderTokens, submittedOrder.CustomerEmail, storeContext.CurrentStore);

                    if (!string.IsNullOrEmpty(emailResponse))
                    {
                        checkoutResult.Warnings.Add(string.Format(@"Something went wrong when we tried to email your order receipt. {0}", emailResponse));
                    }
                }

            }
            catch (Exception ex)
            {
                checkoutResult.Warnings.Add(string.Format(@"Something went wrong when we tried to email your order receipt. {0}", ex.Message));
            }
        }
    }
}
