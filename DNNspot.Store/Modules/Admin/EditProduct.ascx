<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="EditProduct.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.EditProduct" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">
    <h1><%= isEditMode ? "Edit" : "New" %> Product</h1>
    
    <asp:Literal ID="flash" runat="server"></asp:Literal>
        
    <div class="submit adminToolbar">
        <asp:Button ID="btnSave" runat="server" CssClass="adminIconBtn ok" Text="Save" OnClick="btnSave_Click" />
        <% if (product.Id.HasValue) { %>
        <a href="<%= StoreUrls.AdminDeleteProduct(product.Id.Value) %>" onclick="return confirm('Are you sure you want to delete this product?');" class="adminIconBtn delete">Delete</a>    
        <% } %>        
        <a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.Products) %>" class="adminIconBtn cancel">Cancel</a>

        <a href="<%= StoreUrls.Product(product) %>" target="_blank" class="adminIconBtn magnifier" style="margin-left: 36px !important;">View Product</a>
        
        <% if (product.Id.HasValue) { %>
        <div style="float: right;">
            <button class="adminIconBtn add" onclick="location.href = '<%= StoreUrls.AdminAddProduct() %>'; return false;">Add a New Product</button>
        </div>
        <% } %>
    </div>      
    
    <fieldset class="adminForm">
       
        <div id="productTabs">
            <!-- the tabs -->      
            <div id="productMenu">
                <ul>
                    <li class="active"><a href="#tabProduct">Product</a></li>
                    <li><a href="#tabCategories">Categories</a></li>
                    <li><a href="#tabPhotos">Photos</a></li>
                    <li><a href="#tabDescriptions">Descriptions</a></li>
                    <li><a href="#tabInventory">Inventory</a></li>
                    <li><a href="#tabCustomFields">Variants / Attributes</a></li>
                    <li><a href="#tabCheckoutActions">Checkout Actions</a></li>
                    <li><a href="#tabPermissions">Permissions</a></li>
                    <li><a href="#tabRelatedProducts">Related Products</a></li>
                </ul>
            </div>
            
            <div class="validationErrors">Please correct the following errors:<ul></ul></div>
            
            <!-- the panes -->                
                <div id="tabProduct" class="productTab">
                    <ol class="form labelsLeft">
                        <li>
                            <asp:Label ID="Label13" AssociatedControlID="chkIsActive" runat="server">Active/Published:</asp:Label>    
                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />                
                        </li>        
                        <li>
                            <asp:Label ID="Label1" AssociatedControlID="txtName" runat="server">Name: *</asp:Label> 
                            <span>
                                <asp:TextBox ID="txtName" runat="server" style="width: 450px;" CssClass="required" MaxLength="250" title="Name is required"></asp:TextBox>
                                
                                <a id="slugLink" class="inputHelp" style="display: none;" href="#" onclick="generateSlug($name.val()); return false;">Generate new URL name</a>
                                <img id="imgSlugLoading" src="<%= ModuleRootImagePath %>ajax-loader.gif" style="display: none;" />
                            </span>
                        </li>
                        <li>
                            <asp:Label ID="lblUrlSlug" AssociatedControlID="txtSlug" runat="server">URL Name: *</asp:Label> 
                            <span>
                                <asp:TextBox ID="txtSlug" runat="server" style="width: 450px;" CssClass="required" MaxLength="50" title="Please enter a valid URL name"></asp:TextBox>
                                <span class="inputHelp">&nbsp;Only lowercase letters, numbers, hyphens, underscores. No spaces.</span>
                            </span>
                        </li>            
                        <li>
                            <asp:Label ID="Label2" AssociatedControlID="txtPrice" runat="server">Price: *</asp:Label>    
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="required number" min="0" title="Price is required"></asp:TextBox>
                        </li>                            
                        <li>
                            <asp:Label ID="Label14" AssociatedControlID="txtSku" runat="server">SKU:</asp:Label>    
                            <span>
                                <asp:TextBox ID="txtSku" runat="server" style="width: 250px;" MaxLength="50" CssClass="sku"></asp:TextBox>
                                <span class="inputHelp">No spaces allowed.</span>
                            </span>
                        </li>                         
                        <li>
                            <label>Search Engine Optimization:</label>
                            <span>
                                <ol class="form">
                                    <li style="padding-top: 0;">
                                        <asp:Label ID="Label10" AssociatedControlID="txtSeoTitle" runat="server">Page Title:</asp:Label>
                                        <asp:TextBox ID="txtSeoTitle" runat="server" MaxLength="300" style="width: 400px;"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label11" AssociatedControlID="txtSeoDescription" runat="server">Meta Description:</asp:Label>
                                        <asp:TextBox ID="txtSeoDescription" runat="server" TextMode="MultiLine" style="width: 400px; height: 50px;"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label12" AssociatedControlID="txtSeoKeywords" runat="server">Meta Keywords:</asp:Label>
                                        <asp:TextBox ID="txtSeoKeywords" runat="server" TextMode="MultiLine" style="width: 400px; height: 50px;"></asp:TextBox>
                                    </li>                                                
                                </ol>
                            </span>
                        </li>                                                          
                        <li>
                            <asp:Label ID="lblIsTaxable" AssociatedControlID="chkIsTaxable" runat="server">Taxable Item:</asp:Label>    
                            <span>
                                <asp:CheckBox ID="chkIsTaxable" runat="server" Checked="true" />
                                <span class="inputHelp">Should sales tax be charged for this item?</span>
                            </span>
                        </li>                                                                     
                        <li>
                            <asp:Label ID="lblIsPriceDisplayed" AssociatedControlID="chkIsPriceDisplayed" runat="server">Show Price:</asp:Label>    
                            <span>
                                <asp:CheckBox ID="chkIsPriceDisplayed" runat="server" Checked="true" />
                                <span class="inputHelp">Should the price be displayed for this item?</span>
                            </span>
                        </li>  
                        <li>
                            <asp:Label ID="lblIsAvailableForPurchase" AssociatedControlID="chkIsAvailableForPurchase" runat="server">Available for Purchase:</asp:Label>    
                            <span>
                                <asp:CheckBox ID="chkIsAvailableForPurchase" runat="server" Checked="true" />
                                <span class="inputHelp">Should this item be available for purchase? This overrides any settings set in the Permissions tab.</span>
                            </span>
                        </li>                          
                        <li>
                            <asp:Label ID="Label3" AssociatedControlID="rdoDeliveryMethod" runat="server">Delivery Method:</asp:Label>    
                            <span>
                                <asp:RadioButtonList ID="rdoDeliveryMethod" runat="server" CssClass="rdoDeliveryMethod" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                </asp:RadioButtonList>
                                <fieldset id="shipmentFieldset" style="display: none;">
                                    <legend>Shipping Info</legend>
                                    <ol class="form labelsLeft">
                                        <li>
                                            <asp:Label ID="Label15" AssociatedControlID="txtWeight" runat="server">Weight:</asp:Label>    
                                            <span>
                                                <asp:TextBox ID="txtWeight" runat="server" CssClass="number" min="0"></asp:TextBox>
                                                <span class="inputHelp">lbs.</span>                                    
                                            </span>                                
                                        </li>                
                                        <li>
                                            <asp:Label ID="Label4" AssociatedControlID="txtAdditionalShippingFeePerItem" runat="server">Add'l Per Item Fee:</asp:Label>    
                                            <span>
                                                <asp:TextBox ID="txtAdditionalShippingFeePerItem" runat="server" CssClass="number" min="0"></asp:TextBox>
                                                <br />
                                                <span class="inputHelp">additional amount to charge for shipping per item, will be added to the shipping total for the order</span>
                                            </span>
                                        </li>                                                                  
                                        <li>
                                            <label>Dimensions:</label>
                                            <span>
                                                <asp:TextBox ID="txtLength" runat="server" CssClass="number" min="0" style="width: 50px;"></asp:TextBox> L
                                                &nbsp;
                                                <asp:TextBox ID="txtWidth" runat="server" CssClass="number" min="0" style="width: 50px;"></asp:TextBox> W
                                                &nbsp;
                                                <asp:TextBox ID="txtHeight" runat="server" CssClass="number" min="0" style="width: 50px;"></asp:TextBox> H
                                            </span>                                
                                        </li>   
                                    </ol>
                                </fieldset>                
                                <fieldset id="digitalFileFieldset" style="display: none;">
                                    <legend>Digital File</legend>
                                    <ol class="form labelsLeft">
                                        <li>
                                            <label>Current File:</label>
                                            <span id="digitalFileSpan">
                                                <% if (product.HasDigitalFile) { %>
                                                <a href="<%= StoreUrls.ProductFile(product.DigitalFilename) %>" target="_blank"><%= product.DigitalFileDisplayName %></a> (<%= System.IO.Path.GetExtension(product.DigitalFilename) %>)
                                                <a href="<%= StoreUrls.AdminDeleteProductFile(product.Id.GetValueOrDefault(-1)) %>" title="delete file" onclick="return confirm('are you sure you want to delete this file?')"><img src="<%= ModuleRootImagePath %>icons/delete.png" alt="delete file" /></a>
                                                <% } %>
                                            </span>                        
                                        </li>
                                        <li>
                                            <label>New File:</label>
                                            <asp:FileUpload ID="fupDigitalFile" runat="server" />                       
                                        </li>
                                    </ol>
                                </fieldset>
                            </span>
                        </li>   
                        <li>
                            <asp:Label ID="lblSpecialNotes" AssociatedControlID="txtSpecialNotes" runat="server">Special Notes:</asp:Label>    
                            <span>
                                <asp:TextBox ID="txtSpecialNotes" runat="server" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                            </span>
                        </li>                                             
                        <li id="liQuantityWidget" runat="server" visible="false">
                            <asp:Label ID="Label5" AssociatedControlID="rdoQuantityWidget" runat="server">Quantity Widget:</asp:Label>
                            <span>
                                <asp:RadioButtonList ID="rdoQuantityWidget" runat="server" CssClass="rdoQuantityWidget" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="textbox" Selected="True"> Textbox</asp:ListItem>
                                    <asp:ListItem Value="dropdown"> Dropdown List</asp:ListItem>
                                </asp:RadioButtonList>                  
                                <fieldset id="qtyOptionsFieldset" style="display: none;">
                                    <legend>Dropdown Choices</legend>
                                    <span class="inputHelp">separate values with commas</span>
                                    <br />
                                    <asp:TextBox ID="txtQuantityOptions" runat="server" Width="350px"></asp:TextBox> 
                                    <br />
                                    <span class="inputHelp">e.g. "1-10" or "1,5,10,15,20,25,50,100"</span>                
                                </fieldset>         
                            </span>
                        </li>                                                    
                    </ol>
                </div>
                <div id="tabCategories" class="productTab" style="display: none;">
                    <fieldset class="productCategories">                    
                        <asp:Literal ID="litCategories" runat="server"></asp:Literal>
                    </fieldset>                
                </div>
                <div id="tabPhotos" class="productTab" style="display: none;">
                    <p class="flash">
                        Photos that are uploaded, sorted, or deleted are saved <span style="font-style: italic; font-weight: bold;">immediately</span>.
                    </p>
                    <div class="photos">
                        <!-- Filled via ajax -->
                        <ol>
                        </ol>
                    </div>                
                    <div class="uploadArea">
                        <div id="uploadifyPhotos"></div>
                    </div>                    
                </div>
                <div id="tabDescriptions" class="productTab" style="display: none;">                
                    <div id="descriptors">
                        <!-- the tabs --> 
                        <ul>            
                            <li><a href="#descriptor-1"><asp:Literal ID="litDescriptorName1" runat="server" Text="Description"></asp:Literal></a></li>
                            <li><a href="#descriptor-2"><asp:Literal ID="litDescriptorName2" runat="server" Text="Tab 2"></asp:Literal></a></li>
                            <li><a href="#descriptor-3"><asp:Literal ID="litDescriptorName3" runat="server" Text="Tab 3"></asp:Literal></a></li>
                            <li><a href="#descriptor-4"><asp:Literal ID="litDescriptorName4" runat="server" Text="Tab 4"></asp:Literal></a></li>
                            <li><a href="#descriptor-5"><asp:Literal ID="litDescriptorName5" runat="server" Text="Tab 5"></asp:Literal></a></li>
                        </ul>                      
                        <!-- tab "panes" -->                                     
                        <div id="descriptor-1">
                            <label>Tab Name:</label> <asp:TextBox ID="txtDescriptorName1" runat="server" CssClass="tabName" Text="Description"></asp:TextBox>
                            <dnn:TextEditor id="txtDescriptorText1" runat="server" Width="550" Height="250"></dnn:TextEditor>
                        </div>

                        <div id="descriptor-2">
                            <label>Tab Name:</label> <asp:TextBox ID="txtDescriptorName2" runat="server" CssClass="tabName" Text="Tab 2"></asp:TextBox>
                            <dnn:TextEditor id="txtDescriptorText2" runat="server" Width="550" Height="250"></dnn:TextEditor>
                        </div>

                        <div id="descriptor-3">
                            <label>Tab Name:</label> <asp:TextBox ID="txtDescriptorName3" runat="server" CssClass="tabName" Text="Tab 3"></asp:TextBox>
                            <dnn:TextEditor id="txtDescriptorText3" runat="server" Width="550" Height="250"></dnn:TextEditor>
                        </div>

                        <div id="descriptor-4">
                            <label>Tab Name:</label> <asp:TextBox ID="txtDescriptorName4" runat="server" CssClass="tabName" Text="Tab 4"></asp:TextBox>
                            <dnn:TextEditor id="txtDescriptorText4" runat="server" Width="550" Height="250"></dnn:TextEditor>
                        </div>

                        <div id="descriptor-5">
                            <label>Tab Name:</label> <asp:TextBox ID="txtDescriptorName5" runat="server" CssClass="tabName" Text="Tab 5"></asp:TextBox>
                            <dnn:TextEditor id="txtDescriptorText5" runat="server" Width="550" Height="250"></dnn:TextEditor>
                        </div>                                                                                                                                                                  
                    </div>                   
                </div>
                <div id="tabInventory" class="productTab" style="display: none;">
                    <ol class="form labelsLeft" style="margin-bottom: 1em;">
                        <li>
                            <asp:Label ID="Label6" AssociatedControlID="chkInventoryIsEnabled" runat="server">Enable Inventory Management:</asp:Label>
                            <span>
                                <asp:CheckBox ID="chkInventoryIsEnabled" runat="server" Checked="false" />                                              
                            </span>
                        </li>                        
                    </ol>                    
                    <ol id="inventoryControls" class="form labelsLeft" style="display: none;">
                        <li>
                            <asp:Label ID="Label8" AssociatedControlID="txtInventoryQtyInStock" runat="server">Stock Level:</asp:Label>
                            <span>
                                <asp:TextBox ID="txtInventoryQtyInStock" runat="server" CssClass="number"></asp:TextBox> 
                                <br /><span class="inputHelp">How many do you currently have in stock? (e.g. 500)</span>
                            </span>
                        </li>
                        <li>
                            <asp:Label ID="Label9" AssociatedControlID="txtInventoryQtyLowThreshold" runat="server">Qty. Low Threshold:</asp:Label>
                            <asp:TextBox ID="txtInventoryQtyLowThreshold" runat="server" CssClass="number" min="0"></asp:TextBox> 
                        </li>                                                
                        <li>
                            <asp:Label ID="Label7" AssociatedControlID="chkInventoryAllowNegativeStockLevel" runat="server">
                            Allow Negative Stock Level:
                            <br /><span class="inputHelp">(allow customers to purchase this product even if it is out of stock)</span>
                            </asp:Label>
                            <span>
                                <asp:CheckBox ID="chkInventoryAllowNegativeStockLevel" runat="server"></asp:CheckBox>                                 
                            </span>
                        </li>                        
                    </ol>                      
                </div>
                <div id="tabCustomFields" class="productTab" style="display: none;">
                    <div style="overflow: auto;">
                        <input type="button" class="adminIconBtn add" value="New Variant / Attribute" onclick="location.href = '<%= StoreUrls.AdminAddProductField(product.Id.GetValueOrDefault(-1)) %>'; return false;" />                                      
                    </div>
                    <div id="customFieldList">
                        <asp:Repeater ID="rptCustomFields" runat="server" OnItemDataBound="rptCustomFields_ItemDataBound">
                            <ItemTemplate>
                                <div class="customFieldContainer" id="productField-<%# (Container.DataItem as ProductField).Id %>">
                                    <h4>
                                        <%# (Container.DataItem as ProductField).Name %>
                                        <span class="menu">
                                            <a href="<%# StoreUrls.AdminEditProductField((Container.DataItem as ProductField).Id.Value) %>"><img src="<%= ModuleRootImagePath %>icons/edit.png" title="edit" alt="edit" /></a>
                                            &nbsp;
                                            <img class="moveHandle" src="<%= ModuleRootImagePath %>icons/move.png" alt="move" title="move" />
                                            &nbsp;&nbsp;&nbsp;
                                            <%# HtmlHelper.ConfirmDeleteImage(StoreUrls.AdminDeleteProductField((Container.DataItem as ProductField).Id.Value))%>
                                        </span>
                                    </h4>
                                    <ol class="form labelsLeft">
                                        <li>
                                            <label>Widget:</label>
                                            <span><%# (Container.DataItem as ProductField).WidgetType %></span>
                                        </li>
                                        <li>
                                            <label>Required:</label>
                                            <span><%# (Container.DataItem as ProductField).IsRequired.YesNoString() %></span>
                                        </li>                                                                        
                                        <li <%# (Container.DataItem as ProductField).PriceAdjustment.GetValueOrDefault(0) == 0 ? "style='display:none;'" : "" %>>
                                            <label>Price Adjustment</label>
                                            <span><%# (Container.DataItem as ProductField).PriceAdjustment.HasValue ? StoreContext.CurrentStore.FormatCurrency((Container.DataItem as ProductField).PriceAdjustment.Value) : "" %></span>
                                        </li>
                                        <li <%# (Container.DataItem as ProductField).WeightAdjustment.GetValueOrDefault(0) == 0 ? "style='display:none;'" : "" %>>
                                            <label>Weight Adjustment</label>
                                            <span><%# (Container.DataItem as ProductField).WeightAdjustment.HasValue ? (Container.DataItem as ProductField).WeightAdjustment.Value.ToString("N2") : "" %></span>
                                        </li>  
                                        <li id="liOptions" runat="server">
                                            <label>Options:</label>
                                            <span>      
                                                <asp:Repeater ID="rptFieldChoices" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="grid gridLight" cellpadding="0" cellspacing="0">
                                                            <thead>
                                                                <tr>
                                                                    <th>Name</th>
                                                                    <th>Value</th>
                                                                    <th>Price Adjust ($)</th>
                                                                    <th>Weight Adjust (lbs.)</th>                                                            
                                                                </tr>
                                                            </thead>
                                                            <tbody>                                                                                                
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%# (Container.DataItem as ProductFieldChoice).Name %>
                                                                    </td>        
                                                                    <td>
                                                                        <%# (Container.DataItem as ProductFieldChoice).Value %>
                                                                    </td>                                                                            
                                                                    <td class="price">
                                                                        <%# (Container.DataItem as ProductFieldChoice).PriceAdjustment.HasValue ? StoreContext.CurrentStore.FormatCurrency((Container.DataItem as ProductFieldChoice).PriceAdjustment.Value) : "" %>
                                                                    </td>                              
                                                                    <td class="weight">
                                                                        <%# (Container.DataItem as ProductFieldChoice).WeightAdjustment.HasValue ? (Container.DataItem as ProductFieldChoice).WeightAdjustment.Value.ToString("N2") : "" %>
                                                                    </td>                                                                                           
                                                                </tr>  
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                            </tbody>
                                                        </table>                                                
                                                    </FooterTemplate>
                                                </asp:Repeater>                                                    
                                            </span>
                                        </li>                                                                      
                                    </ol>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>    
                    </div>                                                                                  
                </div>  
                <div id="tabCheckoutActions" class="productTab" style="display: none;">
                    <ol class="form labelsLeft">
                        <li>
                            <label>Add user to role(s)<br /><span class="inputHelp">(at checkout)</span></label>
                            <span>
                                <span class="inputHelp">Only applies to users that have signed-in before completing checkout.</span>
                                <table>
                                    <thead>
                                        <tr>
                                            <th>Role</th>
                                            <th>Days until role expires for user (optional)</th>
                                        </tr>
                                    </thead>
                                    <tbody>     
                                        <asp:Literal ID="litCheckoutRolesPickerUi" runat="server"></asp:Literal>              
                                    </tbody>
                                </table>                                                              
                            </span>
                        </li>                      
                    </ol>
                </div>
                <div id="tabPermissions" class="productTab" style="display: none;">
                    <ol class="form labelsLeft">
                        <li>
                            <label>View Permissions<br /><span class="inputHelp">(Roles that can view this product)</span></label>
                                <table>
                                    <thead>
                                        <tr>
                                            <th>Role</th>
                                        </tr>
                                    </thead>
                                    <tbody>     
                                        <asp:Literal ID="litViewPermissions" runat="server"></asp:Literal>              
                                    </tbody>
                                </table>                                                              
                            </span>
                        </li>                      
                        <li>
                            <label>Add to Cart Permissions<br /><span class="inputHelp">(Roles that can add this product to their cart)</span></label>
                                <table>
                                    <thead>
                                        <tr>
                                            <th>Role</th>
                                        </tr>
                                    </thead>
                                    <tbody>     
                                        <asp:Literal ID="litCartPermissions" runat="server"></asp:Literal>              
                                    </tbody>
                                </table>                                                              
                            </span>
                        </li>  
                    </ol>
                </div>
                <div id="tabRelatedProducts" class="productTab" style="display: none;">
                    <asp:CheckBoxList ID="cblRelatedProducts" runat="server">
                    </asp:CheckBoxList>
                </div>
        </div>                                  
    </fieldset>                                  
    
    <div id="toolbarBottom" class="adminToolbar"></div>
    
