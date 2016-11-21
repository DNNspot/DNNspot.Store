<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCategory.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.EditCategory" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">    

    <asp:Literal ID="flash" runat="server"></asp:Literal>
    
    <h1><%= isEditMode ? "Edit" : "New" %> Category</h1>
    
    <div id="toolbarTop" class="adminToolbar"></div>
    
    <fieldset>
        <legend><%= isEditMode ? "Edit" : "New" %> Category</legend>
        
        <div class="validationErrors">Please correct the following errors:<ul></ul></div>
        
        <ol class="form labelsLeft">                             
            <li>
                <asp:Label ID="Label1" AssociatedControlID="txtName" runat="server">Name: *</asp:Label> 
                <span>
                    <asp:TextBox ID="txtName" runat="server" style="width: 450px;" CssClass="required" MaxLength="150" title="Please enter a category name"></asp:TextBox>
                    <a id="slugLink" class="inputHelp" style="display: none;" href="#" onclick="generateSlug($name.val()); return false;">Generate New Slug</a>
                </span>
            </li>          
            <li>
                <asp:Label ID="Label5" AssociatedControlID="txtTitle" runat="server">Title: *</asp:Label> 
                <span>
                    <asp:TextBox ID="txtTitle" runat="server" style="width: 450px;" CssClass="required" MaxLength="150" title="Please enter a category title"></asp:TextBox>
                </span>
            </li>                      
            <li>
                <asp:Label ID="Label9" AssociatedControlID="txtSlug" runat="server">URL Name: *                
                </asp:Label> 
                <span>
                    <asp:TextBox ID="txtSlug" runat="server" style="width: 300px;" CssClass="required slug" MaxLength="50" title="Please enter a valid URL name"></asp:TextBox>
                    <span class="inputHelp">&nbsp;Only lowercase letters, numbers, hyphens, underscores. No spaces.</span>
                </span>
            </li>               
            <% if(!isSystemCategory) { %>            
            <li>
                <asp:Label ID="Label4" AssociatedControlID="txtName" runat="server">Subcategory of:</asp:Label> 
                <asp:DropDownList ID="ddlParentId" runat="server">
                </asp:DropDownList>
            </li>            
            <% } %>
            <li>
                <asp:Label ID="lblIsFeaturedCategory" AssociatedControlID="chkIsFeaturedCategory" runat="server">Featured Category:</asp:Label>        
                <span>
                    <asp:CheckBox ID="chkIsFeaturedCategory" runat="server" />
                    <span class="inputHelp">Featured categories are not deletable and are listed at the top of the category list.</span>
                </span>         
            </li>      
            <li>
                <asp:Label ID="Label3" AssociatedControlID="chkIsHidden" runat="server">Hidden Category:</asp:Label>        
                <span>
                    <asp:CheckBox ID="chkIsHidden" runat="server" />
                    <span class="inputHelp">Hidden categories are not displayed to the customer.</span>
                </span>         
            </li>              
            <li>
                <asp:Label ID="Label2" AssociatedControlID="txtDescription" runat="server">Description:</asp:Label> 
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
            </li>                      
            <li>
                <label>Search Engine Optimization:</label>
                <span>
                    <ol class="form">
                        <li>
                            <asp:Label ID="Label13" AssociatedControlID="txtSeoTitle" runat="server">Page Title:</asp:Label>
                            <asp:TextBox ID="txtSeoTitle" runat="server" MaxLength="300" style="width: 400px;"></asp:TextBox>
                        </li>
                        <li>
                            <asp:Label ID="Label14" AssociatedControlID="txtSeoDescription" runat="server">Meta Description:</asp:Label>
                            <asp:TextBox ID="txtSeoDescription" runat="server" TextMode="MultiLine" style="width: 400px; height: 50px;"></asp:TextBox>
                        </li>
                        <li>
                            <asp:Label ID="Label15" AssociatedControlID="txtSeoKeywords" runat="server">Meta Keywords:</asp:Label>
                            <asp:TextBox ID="txtSeoKeywords" runat="server" TextMode="MultiLine" style="width: 400px; height: 50px;"></asp:TextBox>
                        </li>                                                
                    </ol>                
                </span>
            </li>                  
            <li class="submit adminToolbar">
                <asp:Button ID="btnSave" runat="server" CssClass="adminIconBtn ok" Text="Save" OnClick="btnSave_Click" />
                <% if(!category.Id.HasValue) { %>
                    <asp:Button ID="Button1" runat="server" CssClass="adminIconBtn ok" Text="Save and Create Another" OnClick="btnSaveAndNew_Click" />
                <% } %>
                <% if (category.Id.HasValue && !category.IsSystemCategory.GetValueOrDefault()) { %>
                    <a href="<%= StoreUrls.AdminDeleteCategory(category.Id.GetValueOrDefault(-1)) %>" onclick="return confirm('Are you sure you want to delete this category?');" class="adminIconBtn delete">Delete</a>    
                <% } %>                
                <a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.Categories) %>" class="adminIconBtn cancel">Cancel</a>    
            </li>            
        </ol>
    </fieldset>        
    
</div>    


<script type="text/javascript">
var urlToSlugService = "<%= StoreUrls.SlugService %>";
var $name = null;
var $title = null;
var $slug = null;
var $seoTitle = null;
            
    jQuery(function($) {
        $name = jQuery('#<%= txtName.ClientID %>');
        $title = jQuery('#<%= txtTitle.ClientID %>');
        $slug = jQuery('#<%= txtSlug.ClientID %>');
        $seoTitle = jQuery('#<%= txtSeoTitle.ClientID %>');
        
        jQuery('#toolbarTop').append(jQuery('li.adminToolbar').html());            

        // MaxLength plugin
        $name.maxlength();
        $title.maxlength();
        $slug.maxlength();
        jQuery('#<%= txtSeoDescription.ClientID %>').maxlength({ maxCharacters: 500 });
        jQuery('#<%= txtSeoKeywords.ClientID %>').maxlength({ maxCharacters: 500 });

        // Title / slug prefill logic
        if(<%= (!isEditMode).ToString().ToLower() %>) {            
            $name.focus();
            $name.blur(function() {                                
                if($title.val() == '')
                {
                    $title.val($name.val());                
                }
                if($slug.val() == '')
                {
                    generateSlug($name.val());
                }
                if($seoTitle.val() == '') {
                    $seoTitle.val($name.val());
                }                
            });
        }
        else {
            $name.change(function() {
                jQuery('#slugLink').show();
            });
        }        
        
        // Form Validation
        var $validator = $('form').validate({
            errorContainer: ".validationErrors",
            errorLabelContainer: ".validationErrors > ul",
            wrapper: "li",
            onfocusout: false,
            onkeyup: false
        });   
                  
    });
    
    function generateSlug(sourceText) {
        if(sourceText) {
            jQuery.post(urlToSlugService, { 'q': sourceText }, function(data) {
                if(data) {
                    $slug.val(data);
                }
            }, 'text');    
        }
    }    
    
</script>

