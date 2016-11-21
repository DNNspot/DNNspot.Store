<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CatalogCategoryMenu.ascx.cs" Inherits="DNNspot.Store.Modules.Catalog.CatalogCategoryMenu" %>

<div class="dstore catalogCategoryMenu">    
    <asp:Literal ID="litCategoriesTree" runat="server"></asp:Literal>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#catId-<%= StoreContext.Category.Id %>").addClass("activeNode");
            jQuery("<%= StoreContext.CategoryBreadcrumb.Select(c => "#catId-" + c.Id.ToString()).ToList().ToDelimitedString(", ") %>").addClass("activePath");
        });
    </script>
</div>
