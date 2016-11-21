<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddressForm.ascx.cs" Inherits="DNNspot.Store.UserControls.AddressForm" %>

<% if(showBusinessNameField) { %>
<div class="isBusinessAddress">
    <label><input type="radio" id="<%= ControlPrefix %>isBusinessAddressNo" name="<%= ControlPrefix %>isBusinessAddress" value="false" <%= addressInfo.IsResidential ? "checked='checked'" : string.Empty %> /> Residential Address</label>
    <label><input type="radio" id="<%= ControlPrefix %>isBusinessAddressYes" name="<%= ControlPrefix %>isBusinessAddress" value="true" <%= addressInfo.IsResidential ? string.Empty : "checked='checked'" %> /> Business/Commercial Address</label>
</div>        
<% } %>

<fieldset class="addressForm">
    <ol>
        <li class="firstName">
            <label for="<%= ControlPrefix %>firstName">First Name</label>
            <input type="text" id="<%= ControlPrefix %>firstName" name="<%= ControlPrefix %>firstName" value="<%= addressInfo.FirstName %>" class="required" title="Please enter your First Name" />
        </li>
        <li class="lastName">
            <label for="<%= ControlPrefix %>lastName">Last Name</label>
            <input type="text" id="<%= ControlPrefix %>lastName" name="<%= ControlPrefix %>lastName" value="<%= addressInfo.LastName %>" class="required" title="Please enter your Last Name" />
        </li>
        <% if(showEmailField) { %>
        <li class="email">
            <label for="<%= ControlPrefix %>email">Email</label>
            <input type="text" id="<%= ControlPrefix %>email" name="<%= ControlPrefix %>email" value="<%= addressInfo.Email %>" class="required email" title="Please enter your Email" />
        </li>
        <% } %>
        <% if(showBusinessNameField) { %>
        <li class="businessName">
            <label for="<%= ControlPrefix %>businessName">Business / Org. Name</label>
            <input type="text" id="<%= ControlPrefix %>businessName" name="<%= ControlPrefix %>businessName" value="<%= addressInfo.BusinessName %>" />
        </li>             
        <% } %>
        <li class="telephone">
            <label for="<%= ControlPrefix %>telephone">Telephone</label>
            <input type="text" id="<%= ControlPrefix %>telephone" name="<%= ControlPrefix %>telephone" value="<%= addressInfo.Telephone %>" class="required loosePhone" title="Please enter a valid phone number" />
        </li>
        <li class="address">
            <label for="<%= ControlPrefix %>address1">Address</label>
            <input type="text" id="<%= ControlPrefix %>address1" name="<%= ControlPrefix %>address1" value="<%= addressInfo.Address1 %>" class="required" title="Please enter your Adddress" />
            <input type="text" id="<%= ControlPrefix %>address2" name="<%= ControlPrefix %>address2" value="<%= addressInfo.Address2 %>" />
        </li>
        <li class="city">
            <label for="<%= ControlPrefix %>city">City</label>
            <input type="text" id="<%= ControlPrefix %>city" name="<%= ControlPrefix %>city" value="<%= addressInfo.City %>" class="required" title="Please enter your City" />
        </li>
        <li class="region">
            <label for="<%= ControlPrefix %>region">State/Province/Region</label>            
            <span>
                <input type="text" id="<%= ControlPrefix %>regionName" name="<%= ControlPrefix %>regionName" value="<%= addressInfo.Region %>" title="Please enter your Province/Region" />
                <select id="<%= ControlPrefix %>regionCode" name="<%= ControlPrefix %>regionCode" title="Please choose your State/Province/Region">
                    <asp:Literal ID="litRegionCodeOptions" runat="server"></asp:Literal>
                </select>   
            </span>
        </li>
        <li class="postalCode">
            <label for="<%= ControlPrefix %>postalCode">Zip/Postal Code</label>
            <input type="text" id="<%= ControlPrefix %>postalCode" name="<%= ControlPrefix %>postalCode" maxlength="50" value="<%= addressInfo.PostalCode %>" class="required" title="Please enter your ZIP or Postal Code" />
        </li>
        <li class="country">
            <label for="<%= ControlPrefix %>country">Country</label>                        
            <asp:DropDownList ID="ddlCountry" runat="server" class="required" title="Please choose your Country">
            </asp:DropDownList>
        </li>                                                           
    </ol>
</fieldset>


<script type="text/javascript">
    var $ddlRegion = null;
    var $txtRegion = null;    
    var $ddlCountry = null;
    var urlToDropDownHandler = "<%= StoreUrls.GetModuleFolderUrlRoot() %>Handlers/DropDownHandler.ashx";

    jQuery(function($) {
        $txtRegion = jQuery('#<%= ControlPrefix %>regionName');
        $ddlRegion = jQuery('#<%= ControlPrefix %>regionCode');
        $ddlCountry = jQuery('#<%= ddlCountry.ClientID %>');

        showHideRegionWidgets();

        $ddlCountry.change(function() {
            var $this = jQuery(this);
            //console.log($this.val());
            $ddlRegion.load(urlToDropDownHandler, { "action": "getRegions", "country": $this.val(), "emptyFirstOption": true }, function() {
                //console.log('regions loaded');
                if (jQuery("option", $ddlRegion).length <= 1) {
                    $txtRegion.val('');
                }
                showHideRegionWidgets();
            });

        });
    });

    function showHideRegionWidgets() {
        if (jQuery("option", $ddlRegion).length <= 1) {
            //console.log('no regions for this country');
            $ddlRegion.hide();

            //$txtRegion.val('');
            $txtRegion.show();
        }
        else {
            $ddlRegion.show();
            $txtRegion.hide();
        }
    }
</script>

