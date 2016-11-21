<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckoutPayment.ascx.cs" Inherits="DNNspot.Store.Modules.Checkout.CheckoutPayment" %>
<%@ Register TagPrefix="dstore" TagName="AddressForm" Src="../../UserControls/AddressForm.ascx" %>

<div class="dstore checkout">
    <h2>Checkout</h2>
    
    <div id="msgFlash" runat="server" class="flash" visible="false"></div>
    
    <div class="stepsNav payment" id="stepsNav" runat="server">
        <ol>
            <li class="pnlBilling"><a href="<%= StoreUrls.CheckoutBilling() %>"><span><asp:Literal ID="litBillingBreadcrumb" runat="server"></asp:Literal></span></a></li>
            <li class="pnlShipping"><a href="<%= StoreUrls.CheckoutShipping() %>"><span><asp:Literal ID="litShippingBreadcrumb" runat="server"></asp:Literal></span></a></li>
            <li class="pnlShippingMethod"><a href="<%= StoreUrls.CheckoutShippingMethod() %>"><span><asp:Literal ID="litShippingMethodBreadcrumb" runat="server"></asp:Literal></span></a></li>
            <li class="pnlPayment"><span class="active"><asp:Literal ID="litPaymentBreadcrumb" runat="server"></asp:Literal></span></li>
            <li><span><asp:Literal ID="litReviewOrderBreadcrumb" runat="server"></asp:Literal></span></li>
        </ol>
    </div>
    
    <fieldset id="pnlPayment" class="panel">        
        <legend style="margin-bottom: 0;">Payment Method</legend>
        <table>
            <tr>
                <td>
                    <ol class="form">
                        <li style="padding: 0;">                
                            <ul class="paymentMethods">
                                <li id="liBillMeLater" runat="server">
                                    <input type="radio" name="paymentMethod" id="paymentMethod-payLater" value="payLater" />
                                    <label for="paymentMethod-payLater" id="lblPayLater" runat="server"></label>
                                    <p id="payLaterInstructions" runat="server"></p>
                                </li>
                                <li id="liPayPalExpressCheckoutPaymentMethod" runat="server">
                                    <input type="radio" name="paymentMethod" id="paymentMethod-paypalEC" value="payPalExpressCheckout" />
                                    <label for="paymentMethod-paypalEC"><img src="http://www.paypal.com/en_US/i/logo/PayPal_mark_37x23.gif" alt="PayPal Express Checkout"></label>
                                </li>                
                                <li id="liCreditCardCapture" runat="server">
                                    <input type="radio" name="paymentMethod" id="paymentMethod-creditCard" value="creditCard" />
                                    <label for="paymentMethod-creditCard">Credit Card</label>
                        
                                    <ol id="ccFields" class="form">            
                                        <li>
                                            <asp:Label ID="Label5" AssociatedControlID="ddlCcType" runat="server" Text="Card Type"></asp:Label>
                                            <asp:DropDownList ID="ddlCCType" runat="server" title="Please choose a credit card type">
                                            </asp:DropDownList>
                                        </li>            
                                        <li>
                                            <asp:Label ID="Label1" AssociatedControlID="txtCCNumber" runat="server" Text="Card number"></asp:Label>
                                            <asp:TextBox ID="txtCCNumber" AutoCompleteType="Disabled" AutoComplete="Off" runat="server" style="width: 160px;" CssClass="digits" minlength="14" MaxLength="16" title="Please enter a valid credit card number"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label2" AssociatedControlID="ddlCCExpireMonth" runat="server" Text="Expiration date"></asp:Label>
                                            <asp:DropDownList ID="ddlCCExpireMonth" runat="server" style="margin-right: 0.5em;" title="Please choose an expiration month">
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
                                            <asp:DropDownList ID="ddlCCExpireYear" runat="server" title="Please choose an expiration year">
                                            </asp:DropDownList>                
                                        </li>
                                        <li>
                                            <asp:Label ID="Label3" AssociatedControlID="txtCCSecurityCode" runat="server" Text="Security code / CVV"></asp:Label>
                                            <asp:TextBox ID="txtCCSecurityCode" AutoCompleteType="Disabled" AutoComplete="Off" runat="server" style="width: 50px;" CssClass="digits" minlength="3" MaxLength="4" title="Please enter your verification code"></asp:TextBox>
                                            <span class="inputHelp">3 or 4 digits</span>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label4" AssociatedControlID="txtCCNameOnCard" runat="server" Text="Name on card"></asp:Label>
                                            <asp:TextBox ID="txtCCNameOnCard" AutoCompleteType="Disabled" AutoComplete="Off" runat="server" style="width: 200px;" title="Please enter the name on the card"></asp:TextBox>
                                        </li>                                    
                                    </ol>                        
                                </li>                    
                            </ul>
                        </li>
                    </ol>                
                </td>
                <td>
                    <div id="orderTotals" class="priceSummary">
                        <table cellpadding="0" cellspacing="0">
                            <tr class="subtotal">
                                <td>Subtotal</td>
                                <td><%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.SubTotal)%></td>
                            </tr>
                            <tr class="shipping">
                                <td>Shipping &amp; Handling</td>
                                <td><%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.ShippingRate.Rate)%></td>
                            </tr>
                            <% if (checkoutOrderInfo.DiscountAmount > 0) { %>
                            <tr class="discount">
                                <td>Discount</td>
                                <td>(<%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.DiscountAmount)%>)</td>
                            </tr>
                            <% } %>
                            <tr class="tax">
                                <td>Tax</td>
                                <td><%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.TaxAmount)%></td>
                            </tr>
                            <tr class="total">
                                <td>Total</td>
                                <td><%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.Total)%></td>
                            </tr>                                                                
                        </table>            
                    </div>                
                </td>
            </tr>
        </table>

        <div class="buttons">                
            <a href="<%= StoreUrls.CheckoutShippingMethod() %>" class="prev" title="Shipping Method">&laquo; Shipping Method</a>
            <asp:ImageButton ID="btnContinueToPayPal" runat="server" style="display: none;" CssClass="next" AlternateText="Continue to PayPal" ImageUrl="../../images/btnContinueToPayPal.png" OnClick="btnReviewOrder_Click" />
            <asp:Button ID="btnReviewOrder" runat="server" CssClass="next dnnPrimaryAction" Text="Review Order" OnClick="btnReviewOrder_Click" />
        </div>        
    </fieldset>  
    
    <div class="validationErrors" style="display: none;">
        Please correct the following errors:<ul></ul>
    </div>      
      
