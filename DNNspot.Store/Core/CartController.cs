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
using System.Linq;
using System.Web;
using DNNspot.Store.DataModel;
using EntitySpaces.Interfaces;

namespace DNNspot.Store
{
    public class CartController
    {
        readonly StoreContext storeContext;

        public CartController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        public DataModel.Cart GetCart(bool createIfNotExists)
        {
            return GetCartFromDatabase(createIfNotExists);
        }

        private DataModel.Cart GetCartFromDatabase(bool createIfNotExists)
        {
            DataModel.Cart cart = new DataModel.Cart();
            if(!cart.LoadByPrimaryKey(storeContext.CartId) && createIfNotExists)
            {
                // no cart in DB yet, create one...
          
                cart.Id = storeContext.CartId;
                cart.StoreId = storeContext.CurrentStore.Id.Value;
                if (storeContext.UserId.HasValue)
                {
                    cart.UserId = storeContext.UserId.Value;
                }

                cart.Save();
            }

            return cart;
        }

        public void AddProductToCart(int productId, int productQty, string jsonProductFieldData)
        {
            //UpdateCartItemInCart(null, productId, productQty, true, jsonProductFieldData);

            DataModel.Cart cart = null;

            using (esTransactionScope transaction = new esTransactionScope())
            {
                cart = GetCartFromDatabase(true);

                CartItemCollection cartItemCollection = cart.CartItemCollectionByCartId;
                List<CartItem> cartItems = cartItemCollection.ToList();                
                int index = cartItems.FindIndex(ci => (ci.ProductId.Value == productId) && (ci.ProductFieldData == jsonProductFieldData));
                if(index >= 0)    
                {
                    // product is in the cart
                    if (productQty <= 0)
                    {
                        // remove from cart
                        cartItemCollection[index].MarkAsDeleted();
                    }
                    else
                    {
                        // add/update quantity                
                        cartItemCollection[index].Quantity += productQty;

                        // update ProductFieldData
                        if (!string.IsNullOrEmpty(jsonProductFieldData))
                        {
                            cartItemCollection[index].ProductFieldData = jsonProductFieldData;
                        }
                    }
                }
                else if (productQty > 0)
                {
                    // add to cart
                    CartItem newItem = cartItemCollection.AddNew();
                    newItem.ProductId = productId;
                    newItem.Quantity = productQty;
                    newItem.ProductFieldData = jsonProductFieldData;
                }                

                //---- update some cart fields too...
                if (storeContext.UserId.HasValue)
                {
                    cart.UserId = storeContext.UserId.Value;
                }

                cart.Save();

                transaction.Complete();
            }

            //return cart;
        }

        public void RemoveCartItemFromCart(int cartItemId)
        {
            //UpdateCartItemInCart(cartItemId, null, 0, false, string.Empty);

            DataModel.Cart cart = GetCartFromDatabase(false);
            
            CartItemCollection cartItemCollection = cart.CartItemCollectionByCartId;
            CartItem toDelete = cartItemCollection.FindByPrimaryKey(cartItemId);
            if (toDelete != null)
            {
                toDelete.MarkAsDeleted();

                cart.Save();
            }
        }

        public void UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            //UpdateCartItemInCart(cartItemId, null, quantity, false, string.Empty);

            using (esTransactionScope transaction = new esTransactionScope())
            {
                DataModel.Cart cart = GetCartFromDatabase(false);

                CartItemCollection cartItems = cart.CartItemCollectionByCartId;
                cartItems.Filter = cartItems.AsQueryable().Where(x => x.Id == cartItemId);
                //cartItems.Filter = CartItemMetadata.ColumnNames.Id + " = " + cartItemId;

                if (cartItems.Count > 0)
                {
                    // item is in the cart
                    if (quantity <= 0)
                    {
                        // remove from cart
                        cartItems[0].MarkAsDeleted();
                    }
                    else
                    {               
                        cartItems[0].Quantity = quantity;                                  
                    }
                }
                //cartItems.Filter = "";
                cartItems.Filter = null;

                //---- update some cart fields too...
                if (storeContext.UserId.HasValue)
                {
                    cart.UserId = storeContext.UserId.Value;
                }

                cart.Save();

                transaction.Complete();
            }            
        }

        internal void DeleteCart()
        {
            Cart cart = GetCartFromDatabase(false);
            if (cart.es.HasData)
            {
                cart.MarkAsDeleted();
                cart.Save();
            }
            storeContext.RemoveCookieCartId();
        }

        internal void DeleteCartForOrder(Order order)
        {
            if(order.CreatedFromCartId.HasValue)
            {
                var cart = Cart.GetCart(order.CreatedFromCartId.Value);
                if(cart != null)
                {
                    cart.MarkAsDeleted();
                    cart.Save();
                }
            }
            storeContext.RemoveCookieCartId();
        }

        //private DataModel.Cart UpdateCartItemInCart(int? cartItemId, int? productId, int quantity, bool addToExistingQuantity, string jsonProductFieldData)
        //{
        //    DataModel.Cart cart = null;

        //    using (esTransactionScope transaction = new esTransactionScope())
        //    {
        //        cart = GetCartFromDatabase(true);

        //        CartItemCollection cartItems = cart.CartItemCollectionByCartId;
        //        cartItems.Filter = CartItemMetadata.ColumnNames.ProductId + " = " + productId;
        //        if (cartItems.Count > 0)
        //        {
        //            // product is in the cart
        //            if (quantity <= 0)
        //            {
        //                // remove from cart
        //                cartItems[0].MarkAsDeleted();
        //            }
        //            else
        //            {
        //                // add/update quantity
        //                if (addToExistingQuantity)
        //                {
        //                    cartItems[0].Quantity += quantity;
        //                }
        //                else
        //                {
        //                    cartItems[0].Quantity = quantity;
        //                }
        //                // update ProductFieldData
        //                if (!string.IsNullOrEmpty(jsonProductFieldData))
        //                {
        //                    cartItems[0].ProductFieldData = jsonProductFieldData;
        //                }
        //            }
        //        }
        //        else if (quantity > 0)
        //        {
        //            // add to cart
        //            CartItem newItem = cartItems.AddNew();
        //            newItem.ProductId = productId;
        //            newItem.Quantity = quantity;                    
        //            newItem.ProductFieldData = jsonProductFieldData;
        //        }
        //        cartItems.Filter = "";

        //        //---- update some cart fields too...
        //        if (storeContext.UserId.HasValue)
        //        {
        //            cart.UserId = storeContext.UserId.Value;
        //        }
                
        //        cart.Save();
                
        //        transaction.Complete();
        //    }

        //    return cart;
        //}
    }
}
