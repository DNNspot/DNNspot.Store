using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DNNspot.Store.PaymentProviders;
using DotNetNuke.Services.Exceptions;
using WA.Extensions;

namespace DNNspot.Store.Modules.Checkout
{
    public partial class Checkout : StoreCheckoutModuleBase
    {
        protected DataModel.Order completedOrder;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int? completedOrderId = WA.Parser.ToInt(Request.Params["completedOrderId"]);
                if (completedOrderId.HasValue)
                {
                    Order order = new Order();
                    if (order.LoadByPrimaryKey(completedOrderId.Value))
                    {
                        ShowOrderComplete(order);
                    }
                }



                if (pnlAjaxPanels.Visible)
                {
                    FillListControlsForAjaxPanels();
                }
            }
        }

        private void FillListControlsForAjaxPanels()
        {
            //---- Shipping Options
            List<ShippingOption> shippingOptions = ShippingOptionCollection.GetList();

            //---- CC Types
            List<string> cardTypes = new List<string>(WA.Enum<CreditCardType>.GetNames());
            cardTypes.Remove(CreditCardType.UNKNOWN.ToString());
            ddlCCType.Items.Clear();
            ddlCCType.Items.AddRange(cardTypes.ConvertAll(s => new ListItem() { Text = s, Value = s }).ToArray());
            ddlCCType.Items.Insert(0, "");

            //---- CC Expire Year
            ddlCCExpireYear.Items.Clear();
            ddlCCExpireYear.Items.Add("");
            int maxYear = DateTime.Now.Year + 12;
            for(int y = DateTime.Now.Year; y <= maxYear; y++)
            {
                ddlCCExpireYear.Items.Add(new ListItem() { Text = y.ToString(), Value = y.ToString() });
            }

            //---- Payment Methods
            liPayPalExpressCheckoutPaymentMethod.Visible = StoreContext.CurrentStore.IsPaymentProviderEnabled(PaymentProviderName.PayPalExpressCheckout);
        }

        protected void btnReviewOrder_Click(object sender, EventArgs e)
        {
            ShowReviewOrder();
        }

        private CheckoutOrderInfo FillCheckoutOrderInfo()
        {
            CheckoutOrderInfo checkoutOrderInfo = new CheckoutOrderInfo();
            checkoutOrderInfo.PaymentProvider = PaymentProviderName.UNKNOWN;

            //---- Check for the PayPal Express Checkout return page variables            
            Dictionary<string, string> payPalVariables = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(Request.QueryString["token"]) && !string.IsNullOrEmpty(Request.QueryString["PayerID"]))
            {
                payPalVariables["token"] = Request.QueryString["token"];
                payPalVariables["PayerID"] = Request.QueryString["PayerID"];

                checkoutOrderInfo.PayPalVariables.Merge(payPalVariables);
                checkoutOrderInfo.PaymentProvider = PaymentProviderName.PayPalExpressCheckout;
            }
            else
            {
                // user-selected payment method (credit card or PayPal)
                string userSelectedPaymentMethod = Request.Form["paymentMethod"] ?? "";
                if (userSelectedPaymentMethod == "creditCard")
                {
                    checkoutOrderInfo.PaymentProvider = StoreContext.CurrentStore.GetOnsiteCreditCardPaymentProvider();
                }
            }

            if (checkoutOrderInfo.PaymentProvider == PaymentProviderName.UNKNOWN)
            {
                throw new ApplicationException("Unable to determine PaymentProvider for CheckoutOrderInfo!");
            }

            //---- Cart
            checkoutOrderInfo.Cart = cart;

            //---- Billing/Shipping Address
            // TODO - sometimes we need to grab billing/shipping info from Payment Provider (e.g. PayPal Express Checkout)
            AddressInfo billingAddress = billingAddressForm.GetPostedAddressInfo();
            checkoutOrderInfo.BillingAddress = billingAddress;

            AddressInfo shippingAddress = shippingAddressForm.GetPostedAddressInfo();
            checkoutOrderInfo.ShippingAddress = shippingAddress;

            //---- Credit Card Info
            CreditCardInfo creditCard = new CreditCardInfo()
            {
                CardType = WA.Enum<CreditCardType>.TryParseOrDefault(ddlCCType.SelectedValue, CreditCardType.UNKNOWN),
                CardNumber = txtCCNumber.Text,
                ExpireMonth = WA.Parser.ToShort(ddlCCExpireMonth.SelectedValue),
                ExpireYear = WA.Parser.ToShort(ddlCCExpireYear.SelectedValue),
                NameOnCard = txtCCNameOnCard.Text,
                SecurityCode = WA.Parser.ToShort(txtCCSecurityCode.Text)
            };
            checkoutOrderInfo.CreditCard = creditCard;

            // TODO - apply the shipping type the user selected, recalculate discounts too?

            //--- Stick the CheckoutOrderInfo into the session so we can grab it when the user clicks "Place Order"           
            Session[SessionKey_CheckoutOrderInfo] = checkoutOrderInfo;

            return checkoutOrderInfo;
        }

        private void ShowReviewOrder()
        {
            CheckoutOrderInfo checkoutOrderInfo = FillCheckoutOrderInfo();

            //--- DataBind the cart items
            List<vCartItem> cartItems = checkoutOrderInfo.Cart.GetCartViewItems();
            rptCheckoutItems.DataSource = cartItems;
            rptCheckoutItems.DataBind();

            //--- Billing/Shipping Summaries
            litBillToSummary.Text = checkoutOrderInfo.BillingAddress.ToHumanFriendlyString("<br />");
            litShipToSummary.Text = checkoutOrderInfo.ShippingAddress.ToHumanFriendlyString("<br />");

            // TODO Payment Summary ??

            // TODO Discount(s) Summary ??

            pnlReviewOrder.Visible = true;
            pnlAjaxPanels.Visible = false;            
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {            
            PlaceOrder();
        }

        private void PlaceOrder()
        {
            CheckoutOrderInfo checkoutOrderInfo = Session[SessionKey_CheckoutOrderInfo] as CheckoutOrderInfo;
            if (checkoutOrderInfo != null)
            {
                try
                {
                    OrderController orderController = new OrderController(StoreContext);
                    Order submittedOrder = null;

                    if (checkoutOrderInfo.PaymentProvider == PaymentProviderName.PayPalExpressCheckout)
                    {
                        //---- PayPal Express Checkout                        
                        submittedOrder = orderController.CompleteExpressCheckoutPayment(checkoutOrderInfo.PayPalVariables);
                    }
                    else
                    {
                        //---- Non-PayPal Process
                        submittedOrder = orderController.CheckoutWithPayment(checkoutOrderInfo);
                    }
         
                    if ((submittedOrder != null) && (submittedOrder.OrderStatus == OrderStatusName.Completed))
                    {
                        //---- Send Order Email(s)
                        EmailController emailController = new EmailController(StoreContext);
                        Dictionary<string, string> emailTokens = orderController.GetTemplateTokens(submittedOrder);

                        string adminEmailResponse = emailController.SendEmailTemplate(EmailTemplateNames.OrderCompletedAdmin, emailTokens, StoreContext.CurrentStore.GetSetting(StoreSettingNames.OrderCompletedEmailRecipient));
                        string emailResponse = emailController.SendEmailTemplate(EmailTemplateNames.OrderCompleted, emailTokens, submittedOrder.CustomerEmail);

                        if(!string.IsNullOrEmpty(emailResponse))
                        {
                            msgFlash.Visible = true;
                            msgFlash.InnerHtml = string.Format(@"Oh no! Something went wrong when we tried to email your order receipt. {0}", emailResponse);

                            try
                            {
                                LogToDnnEventLog(string.Format(@"SEND ORDER EMAIL ERROR. PortalId: {0} StoreId: {1} OrderId : {2}. Email Error: {3}", PortalId, StoreContext.CurrentStore.Id, submittedOrder.Id, emailResponse));
                                LogToDnnEventLog(submittedOrder);
                            }
                            catch (Exception ex)
                            {
                                // eat it
                            }                               
                        }

                        ShowOrderComplete(submittedOrder);

                        Session.Remove(SessionKey_CheckoutOrderInfo);
                        cartController.EmptyCart();                                                    
                    }
                    else
                    {
                        string paymentError = "";
                        PaymentTransaction paymentTransaction = submittedOrder.GetMostRecentPaymentTransaction();
                        if (paymentTransaction != null)
                        {
                            paymentError = string.Format("Payment Error: <p>{0}</p> <p>{1}</p>", paymentTransaction.GatewayError, paymentTransaction.GatewayRawResponse);
                        }
                        msgFlash.Visible = true;
                        msgFlash.InnerHtml = string.Format(@"Oh no! Something went wrong when we tried to process your order. {0}", paymentError);

                        try
                        {
                            LogToDnnEventLog(string.Format(@"CHECKOUT ERROR. PortalId: {0} StoreId: {1} OrderId : {2}. Payment Error: {3}", PortalId, StoreContext.CurrentStore.Id, submittedOrder.Id, paymentError));
                            LogToDnnEventLog(submittedOrder);
                        }
                        catch (Exception ex)
                        {
                            // eat it
                        }
                    }                        
                    
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);

                    msgFlash.Visible = true;
                    msgFlash.InnerHtml = string.Format(@"Oops! An unexpected error has occurred while trying to process your order. Error: {0} {1}", ex.Message, ex.StackTrace);
                }
            }
            else
            {
                throw new ModuleLoadException("CheckoutOrderInfo is NULL in the Session!");
            }            
        }

        private void ShowOrderComplete(Order completedOrder)
        {
            this.completedOrder = completedOrder;

            stepsNav.Visible = false;
            pnlOrderComplete.Visible = true;
            pnlAjaxPanels.Visible = false;
            pnlReviewOrder.Visible = false;
        }
    }
}