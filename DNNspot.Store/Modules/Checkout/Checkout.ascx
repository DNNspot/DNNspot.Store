<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Checkout.ascx.cs" Inherits="DNNspot.Store.Modules.Checkout.Checkout" %>
<%@ Register TagPrefix="dstore" TagName="AddressForm" Src="../../UserControls/AddressForm.ascx" %>

<div class="dstore checkout">
    <h2>Checkout</h2>
    
    <div id="msgFlash" runat="server" class="flash" visible="false"></div>
    
    <div class="stepsNav" id="stepsNav" runat="server">
        <ol>
            <li class="pnlBilling"><span class="active">Billing &raquo;</span></li>
            <li class="pnlShipping"><span>Shipping &raquo;</span></li>
            <li class="pnlPayment"><span>Payment &raquo;</span></li>
            <li><span>Review Order</span></li>
        </ol>
    </div>
    
    <asp:Panel ID="pnlAjaxPanels" runat="server">
        <fieldset id="pnlBilling" class="panel">
            <legend>Billing Information</legend>
            <dstore:AddressForm ID="billingAddressForm" runat="server" ShowEmail="true" ShowBusinessName="false"></dstore:AddressForm>
            <div>
                <input type="radio" id="copyBillingAddress_true" name="copyBillingAddress" value="true" checked="checked" />
                <label for="copyBillingAddress_true">Ship to this address</label>
                
                <input type="radio" id="copyBillingAddress_false" name="copyBillingAddress" value="false" />
                <label for="copyBillingAddress_false">Ship to a different address</label>
            </div>
            <div class="buttons">  
                <button class="next" onclick="moveToPanel($pnlShipping); return false;">Continue</button>
            </div>
        </fieldset>
        
        <fieldset id="pnlShipping" class="panel" style="display: none;">
            <legend>Shipping Information</legend>
            <dstore:AddressForm ID="shippingAddressForm" runat="server" ShowEmail="false" ShowBusinessName="true"></dstore:AddressForm>
            <fieldset class="form" style="margin: 1em 0 0 0; border: none; padding: 0;">
                <ol>
                    <li>
                        <label for="<%= ddlShippingType.ClientID %>">Shipping Speed / Carrier</label>
                        <asp:DropDownList ID="ddlShippingType" runat="server">
                        </asp:DropDownList>                
                    </li>
                </ol>
            </fieldset>        
            <div class="buttons">
                <button class="prev" onclick="moveToPanel($pnlBilling); return false;">&laquo; Billing</button>
                <button class="next" onclick="moveToPanel($pnlPayment); return false;">Continue</button>
            </div>
        </fieldset>
        
        <fieldset id="pnlPayment" class="panel form" style="display: none;">
            <legend>Payment</legend>
            <ol>
                <li>
                    <label>Payment Method</label>
                    <ul class="paymentMethods">
                        <li>
                            <input type="radio" name="paymentMethod" id="paymentMethod-creditCard" value="creditCard" checked="checked" />
                            <label for="paymentMethod-creditCard"><img src="<%= ModuleRootImagePath %>cardLogos.gif" alt="Major Credit Cards Accepted" /></label>
                        </li>                    
                        <li id="liPayPalExpressCheckoutPaymentMethod" runat="server">
                            <input type="radio" name="paymentMethod" id="paymentMethod-paypalEC" value="payPalExpressCheckout" />
                            <label for="paymentMethod-paypalEC"><img src="https://www.paypal.com/en_US/i/logo/PayPal_mark_37x23.gif" alt="PayPal Express Checkout"></label>
                        </li>
                    </ul>
                </li>
                <li>
                    <asp:Label ID="Label5" AssociatedControlID="ddlCcType" runat="server" Text="Card Type"></asp:Label>
                    <asp:DropDownList ID="ddlCCType" runat="server">
                    </asp:DropDownList>
                </li>            
                <li>
                    <asp:Label ID="Label1" AssociatedControlID="txtCCNumber" runat="server" Text="Card number"></asp:Label>
                    <asp:TextBox ID="txtCCNumber" runat="server" style="width: 160px;"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label2" AssociatedControlID="ddlCCExpireMonth" runat="server" Text="Expiration date"></asp:Label>
                    <asp:DropDownList ID="ddlCCExpireMonth" runat="server" style="margin-right: 0.5em;">
                        <asp:ListItem Value="" Text=" " />
                        <asp:ListItem Value="1" Text="01" />
                        <asp:ListItem Value="2" Text="02" />
                        <asp:ListItem Value="3" Text="03" />
                        <asp:ListItem Value="4" Text="04" />
                        <asp:ListItem Value="5" Text="05" />
                        <asp:ListItem Value="6" Text="06" />
                        <asp:ListItem Value="7" Text="07" />
                        <asp:ListItem Value="8" Text="08" />
                        <asp:ListItem Value="9" Text="09" />
                        <asp:ListItem Value="10" Text="10" />
                        <asp:ListItem Value="11" Text="11" />
                        <asp:ListItem Value="12" Text="12" />
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlCCExpireYear" runat="server">
                    </asp:DropDownList>                
                </li>
                <li>
                    <asp:Label ID="Label3" AssociatedControlID="txtCCSecurityCode" runat="server" Text="Security code / CVV"></asp:Label>
                    <asp:TextBox ID="txtCCSecurityCode" runat="server" style="width: 50px;"></asp:TextBox>
                    <span class="inputHelp">3 or 4 digits</span>
                </li>
                <li>
                    <asp:Label ID="Label4" AssociatedControlID="txtCCNameOnCard" runat="server" Text="Name on card"></asp:Label>
                    <asp:TextBox ID="txtCCNameOnCard" runat="server" style="width: 200px;"></asp:TextBox>
                </li>                                    
            </ol>
            <div class="buttons">
                <button class="prev" onclick="moveToPanel($pnlShipping); return false;">&laquo; Shipping</button>
                <asp:Button ID="btnReviewOrder" runat="server" CssClass="next" Text="Review Order" OnClick="btnReviewOrder_Click" />
            </div>
        </fieldset>    
    </asp:Panel>
    
    <asp:Panel ID="pnlReviewOrder" runat="server" Visible="false">           
    
        <fieldset class="panel">
            <legend>Review Order</legend>
            <asp:Repeater ID="rptCheckoutItems" runat="server">
                <HeaderTemplate>
                    <table class="checkoutItems" cellpadding="0" cellspacing="0">
                        <thead>
                            <tr>
                                <th class="colProduct">Product</th>
                                <th class="colQty">Qty.</th>
                                <th class="colSubtotal">Subtotal</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                            <tr>                        
                                <td class="colProduct"><%# (Container.DataItem as vCartItem).ProductName %></td>
                                <td class="colQty"><%# (Container.DataItem as vCartItem).Quantity %> x <%# (Container.DataItem as vCartItem).ItemPrice.Value.ToString("C") %></td>                        
                                <td class="colSubtotal"><%# (Container.DataItem as vCartItem).Subtotal.Value.ToString("C") %></td>                        
                            </tr>
                </ItemTemplate>            
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="billingSummary">
                Bill To:
                <p>
                    <asp:Literal ID="litBillToSummary" runat="server"></asp:Literal>
                </p>
            </div>
            <div class="shippingSummary">
                <p>
                    <asp:Literal ID="litShipToSummary" runat="server"></asp:Literal>
                </p>
            </div>            
            <div class="priceSummary">
                <div class="subtotal">
                    <label>Subtotal</label>
                    <span><%= cart.Subtotal.GetValueOrDefault(0).ToString("C") %></span>
                </div>
                <div class="shipping">
                    <label>Shipping/Handling</label>
                    <span></span>
                </div>                
                <div class="discount">
                    <label>Discount(s)</label>
                    <span><%= cart.Discount.GetValueOrDefault(0).ToString("C") %></span>
                </div>
                <div class="total">
                    <label>Total</label>
                    <span><%= cart.Total.GetValueOrDefault(0).ToString("C") %></span>
                </div>                                                
            </div>            
            <asp:Button ID="btnPlaceOrder" runat="server" CssClass="btnPlaceOrder" Text="Place Order" OnClick="btnPlaceOrder_Click" />           
        </fieldset>       
        
    </asp:Panel>
    
    <asp:Panel ID="pnlOrderComplete" runat="server" Visible="false">
        <h2>Thank You!</h2>
        <h4>Your order has been received.</h4>
        <p>
            Your order # is: <%= completedOrder.OrderNumber %>
        </p>        
        <p>
            You will receive an order confirmation email with the details of your order.
        </p>
        <button onclick="location.href='<%= StoreUrls.CategoryHome() %>'; return false;">Continue Shopping</button>
    </asp:Panel>
          
