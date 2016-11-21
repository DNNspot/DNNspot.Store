<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Shipping.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.Shipping" %>
<div class="dstore admin<%= this.GetType().BaseType.Name %>">
    <h1>
        Shipping Configuration</h1>
    <asp:Literal ID="flash" runat="server"></asp:Literal>
    <div style="clear: both; margin: 1em 0;">
        <ol class="form labelsLeft">
            <li>
                <label style="font-weight: bold; width: 220px;">
                    Default Shipment Packaging:
                </label>
                <span>
                    <asp:DropDownList runat="server" ID="ddlShipmentPackaging">
                        <asp:ListItem Value="SingleBox" Text="Single Box - Entire order in one box"></asp:ListItem>
                        <asp:ListItem Value="BoxPerProductType" Text="Multiple Boxes - One box per product type"></asp:ListItem>
                        <asp:ListItem Value="BoxPerItem" Text="Multiple Boxes - One box per item"></asp:ListItem>
                    </asp:DropDownList>
                </span></li>
        </ol>
    </div>
    &nbsp;
    <!-- NEW -->
    <div id="shipServiceTabs">
        <!-- the tabs -->
        <ul>
            <li><a href="#shipServiceTab-1">Custom Shipping</a></li>
            <li><a href="#shipServiceTab-2">FedEx</a></li>
            <li><a href="#shipServiceTab-3">UPS</a></li>
            <li><a href="#shipServiceTab-4">Logs</a></li>
        </ul>
        <!-- tab "panes" -->
        <div id="shipServiceTab-1">
            <p>
                <asp:CheckBox ID="chkShippingTablesEnabled" runat="server" />
            </p>
            <p>
                Shipping cost is calculated based on the total weight of all products in the user's
                shopping cart.
                <br />
                The shipping cost below is the total amount you'd like to charge for the entire
                order.
            </p>
            <hr />
            <h4>
                New Shipping Option</h4>
            <label>
                Name:</label>
            <span>
                <asp:TextBox ID="txtNewShippingRateType" runat="server" Style="width: 200px;" MaxLength="50"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Button ID="btnCreateShippingRateType" runat="server" CssClass="adminIconBtn add"
                    Style="float: none; display: inline;" Text="Create" OnClick="btnCreateShippingRateType_Click" />
            </span>
            <hr />
            <div id="shippingMethods">
                <asp:Repeater ID="rptShippingRateTypes" runat="server">
                    <ItemTemplate>
                        <h2>
                            <span id="shippingId-<%# (Container.DataItem as DNNspot.Store.DataModel.ShippingServiceRateType).Id %>"
                                class="editable">
                                <%# (Container.DataItem as DNNspot.Store.DataModel.ShippingServiceRateType).DisplayName%></span>&nbsp;&nbsp;<img
                                    src="<%= ModuleRootImagePath %>icons/edit.png" title="edit" alt="edit" style="cursor: pointer;
                                    margin-right: 12px;" onclick="jQuery(this).prev('span.editable').click(); return false;" />
                            <%# HtmlHelper.ConfirmDeleteImage(StoreUrls.AdminShipping("delete=" + (Container.DataItem as DNNspot.Store.DataModel.ShippingServiceRateType).Id), "Are you sure you want to delete this ENTIRE Shipping Option?")%>
                        </h2>
                        <table id="tblShippingMethod-<%# Eval("Id") %>" class="grid gridLight" style="width: auto;
                            min-width: 50%;">
                            <thead>
                                <tr>
                                    <th>
                                        Country / Region
                                    </th>
                                    <th>
                                        Weight Range (lbs.)
                                    </th>
                                    <th>
                                        Cost
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="4">
                                        <a href="#" onclick="return addRateRows('#tblShippingMethod-<%# Eval("Id") %>',<%# Eval("Id") %>);">
                                            Add More Rates</a>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div id="shipServiceTab-2">
            <p>
                <asp:CheckBox ID="chkFedExEnabled" runat="server" />
            </p>
            <ol class="form labelsLeft">
                <li>
                    <label style="font-weight: bold;">
                        Account / API</label>
                    <span></span></li>
                <li>
                    <label>
                        Test Gateway:</label>
                    <span>
                        <asp:CheckBox ID="chkFedExIsTestGateway" runat="server" />
                        <span class="inputHelp">This should be OFF for Production use!</span> </span>
                </li>
                <li>
                    <label>
                        Account Number:</label>
                    <span>
                        <asp:TextBox ID="txtFedExAccountNum" runat="server"></asp:TextBox></span>
                </li>
                <li>
                    <label>
                        Meter Number:</label>
                    <span>
                        <asp:TextBox ID="txtFedExMeterNum" runat="server"></asp:TextBox></span>
                </li>
                <li>
                    <label>
                        API Key:</label>
                    <span>
                        <asp:TextBox ID="txtFedExApiKey" runat="server"></asp:TextBox></span> </li>
                <li>
                    <label>
                        API Password:</label>
                    <span>
                        <asp:TextBox ID="txtFedExApiPassword" runat="server" Style="width: 230px;"></asp:TextBox></span>
                </li>
                <li>
                    <label>
                        SmartPost Hub ID: <span class="inputHelp">(optional)</span></label>
                    <span>
                        <asp:TextBox ID="txtFedExSmartPostHub" runat="server"></asp:TextBox></span>
                </li>
                <li>
                    <label>
                        Label Stock Type:</label>
                    <span>
                        <asp:DropDownList ID="ddlLabelStockType" runat="server">
                            <asp:ListItem Value="0" Text="Paper 4x6"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Paper 7x4.75"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Stock 4x6"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Stock 4x6.75 Leading Doc Tab"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Stock 4x8"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Stock 4x9 Leading Doc Tab"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Paper 4x8"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Paper 4x9"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Paper 8.5x11 Bottom Half Label"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Paper 8.5x11 Top Half Label"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Stock 4x6.75 Trailing Doc Tab"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Stock 4x9 Trailing Doc Tab"></asp:ListItem>
                        </asp:DropDownList>
                    </span></li>
            </ol>
        </div>
        <div id="shipServiceTab-3">
            <p>
                <asp:CheckBox ID="chkUpsEnabled" runat="server" />
            </p>
            <ol class="form labelsLeft">
                <li>
                    <label style="font-weight: bold;">
                        Account / API</label>
                    <span></span></li>
                <li>
                    <label>
                        Test Gateway:</label>
                    <span>
                        <asp:CheckBox ID="chkUpsIsTestGateway" runat="server" />
                        <span class="inputHelp">This should be OFF for Production use!</span> </span>
                </li>
                <li>
                    <label>
                        User Id:</label>
                    <span>
                        <asp:TextBox ID="txtUpsUserId" runat="server"></asp:TextBox></span> </li>
                <li>
                    <label>
                        Password:</label>
                    <span>
                        <asp:TextBox ID="txtUpsPassword" runat="server"></asp:TextBox></span> </li>
                <li>
                    <label>
                        Account Number:</label>
                    <span>
                        <asp:TextBox ID="txtUpsAccountNumber" runat="server"></asp:TextBox></span>
                </li>
                <li>
                    <label>
                        Access Key:</label>
                    <span>
                        <asp:TextBox ID="txtUpsAccessKey" runat="server"></asp:TextBox></span> </li>
            </ol>
        </div>
        <div id="shipServiceTab-4">
            <asp:Repeater ID="rptShippingLogs" runat="server">
                <HeaderTemplate>
                    <table class="grid gridLight" cellpadding="0" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th>
                                    Date
                                </th>
                                <th>
                                    Shipping Type
                                </th>
                                <th>
                                    XML Sent to Shipping Provider
                                </th>
                                <th>
                                    XML Received back from Shipping Provider
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# (Container.DataItem as ShippingLog).CreatedOn.Value.ToString() %>
                        </td>
                        <td>
                            <%# (Container.DataItem as ShippingLog).ShippingRequestType %>
                        </td>
                        <td>
                            <textarea><%# (Container.DataItem as ShippingLog).RequestSent %></textarea>
                        </td>
                        <td>
                            <textarea><%# (Container.DataItem as ShippingLog).ResponseReceived %></textarea>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody> </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="productPagination">
                <asp:Literal runat="server" ID="litPaginationLinks"></asp:Literal>
            </div>
        </div>
    </div>
    <!-- END NEW -->
    <div class="adminToolbar">
        <asp:Button ID="btnSave" runat="server" CssClass="adminIconBtn ok" Text="Save" OnClick="btnSave_Click" />
    </div>
