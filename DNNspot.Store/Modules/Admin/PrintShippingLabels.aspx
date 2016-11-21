<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintShippingLabels.aspx.cs"
    Inherits="DNNspot.Store.Modules.Admin.PrintShippingLabels" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Order Details</title>
    <link rel="stylesheet" type="text/css" href="../../module.css" />
</head>
<body id="adminShippingLabelsView">
    <form id="form1" runat="server">
    <h2>
        Shipping Labels</h2>
    <div class="dstore admin<%= this.GetType().BaseType.Name %> adminViewOrder admin">
        <div>
            <asp:Panel ID="pnlOrderDetails" runat="server">
                <h2>Ship to:</h2>
                <ol>
                    <li>

                        <%= order.ShipRecipientName + (!string.IsNullOrEmpty(order.ShipRecipientBusinessName) ? "<br />" + order.ShipRecipientBusinessName : "")%></li>
                    <li style="margin: 0px; padding: 0px;">
                        <label>
                            &nbsp;</label><span>
                                <%= order.ShipAddress1%>
                                <%= order.ShipAddress2%></span> </li>
                    </li>
                    <li style="margin: 0px; padding: 0px;">
                        <label>
                            &nbsp;</label><span> <span>
                                <%= order.ShipCity%>,
                                <%= order.ShipRegion%>
                                <%= order.ShipPostalCode%></span> </li>
                    <li style="margin: 0px; padding: 0px;">
                        <label>
                            &nbsp;</label><span> <span>
                                <%= order.ShipCountryCode%></span> </li>
                </ol>
                <h2>Ship from:</h2>
                <ol>
                    <li><%= StoreName %></li>
                    <li><%= StoreStreet %></li>
                    <li><%= StoreCity %>, <%= StoreRegion %> <%= StorePostalCode %></li>
                    <li><%= StoreCountryCode %></li>

                </ol>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>
