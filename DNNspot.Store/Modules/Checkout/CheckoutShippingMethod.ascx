<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckoutShippingMethod.ascx.cs" Inherits="DNNspot.Store.Modules.Checkout.CheckoutShippingMethod" %>

<div class="dstore checkout">
    <h2>Checkout</h2>
    
    <div id="msgFlash" runat="server" class="flash" visible="false"></div>
    
    <div class="stepsNav shipping" id="stepsNav" runat="server">
        <ol>
            <li class="pnlBilling"><a href="<%= StoreUrls.CheckoutBilling() %>"><span><asp:Literal ID="litBillingBreadcrumb" runat="server"></asp:Literal></span></a></li>
            <li class="pnlShipping"><a href="<%= StoreUrls.CheckoutShipping() %>"><span><asp:Literal ID="litShippingBreadcrumb" runat="server"></asp:Literal></span></a></li>
            <li class="pnlShippingMethod"><span class="active"><asp:Literal ID="litShippingMethodBreadcrumb" runat="server"></asp:Literal></span></li>
            <li class="pnlPayment"><span><asp:Literal ID="litPaymentBreadcrumb" runat="server"></asp:Literal></span></li>
            <li><span><asp:Literal ID="litReviewOrderBreadcrumb" runat="server"></asp:Literal></span></li>
        </ol>
    </div>
        
    <fieldset id="pnlShipping" class="panel">
        <legend>Shipping Method</legend>         
        <ol class="form" style="margin: 1em 0;">
            <li>
                <label for="<%= ddlShippingOption.ClientID %>">Shipping Speed / Carrier *</label>
                <asp:DropDownList ID="ddlShippingOption" runat="server" CssClass="required" title="Please choose a shipping option">
                </asp:DropDownList>                
            </li>
            <li>
                <label></label>
                <span>
                    <%= checkoutOrderInfo.ShippingAddress.ToHumanFriendlyString("<br />", false, false) %>    
                </span>
            </li>
        </ol>                                
        <div class="buttons">            
            <a href="<%= StoreUrls.CheckoutShipping() %>" class="prev" title="Shipping">&laquo; Shipping</a>
            <asp:Button ID="btnNext" runat="server" CssClass="next dnnPrimaryAction" Text="Continue" OnClick="btnNext_Click" />
        </div>
    </fieldset>       
    
    <div class="validationErrors">Please correct the following errors:<ul></ul></div>                  
          
</div>

<script type="text/javascript">

    jQuery(function ($) {

        // Form Validation
        var $billingValidator = $('form').validate({
            errorContainer: ".validationErrors",
            errorLabelContainer: ".validationErrors > ul",
            wrapper: "li",
            onfocusout: false,
            onkeyup: false,
            onclick: false
        });

    });

</script>