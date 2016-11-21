<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckoutShipping.ascx.cs" Inherits="DNNspot.Store.Modules.Checkout.CheckoutShipping" %>
<%@ Register TagPrefix="dstore" TagName="AddressForm" Src="../../UserControls/AddressForm.ascx" %>

<div class="dstore checkout">
    <h2>Checkout</h2>
    
    <div id="msgFlash" runat="server" class="flash" visible="false"></div>
    
    <div class="stepsNav shipping" id="stepsNav" runat="server">
        <ol>
            <li class="pnlBilling"><a href="<%= StoreUrls.CheckoutBilling() %>"><span><asp:Literal ID="litBillingBreadcrumb" runat="server"></asp:Literal></span></a></li>
            <li class="pnlShipping"><span class="active"><asp:Literal ID="litShippingBreadcrumb" runat="server"></asp:Literal></span></li>
            <li class="pnlShippingMethod"><span><asp:Literal ID="litShippingMethodBreadcrumb" runat="server"></asp:Literal></span></li>
            <li class="pnlPayment"><span><asp:Literal ID="litPaymentBreadcrumb" runat="server"></asp:Literal></span></li>
            <li><span><asp:Literal ID="litReviewOrderBreadcrumb" runat="server"></asp:Literal></span></li>
        </ol>
    </div>
        
    <fieldset id="pnlShipping" class="panel">
        <legend>Shipping Information</legend>         
        <dstore:AddressForm ID="shippingAddressForm" runat="server" ShowEmail="false" ShowBusinessName="true"></dstore:AddressForm>        
        <asp:Panel ID="pnlExtraShippingFields" runat="server" Visible="false">
            <ol class="form">
                <li>
                    <asp:Label ID="Label1" AssociatedControlID="txtExtraShipToRecipientName" runat="server" Text="Business / Org. Name"></asp:Label>
                    <span>
                        <asp:TextBox ID="txtExtraShipToRecipientName" runat="server"></asp:TextBox>             
                        &nbsp;<span class="inputHelp">(optional)</span>
                    </span>
                </li>
            </ol>                              
        </asp:Panel>        
        <div class="buttons">            
            <a href="<%= StoreUrls.CheckoutBilling() %>" class="prev" title="Billing">&laquo; Billing</a>
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
                "shippingAddressForm_regionCode": {
                    required: { depends: function(element) { return jQuery('#shippingAddressForm_regionCode').is(':visible'); } }
                },
                "shippingAddressForm_regionName": {
                    required: { depends: function(element) { return jQuery('#shippingAddressForm_regionName').is(':visible'); } }
                }
            }
        });

    });

</script>
