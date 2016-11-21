<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaxRegions.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.TaxRegions" EnableViewState="false" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">
    
    <h1>Tax Regions</h1>
    
    <asp:Literal ID="flash" runat="server"></asp:Literal>

    <ol class="form">
        <li>
            <label>Charge Tax based on:</label>
            <span>
                <asp:RadioButtonList runat="server" ID="rdoChargeTaxBasedOn" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Billing" Selected="True">Billing Address</asp:ListItem>
                    <asp:ListItem Value="Shipping">Shipping Address</asp:ListItem>
                </asp:RadioButtonList>
            </span>
        </li>
    </ol>

    <p>
        Sales tax is calculated based on the customer's billing/shipping address and the table below.
        <br />
        Sales Tax = (SubTotal - Discounts + Shipping & Handling) x TaxRate
        <br />
        Tax Rate is a decimal number, e.g. 0.06 = 6.0%
    </p>
        
    <table id="cloneSource" style="display: none;">
        <tr>
            <td>
                <select name="ddlCountryRegion">
                    <asp:Literal ID="litCountryRegionOptions" runat="server"></asp:Literal>
                </select>
            </td>
            <td>
                <input type="text" name="taxRate" style="width: 60px;" />
                <span class="inputHelp">(e.g. 0.06 = 6.0%)</span>
            </td>
            <td><a href="#" title="delete" onclick="jQuery(this).parents('tr').eq(0).remove(); return false;"><img src="<%= ModuleRootImagePath %>icons/delete.png" alt="delete" /></a></td>
        </tr>    
    </table>
    
    <div class="adminToolbar">
        <button class="adminIconBtn add" onclick="addTaxRow(); return false;">Add New Tax Region</button>
    </div>

    <table id="taxRates" class="grid" style="width: auto;" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>Country and Region</th>                
                <th>Tax Rate</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptTaxRates" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <input type="hidden" name="ddlCountryRegion" value="<%# (Container.DataItem as TaxRegion).CountryCode %>-<%# (Container.DataItem as TaxRegion).Region %>">
                            <span><%# GetCountryName((Container.DataItem as TaxRegion).CountryCode) %> <%# GetRegionName((Container.DataItem as TaxRegion).Region) %></span>
                        </td>
                        <td>
                            <input type="text" name="taxRate" style="width: 60px;" value="<%# (Container.DataItem as TaxRegion).TaxRate.Value.ToString("F4") %>" />
                        </td>
                        <td><a href="#" title="delete" onclick="jQuery(this).parents('tr').eq(0).remove(); return false;"><img src="<%= ModuleRootImagePath %>icons/delete.png" alt="delete" /></a></td>
                    </tr>                
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <br />
    <div class="adminToolbar">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="adminIconBtn ok" OnClick="btnSave_Click" />
    </div>
</div>

<script runat="server">
    public string GetCountryName(string countryCode)
    {
        return countryNameHash.ContainsKey(countryCode) ? countryNameHash[countryCode] : countryCode;
    }
    
    public string GetRegionName(string region)
    {
        string name = regionNameHash.ContainsKey(region) ? regionNameHash[region] : region;
        
        return !string.IsNullOrEmpty(name) ? "| " + name : "";
    }
</script>


<script type="text/javascript">
    var $trToClone = null;
    var $tblTaxRates = null;

    jQuery(function($) {
        $trToClone = jQuery('tr', '#cloneSource').eq(0);
        $tblTaxRates = jQuery('#taxRates');
    });

    function addTaxRow() {
        jQuery('tbody', $tblTaxRates).append($trToClone.clone());
        return false;
    }

</script>