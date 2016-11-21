<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckoutBilling.ascx.cs" Inherits="DNNspot.Store.Modules.Checkout.CheckoutBilling" %>
<%@ Register TagPrefix="dstore" TagName="AddressForm" Src="../../UserControls/AddressForm.ascx" %>

<div class="dstore checkout checkoutBilling">
    <h2><asp:Literal ID="litCheckoutHeader" runat="server"></asp:Literal></h2>
    
    <div id="msgFlash" runat="server" class="flash" visible="false"></div>
    
    <div class="stepsNav" id="stepsNav" runat="server">
        <ol>
            <li class="pnlBilling"><span class="active"><asp:Literal ID="litBillingBreadcrumb" runat="server"></asp:Literal></span></li>
            <li class="pnlShipping"><span><asp:Literal ID="litShippingBreadcrumb" runat="server"></asp:Literal></span></li>
            <li class="pnlShippingMethod"><span><asp:Literal ID="litShippingMethodBreadcrumb" runat="server"></asp:Literal></span></li>
            <li class="pnlPayment"><span><asp:Literal ID="litPaymentBreadcrumb" runat="server"></asp:Literal></span></li>
            <li><span><asp:Literal ID="litReviewOrderBreadcrumb" runat="server"></asp:Literal></span></li>
        </ol>
    </div>
    
    <fieldset id="pnlBilling" class="panel">
        <legend><asp:Literal ID="litBillingInformation" runat="server"></asp:Literal></legend>                
                
        <dstore:AddressForm ID="billingAddressForm" runat="server" ShowEmail="true" ShowBusinessName="false"></dstore:AddressForm>
        <div style="margin-top: 1em; text-align: left;">        
            <asp:CheckBox ID="chkCopyBillingAddress" runat="server" />
            <asp:Label ID="lblCopyBilling" runat="server" AssociatedControlID="chkCopyBillingAddress" Text="Ship to this address"></asp:Label>
        </div>
        <div class="buttons">              
            <asp:Button ID="btnNext" runat="server" CssClass="next dnnPrimaryAction" Text="Continue" OnClick="btnNext_Click" />
        </div>
    </fieldset>                      
                  
    <div class="validationErrors">Please correct the following errors:<ul></ul></div>                  
    
</div>

<script type="text/javascript">

    jQuery(function($) {
        // Form Validation
        var $billingValidator = $('form').validate({
            errorContainer: ".validationErrors",
            errorLabelContainer: ".validationErrors > ul",
            wrapper: "li",
            onfocusout: false,
            onkeyup: false,
            onclick: false,
            rules: {
                "billingAddressForm_regionCode": {
                    required: { depends: function(element) { return jQuery('#billingAddressForm_regionCode').is(':visible'); } }
                },
                "billingAddressForm_regionName": {
                    required: { depends: function(element) { return jQuery('#billingAddressForm_regionName').is(':visible'); } }
                }
            }
        });
    });

</script>

