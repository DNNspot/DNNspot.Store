<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewOrder.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.ViewOrder" %>
<%@ Import Namespace="WA.Extensions" %>
<div class="dstore admin<%= this.GetType().BaseType.Name %>">
    <h1>
        Order Details for #<%= order.OrderNumber %></h1>
    <asp:Literal ID="flash" runat="server"></asp:Literal>
    <div class="adminToolbar">
        <asp:Button ID="btnMarkOfflinePaid" runat="server" Text="Mark as Paid" OnClick="btnMarkPaid_Click"
            Visible="false" CssClass="adminIconBtn dollar" />
        <asp:Button ID="btnGetShippingTrackingLabels" runat="server" Text="UPS/FedEx Tracking # and Labels"
            CssClass="adminIconBtn box" OnClick="btnGetShippingTrackingLabels_Click" />
        <asp:Button ID="btnMarkShipped" runat="server" Text="Mark as Shipped / Complete Order"
            CssClass="adminIconBtn lorry" OnClick="btnMarkShipped_Click" />
        <asp:Button ID="btnSendShippingEmail" runat="server" Text="Re-Send Shipping notification email"
            CssClass="adminIconBtn noicon" OnClick="btnSendShippingEmail_Click" />
        <asp:Button ID="btnEmailCustomer" runat="server" Text="Send Email to Customer" CssClass="adminIconBtn email2"
            OnClick="btnEmailCustomer_Click" />
        <a target="_blank" href="<%= StoreUrls.PrintOrderDetailsUrlBase + order.CreatedFromCartId.ToString() %>"
            class="adminIconBtn" style="display: block;">Print Order Details</a>
        <% if (order.HasShippableItems)
           { %>
        <a target="_blank" href="<%= StoreUrls.PrintShippingLabelsUrlBase + order.CreatedFromCartId.ToString() %>"
            class="adminIconBtn" style="display: block;">Print Basic Shipping Label</a>
        <% } %>
        <div style="clear: both; padding: 1em 1em 0 1em;" id="divShipmentPackaging" runat="server">
            <label style="font-weight: bold;">
                Shipment Packaging:</label>
            <asp:DropDownList runat="server" ID="ddlPackageGrouping">
                <asp:ListItem Value="SingleBox" Text="Single Box - Entire order in one box"></asp:ListItem>
                <asp:ListItem Value="BoxPerProductType" Text="Multiple Boxes - One box per product type"></asp:ListItem>
                <asp:ListItem Value="BoxPerItem" Text="Multiple Boxes - One box per item"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="box" id="orderBox">
        <fieldset>
            <legend>Order</legend>
            <ol class="form labelsLeft">
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
        </fieldset>
    </div>
    <div class="box" id="customerBox">
        <fieldset>
            <legend>Customer</legend>
            <ol class="form labelsLeft">
                <li>
                    <label>
                        Name:</label>
                    <span>
                        <%= order.CustomerFirstName %>
                        <%= order.CustomerLastName %>
                        <%= order.UserId.HasValue ? string.Format(@"&nbsp;( <a href=""{0}"">{1}</a> )", StoreUrls.UserProfileUrl(order.UserId.Value), DnnHelper.GetUserInfo(order.UserId.Value, PortalId).Username) : "" %>
                    </span></li>
                <li>
                    <label>
                        Email:</label>
                    <span>
                        <%= !string.IsNullOrEmpty(order.CustomerEmail) ? string.Format(@"<a href=""mailto:{0}?subject=Order # {1}"">{0}</a>", order.CustomerEmail, order.OrderNumber) : "" %></span>
                </li>
            </ol>
        </fieldset>
    </div>
    <div class="box" id="paymentBox">
        <fieldset>
            <legend>Payment</legend>
            <ol class="form labelsLeft left">
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
                        <% if (rptPaymentTransactions.Items.Count > 0)
                           { %>
                        &nbsp;&nbsp;<a href="#" onclick="jQuery('#paymentTransactions').toggle(); return false;">Payment
                            Transactions</a>
                        <% } %>
                    </span></li>
            </ol>
            <ol class="form labelsLeft right">
                <li class="creditCard">
                    <label>
                        &nbsp;</label>
                    <span>
                        <%= order.PaymentSummary %>
                    </span></li>
            </ol>
            <div id="paymentTransactions" class="paymentTransactions" style="display: none;">
                <asp:Repeater ID="rptPaymentTransactions" runat="server">
                    <HeaderTemplate>
                        <table class="grid gridLight" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        Date
                                    </th>
                                    <th>
                                        Amount
                                    </th>
                                    <th>
                                        Processor
                                    </th>
                                    <th>
                                        Transaction Id
                                    </th>
                                    <th>
                                        Response
                                    </th>
                                    <th>
                                        Errors
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# (Container.DataItem as PaymentTransaction).CreatedOn.Value.ToString() %>
                            </td>
                            <td>
                                <%# (Container.DataItem as PaymentTransaction).Amount.HasValue ? (Container.DataItem as PaymentTransaction).Amount.Value.ToString("C2") : "" %>
                            </td>
                            <td>
                                <%# (Container.DataItem as PaymentTransaction).UpToPaymentProviderByPaymentProviderId.Name %>
                            </td>
                            <td>
                                <%# (Container.DataItem as PaymentTransaction).GatewayTransactionId %>
                            </td>
                            <td>
                                <%# (Container.DataItem as PaymentTransaction).GatewayResponse %>
                                &nbsp;&nbsp;<a href="#" onclick="jQuery('#response-<%# (Container.DataItem as PaymentTransaction).Id %>').toggle(); return false;"
                                    <%# string.IsNullOrEmpty((Container.DataItem as PaymentTransaction).GatewayDebugResponse) ? "style='display:none;'" : "" %>>(more)</a>
                                <div id="response-<%# (Container.DataItem as PaymentTransaction).Id %>" style="display: none;">
                                    <textarea><%# (Container.DataItem as PaymentTransaction).GatewayDebugResponse %></textarea></div>
                            </td>
                            <td>
                                <%# (Container.DataItem as PaymentTransaction).GatewayError %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </fieldset>
    </div>
    <div class="box" id="billingBox">
        <fieldset>
            <legend>Billing</legend>
            <ol class="form labelsLeft">
                <li>
                    <label>
                        Name:</label>
                    <span>
                        <%= order.CustomerFirstName %>
                        <%= order.CustomerLastName %></span> </li>
                <li>
                    <label>
                        Email:</label>
                    <span>
                        <%= order.CustomerEmail %></span> </li>
                <li>
                    <label>
                        Address:</label>
                    <span>
                        <%= order.BillAddress1 %>
                        <%= !string.IsNullOrEmpty(order.BillAddress2) ? string.Format("<br />{0}", order.BillAddress2) : "" %>
                    </span></li>
                <li>
                    <label>
                        City:</label>
                    <span>
                        <%= order.BillCity %></span> </li>
                <li>
                    <label>
                        State/Region:</label>
                    <span>
                        <%= order.BillRegion %></span> </li>
                <li>
                    <label>
                        Zip/Postal Code:</label>
                    <span>
                        <%= order.BillPostalCode %></span> </li>
                <li>
                    <label>
                        Country:</label>
                    <span>
                        <%= order.BillCountryCode %></span> </li>
                <li>
                    <label>
                        Telephone:</label>
                    <span>
                        <%= order.BillTelephone %></span> </li>
            </ol>
        </fieldset>
    </div>
    <% if (order.HasShippableItems)
       { %>
    <div class="box" id="shippingBox">
        <fieldset>
            <legend>Shipping</legend>
            <ol class="form labelsLeft">
                <li>
                    <label>
                        Shipping Option:</label>
                    <span>
                        <%= order.ShippingServiceProvider %>
                        <%= order.ShippingServiceOption %>
                        <%= order.ShippingServiceType %> </li>
                <li>
                    <label>
                        Tracking #:</label>
                    <span>
                        <% foreach (string trackingNumber in order.TrackingNumbers)
                           { %>
                        <span style="display: block;">
                            <%= trackingNumber %></span>
                        <% } %>
                        <asp:TextBox ID="txtShippingTrackingNumber" runat="server" Style="width: 180px; height: 22px;
                            margin-right: 4px;"></asp:TextBox>
                        <asp:Button ID="btnSaveTrackingNumber" runat="server" Text="Save" CssClass="adminIconBtn noicon"
                            Style="float: right;" OnClick="btnSaveTrackingNumber_Click" />
                    </span></li>
                <% if (order.HasShippingLabel)
                   { %>
                <li>
                    <label>
                        Shipping Labels:</label>
                    <span>
                        <% foreach (string label in order.ShippingLabels)
                           { %>
                        <a target="_blank" href="<%= StoreUrls.ShippingLabelFolderUrlRoot + label %>" style="display: block;">
                            <%= label %></a>
                        <% } %>
                    </span></li>
                <% } %>
                <li>
                    <label>
                        Business Name:</label>
                    <span>
                        <%= order.ShipRecipientBusinessName %></span> </li>
                <li>
                    <label>
                        Name:</label>
                    <span>
                        <%= order.ShipRecipientName %></span> </li>
                <li>
                    <label>
                        Address:</label>
                    <span>
                        <%= order.ShipAddress1 %>
                        <%= !string.IsNullOrEmpty(order.ShipAddress2) ? string.Format("<br />{0}", order.ShipAddress2) : ""%>
                    </span></li>
                <li>
                    <label>
                        City:</label>
                    <span>
                        <%= order.ShipCity %></span> </li>
                <li>
                    <label>
                        State/Region:</label>
                    <span>
                        <%= order.ShipRegion %></span> </li>
                <li>
                    <label>
                        Zip/Postal Code:</label>
                    <span>
                        <%= order.ShipPostalCode %></span> </li>
                <li>
                    <label>
                        Country:</label>
                    <span>
                        <%= order.ShipCountryCode %></span> </li>
                <li>
                    <label>
                        Telephone:</label>
                    <span>
                        <%= order.ShipTelephone %></span> </li>
            </ol>
        </fieldset>
    </div>
    <div class="box" id="shippingTransactionLog">
        <fieldset>
            <legend>Shipping Transaction Logs</legend>
            <% if (rptShippingLog.Items.Count > 0)
               { %>
            &nbsp;&nbsp;<a href="#" onclick="jQuery('#shippingLog').toggle(); return false;">Shipping
                Request Log</a>
            <% } %>
            <div id="shippingLog" class="paymentTransactions" style="display: none;">
                <asp:Repeater ID="rptShippingLog" runat="server">
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
            </div>
        </fieldset>
    </div>
    <% } %>
    <% else
       { %>
    <div class="box">
        <fieldset>
            <legend>Shipping</legend>
            <p>
                This order has no shippable items.</p>
        </fieldset>
    </div>
    <% } %>
    <div class="box" id="orderNotes">
        <fieldset>
            <legend>Order Notes</legend>
            <%= order.OrderNotes %>
        </fieldset>
    </div>
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
                                <th class="rightAlign">
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
                            <%# (Container.DataItem as OrderItem).HasDigitalFile ? string.Format(@"(Download: <a href=""{0}"">{1}</a>)", StoreUrls.ProductFile((Container.DataItem as OrderItem).DigitalFilename), (Container.DataItem as OrderItem).Name) : ""%>
                        </td>
                        <td>
                            <%# (Container.DataItem as OrderItem).Sku %>
                        </td>
                        <td class="rightAlign">
                            <%# (Container.DataItem as OrderItem).Quantity.Value.ToString("N0") %>
                        </td>
                        <td class="rightAlign">
                            <%# (Container.DataItem as OrderItem).PriceTotal.Value.ToString("C2") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="3" class="rightAlign">
                                SubTotal
                            </td>
                            <td class="rightAlign">
                                <%= order.SubTotal.Value.ToString("C2") %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="rightAlign">
                                Shipping &amp; Handling
                            </td>
                            <td class="rightAlign">
                                <%= order.ShippingAmount.Value.ToString("C2") %>
                            </td>
                        </tr>
                        <% if (order.DiscountAmount.GetValueOrDefault(0) > 0)
                           { %>
                        <tr>
                            <td colspan="3" class="rightAlign">
                                Discount
                                <div>
                                    <%= order.GetCoupons().ToDelimitedString("<br />", c => string.Format("{0} ({1})", c.CouponCode, StoreContext.CurrentStore.FormatCurrency(c.DiscountAmount))) %></div>
                            </td>
                            <td class="rightAlign">
                                (<%= order.DiscountAmount.Value.ToString("C2") %>)
                            </td>
                        </tr>
                        <% } %>
                        <tr>
                            <td colspan="3" class="rightAlign">
                                Tax
                            </td>
                            <td class="rightAlign">
                                <%= order.TaxAmount.Value.ToString("C2") %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="rightAlign" style="font-weight: bold;">
                                Total
                            </td>
                            <td class="rightAlign" style="font-weight: bold;">
                                <%= order.Total.Value.ToString("C2") %>
                            </td>
                        </tr>
                    </tfoot>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </fieldset>
    </div>
</div>
<script type="text/javascript">
    var $ddlOrderStatus = null;
    var $ddlPaymentStatus = null;
    var $txtTrackingNumber = null;

    jQuery(function ($) {

        $ddlOrderStatus = jQuery('#<%=ddlOrderStatus.ClientID %>');
        $ddlPaymentStatus = jQuery('#<%=ddlPaymentStatus.ClientID %>');
        $txtTrackingNumber = jQuery('#<%=txtShippingTrackingNumber.ClientID %>');

        $ddlOrderStatus.change(function () {
            if (jQuery(this).val() == '<%= OrderStatusName.Completed %>' && $ddlPaymentStatus.val() != '<%= PaymentStatusName.Completed %>') {
                if (confirm('You have changed the Order Status to "Completed", would you like to also change Payment Status to "Completed" ?')) {
                    $ddlPaymentStatus.val('<%= PaymentStatusName.Completed %>');
                }
            }
        });

        jQuery('a.fancyInline').fancybox({
            'hideOnContentClick': false
        });
    });
</script>
