<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintOrder.aspx.cs" Inherits="DNNspot.Store.Modules.Admin.PrintOrder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Order Details</title>
    <link rel="stylesheet" type="text/css" href="../../module.css" />
</head>
<body id="adminPrintOrderView">
    <form id="form1" runat="server">
    <h1>
        <%= storeContext.CurrentStore.Name %></h1>
    <h2>
        Order Details</h2>
    <div class="dstore admin<%= this.GetType().BaseType.Name %> adminViewOrder admin">
        <div>
            <asp:Panel ID="pnlOrderDetails" runat="server">
                <div class="box" id="orderBox">
                    <fieldset>
                        <legend>Order Details</legend>
                        <div>
                            <div class="printColumn">
                                <ol class="form labelsLeft">
                                    <li>
                                        <label>
                                            Order Id:</label>
                                        <span>
                                            <%= order.Id %>
                                        </span></li>
                                    <li>
                                        <label>
                                            Order Date:</label>
                                        <span>
                                            <%= order.CreatedOn.Value.ToString() %></span> </li>
                                    <li>
                                        <label>
                                            Order Status:</label>
                                        <span>
                                            <%= order.OrderStatus %>
                                            <asp:DropDownList ID="ddlOrderStatus" runat="server" Visible="false">
                                            </asp:DropDownList>
                                        </span></li>
                                </ol>
                            </div>
                            <div class="printColumn paymentDetails">
                                <ol class="form labelsLeft left">
                                    <li>
                                        <label>
                                            <strong>Payment</strong></label></li>
                                    <li>
                                        <label>
                                            Payment Status:</label>
                                        <span>
                                            <%= order.PaymentStatus %>
                                            <asp:DropDownList ID="ddlPaymentStatus" runat="server" Visible="false">
                                            </asp:DropDownList>
                                        </span></li>
                                    <li>
                                        <label>
                                            Payment Processor:</label>
                                        <span>
                                            <%= order.GetPaymentProviderName() %>
                                        </span></li>
                                </ol>
                                <ol class="form labelsLeft rightAlign">
                                    <li class="creditCard">
                                        <label>
                                            &nbsp;</label>
                                        <span>
                                            <% if ((order.PaymentStatus != PaymentStatusName.Completed) && UserIsAdmin)
                                               { %>
                                            <%= order.GetPaymentSummary(true) %>
                                            <% }
                                               else
                                               { %>
                                            <%= order.PaymentSummary%>
                                            <% } %>
                                        </span></li>
                                </ol>
                            </div>
                        </div>
                        <hr style="margin:10px 0px;width:100%;display:inline-block;" />
                        <div>
                            <div class="printColumn billingInformation">
                                <ol class="form labelsLeft">
                                    <li>
                                        <label>
                                            <strong>Billing</strong></label></li>
                                    <li>
                                        <label>
                                            Customer Info</label><span>
                                                <%= order.CustomerFirstName %>
                                                <%= order.CustomerLastName %><br />
                                                <%= order.BillAddress1 %><br />
                                                <%= !string.IsNullOrEmpty(order.BillAddress2) ? string.Format("<br />{0}", order.BillAddress2) : "" %>
                                                <span>
                                                    <%= order.BillCity %>,
                                                    <%= order.BillRegion %>
                                                    <%= order.BillPostalCode %></span><span>
                                                        <%= order.BillCountryCode %></span> </li>
                                    <li>
                                        <label>
                                            Telephone:</label>
                                        <span>
                                            <%= order.BillTelephone %></span> </li>
                                    <li>
                                        <label>
                                            Email:</label><span>
                                                <%= order.CustomerEmail %></span></li>
                                </ol>
                            </div>
                            <% if (order.HasShippableItems)
                               { %>
                            <div class="printColumn billingInformation">
                                <ol class="form labelsLeft">
                                    <li>
                                        <label>
                                            <strong>Billing</strong></label></li>
                                    <li>
                                        <label>
                                            Customer Info</label><span>
                                                <%= order.CustomerFirstName %>
                                                <%= order.CustomerLastName %><br />
                                                <%= order.BillAddress1 %><br />
                                                <%= !string.IsNullOrEmpty(order.BillAddress2) ? string.Format("<br />{0}", order.BillAddress2) : "" %>
                                                <span>
                                                    <%= order.BillCity %>,
                                                    <%= order.BillRegion %>
                                                    <%= order.BillPostalCode %></span><span>
                                                        <%= order.BillCountryCode %></span> </li>
                                    <li>
                                        <label>
                                            Telephone:</label>
                                        <span>
                                            <%= order.BillTelephone %></span> </li>
                                    <li>
                                        <label>
                                            Email:</label><span>
                                                <%= order.CustomerEmail %></span></li>
                                </ol>
                            </div>
                            <div class="box printColumn shippingInformation" id="shippingBox">
                                <fieldset>
                                    <legend>Shipping</legend>
                                    <ol class="form labelsLeft">
                                        <li>
                                            <label>
                                                Shipping Option:</label>
                                            <span>
                                                <%= order.ShippingServiceProvider%>
                                                <%= order.ShippingServiceOption%> </li>
                                        <li>
                                            <label>
                                                Tracking #:</label>
                                            <span>
                                                <% foreach (string trackingNumber in order.TrackingNumbers)
                                                   { %>
                                                <span style="display: block;">
                                                    <%= trackingNumber%></span>
                                                <% } %>
                                                <asp:TextBox ID="txtShippingTrackingNumber" runat="server" Style="width: 180px; height: 22px;
                                                    margin-right: 4px;"></asp:TextBox>
                                            </span></li>
                                        <li>
                                            <label>
                                                Ship to:</label>
                                            <%= order.ShipRecipientName + (!string.IsNullOrEmpty(order.ShipRecipientBusinessName) ? "<br />" + order.ShipRecipientBusinessName : "")%>
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
                                        <li>
                                            <label>
                                                Telephone:</label>
                                            <span>
                                                <%= order.ShipTelephone%></span> </li>
                                    </ol>
                                </fieldset>
                            </div>
                            <% } %>
                        </div>
                    </fieldset>
                </div>
                <%--<div class="box" id="customerBox">
                    <fieldset>
                        <legend>Customer</legend>
                        <ol class="form labelsLeft">
                            <li>
                                <label>
                                    Name:</label>
                                <span>
                                    <%= order.CustomerFirstName %>
                                    <%= order.CustomerLastName %>
                                    <%= order.UserId.HasValue ? string.Format(@"&nbsp;( <a href=""{0}"">{1}</a> )", storeUrls.UserProfileUrl(order.UserId.Value), DnnHelper.GetUserInfo(order.UserId.Value, PortalId).Username) : "" %>
                                </span></li>
                            <li>
                                <label>
                                    Email:</label>
                                <span>
                                    <%= !string.IsNullOrEmpty(order.CustomerEmail) ? string.Format(@"<a href=""mailto:{0}?subject=Order # {1}"">{0}</a>", order.CustomerEmail, order.OrderNumber) : "" %></span>
                            </li>
                        </ol>
                    </fieldset>
                </div>--%>
                <% if (!string.IsNullOrEmpty(order.OrderNotes))
                   { %>
                <div class="box" id="orderNotes">
                    <fieldset>
                        <legend>Order Notes</legend>
                        <%= order.OrderNotes %>
                    </fieldset>
                </div>
                <% } %>
                <div class="box" id="orderItemsBox">
                    <fieldset>
                        <legend>Order Items</legend>
                        <asp:Repeater ID="rptOrderItems" runat="server">
                            <HeaderTemplate>
                                <table class="grid gridLight" cellpadding="0" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>
                                                Name
                                            </th>
                                            <th>
                                                SKU
                                            </th>
                                            <th class="rightAlign">
                                                Qty.
                                            </th>
                                            <th class="rightAlign printTotals">
                                                Total
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# (Container.DataItem as OrderItem).Name %>
                                        <%# (Container.DataItem as OrderItem).GetProductFieldDataHtmlDisplayString() %>
                                        <%# (Container.DataItem as OrderItem).HasDigitalFile ? string.Format(@"(Download: <a href=""{0}"">{1}</a>)", storeUrls.ProductFile((Container.DataItem as OrderItem).DigitalFilename), (Container.DataItem as OrderItem).Name) : ""%>
                                    </td>
                                    <td>
                                        <%# (Container.DataItem as OrderItem).Sku %>
                                    </td>
                                    <td class="rightAlign">
                                        <%# (Container.DataItem as OrderItem).Quantity.Value.ToString("N0") %>
                                    </td>
                                    <td class="rightAlign printCost">
                                        <%# (Container.DataItem as OrderItem).PriceTotal.Value.ToString("C2") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                <tfoot class="printFooterTotals">
                                    <tr>
                                        <td colspan="3" class="rightAlign printSubTotal">
                                            SubTotal
                                        </td>
                                        <td class="rightAlign printSubTotal">
                                            <%= order.SubTotal.Value.ToString("C2") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="rightAlign printShippingAndHandling">
                                            Shipping &amp; Handling
                                        </td>
                                        <td class="rightAlign printShippingAndHandling">
                                            <%= order.ShippingAmount.Value.ToString("C2") %>
                                        </td>
                                    </tr>
                                    <% if (order.DiscountAmount.GetValueOrDefault(0) > 0)
                                       { %>
                                    <tr>
                                        <td colspan="3" class="rightAlign printDiscount">
                                            Discount
                                            <div>
                                                <%= order.GetCoupons().ToDelimitedString("<br />", c => string.Format("{0} ({1})", c.CouponCode, storeContext.CurrentStore.FormatCurrency(c.DiscountAmount))) %></div>
                                        </td>
                                        <td class="rightAlign printDiscount">
                                            (<%= order.DiscountAmount.Value.ToString("C2") %>)
                                        </td>
                                    </tr>
                                    <% } %>
                                    <tr>
                                        <td colspan="3" class="rightAlign printTaxAmount">
                                            Tax
                                        </td>
                                        <td class="rightAlign printTaxAmount">
                                            <%= order.TaxAmount.Value.ToString("C2") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="rightAlign printTotal" style="font-weight: bold;">
                                            Total
                                        </td>
                                        <td class="rightAlign printTotal" style="font-weight: bold;">
                                            <%= order.Total.Value.ToString("C2") %>
                                        </td>
                                    </tr>
                                </tfoot>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </fieldset>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
