<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cart.ascx.cs" Inherits="DNNspot.Store.Modules.Cart.Cart" %>
<div class="dstore cart">
    <h2><asp:Literal ID="litNameOfCart" runat="server"></asp:Literal></h2>
    <asp:Literal runat="server" ID="flash2"></asp:Literal>
    <div id="flash" runat="server" class="flash" visible="false" />
    <a href="<%= StoreUrls.CategoryHome() %>">&laquo; Continue Shopping</a>
    <asp:Panel ID="pnlEmptyCart" runat="server">
        <p>
            Your cart is currently empty.</p>
    </asp:Panel>
    <asp:Panel ID="pnlCart" runat="server">
        <table class="cartItems" cellpadding="0" cellspacing="0">
            <thead>
                <tr>
                    <th colspan="2">
                        Product
                    </th>
                    <th class="colPrice">
                        Price
                    </th>
                    <th class="colQty">
                        Qty.
                    </th>
                    <th class="colSubtotal">
                        Subtotal
                    </th>
                    <th class="colRemove">
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptCartItems" runat="server">
                    <ItemTemplate>
                        <tr <%# (Container.ItemIndex % 2 == 1 ? "class=\"alt\"" : "") %>>
                            <td class="colImage">
                                <%# string.Format(@"<img src=""{0}"" />", StoreUrls.ProductPhoto((Container.DataItem as vCartItemProductInfo).MainPhotoFilename, 35, 35)) %>
                            </td>
                            <td class="colProduct">
                                <a href="<%# StoreUrls.Product((Container.DataItem as vCartItemProductInfo).ProductSlug) %>">
                                    <%# (Container.DataItem as vCartItemProductInfo).ProductName %></a>
                                <%# (Container.DataItem as vCartItemProductInfo).GetProductFieldDataDisplayString() %>
                            </td>
                            <td class="colPrice">
                                <%# StoreContext.CurrentStore.FormatCurrency((Container.DataItem as vCartItemProductInfo).GetPriceForSingleItem()) %>
                            </td>
                            <td class="colQty">
                                <input type="text" name="<%# cartItemQtyInputNamePrefix + (Container.DataItem as vCartItemProductInfo).Id %>"
                                    autocomplete="off" class="quantity" value="<%# (Container.DataItem as vCartItemProductInfo).Quantity %>" />
                                <asp:LinkButton ID="lbtnUpdateQty" runat="server" OnClick="lbtnUpdateQty_Click" CssClass="updateQty">update</asp:LinkButton>
                            </td>
                            <td class="colSubtotal">
                                <%# StoreContext.CurrentStore.FormatCurrency((Container.DataItem as vCartItemProductInfo).GetPriceForQuantity())%>
                            </td>
                            <td class="colRemove">
                                <a href="<%# StoreUrls.CartRemoveCartItem((Container.DataItem as vCartItemProductInfo).Id.Value) %>"
                                    onclick="return confirm('Are you sure you want to remove this product from your cart?');"
                                    title="remove from cart">
                                    <img src="<%= ModuleRootImagePath %>icons/cross.png" title="remove from cart" alt="remove from cart" /></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="6">
                        <asp:Panel ID="pnlShippingQuoteForm" runat="server" DefaultButton="btnEstimateShipping">
                            <div id="shippingQuoteForm" class="checkoutInputBox">
                                <h5>
                                    Estimate Shipping</h5>
                                <asp:TextBox ID="txtShippingEstimateZip" runat="server" class="watermark" title="ZIP/Postal Code"></asp:TextBox>
                                <asp:Button ID="btnEstimateShipping" runat="server" Text="&raquo;" CssClass="raquoButton dnnPrimaryAction" OnClick="btnEstimateShipping_Click" />
                                <div>
                                    <asp:CheckBox id="chkShippingAddressIsBusiness" runat="server" Checked="false" Text="Business address" />
                                </div>
                                <asp:RadioButtonList ID="rblShippingRateEstimates" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblShippingRateEstimates_SelectedIndexChanged">
                                </asp:RadioButtonList>
                                <%--<asp:Repeater ID="rptShippingRateEstimates" runat="server">
                                    <HeaderTemplate>
                                        <table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# (Container.DataItem as ShippingRate).DisplayName %></td>
                                            <td class="money"><%# (Container.DataItem as IShippingRate).Rate > 0 ? StoreContext.CurrentStore.FormatCurrency((Container.DataItem as IShippingRate).Rate) : "Free" %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>        --%>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlCouponCodeForm" runat="server" DefaultButton="btnApplyCouponCode">
                            <div id="couponCodeForm" class="checkoutInputBox">
                                <h5>
                                    Apply Coupon Code</h5>
                                <asp:TextBox ID="txtCouponCode" runat="server" class="watermark" title="Enter Code"></asp:TextBox>
                                <asp:Button ID="btnApplyCouponCode" runat="server" Text="&raquo;" CssClass="dnnPrimaryAction raquoButton"
                                    OnClick="btnApplyCouponCode_Click" />
                                <div id="couponStatusMessage" runat="server" class="couponStatusMessage" visible="false" />
                            </div>
                        </asp:Panel>
                        <div class="priceSummary">
                            <table cellpadding="0" cellspacing="0">
                                <tr class="subtotal">
                                    <td>
                                        Subtotal
                                    </td>
                                    <td class="money">
                                        <%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.SubTotal)%>
                                    </td>
                                </tr>
                                <% if (checkoutOrderInfo.ShippingRate.Rate > 0)
                                   { %>
                                <tr class="shipping">
                                    <td>
                                        Shipping &amp; Handling
                                    </td>
                                    <td class="money">
                                        <%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.ShippingRate.Rate)%>
                                    </td>
                                </tr>
                                <% } %>
                                <% if (checkoutOrderInfo.DiscountAmount > 0)
                                   { %>
                                <tr class="discount">
                                    <td>
                                        Discount <span class="appliedCoupons">
                                            <%= checkoutOrderInfo.GetAppliedCoupons().ConvertAll(c => c.CouponCode).ToDelimitedString(", ") %></span>
                                    </td>
                                    <td class="money">
                                        (<%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.DiscountAmount)%>)
                                    </td>
                                </tr>
                                <% } %>
                                <% if (checkoutOrderInfo.TaxAmount > 0)
                                   { %>
                                <tr class="tax">
                                    <td>
                                        Tax
                                    </td>
                                    <td class="money">
                                        <%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.TaxAmount)%>
                                    </td>
                                </tr>
                                <% } %>
                                <tr class="total">
                                    <td>
                                        Total
                                    </td>
                                    <td class="money">
                                        <%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.Total)%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="checkoutButtons">
                            <asp:Button ID="btnCheckoutOnsite" CssClass="dnnPrimaryAction btnCheckoutOnSite" runat="server" OnClick="btnCheckoutOnsite_Click" />
                            <span id="spnOr" runat="server" visible="false" style="display: block; clear: both;
                                float: right; width: 145px; text-align: center;">or</span>
                            <asp:ImageButton ID="btnCheckoutPayPalStandard" runat="server" AlternateText="Check out with PayPal Standard"
                                title="Check out with PayPal Standard" ImageUrl="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif"
                                OnClientClick="return promptForPayPalStdShipping();" OnClick="btnCheckoutPayPalStandard_Click" />
                            <asp:ImageButton ID="ibtnPayPalExpressCheckout" runat="server" AlternateText="Check out with PayPal Express"
                                title="Check out with PayPal Express" ImageUrl="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif"
                                OnClick="ibtnPayPalExpressCheckout_Click" />
                        </div>
                    </td>
                </tr>
            </tfoot>
        </table>
        <div id="paypalStdShippingDialog" style="display: none;">
            <div class="shippingError"></div>
            <div class="collectAddress">
                <h5>
                    Please enter your Shipping Address</h5>
                <ol class="form">
                    <li style="display: none;">
                        <label>
                            Address:</label>
                        <span>
                            <input type="text" name="paypalStdAddress1" />
                            <br />
                            <input type="text" name="paypalStdAddress2" />
                        </span></li>
                    <li style="display: none;">
                        <label>
                            City:</label>
                        <span>
                            <input type="text" name="paypalStdCity" />
                        </span></li>
                    <li>
                        <label>
                            Country: *</label>
                        <span>
                            <select name="paypalStdCountry">
                                <asp:Literal runat="server" ID="litCountryOptionsHtml"></asp:Literal>
                            </select>
                        </span></li>
                    <li>
                        <label>
                            Region Code: *<br />
                            <span style="font-size: 10px;">Example: NY</span></label>
                        <span>
                            <input type="text" name="paypalStdRegion" style="width: 40px;" />
                        </span></li>
                    <li>
                        <label>
                            Postal Code: *</label>
                        <span>
                            <input id="paypalStdPostalCode" type="text" name="paypalStdPostalCode" />
                        </span></li>
                    <li>
                        <div>
                            <label>Business address
                                <input name="paypalStdIsBusiness" type="checkbox" />
                            </label>
                        </div>    
                    </li>
                    <li><span>
                        <input type="button" value="Continue &raquo;" onclick="showPayPalStdShippingOptions(this);" />
                        &nbsp;&nbsp;&nbsp; <a href="#" onclick="jQuery('#paypalStdShippingDialog').toggle(); return false;">
                            Cancel</a> </span></li>
                </ol>
            </div>
            <div class="chooseShippingOption" style="display: none;">
                <h5>
                    Please choose a Shipping Option</h5>
                <ol class="form">
                    <li>
                        <label>
                        </label>
                        <span>
                            <select id="ddlPaypalStdShippingOption" name="paypalStdShippingCost">
                            </select>
                        </span></li>
                    <li><span>
                        <asp:ImageButton ID="btnCheckoutPayPalStandardWithShipping" runat="server" AlternateText="Check out with PayPal Standard"
                            title="Check out with PayPal Standard" ImageUrl="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif"
                            OnClick="btnCheckoutPayPalStandardWithShipping_Click" />
                    </span></li>
                </ol>
            </div>
        </div>
        <script type="text/javascript">
            var $cartItems = null;
            var $btnPayPalStdWithShipping = null;
            var urlToPayPalPostCart = '<%= string.Format("{0}PayPal/PayPalStandardPostCart.aspx?PortalId={1}", ModuleRootWebPath, PortalId) %>';

            jQuery(function ($) {
                $cartItems = jQuery('table.cartItems');

                jQuery('input.quantity', $cartItems).focus(function () {
                    jQuery(this).select();
                    jQuery(this).next().show();
                });

                jQuery('input.quantity', $cartItems).keypress(function (e) {
                    if (e.which == 13) { // enter key
                        e.preventDefault();
                        e.stopPropagation();

                        var $updateAnchor = jQuery(this).next('a');
                        var postBackCall = $updateAnchor.attr('href').replace('javascript:', '');
                        eval(postBackCall);
                    }
                });

                //jQuery.updnWatermark.attachAll();

                jQuery('.watermark').each(function () {
                    var $this = jQuery(this);
                    $this.watermark($this.attr('title'));
                });

                $btnPayPalStdWithShipping = jQuery('#<%= btnCheckoutPayPalStandardWithShipping.ClientID %>');
                $btnPayPalStdWithShipping.click(function (e) {
                    var shipCost = jQuery('#ddlPaypalStdShippingOption').val();
                    if (shipCost == '') {
                        e.preventDefault();
                        e.stopPropagation();                        
                        alert('Please choose a shipping option');
                        return false;
                    }
                });

            });

            function promptForPayPalStdShipping() {
                var zipCode = jQuery("#<%= txtShippingEstimateZip.ClientID %>").val();
                if(zipCode != 'ZIP/Postal Code')
                {
                    jQuery("#paypalStdPostalCode").val(zipCode);
                }

                if(<%= collectPayPalStandardShipping.ToString().ToLower() %>) {
                    showPayPalStdShippingForm();
                    return false;
                } else {
                    return true;
                }
            }

            function showPayPalStdShippingForm() {
                var $dialog = jQuery('#paypalStdShippingDialog');
                $dialog.show();

                jQuery('.collectAddress', $dialog).show();
                jQuery('.chooseShippingOption', $dialog).hide();
            }

            function showPayPalStdShippingOptions(btn) {
                jQuery(".shippingError").hide();
                var $dialog = jQuery('#paypalStdShippingDialog');
                var address1 = jQuery(':input[name="paypalStdAddress1"]', $dialog).val();
                var address2 = jQuery(':input[name="paypalStdAddress1"]', $dialog).val();
                var city = jQuery(':input[name="paypalStdCity"]', $dialog).val();
                var country = jQuery(':input[name="paypalStdCountry"]', $dialog).val();
                var region = jQuery(':input[name="paypalStdRegion"]', $dialog).val();
                var postalCode = jQuery(':input[name="paypalStdPostalCode"]', $dialog).val();
                var isBusiness = jQuery(':input[name="paypalStdIsBusiness"]', $dialog).is(":checked");


                if(country == '' || region == '' || postalCode == '') {
                    alert('Please enter your Country, Region, and Postal Code');
                    return;
                }

                btn.value = "Calculating shipping...";
                jQuery(btn).attr('disabled','disabled');

                // get shipping options
                var action = 'getShippingOptionEstimatesJson';
                var url = '<%= StoreUrls.AdminAjaxHandler %>';

                jQuery.post(url, { 'action': action, 'country': country, 'address1': address1, 'address2': address2, 'city': city, 'region': region, 'postalCode': postalCode, 'isBusiness': isBusiness }, function(data) {                                        
                    if((data.success != 'false') && (data.length != 0))
                    {                        
                        var $select = jQuery('#ddlPaypalStdShippingOption');
                        $select.empty();
                        jQuery.each(data, function() {
                            var val = this.Value + "|" + this.Name;
                            $select.append('<option value="' + val + '">' + this.Text + '</option>');
                        });
                        jQuery('.collectAddress', $dialog).hide();
                        jQuery('.chooseShippingOption', $dialog).show();                    
                    }
                    else
                    {
                        btn.value = "Continue »";
                        jQuery(btn).removeAttr('disabled');
                        jQuery(".shippingError").text("We were unable to find a shipping option for you. Please try again");
                        jQuery(".shippingError").show();
                    }
                }, 'json');
            }
        </script>
    </asp:Panel>
</div>
