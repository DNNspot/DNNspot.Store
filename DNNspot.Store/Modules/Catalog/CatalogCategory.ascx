<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CatalogCategory.ascx.cs" Inherits="DNNspot.Store.Modules.Catalog.CatalogCategory" %>

<div class="dstore catalogCategory catalog<%= category.Id %>">       
    <h2><%= category.Title %></h2>
    
    <asp:Literal ID="litBreadcrumb" runat="server"></asp:Literal>
    
    <%= category.Description %>

    <div class="productResults">
        <asp:Panel ID="pnlCategoryProductResults" runat="server">
            <div class="resultsHeader">
                <span class="resultCount"><%= productList.TotalItemCount %> products in this category</span>
                <span class="sortBy">
                    Sort by: 
                    <%= GetSortByFieldLinks().ToDelimitedString("&nbsp;|&nbsp;") %> 
                </span>        
            </div>
            <asp:Repeater ID="rptCategoryProducts" runat="server">
                <HeaderTemplate>                                               
                    <ul class="productList">
                </HeaderTemplate>
                <ItemTemplate>
                        <li class="product<%# (Container.DataItem as Product).Id %> <%# (Container.DataItem as Product).HasVariants ? "hasVariants" : "hasNoVariants" %>">
                            <span class="productImage categoryImage<%# (Container.DataItem as Product).Id %>">
                                <a href="<%# StoreUrls.Product(Container.DataItem as Product) %>"><img src="<%# StoreUrls.ProductPhoto((Container.DataItem as Product).GetMainPhoto(), 120, null) %>" alt="<%# (Container.DataItem as Product).Name %>" /></a>
                            </span>
                            <span class="productLink productLink<%# (Container.DataItem as Product).Id %>">
                                <a href="<%# StoreUrls.Product(Container.DataItem as Product) %>"><%# (Container.DataItem as Product).Name %></a>
                            </span>
                            
                            <span class="productPrice productPrice<%# (Container.DataItem as Product).Id %>">
                                <%# (Container.DataItem as Product).GetPriceForDisplay() %>
                            </span>
                            <% if (WA.Parser.ToBool(StoreContext.CurrentStore.GetSetting(StoreSettingNames.ShowPriceAndQuantityInCatalog)).GetValueOrDefault(false))
                               {%>
                               Quantity: <input data-url="<%# StoreUrls.AddProductToCart(Container.DataItem as Product) %>" type="text" size="1" id="productQuantity<%# (Container.DataItem as Product).Id %>" class="productCategoryQuantity" />
                               <a href="<%# StoreUrls.AddProductToCart(Container.DataItem as Product) %>" class="btnCatalogAddToCart">Add to Cart</a>
                            <%
                               }%>

                        </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
            <div class="productPagination">
                <asp:Literal runat="server" ID="litPaginationLinks"></asp:Literal>
            </div>
        </asp:Panel>        
        <asp:Panel ID="pnlNoResults" runat="server" Visible="false">
            No products found in this category.
        </asp:Panel>
    </div>
    
</div>


<script type="text/javascript">
    jQuery(function($) {
        if (jQuery.browser.safari || jQuery.browser.chrome) {
            setTimeout(function() { jQuery('.catalogCategory ul.productList').equalHeights(true); }, 100);            
        }
        else {
            jQuery('.catalogCategory ul.productList').equalHeights(true);
        }

        
        <% if (WA.Parser.ToBool(StoreContext.CurrentStore.GetSetting(StoreSettingNames.ShowPriceAndQuantityInCatalog)).GetValueOrDefault(false))
            {%>
                jQuery(".productCategoryQuantity").change(function() {
                    jQuery(this).next("a").attr("href", jQuery(this).data("url") + "&q=" + jQuery(this).val());
                });
        <%
            }%>
    });
</script>

