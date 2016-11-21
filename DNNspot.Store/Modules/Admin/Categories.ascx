<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Categories.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.Categories" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">
    
    <h1>Categories</h1>
    
    <asp:Literal ID="flash" runat="server"></asp:Literal>
    
    <div class="adminToolbar">
        <a href="<%= StoreUrls.AdminAddCategory() %>" class="adminIconBtn add positive">Add Category</a>
    </div>    

    <div id="categories">        
        <asp:Literal ID="litCategories" runat="server"></asp:Literal>              
    </div>
    
</div>


<script type="text/javascript">

    jQuery(function($) {

        if (<%= (!dragDropSupported).ToString().ToLower() %>) {
            jQuery('div.root:first-child').find('span:first a.moveUp').hide();
            jQuery('div.level1:first-child').find('span:first a.moveUp').hide();
            jQuery('div.level2:first-child').find('span:first a.moveUp').hide();
            jQuery('div.level3:first-child').find('span:first a.moveUp').hide();
            jQuery('div.level4:first-child').find('span:first a.moveUp').hide();

            jQuery('div.root:last-child').find('span:first a.moveDown').hide();
            jQuery('div.level1:last-child').find('span:first a.moveDown').hide();
            jQuery('div.level2:last-child').find('span:first a.moveDown').hide();
            jQuery('div.level3:last-child').find('span:first a.moveDown').hide();
            jQuery('div.level4:last-child').find('span:first a.moveDown').hide();
        }

        jQuery('#categories div').removeClass('hidden');
    });
</script>

