<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Products.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.Products" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">

    <h1>Products</h1>
        
    <div class="adminToolbar">
        <a href="<%= StoreUrls.AdminAddProduct() %>" class="adminIconBtn add positive">Add Product</a>
        
        <div class="search" style="clear: both; padding: 12px 0;">            
            <asp:TextBox runat="server" id="txtSearch" style="width: 150px;"></asp:TextBox>
            <asp:Button runat="server" id="btnSearch" OnClick="btnSearch_Click" Text="Search" class="adminIconBtn magnifier" style="margin-left: 8px !important;" />
            <a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.Products, "viewall=true") %>" style="display: block; float:left; line-height: 26px; margin-left: 8px;">View All Products</a>
        </div>
    </div>    
    
    <% if(products.Count > 0) { %>
    <table class="grid hilite" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th style="width: 24px;"></th>
                <th>SKU</th>
                <th>Name</th>
                <th style="width: 80px; text-align: right;">Price</th>
                <th style="width: 80px; text-align: right;">Stock Level</th>
                <th style="width: 60px; text-align: right;">Active?</th>
                <th style="width: 24px;"></th>
            </tr>
        </thead>
        <tbody>
        
    <asp:Repeater ID="rptProducts" runat="server">
        <ItemTemplate>
                <tr <%# Container.ItemIndex % 2 == 1 ? "class=\"alt\"" : "" %>>
                    <td style="width: 24px;"><a href="<%# StoreUrls.AdminEditProduct((Container.DataItem as Product).Id.Value) %>" title="edit"><img src="<%= ModuleRootImagePath %>icons/edit.png" alt="edit" title="edit" /></a></td>
                    <td><%# Eval(ProductMetadata.PropertyNames.Sku)%></td>
                    <td><%# Eval(ProductMetadata.PropertyNames.Name)%></td>
                    <td style="width: 80px; text-align: right;"><%# (Container.DataItem as Product).Price.GetValueOrDefault(0).ToString("C2") %></td>
                    <td style="width: 80px; text-align: right;"><%# (Container.DataItem as Product).InventoryQtyInStockForDisplay %></td>
                    <td style="width: 60px; text-align: right;"><%# (Container.DataItem as Product).IsActive.YesNoString() %></td>
                    <td style="width: 24px;"><a href="<%# StoreUrls.AdminDeleteProduct((Container.DataItem as Product).Id.Value) %>" title="delete" onclick="return confirm('are you sure you want to delete?');"><img src="<%= ModuleRootImagePath %>icons/delete.png" alt="delete" title="delete" /></a></td>
                </tr>
        </ItemTemplate>
    </asp:Repeater>
        </tbody>
    </table>
    <% } else { %>
        <p>
            <% if(!IsPostBack) { %>
            Please search for products above
            <% } else { %>
            No products found for your search term.
            <% } %>
        </p>
    <% } %>
</div>

<script type="text/javascript">        
    jQuery(function ($) {
        $('#<%= txtSearch.ClientID %>').keydown(function(e) {
            if (e.which == 13) { // enter key
                $('#<%= btnSearch.ClientID %>').click();
                e.preventDefault();                
            }
        });
    });    
</script>