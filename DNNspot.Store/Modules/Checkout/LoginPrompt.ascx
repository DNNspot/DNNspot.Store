<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginPrompt.ascx.cs" Inherits="DNNspot.Store.Modules.Checkout.LoginPrompt" %>

<div class="dstore loginPrompt">
    <h2><asp:Literal ID="litCheckoutLoginOrContinue" runat="server"></asp:Literal></h2>
    
    <asp:Panel ID="pnlAccount" runat="server" CssClass="box account">
        <h4>Yes, I have a user account</h4>
        
        <asp:Panel ID="pnlLoginError" runat="server" Visible="false" CssClass="loginError">
            Login failed: <asp:Literal ID="litLoginError" runat="server"></asp:Literal>
        </asp:Panel>
        
        <fieldset>
            <ol>
                <li>
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtUsername" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="txtPassword" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                </li>               
                <li>
                    <asp:Button ID="btnLoginAndCheckout" runat="server" OnClick="btnLoginAndCheckout_Click" />
                </li> 
            </ol>
        </fieldset>
    </asp:Panel>
    
    <asp:Panel ID="pnlNoAccount" runat="server" CssClass="box noAccount">        
        <h4>No, I don't have an account</h4>        
        <p>
            <% if(allowAnonymous) { %>
            <p>An account is not required to place an order.</p>
            <span class="action">
                <% if(showRegisterLink) { %>
                <span class="action"><a href="<%= StoreUrls.CreateAccount(string.Empty) %><%= returnUrl %>">Create a new account</a></span>
                <p>- or -</p>
                <% } %>
                <button onclick="location.href='<%= StoreUrls.Checkout() %>'; return false;"><asp:Literal ID="litCheckoutAsAGuest" runat="server"></asp:Literal></button>
            </span>
            <% } else if(showRegisterLink) { %>
                <p>Please create an account to continue placing your order.</p>
                <span class="action"><a href="<%= StoreUrls.CreateAccount(string.Empty) %><%= returnUrl %>">Create a new account</a></span>
            <% } else { %>
                <p>Please login to continue placing your order.</p>
            <% } %>
        </p>
    </asp:Panel>
    
</div>