</div>
<!-- HTML templates for jTemplate -->
<textarea id="shippingRowTemplate" rows="0" cols="0" style="display: none;"><!--
{#foreach $T.rates as rate}
<tr>
    <td>                
        <select id="ddlCountryRegion-{$T.rate.RateTypeId}-{$P.rowId}" name="shippingRow[{$T.rate.RateTypeId}][ddlCountryRegion]">
            <%= countryRegionOptionsHtml %>
        </select>
        <script type="text/javascript">
            jQuery('#ddlCountryRegion-{$T.rate.RateTypeId}-{$P.rowId}').val('{$T.rate.CountryRegionValue}');
            globalRowIndex++;
        </script>
    </td>
    <td>
        <input type="text" name="shippingRow[{$T.rate.RateTypeId}][minWeight]" value="{$T.rate.MinWeight}" style="width: 60px;" />
        to <input type="text" name="shippingRow[{$T.rate.RateTypeId}][maxWeight]" value="{$T.rate.MaxWeight}" style="width: 60px;" />         
    </td>
    <td>
        $ <input type="text" name="shippingRow[{$T.rate.RateTypeId}][cost]" value="{$T.rate.Cost}" style="width: 60px;" />
    </td>
    <td><a href="#" title="delete" onclick="jQuery(this).parents('tr').eq(0).remove(); return false;"><img src="<%= ModuleRootImagePath %>icons/delete.png" alt="delete" /></a></td>
</tr>
{#param name=rowId value=$P.rowId+1}
{#/for}
-->
</textarea>
<script type="text/javascript">
    var urlToAjaxHandler = "<%= ModuleRootWebPath %>Modules/Admin/AjaxHandler.ashx";
    var globalRowIndex = 1;
    var optionsLoadingCount = 0;

    jQuery(function ($) {

        // jQuery iButton (must be called before the UI Tabs)
        jQuery('#<%= chkShippingTablesEnabled.ClientID %>, #<%= chkFedExEnabled.ClientID %>, #<%= chkFedExIsTestGateway.ClientID %>, #<%= chkUpsEnabled.ClientID %>, #<%= chkUpsIsTestGateway.ClientID %>').iButton();

        // jQuery UI - Tabs

        <% if(Request.Params["pg"] != null)
           {%>
        $( "#shipServiceTabs" ).tabs({ active: 3 });
        <% } else { %>
        jQuery('#shipServiceTabs').tabs();

        <% } %>
        jQuery('#shippingMethods h2 span.editable').editable({
            submit: 'save',
            cancel: 'cancel',
            onEdit: function () {
                jQuery(this).next('img').hide();
            },
            onSubmit: function (content) {
                //alert(content.current + ':' + content.previous);                
                if (content.current != content.previous) {
                    // update the name
                    var $this = jQuery(this);
                    var shippingServiceRateTypeId = $this.attr('id').replace('shippingId-', '');
                    jQuery.post(urlToAjaxHandler, { 'action': 'updateShippingRateTypeName', 'shippingServiceRateTypeId': shippingServiceRateTypeId, 'name': content.current, 'PortalId': '<%= PortalId %>' }, function (data) {
                        if (data.success) {
                            //console.log('successfully updated shipping name');
                        }
                    }, "json");
                }
                jQuery(this).next('img').show();
            },
            onCancel: function () { jQuery(this).next('img').show(); }
        });

        //--- Form Validation
        jQuery('form').validate({
            onfocusout: false,
            onkeyup: false
        });

        //---- jTemplate
        jQuery('#shippingMethods').block({
            message: '<h1 style="color: white; font-family: verdana; font-size: 14px;"> <img src="<%= ModuleRootImagePath %>ajax-spinner-white.gif" alt="loading" /> Loading shipping options...</h1>'
                , css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: 0.8,
                    color: '#fff'
                }
        });
        jQuery('#shippingMethods > table').each(function (i) {
            var $this = jQuery(this);
            var shipMethodId = $this.attr('id').split('-')[1];
            getShippingRates(shipMethodId);
        });
    });

    function getShippingRates(shippingServiceRateTypeId) {

        optionsLoadingCount++;
        jQuery.post(urlToAjaxHandler, { 'action': 'getShippingRatesJson', 'shippingServiceRateTypeId': shippingServiceRateTypeId, 'PortalId': '<%= PortalId %>' }, function (data) {
            //console.log(data);
            var $tbody = jQuery('tbody', '#tblShippingMethod-' + shippingServiceRateTypeId);
            $tbody.empty();
            $tbody.setTemplateElement("shippingRowTemplate");
            $tbody.setParam('rowId', globalRowIndex);
            var templateData = { 'rates': data };
            $tbody.processTemplate(templateData);
            optionsLoadingCount--;
            if (optionsLoadingCount <= 0) {
                jQuery('#shippingMethods').unblock();
            }
        }, "json");
    }

    function addRateRows(tableId, shippingServiceRateTypeId) {

        var $elm = jQuery('<tbody></tbody>');
        $elm.setTemplateElement("shippingRowTemplate");
        $elm.setParam('rowId', globalRowIndex);
        var templateData = { 'rates': [{ "RateTypeId": shippingServiceRateTypeId }, { "RateTypeId": shippingServiceRateTypeId }, { "RateTypeId": shippingServiceRateTypeId}] };
        $elm.processTemplate(templateData);
        //console.log($fakeTbody.html());
        jQuery('tbody', tableId).append($elm.html());

        return false;
    }        
</script>
