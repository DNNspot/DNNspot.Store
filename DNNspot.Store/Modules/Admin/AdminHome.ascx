<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminHome.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.AdminHome" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">
    
    <asp:Literal ID="flash" runat="server"></asp:Literal>
    
    <ul style="margin-right: 7em;">
        <li>
            <a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.StoreSettings) %>"><img src="<%= ModuleRootImagePath %>icons/cog32.png" /> Store Settings</a>
            <ul>
                <li><a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.EmailTemplates) %>"><img src="<%= ModuleRootImagePath %>icons/email32.png" /> Email Templates</a></li>
                <li><a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.Shipping) %>"><img src="<%= ModuleRootImagePath %>icons/lorry32.png" /> Shipping Options</a></li>
                <li><a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.TaxRegions) %>"><img src="<%= ModuleRootImagePath %>icons/money_dollar32.png" /> Sales Tax</a></li>    
                <li><a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.PaymentMethods) %>"><img src="<%= ModuleRootImagePath %>icons/creditcards32.png" /> Payment Processors</a></li>                      
                <li><a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.CatalogSettings) %>"><img src="<%= ModuleRootImagePath %>icons/application_side_list32.png" /> Catalog Settings</a></li> 
            </ul>
        </li>
    </ul>   
    
    <ul>            
        <li><a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.Categories) %>"><img src="<%= ModuleRootImagePath %>icons/text_list_bullets32.png" /> Categories</a></li>    
        <li>
            <a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.Products) %>"><img src="<%= ModuleRootImagePath %>icons/package32.png" /> Products</a>
            <ul>
                <li>
                    <a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.ProductImport) %>"><img src="<%= ModuleRootImagePath %>icons/package_add.png" /> Import</a>
                    &nbsp;&nbsp;
                    <a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.ProductExport) %>"><img src="<%= ModuleRootImagePath %>icons/package_go.png" /> Export</a>
                </li>                
            </ul>
        </li>                
        <li><a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.Coupons) %>"><img src="<%= ModuleRootImagePath %>icons/tag_blue32.png" /> Coupons</a></li>
        <li><a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.Discounts) %>" title="discounts"><img src="<%= ModuleRootImagePath %>icons/tag_green32.png" /> Discounts</a></li>
        <li><a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.Orders) %>"><img src="<%= ModuleRootImagePath %>icons/cart32.png" /> Orders</a></li>
    </ul>    
    
    <ul>
        <li>
            Reports
            <ul>
                <li><a href="<%= StoreUrls.GetModuleFolderUrlRoot() %>Modules/Admin/Reports/CustomerList.ashx?storeId=<%= StoreContext.CurrentStore.Id %>"><img src="<%= ModuleRootImagePath %>icons/report_disk32.png" /> Customer List (.csv)</a></li>
                <li><a href="<%= StoreUrls.GetModuleFolderUrlRoot() %>Modules/Admin/Reports/OrderList.ashx?storeId=<%= StoreContext.CurrentStore.Id %>"><img src="<%= ModuleRootImagePath %>icons/report_disk32.png" /> Order List (.xml)</a></li>                    
            </ul>
        </li>        
    </ul>   
    
</div>