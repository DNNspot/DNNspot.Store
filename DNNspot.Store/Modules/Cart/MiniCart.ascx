<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MiniCart.ascx.cs" Inherits="DNNspot.Store.Modules.Cart.MiniCart" %>
<div class="dstore minicart">
    <asp:Panel ID="pnlCart" runat="server">
        <img src="<%= ModuleRootImagePath %>icons/cart.png" title="cart" alt="cart" />
        <%= UserCart.ItemCount %>
        item(s)
        <% if (WA.Parser.ToBool(StoreContext.CurrentStore.GetSetting(StoreSettingNames.ShowPrices)).GetValueOrDefault(true))
           {%>:
        <%=StoreContext.CurrentStore.FormatCurrency(UserCart.GetTotal())%>
        <%
                       }%>
        <p>
            <a href="<%= StoreUrls.Cart() %>">
                <asp:Literal ID="litViewCart" runat="server"></asp:Literal></a>
        </p>
    </asp:Panel>
    <asp:Panel ID="pnlEmptyCart" runat="server">
        <asp:Literal ID="litCartIsEmpty" runat="server"></asp:Literal>
    </asp:Panel>
</div>
