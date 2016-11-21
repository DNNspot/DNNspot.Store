<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CatalogProduct.ascx.cs" Inherits="DNNspot.Store.Modules.Catalog.CatalogProduct" %>

<div class="dstore catalogProduct product<%= product.Id %>">

    <asp:Literal runat="server" ID="flash"></asp:Literal>

    <asp:Panel ID="pnlProductNotActive" runat="server" Visible="false">
        <p>We're sorry, this product is currently unavailable.</p>
    </asp:Panel>
    <asp:Panel ID="pnlProductNotViewable" runat="server" Visible="false">
        <p>We're sorry, you don't have sufficient privilege to view this product.</p>
    </asp:Panel>
    <asp:Panel ID="pnlProduct" runat="server">
        <h2 class="productTitle"><%= IsEditable ? string.Format(@"<a href=""{0}"" title=""edit product""><img src=""{1}icons/edit.png"" alt=""edit product"" /></a>", StoreUrls.AdminEditProduct(product.Id.Value), ModuleRootImagePath) : string.Empty %><%= product.Name %></h2>

        <div class="leftCol">
            <div class="photoArea">       
                <% if(rptPhotoList.Items.Count == 0) { %>
                    <img src="<%= StoreUrls.ProductPhoto("", 250, null) %>" title="no photo" alt="no photo" />
                <% } else { %>                              
                    <div class="photoSpotlight">
                        <a href="<%= StoreUrls.ProductPhoto(mainProductPhoto, 600, null) %>"><img src="<%= StoreUrls.ProductPhoto(mainProductPhoto, 250, null) %>" alt="<%= mainProductPhoto.DisplayName %>" /></a>	
                    </div>          
                    <div class="photoControls">
                        <% if(rptPhotoList.Items.Count > 1) { %>   
	                    <a href="#" id="photoPrev"><img src="<%= ModuleRootImagePath %>photoPrev.png" alt="Previous Image" /></a>
                        <% } %>
	                    <a href="#" id="photoZoom"><img src="<%= ModuleRootImagePath %>photoZoom.png" alt="Zoom" /></a>
                        <% if(rptPhotoList.Items.Count > 1) { %>   
	                    <a href="#" id="photoNext"><img src="<%= ModuleRootImagePath %>photoNext.png" alt="Next Image" /></a>
                        <% } %>
                    </div>             
                    <% if(rptPhotoList.Items.Count > 1) { %>                   
                    <asp:Repeater ID="rptPhotoList" runat="server">
                        <HeaderTemplate>
                            <div class="photoThumbs">
                                <ul>                
                        </HeaderTemplate>
                        <ItemTemplate>
                                    <li><a href="<%# StoreUrls.ProductPhoto(Container.DataItem as ProductPhoto, 250, null) %>"><img src="<%# StoreUrls.ProductPhoto(Container.DataItem as ProductPhoto, 35, 30) %>" alt="<%# (Container.DataItem as ProductPhoto).DisplayName %>" <%# Container.ItemIndex == 0 ? "class='active-photo'" : "" %> /></a></li>
                        </ItemTemplate>
                        <FooterTemplate>
                                </ul>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>  
                    <% } %>
                    <div class="photoFooter">&nbsp;</div>
                <% } %>
            </div>
        </div>
        
        <div class="rightCol">
            <div class="attributeArea">            
                <ol class="form">
                    <li class="sku">
                        <span><%= !string.IsNullOrEmpty(product.Sku) ? "#" + product.Sku : "" %></span>
                    </li>
                    <% if (product.IsPriceDisplayed.GetValueOrDefault(true) && WA.Parser.ToBool(StoreContext.CurrentStore.GetSetting(StoreSettingNames.ShowPrices)).GetValueOrDefault(true))
                       { %>
                        <li class="price">
                            <label>Price</label>
                            <% if(product.HasActiveDiscounts) { %>
                                <span class="price originalPrice">                            
                                    <%= StoreContext.CurrentStore.FormatCurrency(product.GetPriceWithoutDiscount())%>
                                </span>
                                <span class="price discountedPrice">                            
                                    <%= StoreContext.CurrentStore.FormatCurrency(product.GetPrice())%>
                                </span>                        
                            <% } else { %>
                                <span class="price">                            
                                    <%= StoreContext.CurrentStore.FormatCurrency(product.GetPrice())%>
                                </span>                                                    
                            <% } %>
                        </li> 
                    <% } %>                      
                    <% if(product.IsInStock) { %>
                    <asp:Literal ID="litProductFields" runat="server"></asp:Literal>
                        <% if (product.IsPurchaseableByUser)
                           { %>                    
                            <li class="qty">
                                <asp:Label ID="lblQty" runat="server" AssociatedControlID="txtQuantity" Text="Quantity"></asp:Label>
                                <asp:TextBox ID="txtQuantity" runat="server" Text="1"></asp:TextBox>
                            </li>
                            <li class="validationErrors" style="display: none;">
                                Please correct the following errors:
                                <ul></ul>
                            </li>
                            <li class="addToCart">
                                <label>&nbsp;</label>
                                <span><asp:Button ID="btnAddToCart" runat="server" CssClass="btnAddToCart dnnPrimaryAction" Text="Add to Cart" onclick="btnAddToCart_Click" /></span>
                            </li>
                        <% } %> 
                    <% } else { %>
                    <li class="outOfStock">
                        <label>&nbsp;</label>
                        <span>Sold Out!</span>
                    </li>
                    <% } %>
                </ol>            
            </div>
            
            <div class="specialNotes">
                <%= product.SpecialNotes %>
            </div>
        </div>
        
        
        <asp:Panel ID="pnlTabWidget" runat="server" Visible="false">
            <div id="productDescriptors">    
                <!-- the tabs --> 
                <ul>            
                    <asp:Repeater ID="rptDescriptorTabNames" runat="server">
                        <ItemTemplate>
                            <li><a href="#prodDescriptorTab-<%# Container.ItemIndex %>"><%# (Container.DataItem as ProductDescriptor).Name %></a></li>
                        </ItemTemplate>               
                    </asp:Repeater>            
                </ul>                      
                <!-- tab "panes" -->                 
                <asp:Repeater ID="rptDescriptorTabContents" runat="server">
                    <ItemTemplate>
                        <div id="prodDescriptorTab-<%# Container.ItemIndex %>"><%# (Container.DataItem as ProductDescriptor).TextHtmlDecoded %></div> 
                    </ItemTemplate>                
                </asp:Repeater>                                             
            </div>
        </asp:Panel>
        
    <asp:Literal ID="litBreadcrumb" runat="server"></asp:Literal>
    



    <asp:Panel ID="pnlRelatedProducts" runat="server">        
                <asp:Repeater ID="rptRelatedProducts" runat="server">
                    <HeaderTemplate>                                               
                      <div class="productResults catalogCategory">
                        <h3>Related Products</h3>
                        <ul class="productList">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="product<%# (Container.DataItem as Product).Id %>">
                            <span class="productImage categoryImage<%# (Container.DataItem as Product).Id %>">
                                <a href="<%# StoreUrls.Product(Container.DataItem as Product) %>"><img src="<%# StoreUrls.ProductPhoto((Container.DataItem as Product).GetMainPhoto(), 120, null) %>" alt="<%# (Container.DataItem as Product).Name %>" /></a>
                            </span>
                            <span class="productLink productLink<%# (Container.DataItem as Product).Id %>">
                                <a href="<%# StoreUrls.Product(Container.DataItem as Product) %>"><%# (Container.DataItem as Product).Name %></a>
                            </span>
                            
                            <span class="productPrice productPrice<%# (Container.DataItem as Product).Id %>">
                                <%# (Container.DataItem as Product).GetPriceForDisplay() %>
                            </span>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </div>
                    </FooterTemplate>
                </asp:Repeater>
        </asp:Panel>
    


        
        <div class="categoryLinks">
            Categories: <%= GetProductCategoryLinks().ToDelimitedString("&nbsp;|&nbsp;") %>
        </div>    
        
        <script type="text/javascript">

            jQuery(function ($) {
                // START RELATED PRODUCTS
                if (jQuery.browser.safari || jQuery.browser.chrome) {
                    setTimeout(function () { jQuery('.catalogCategory ul.productList').equalHeights(true); }, 100);
                }
                else {
                    jQuery('.catalogCategory ul.productList').equalHeights(true);
                }

                // END RELATED PRODUCTS

                initPhotos();

                // jQuery UI - Tabs
                jQuery('#productDescriptors').tabs();

                // select qty. text on focus
                jQuery('#<%= txtQuantity.ClientID %>').focus(function () {
                    jQuery(this).select();
                });

                var $validator = $('form').validate({
                    errorContainer: ".validationErrors",
                    errorLabelContainer: ".validationErrors > ul",
                    wrapper: "li",
                    onfocusout: false,
                    onkeyup: false,
                    onclick: false,
                    highlight: function (element, errorClass) {
                        var $elm = jQuery(element);
                        $elm.addClass(errorClass);
                        if ($elm.is(':radio')) {
                            $elm.parents('.RadioButtonList').addClass(errorClass);
                        }
                        else if ($elm.is(':checkbox')) {
                            $elm.parents('.CheckboxList').addClass(errorClass);
                            $elm.parents('.Checkbox').addClass(errorClass);
                        }
                    },
                    unhighlight: function (element, errorClass) {
                        var $elm = jQuery(element);
                        $elm.removeClass(errorClass);
                        if ($elm.is(':radio')) {
                            $elm.parents('.RadioButtonList').removeClass(errorClass);
                        }
                        else if ($elm.is(':checkbox')) {
                            $elm.parents('.CheckboxList').removeClass(errorClass);
                            $elm.parents('.Checkbox').removeClass(errorClass);
                        }
                    }
                });
            });

            function initPhotos() {

                //CLICK A THUMBNAIL
                jQuery('.photoThumbs ul li a').click(function (event) {

                    event.preventDefault();
                    event.stopPropagation();

                    var imageSpotlight = jQuery(this).attr('href');
                    jQuery('.photoSpotlight img').attr('src', imageSpotlight);

                    jQuery('.photoThumbs ul li a img').removeClass('active-photo');
                    jQuery('img', this).addClass('active-photo');

                    return false;
                });

                //CLICK ON ZOOM
                jQuery('#photoZoom').click(function (event) {
                    event.preventDefault();
                    event.stopPropagation();

                    var photoIndex = jQuery('.photoArea li img.active-photo').parent().parent().index();
                    var photoUrls = [];
                    <% foreach(var photo in photoList) { %>
                        //photoUrls.push({ href: '<%= StoreUrls.ProductPhoto(photo, 600, null) %>', title: '<%= photo.DisplayName.Replace("'", "\\'") %>' });
                        photoUrls.push('<%= StoreUrls.ProductPhoto(photo, 600, null) %>');
                    <% } %>

                    jQuery.fancybox(photoUrls, {
                        //'content': photoUrls,
                        'index': photoIndex,
                        'padding': 0,
                        'transitionIn': 'none',
                        'transitionOut': 'none',
                        'type': 'image',
                        'changeFade': 0,
                        'cyclic': true
                    });

                    return false;
                });

                // CLICK ON 'Spotlighted' Photo
                jQuery('.photoSpotlight > a').click(function(event){
                    event.preventDefault();
                    event.stopPropagation();

                    jQuery('#photoZoom').click();

                    return false;
                });

                //CLICK PREVIOUS OR NEXT
                jQuery('#photoNext').click(function (event) {
                    event.preventDefault();
                    event.stopPropagation();
                    dnnspotStorePhotoChange(1);
                    return false;
                });
                jQuery('#photoPrev').click(function (event) {
                    event.preventDefault();
                    event.stopPropagation();
                    dnnspotStorePhotoChange(-1);
                    return false;
                });

            }

            //MOVE TO NEXT PHOTO 1 NEXT   -1  PREV
            function dnnspotStorePhotoChange(position) {
                var numberofthumbs = jQuery('.photoThumbs ul li a img').size();

                var pos = 0;

                var activephoto = 0;

                //loop through each image and find the active photo image. Set active postion to be +1 or -1 
                jQuery('.photoThumbs ul li a img').each(function () {
                    if (jQuery(this).attr('class') == 'active-photo') {
                        activephoto = pos + position;
                    }
                    pos++;
                });

                if (activephoto == numberofthumbs)   //if at the end when clicking next go to first item
                    activephoto = 0;
                if (activephoto == -1)               //if at the beginning when clicking prev
                    activephoto = numberofthumbs - 1;

                jQuery('.photoThumbs ul li a:eq(' + activephoto + ')').click();  //Click the next item to go to		
            }
            
        </script>        
    </asp:Panel>    
    
</div>