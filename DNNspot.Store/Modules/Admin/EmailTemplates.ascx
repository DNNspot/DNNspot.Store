<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailTemplates.ascx.cs" Inherits="DNNspot.Store.EmailTemplates" %>

<div class="dstore <%= this.GetType().BaseType.Name %>">
    <h1>Email Templates</h1>
    
    <asp:Repeater ID="rptEmailTemplates" runat="server">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>
        <ItemTemplate>
                <li>
                    <a href="<%# StoreUrls.AdminEditEmailTemplate((Container.DataItem as vStoreEmailTemplate).EmailTemplateId) %>"><%# (Container.DataItem as vStoreEmailTemplate).NameKey %></a>
                    <div>
                        <%# (Container.DataItem as vStoreEmailTemplate).Description %>
                    </div>
                </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</div>