</div>

<script type="text/javascript">
    var $ccFields = null;
    var $btnReviewOrder = null;
    var $btnContinueToPayPal = null;
    var $paymentValidator = null;

    jQuery(function($) {

        $ccFields = jQuery('#ccFields');
        $btnReviewOrder = jQuery('#<%= btnReviewOrder.ClientID %>');
        $btnContinueToPayPal = jQuery('#<%= btnContinueToPayPal.ClientID %>');

        // Form Validation
        $paymentValidator = $('form').validate({
            errorContainer: ".validationErrors",
            errorLabelContainer: ".validationErrors > ul",
            wrapper: "li",
            onfocusout: false,
            onkeyup: false,
            onclick: false,
            rules: {
                "<%= ddlCCType.UniqueID %>": {
                    required: { depends: function(element) { return $ccFields.is(':visible'); } }
                },
                "<%= txtCCNumber.UniqueID %>": {
                    required: { depends: function(element) { return $ccFields.is(':visible'); } }
                },
                "<%= ddlCCExpireMonth.UniqueID %>": {
                    required: { depends: function(element) { return $ccFields.is(':visible'); } }
                },
                "<%= ddlCCExpireYear.UniqueID %>": {
                    required: { depends: function(element) { return $ccFields.is(':visible'); } }
                },
                "<%= txtCCSecurityCode.UniqueID %>": {
                    required: { depends: function(element) { return $ccFields.is(':visible'); } }
                },
                "<%= txtCCNameOnCard.UniqueID %>": {
                    required: { depends: function(element) { return $ccFields.is(':visible'); } }
                },                                                                                
            }
        });

        // Radio button handler
        var $paymentRadios = jQuery('.paymentMethods :radio');
        $paymentRadios.click(function() {
            var $this = jQuery(this);
            if ($this.val() == 'payPalExpressCheckout') {
                // hide ccFields
                $ccFields.hide();
                jQuery('.validationErrors').hide();
                
                $btnReviewOrder.hide();
                $btnContinueToPayPal.show();
            }
            else if($this.val() == 'payLater') {
                disableValidation();
                $ccFields.hide();
            }
            else {
                enableValidation();
                // show ccFields
                $ccFields.show();

                $btnReviewOrder.show();
                $btnContinueToPayPal.hide();
            }
            //console.log($ccFields.is(':visible'));
            //$paymentValidator.valid();
        });
        if(jQuery(':checked',$paymentRadios).length == 0) {
            $paymentRadios.eq(0).click();
        }

        // auto-select 'Credit Card' radio if user enters a CC #
        jQuery('#<%= txtCCNumber.ClientID %>').change(function(e){
            //console.log('cc # changed! to = ' + this.value + ', ' + jQuery(this).val());
            if(this.value) {
                // auto-select the 'Credit Card' radio option
                jQuery('#paymentMethod-creditCard').click();
            }
        });

    });

    function disableValidation() {
        $btnReviewOrder.addClass('cancel');
        $paymentValidator.resetForm();
        $paymentValidator.cancelSubmit = true;
    }

    function enableValidation() {
        $btnReviewOrder.removeClass('cancel');
        $paymentValidator.cancelSubmit = false;
    }

</script>