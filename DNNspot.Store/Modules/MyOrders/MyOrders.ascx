<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyOrders.ascx.cs" Inherits="DNNspot.Store.Modules.MyOrders.MyOrders" %>

<div class="dstore myOrders">

    <asp:Panel ID="pnlFindOrder" runat="server" DefaultButton="btnFindOrder" CssClass="findOrder">    
        <h4>Find an Order:</h4>
        <ol class="form labelsLeft">
            <li>
                <label>Order #:</label>
                <span>
                    <asp:TextBox ID="txtOrderNumber" runat="server" style="width: 150px;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvOrderNumber" runat="server" ControlToValidate="txtOrderNumber" ValidationGroup="findOrder" ErrorMessage=" Order # required"> * required</asp:RequiredFieldValidator>
                </span>
            </li>
            <li>
                <label>Email:</label>
                <span>
                    <asp:TextBox ID="txtOrderEmail" runat="server" style="width: 250px;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvOrderEmail" runat="server" ControlToValidate="txtOrderEmail" ValidationGroup="findOrder" ErrorMessage=" Email required"> * required</asp:RequiredFieldValidator>
                </span>
            </li>            
            <li>
                <label>&nbsp;</label>
                <span>
                    <asp:Button ID="btnFindOrder" runat="server" Text="Find Order" ValidationGroup="findOrder" OnClick="btnFindOrder_Click" />
                </span>
            </li>
        </ol>
    </asp:Panel>
    
    <asp:Panel ID="pnlSearchResults" runat="server" CssClass="searchResults" Visible="false">        
        <h4>Search Results:</h4>
        <asp:Repeater ID="rptSearchResults" runat="server">
            <HeaderTemplate>
                <table class="" cellpadding="0" cellspacing="0">
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>                
                        <tr <%# Container.ItemIndex % 2 == 1 ? "class=\"alt\"" : "" %>>
                            <td><%# (Container.DataItem as Order).CreatedOn.Value.ToShortDateString() %></td>
                            <td><a href="<%# StoreUrls.DispatchView("ViewOrder", "id=" + (Container.DataItem as Order).Id) %>"><%# (Container.DataItem as Order).OrderNumber %></a></td>
                            <td><%# (Container.DataItem as Order).OrderStatus %></td>
                            <td class="price"><%# StoreContext.CurrentStore.FormatCurrency((Container.DataItem as Order).Total.Value)%></td>
                        </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>    
        <asp:Panel ID="pnlNoResults" runat="server" Visible="false">
            No order(s) matched your criteria.
        </asp:Panel>    
    </asp:Panel>    
    
    <asp:Panel ID="pnlRecentOrders" runat="server" CssClass="recentOrders" Visible="false">        
        <h4>Recent Orders:</h4>        
        <asp:Repeater ID="rptRecentOrders" runat="server">
            <HeaderTemplate>
                <table class="" cellpadding="0" cellspacing="0">
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                        <tr <%# Container.ItemIndex % 2 == 1 ? "class=\"alt\"" : "" %>>
                            <td><%# (Container.DataItem as Order).CreatedOn.Value.ToShortDateString() %></td>
                            <td><%# GetOrderNumberLink(Container.DataItem as Order) %></td>
                            <td><%# (Container.DataItem as Order).OrderStatus %></td>
                            <td class="price"><%# StoreContext.CurrentStore.FormatCurrency((Container.DataItem as Order).Total.Value)%></td>
                        </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>      
        <asp:Panel ID="pnlNoRecentOrders" runat="server" Visible="false">
            You have not placed any orders with this store.
        </asp:Panel>         
        <span id="spnShowMore" runat="server" visible="false"><a href="<%= StoreUrls.DispatchView("", "more=true") %>">more...</a></span>  
    </asp:Panel>        
    
</div>

<script runat="server">
public string GetOrderNumberLink(Order order)
{
    if(order.IsDeleted.GetValueOrDefault(false))
    {
        return string.Format("{0} (DELETED)", order.OrderNumber);
    }
    return string.Format(@"<a href=""{0}"">{1}</a>", StoreUrls.DispatchView("ViewOrder", "id=" + order.Id), order.OrderNumber);
}
</script>