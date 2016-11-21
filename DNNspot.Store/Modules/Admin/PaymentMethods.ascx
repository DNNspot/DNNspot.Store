<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentMethods.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.PaymentMethods" %>

<div class="admin adminPaymentMethods">
    <h2>Payment Methods</h2>
    
    <asp:Literal ID="flash" runat="server"></asp:Literal>
    
    <div class="adminToolbar">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="adminIconBtn ok" OnClick="btnSave_Click" />
    </div>    

    <fieldset class="onsite">
        <legend>On-Site / Integrated Processors</legend>
        <p>Customer stays on your site to complete checkout. <strong>** SSL Certificate required **</strong></p>

        <div class="providerAdminContainer">
            <asp:CheckBox ID="chkPayLater" runat="server" />
            <asp:Label runat="server" AssociatedControlID="chkPayLater" Text="Pay by Check, Money Order, Phone, etc."></asp:Label>        
            <fieldset>            
                <p>
                    The customer will not be required to enter any payment information during checkout. Payment for their order will be handled separately/offline.
                    You must manually set each order to "Paid" after receiving the customer's payment.
                </p>
                <ol class="form labelsLeft">
                    <li>
                        <label>Display Text</label>
                        <span>
                            <asp:TextBox ID="txtPayLaterDisplayText" runat="server"></asp:TextBox>
                        </span>
                    </li>
                    <li>
                        <label>Customer Instructions</label>
                        <span>
                            <asp:TextBox ID="txtPayLaterCustomerInstructions" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </span>
                    </li>
                </ol>
            </fieldset>        
        </div>

        <div class="providerAdminContainer">
            <asp:CheckBox ID="chkCardCaptureOnly" runat="server" />
            <asp:Label runat="server" AssociatedControlID="chkCardCaptureOnly" Text="Capture Credit Card - Manual Processing"></asp:Label>        
            <fieldset>            
                <p>
                    The customer will be required to enter their Credit Card information, but their card must be processed manually at a later time (e.g. virtual terminal or card-swipe machine).
                    You must manually set each order as "Paid" after processing the customer's credit card.
                </p>
            </fieldset>        
        </div>

        <div class="providerAdminContainer">
            <asp:CheckBox ID="chkAuthorizeNetAim" runat="server" />
            <asp:Label runat="server" AssociatedControlID="chkAuthorizeNetAim" Text="Authorize.Net - AIM Integration"></asp:Label>        
            <fieldset>            
                <ol class="form labelsLeft">
                    <li>
                        <asp:Label ID="Label15" runat="server" AssociatedControlID="chkAuthorizeNetAimTestGateway" Text="Use Test Gateway"></asp:Label>
                        <span>
                            <asp:CheckBox ID="chkAuthorizeNetAimTestGateway" runat="server" />                
                            <span class="inputHelp">** Make sure to UNCHECK this before making your site public!</span>
                        </span>                    
                    </li>
                    <li>
                        <asp:Label ID="Label7" runat="server" AssociatedControlID="chkAuthorizeNetAimTestTransactions" Text="Send Test Transactions"></asp:Label>
                        <span>
                            <asp:CheckBox ID="chkAuthorizeNetAimTestTransactions" runat="server" />                
                            <span class="inputHelp">** Make sure to UNCHECK this before making your site public!</span>                        
                        </span>                    
                    </li>            
                    <li>
                        <asp:Label ID="Label11" runat="server" AssociatedControlID="txtAuthorizeNetAimApiLoginId" Text="API Login ID"></asp:Label>
                        <asp:TextBox ID="txtAuthorizeNetAimApiLoginId" runat="server"></asp:TextBox>                
                    </li>
                    <li>
                        <asp:Label ID="Label12" runat="server" AssociatedControlID="txtAuthorizeNetAimTransactionKey" Text="Transaction Key"></asp:Label>
                        <asp:TextBox ID="txtAuthorizeNetAimTransactionKey" runat="server"></asp:TextBox>                
                    </li>            
                </ol>
            </fieldset>      
        </div>

        <div class="providerAdminContainer">
            <asp:CheckBox ID="chkPayPalDirect" runat="server" />
            <asp:Label runat="server" AssociatedControlID="chkPayPalDirect" Text="PayPal Pro (Direct Payment)"></asp:Label>        
            <fieldset>            
                <ol class="form labelsLeft">         
                    <li>
                        <asp:Label ID="Label13" runat="server" AssociatedControlID="chkPayPalDirectPaymentIsSandbox" Text="Test / Sandbox Mode"></asp:Label>
                        <span>
                            <asp:CheckBox ID="chkPayPalDirectPaymentIsSandbox" runat="server" />                
                            <span class="inputHelp">** Make sure to UNCHECK this before making your site public!</span>                    
                        </span>                    
                    </li>        
                    <li>
                        <asp:Label ID="Label14" runat="server" AssociatedControlID="txtPayPalDirectPaymentApiUsername" Text="API Username"></asp:Label>
                        <asp:TextBox ID="txtPayPalDirectPaymentApiUsername" runat="server"></asp:TextBox>                
                    </li>
                    <li>
                        <asp:Label ID="Label16" runat="server" AssociatedControlID="txtPayPalDirectPaymentApiPassword" Text="API Password"></asp:Label>
                        <asp:TextBox ID="txtPayPalDirectPaymentApiPassword" runat="server"></asp:TextBox>                
                    </li>
                    <li>
                        <asp:Label ID="Label17" runat="server" AssociatedControlID="txtPayPalDirectPaymentApiSignature" Text="API Signature"></asp:Label>
                        <asp:TextBox ID="txtPayPalDirectPaymentApiSignature" runat="server"></asp:TextBox>                
                    </li>
                </ol>
            </fieldset>             
        </div>

    </fieldset>

    <fieldset class="offsite">
        <legend>Off-Site / External Processors</legend>
        <p>Customer is taken to another secure site to enter their payment information.</p>
                   
        <div class="providerAdminContainer">
            <asp:CheckBox ID="chkPayPalStandard" runat="server" />
            <asp:Label runat="server" AssociatedControlID="chkPayPalStandard" Text="PayPal - Website Payments Standard"></asp:Label>        
            <fieldset>            
                <p>
                    The easiest PayPal option to set up for the store owner. Customers DO NOT need their own PayPal account to complete the checkout process.
                </p>
                <ol class="form labelsLeft">
                    <li>
                        <asp:Label ID="Label8" runat="server" AssociatedControlID="chkPayPalStandardIsSandbox" Text="Test / Sandbox Mode"></asp:Label>
                        <span>
                            <asp:CheckBox ID="chkPayPalStandardIsSandbox" runat="server" />                
                            <span class="inputHelp">** Make sure to UNCHECK this before making your site public!</span>
                        </span>                    
                    </li>                              
                    <li>
                        <asp:Label ID="Label88" runat="server" AssociatedControlID="rdoPayPalStandardShippingLogic" Text="Shipping Calculations"></asp:Label>
                        <asp:RadioButtonList runat="server" ID="rdoPayPalStandardShippingLogic" RepeatDirection="Horizontal">
                            <asp:ListItem Value="PayPal" Selected="True">PayPal Account</asp:ListItem>
                            <asp:ListItem Value="Store">DNNspot Store Shipping</asp:ListItem>
                        </asp:RadioButtonList>
                    </li>
                    <li>
                        <asp:Label ID="Label1" runat="server" AssociatedControlID="txtPayPalStandardEmail" Text="Email"></asp:Label>
                        <span>
                            <asp:TextBox ID="txtPayPalStandardEmail" runat="server"></asp:TextBox>
                            <br /><span class="inputHelp">Email address of your PayPal account.</span>
                        </span>
                    </li>
                </ol>
            </fieldset> 
        </div>
    
        <div class="providerAdminContainer">
            <asp:CheckBox ID="chkPayPalExpress" runat="server" />
            <asp:Label runat="server" AssociatedControlID="chkPayPalExpress" Text="PayPal - Express Checkout"></asp:Label>        
            <fieldset>            
                <p>
                    Slightly more advanced PayPal option. Customers DO need their own PayPal account to complete the checkout process.
                </p>        
                <ol class="form labelsLeft">    
                    <li>
                        <asp:Label ID="Label19" runat="server" AssociatedControlID="chkPayPalExpressCheckoutIsSandbox" Text="Test / Sandbox Mode"></asp:Label>
                        <span>
                            <asp:CheckBox ID="chkPayPalExpressCheckoutIsSandbox" runat="server" />                
                            <span class="inputHelp">** Make sure to UNCHECK this before making your site public!</span>
                        </span>                    
                    </li>                 
                    <li>
                        <asp:Label ID="Label4" runat="server" AssociatedControlID="txtPayPalExpressCheckoutApiUsername" Text="API Username"></asp:Label>
                        <asp:TextBox ID="txtPayPalExpressCheckoutApiUsername" runat="server"></asp:TextBox>                
                    </li>
                    <li>
                        <asp:Label ID="Label5" runat="server" AssociatedControlID="txtPayPalExpressCheckoutApiPassword" Text="API Password"></asp:Label>
                        <asp:TextBox ID="txtPayPalExpressCheckoutApiPassword" runat="server"></asp:TextBox>                
                    </li>
                    <li>
                        <asp:Label ID="Label6" runat="server" AssociatedControlID="txtPayPalExpressCheckoutApiSignature" Text="API Signature"></asp:Label>
                        <asp:TextBox ID="txtPayPalExpressCheckoutApiSignature" runat="server"></asp:TextBox>                
                    </li>
                </ol>
            </fieldset>
         </div>

    </fieldset>
      
    <div id="adminToolbar2">
    </div>
              
</div>


<script type="text/javascript">
    jQuery(function ($) {
        jQuery('#adminToolbar2').append(jQuery('div.adminToolbar').html());

        jQuery('.onsite .providerAdminContainer > :checkbox').click(function () {
            checkboxHandler(this);
        });

        var $offsiteCheckboxes = jQuery('.offsite .providerAdminContainer > :checkbox');
        $offsiteCheckboxes.click(function () {
            $offsiteCheckboxes.not(this).each(function () {
                this.checked = false;
                checkboxHandler(this);
            });
            checkboxHandler(this);
        });

        jQuery('.providerAdminContainer > :checkbox:checked').each(function () {
            checkboxHandler(this);
        });
    });

    function checkboxHandler(checkbox) {
        var $this = jQuery(checkbox);
        if ($this.is(':checked')) {
            $this.nextAll('label').css('font-weight', 'bold');
            $this.nextAll('fieldset').show();
        } else {
            $this.nextAll('label').css('font-weight', 'normal');
            $this.nextAll('fieldset').hide();
        }      
    }
</script>