</div>

<script type="text/javascript">
    var $pnlBilling = null;
    var $pnlShipping = null;
    var $pnlPayment = null;
    var $activePanel = null;
    var $stepsNavLis = null;

    jQuery(function($) {

        $pnlBilling = jQuery('#pnlBilling');
        $pnlShipping = jQuery('#pnlShipping');
        $pnlPayment = jQuery('#pnlPayment');
        $activePanel = $pnlBilling;

        $stepsNavLis = jQuery('li', 'div.dstore div.stepsNav');
        /*
        $stepsNavLis.click(function() {
            var $this = jQuery(this);
            moveToPanel(jQuery('#' + $this.attr('class')));
        });
        */
        if(<%= pnlReviewOrder.Visible.ToString().ToLower() %>) {
            jQuery('span', $stepsNavLis).addClass("active");
        }
    });

    function moveToPanel(toPanel) {
        $activePanel.hide();

        toPanel.show();
        $activePanel = toPanel;
        jQuery('span', $stepsNavLis).removeClass("active");

        var activeId = $activePanel.attr('id');
        if (activeId === $pnlBilling.attr('id')) {
            jQuery('span', $stepsNavLis).eq(0).addClass("active");      
        }
        else if (activeId == $pnlShipping.attr('id')) {
            jQuery('span', $stepsNavLis).slice(0, 2).addClass("active");            
        }
        else if (activeId == $pnlPayment.attr('id')) {
            jQuery('span', $stepsNavLis).slice(0, 3).addClass("active");
        }        
    }
</script>

