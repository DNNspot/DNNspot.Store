<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckoutComplete.ascx.cs"
    Inherits="DNNspot.Store.Modules.Checkout.CheckoutComplete" %>
<%@ Register TagPrefix="dstore" TagName="AddressForm" Src="../../UserControls/AddressForm.ascx" %>
<asp:Literal ID="flash" runat="server"></asp:Literal>
<asp:Panel ID="pnlOrderDetails" runat="server">
    <div class="dstore checkout checkoutComplete">
        <h2><asp:Literal ID="litCheckoutComplete" runat="server"></asp:Literal></h2>
        <h2>
            Thank You!</h2>
        <h4><asp:Literal ID="litOrderReceived" runat="server"></asp:Literal></h4>
        <p><asp:Literal ID="litYouWillReceiveAnEmail" runat="server"></asp:Literal></p>
        <asp:Panel ID="pnlDigitalDownloads" runat="server" Visible="false">
            <div class="digitalDownloads">
                <h4>
                    Your order contained one or more digital downloads, you may download these files
                    by clicking below:</h4>
                <asp:Repeater ID="rptDigitalFiles" runat="server">
                    <HeaderTemplate>
                        <ul class="digitalFiles">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li><a href="<%# StoreUrls.ProductFile((Container.DataItem as OrderItem).DigitalFilename) %>">
                            <%# (Container.DataItem as OrderItem).Name %>
                            (<%# (Container.DataItem as OrderItem).DigitalFileExtension %>)</a> </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlOrderReceipt" runat="server" CssClass="orderReceipt">
            <h2><asp:Literal ID="litOrderReceipt" runat="server"></asp:Literal></h2>
            <h3>
                <asp:Literal ID="litOrderNumber" runat="server"></asp:Literal><%= order.OrderNumber %>
                (<%= order.OrderStatus %>)</h3>
            <div style="font-size: 12px; font-style: italic; margin-bottom: 6px;">
                Created
                <%= order.CreatedOn.Value.ToString("MMMM dd, yyyy  hh:mm:ss tt") %></div>
            <a target="_blank" href="<%= StoreUrls.PrintOrderDetailsUrlBase + order.CreatedFromCartId.ToString() %>"
                class="adminIconBtn" style="display: block;"><asp:Literal ID="litPrintOrderDetails" runat="server"></asp:Literal></a>
            <table cellspacing="0" cellpadding="0" border="0" width="100%" style="width: 100%;">
                <tr>
                    <td class="box" style="width: 49%; border: 1px solid #BEBCB7; vertical-align: top;">
                        <h4><asp:Literal id="litBillingInformation" runat="server"></asp:Literal></h4>
                        <p>
                            <%= order.CustomerFirstName %>
                            <%= order.CustomerLastName %>
                            <br />
                            <%= order.BillAddress1 %>
                            <%= order.BillAddress2 %>
                            <br />
                            <%= order.BillCity %>,
                            <%= order.BillRegion %>
                            <%= order.BillPostalCode %>
                            <br />
                            <%= order.BillCountryCode %>
                            <br />
                            Telephone:
                            <%= order.BillTelephone %>
                        </p>
                    </td>
                    <td style="width: 2%;">
                        &nbsp;
                    </td>
                    <td class="box checkoutCompletePaymentDetails" style="width: 49%; border: 1px solid #BEBCB7; vertical-align: top;">
                        <h4>
                            Payment (<%= order.PaymentStatus %>)</h4>
                        <p>
                            <asp:Literal ID="litPaymentSummary" runat="server"></asp:Literal>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                </tr>
                <% if (order.HasShippableItems)
                   { %>
                <tr>
                    <td class="box shippingInformation" style="width: 49%; border: 1px solid #BEBCB7; vertical-align: top;">
                        <h4>
                            Shipping Information</h4>
                        <p>
                            <%= order.ShipRecipientName + (!string.IsNullOrEmpty(order.ShipRecipientBusinessName) ? "<br />" + order.ShipRecipientBusinessName : "")%>
                            <br />
                            <%= order.ShipAddress1%>
                            <%= order.ShipAddress2%>
                            <br />
                            <%= order.ShipCity%>,
                            <%= order.ShipRegion%>
                            <%= order.ShipPostalCode%>
                            <br />
                            <%= order.ShipCountryCode%>
                            <br />
                            Telephone:
                            <%= order.ShipTelephone%>
                        </p>
                    </td>
                    <td style="width: 2%;">
                        &nbsp;
                    </td>
                    <td class="box shippingMethod" style="width: 49%; border: 1px solid #BEBCB7; vertical-align: top;">
                        <h4>
                            Shipping Method</h4>
                        <p>
                            <%= order.ShippingServiceOption%>
                        </p>
                    </td>
                </tr>
                <% } %>
                <% else { %>
                <tr><td><asp:Literal ID="litNoShippableItems" runat="server"></asp:Literal></td></tr>
                <% } %>
            </table>
            <asp:Repeater ID="rptOrderItems" runat="server">
                <HeaderTemplate>
                    <table class="orderItems" cellpadding="0" cellspacing="0">
                        <thead>
                            <tr>
                                <th>
                                    Name
                                </th>
                                <th>
                                    SKU
                                </th>
                                <th style="text-align: right">
                                    Qty.
                                </th>
                                <th style="text-align: right" class="orderCompleteTotalCol">
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
                            <%# (Container.DataItem as OrderItem).HasDigitalFile ? string.Format(@"<a href=""{0}"">{1}</a>", StoreUrls.ProductFile((Container.DataItem as OrderItem).DigitalFilename), (Container.DataItem as OrderItem).DigitalFileDisplayName) : ""%>
                        </td>
                        <td>
                            #<%# (Container.DataItem as OrderItem).Sku %>
                        </td>
                        <td style="text-align: right">
                            <%# (Container.DataItem as OrderItem).Quantity.Value.ToString("N0") %>
                        </td>
                        <td style="text-align: right" class="orderCompletePayment">
                            <%# StoreContext.CurrentStore.FormatCurrency((Container.DataItem as OrderItem).PriceTotal.Value) %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot class="orderCompleteTotals">
                        <tr>
                            <td colspan="3">
                                SubTotal
                            </td>
                            <td>
                                <%= order.SubTotal.Value.ToString("C2") %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                Shipping &amp; Handling
                                <div>
                                    <%= order.ShippingServiceOption %></div>
                            </td>
                            <td>
                                <%= StoreContext.CurrentStore.FormatCurrency(order.ShippingAmount.Value) %>
                            </td>
                        </tr>
                        <% if (order.DiscountAmount > 0)
                           { %>
                        <tr>
                            <td colspan="3">
                                Discount
                                <div>
                                    <%= order.GetCoupons().ToDelimitedString("<br />", c => string.Format("{0} ({1})", c.CouponCode, StoreContext.CurrentStore.FormatCurrency(c.DiscountAmount.Value))) %></div>
                            </td>
                            <td>
                                (<%= StoreContext.CurrentStore.FormatCurrency(order.DiscountAmount.Value) %>)
                            </td>
                        </tr>
                        <% } %>
                        <tr>
                            <td colspan="3">
                                Tax
                            </td>
                            <td>
                                <%= StoreContext.CurrentStore.FormatCurrency(order.TaxAmount.Value) %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="right" style="font-weight: bold;">
                                Total
                            </td>
                            <td style="font-weight: bold;">
                                <%= StoreContext.CurrentStore.FormatCurrency(order.Total) %>
                            </td>
                        </tr>
                    </tfoot>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        <input type="button" id="btnContinueShopping" class="dnnPrimaryAction"
           value="Continue Shopping" onclick="location.href='<%= StoreUrls.CategoryHome() %>'; return false;" />
    </div>
    <script type="text/javascript">

        jQuery(function ($) {

        });

    </script>
</asp:Panel>
