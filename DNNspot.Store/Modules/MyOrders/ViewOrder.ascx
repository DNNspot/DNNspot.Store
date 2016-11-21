<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewOrder.ascx.cs" Inherits="DNNspot.Store.Modules.MyOrders.ViewOrder" %>

<a href="<%= StoreUrls.NavigateUrl(TabId) %>">&laquo; My Orders</a>

<div class="dstore viewMyOrder">

    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
    
    <asp:Panel ID="pnlViewOrder" runat="server">
    
        <% if(order.IsDeleted.GetValueOrDefault(false)) { %>
        <div class="flash">
            This order has been DELETED and will not be fulfilled or updated. It is shown here for archive purposes only.
        </div>
        <% } %>
        
        <h3>Order #<%= order.OrderNumber %> <span style="font-size: 12px;">(<%= order.OrderStatus %>)</span></h3>
        <span>Placed on <%= order.CreatedOn.Value.ToString("MMMM dd, yyyy - hh:mm tt") %></span>
        
        <table cellspacing="0" cellpadding="0" border="0" width="100%" style="width: 100%;">
            <tr>
                <td class="box" style="width: 49%; border: 1px solid #BEBCB7; vertical-align: top;">
                    <h4>Billing Information</h4>
                    <p>
                        <%= order.CustomerFirstName %> <%= order.CustomerLastName %>
                        <br />
                        <%= order.BillAddress1 %> <%= order.BillAddress2 %>                            
                        <br />
                        <%= order.BillCity %>, <%= order.BillRegion %> <%= order.BillPostalCode %>
                        <br />
                        <%= order.BillCountryCode %>
                        <br />
                        Telephone: <%= order.BillTelephone %>
                    </p>
                </td>
                <td  style="width: 2%;">&nbsp;</td>
                <td class="box" style="width: 49%; border: 1px solid #BEBCB7; vertical-align: top;">
                    <h4>Payment (<%= order.PaymentStatus %>)</h4>
                    <p>
                        <asp:Literal ID="litPaymentSummary" runat="server"></asp:Literal>
                    </p>        
                </td>
            </tr>
            <tr>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td class="box" style="width: 49%; border: 1px solid #BEBCB7; vertical-align: top;">
                    <h4>Shipping Information</h4>
                    <p>
                        <%= order.ShipRecipientName + (!string.IsNullOrEmpty(order.ShipRecipientBusinessName) ? "<br />" + order.ShipRecipientBusinessName : "")%>
                        <br />
                        <%= order.ShipAddress1 %> <%= order.ShipAddress2 %>                            
                        <br />
                        <%= order.ShipCity %>, <%= order.ShipRegion %> <%= order.ShipPostalCode %>
                        <br />
                        <%= order.ShipCountryCode %>
                        <br />
                        Telephone: <%= order.ShipTelephone %>
                    </p>
                </td>
                <td style="width: 2%;">&nbsp;</td>
                <td class="box" style="width: 49%; border: 1px solid #BEBCB7; vertical-align: top;">
                    <h4>Shipping Method</h4>
                    <p>
                        <%= order.ShippingServiceOption %>
                        <%= !string.IsNullOrEmpty(order.ShippingServiceTrackingNumber) ? "<br />Tracking #: " + order.ShippingServiceTrackingNumber : "" %>
                    </p>        
                </td>
            </tr>       
        </table>
                                       
        <asp:Repeater ID="rptOrderItems" runat="server">
            <HeaderTemplate>
                <table class="orderItems" cellpadding="0" cellspacing="0">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>SKU</th>
                            <th style="text-align: right">Qty.</th>
                            <th style="text-align: right">Total</th>
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
                            <td><%# !string.IsNullOrEmpty((Container.DataItem as OrderItem).Sku) ? "#" + (Container.DataItem as OrderItem).Sku : "" %></td>
                            <td style="text-align: right"><%# (Container.DataItem as OrderItem).Quantity.Value.ToString("N0") %></td>
                            <td style="text-align: right"><%# (Container.DataItem as OrderItem).PriceTotal.Value.ToString("C2") %></td>
                        </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="3">SubTotal</td>
                            <td><%= order.SubTotal.Value.ToString("C2") %></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                Shipping &amp; Handling
                                <div><%= order.ShippingServiceOption %></div>
                            </td>
                            <td><%= order.ShippingAmount.Value.ToString("C2") %></td>
                        </tr>
                        <% if(order.DiscountAmount > 0) { %>
                        <tr>
                            <td colspan="3">
                                Discount
                                <div><%= order.GetCoupons().ToDelimitedString("<br />", c => string.Format("{0} ({1})", c.CouponCode, StoreContext.CurrentStore.FormatCurrency(c.DiscountAmount))) %></div>
                            </td>
                            <td>(<%= order.DiscountAmount.Value.ToString("C2") %>)</td>
                        </tr>
                        <% } %>
                        <tr>
                            <td colspan="3">Tax</td>
                            <td><%= order.TaxAmount.Value.ToString("C2") %></td>
                        </tr>                                                        
                        <tr>
                            <td colspan="3" class="right" style="font-weight: bold;">Total</td>
                            <td style="font-weight: bold;"><%= order.Total.Value.ToString("C2") %></td>
                        </tr>                                                                                    
                    </tfoot>
                </table>
            </FooterTemplate>
        </asp:Repeater> 
        
        <span>If you have questions about your order, please contact: <%= StoreContext.CurrentStore.GetSetting(StoreSettingNames.CustomerServiceEmailAddress) %></span>      
    
    </asp:Panel>    

</div>