</div>

<!-- HTML templates for jTemplate -->
<script type="text/html" id="photoTemplate">
<![CDATA[
{#foreach $T.photos as photo}
<li id="photo-{$T.photo.Id}">
    <a target="_blank" href="{$T.photo.OriginalUrl}"><img src="{$T.photo.ThumbnailUrl}" /></a>
    <br />
    <span class="move">
        <img src="<%=ModuleRootImagePath %>icons/arrow_out.png" title="drag and drop to change sort order" />
    </span>
    <span class="delete">
        <a href="#" title="delete photo" onclick="deletePhoto({$T.photo.Id}); return false;"><img src="<%=ModuleRootImagePath %>icons/delete.png" /></a>
    </span>
</li>
{#/for}
]]>
</script>

<script type="text/javascript">
var urlToAjaxHandler = "<%= StoreUrls.AdminAjaxHandler %>";
var uploadifyHandlerUrl = "<%= StoreUrls.AdminUploadifyHandler %>&guid=<%= Session["uploadGuid"] %>";
var urlToSlugService = "<%= StoreUrls.SlugService %>";
var $name = null;
var $slug = null;    
var $seoTitle = null;
var $photoList = null;
var $imgSlugLoading = null;
var $productTabs = null;
var $sortableProductFields = null;
    
    // Document Ready
    jQuery(function($) {
    
        jQuery('#toolbarBottom').append(jQuery('div.submit.adminToolbar').html());


        $price = jQuery("#<%= txtPrice.ClientID %>");        
        $weight = jQuery("#<%= txtWeight.ClientID %>");

        var price = $price.val();        
        var weight = $weight.val();



        $name = jQuery('#<%= txtName.ClientID %>');        
        $slug = jQuery('#<%= txtSlug.ClientID %>');        
        $seoTitle = jQuery('#<%= txtSeoTitle.ClientID %>');
        
        // Product Menu / Tabs
        $productTabs = jQuery('#productTabs div.productTab');
        $productMenuLis = jQuery('#productMenu li');
        jQuery('#productMenu li a').click(function(e) {
            e.stopPropagation();
            e.preventDefault();
            var $this = jQuery(this);
            if($this.attr('disabled') != 'disabled') {
                var $tabToShow = jQuery($this.attr('href'));
                
                $productTabs.hide();            
                $tabToShow.show();
                
                $productMenuLis.removeClass('active');
                $this.parent('li').addClass('active');
            }
            return false;
        });        
        if(<%= (!isEditMode).ToString().ToLower() %>) {
            var $disabledLis = $productMenuLis.filter(':gt(1)');
            $disabledLis.addClass('disabled');
            jQuery('a', $disabledLis).attr('disabled','disabled').click(function(e) {
                e.stopPropagation();
                e.preventDefault();            
                
                alert('Please save this product before accessing these section');    
                
                return false;            
            });
        }
        
        // jQuery UI - Tabs 
        jQuery('#descriptors').tabs();

        // Delivery Method
        initRadiosWithLinkedElementToggle(jQuery('.rdoDeliveryMethod', 'fieldset'), jQuery('#shipmentFieldset'), "<%= (short)DeliveryMethod.Types.Shipped %>");
        initRadiosWithLinkedElementToggle(jQuery('.rdoDeliveryMethod', 'fieldset'), jQuery('#digitalFileFieldset'), "<%= (short)DeliveryMethod.Types.Downloaded %>");

        // Qty. Picker
        initRadiosWithLinkedElementToggle(jQuery('.rdoQuantityWidget', 'fieldset'), jQuery('#qtyOptionsFieldset'), "dropdown");

        // Inventory Control
        //initCheckboxWithLinkedElementToggle(jQuery('#<%=chkInventoryIsEnabled.ClientID %>'), jQuery('#inventoryFieldset'));
        var $chkIsInventoryEnabled = jQuery('#<%=chkInventoryIsEnabled.ClientID %>');
        if($chkIsInventoryEnabled.is(':checked')) {
            jQuery('#inventoryControls').show();
        }
        $chkIsInventoryEnabled.click(function() {
            var $this = jQuery(this);
            if($this.is(':checked')) {
                jQuery('#inventoryControls').show();    
            }
            else {
                jQuery('#inventoryControls').hide();
            }
        });

        // jQuery Uploadify
        initUploadify();

        // Photos, Template and Sortable                
        $photoList = jQuery(".photos ol");
        var photoTemplateString = jQuery('#photoTemplate').html().replace(/\s*<!\[CDATA\[|\]\]>\s*/g, '');
        $photoList.setTemplate( photoTemplateString );
        populatePhotoList();           

        // Maxlength plugin
        $name.maxlength();        
        $slug.maxlength();        
        jQuery('#<%= txtSku.ClientID %>').maxlength();
        jQuery('#<%= txtSpecialNotes.ClientID %>').maxlength({ maxCharacters: 500 });
        jQuery('#<%= txtSeoDescription.ClientID %>').maxlength({ maxCharacters: 500 });
        jQuery('#<%= txtSeoKeywords.ClientID %>').maxlength({ maxCharacters: 500 });
        
        // Title / slug prefill logic
        if(<%= (!isEditMode).ToString().ToLower() %>) {            
            $name.focus();            
            $name.blur(function() {                                
                if($slug.val() == '') {
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
        $imgSlugLoading = jQuery('#imgSlugLoading');
        
        // Custom Fields - sortable
        var sortableOptions = {};
        sortableOptions.handle = ".moveHandle";
        sortableOptions.axis = "y";
        sortableOptions.placeholder = "ui-drop-placeholder";
        sortableOptions.tolerance = "pointer";
        sortableOptions.opacity = 0.5;        
        sortableOptions.sort = function(event, ui) {
            var itemHeightPx = ui.item.css('height');
            if (itemHeightPx) {
                ui.placeholder.css('height', itemHeightPx);
            }
        };
        sortableOptions.update = function(event, ui) {
            
            var sortedArray = $sortableProductFields.sortable('toArray');
            var productFieldIdArray = [];
            jQuery.each(sortedArray, function(i, val) {
                productFieldIdArray.push(parseInt(val.replace("productField-", "")));
            });
            if (productFieldIdArray.length > 1) {                
                jQuery.post(urlToAjaxHandler, { 'action': 'updateProductFieldSortOrder', 'sortedProductFieldIds': productFieldIdArray, 'PortalId': <%= PortalId %> }, function(data) {
                    if (data.success) {
                        $sortableProductFields.effect('highlight', { backgroundColor: '#FFFF40' }, 1500);
                    }
                    else {
                        //console.log('error');
                        //if (data.error) console.log(data.error);
                    }
                }
                , "json");
            }
        };        
        $sortableProductFields = jQuery("#customFieldList");
        $sortableProductFields.sortable(sortableOptions);
        //$sortableProductFields.disableSelection();
        
        // Form Validation
        var $validator = $('form').validate({
            errorContainer: ".validationErrors",
            errorLabelContainer: ".validationErrors > ul",
            wrapper: "li",
            onfocusout: false,
            onkeyup: false
        });        

    });  
        
    function initUploadify() {
        jQuery('#uploadifyPhotos').uploadify({
            'uploader': uploadifyHandlerUrl
            , 'swf': '<%= ModuleRootWebPath %>uploadify/uploadify.swf'
            , 'folder': '<%= PhotoUploadFolder %>ProductPhotos'
            , 'buttonImage': '<%= ModuleRootImagePath %>btnUploadPhotos.png'
            , 'width': 141
            , 'height': 31
            , 'wmode': 'transparent'
            , 'cancelImg': '<%= ModuleRootWebPath %>uploadify/cancel.png'
            , 'auto': true
            , 'multi': true
            , 'fileTypeDesc': 'Image Files (.jpg, .png, .gif)'
            , 'fileTypeExts': '*.jpg;*.jpeg;*.png;*.gif'
            , 'formData': { 'productId': '<%= product.Id.GetValueOrDefault(-1) %>', 'type': 'photo' }
            //, 'onComplete': function(event, queueID, fileObj, response, data) {
                //console.log('single file upload completed');
            //}
            , 'onQueueComplete': function(event, data) {
                //console.log('ALL uploads completed');                
                populatePhotoList();                                     
            }
        });    
    }          
    
    function populatePhotoList() {
        jQuery.post(urlToAjaxHandler, { 'action': 'getProductPhotosJson', 'productId': <%= product.Id.GetValueOrDefault(-1) %>, 'PortalId': <%= PortalId %> }, function(data) {                
                $photoList.empty();
                if(data.length > 0) {                
                    // jTemplate                                            
                    var templateData = { 'photos': data };                                    
                    $photoList.processTemplate(templateData);         
                    
                    // re-apply the jQuery 'sortable' plugin
                    $photoList.sortable({
                        handle: 'span.move'
                        , tolerance: 'pointer'
                        , update: function(event, ui) {
                            //console.log('sorting update');
                            var sortedPhotoArray = $photoList.sortable('toArray');
                            //var sortedPhotoList = sortedPhotoArray.join(',');
                            //console.log(sortedPhotoArray);
                            jQuery.post(urlToAjaxHandler, { 'action': 'updateProductPhotoSortOrder', 'productId': <%= product.Id.GetValueOrDefault(-1) %>, 'photoList': sortedPhotoArray, 'PortalId': <%= PortalId %> }, function() {
                                //console.log('callback from ajax update');
                            }, 'json');
                        }
                    });
                    $photoList.disableSelection();                            
                }
                else {
                    jQuery('<li></li>').html('No photos uploaded').appendTo($photoList);                    
                }
            }
        , "json");    
    }
    
    function deletePhoto(photoId) {
        if(confirm("Are you sure you want to delete this photo?")) {
            jQuery.post(urlToAjaxHandler, { 'action': 'deleteProductPhoto', 'photoId': photoId, 'PortalId': <%= PortalId %> }, function(data) {                    
                    if(data.success) {
                        populatePhotoList();
                    }                       
                    else {                        
                        console.error(data.error);
                    }
                }
            , "json");                
        }
    }    
    
    function generateSlug(sourceText)
    {        
        if(sourceText) {                    
            $imgSlugLoading.show(); 
            jQuery.post(urlToSlugService, { 'q': sourceText, 'PortalId': <%= PortalId %> }, function(data) {   
                if(data) {
                    $slug.val(data);
                }
                $imgSlugLoading.hide();
            }, 'text');
        }
    }

    function initRadiosWithLinkedElementToggle($radioContainer, $linkedElement, showOnValue) {
        
        if (jQuery(":radio:checked", $radioContainer).val() == showOnValue) {
            $linkedElement.show();
        }
        jQuery(":radio", $radioContainer).click(function() {
            var $this = jQuery(this);

            if ($this.val() == showOnValue) {
                $linkedElement.show();
            }
            else {
                $linkedElement.hide();
            }
        });
    }

    function initCheckboxWithLinkedElementToggle($checkbox, $linkedElement) {
        if ($checkbox.is(':checked')) {
            $linkedElement.show();
        }
        $checkbox.click(function() {
            $linkedElement.toggle();
        });
    }
</script>
<div id="msg"></div>