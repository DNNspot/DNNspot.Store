<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="DNNspot.Store.Modules.Featured.Settings" %>

<div class="dstore featuredSettings">

    <h2>Filter</h2>
    <table>
        <tr>
            <th style="text-align: right;">Sort By:</th>
            <td>
                <asp:DropDownList runat="server" ID="ddlSortBy">                    
                </asp:DropDownList>        
            </td>
        </tr>
        <tr>
            <th style="text-align: right;">Max # Products:</th>
            <td>
                <asp:TextBox runat="server" ID="txtMaxProducts" style="width: 50px;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="text-align: right;">Categories:</th>
            <td>
                <asp:RadioButtonList runat="server" ID="rdoCategoryFilterMethod" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="2">
                    <asp:ListItem Value="Any" Selected="True">Match ANY</asp:ListItem>
                    <asp:ListItem Value="All">Match ALL</asp:ListItem>
                </asp:RadioButtonList>
                <asp:CheckBoxList runat="server" ID="chkCategoryIds" RepeatLayout="Table" RepeatColumns="1" RepeatDirection="Vertical">
                </asp:CheckBoxList>        
            </td>
        </tr>
    </table>

    <h2>Template</h2>
    <table>
        <tr>
            <th style="text-align: right;">Header</th>
            <td>
                <asp:TextBox runat="server" ID="txtTemplateHeader" TextMode="MultiLine" style="width: 450px; height: 40px;" MaxLength="2000"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="text-align: right;">Product</th>
            <td>
                <asp:TextBox runat="server" ID="txtTemplateProduct" TextMode="MultiLine" style="width: 450px; height: 150px;" MaxLength="2000"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="text-align: right;">Footer</th>
            <td>
                <asp:TextBox runat="server" ID="txtTemplateFooter" TextMode="MultiLine" style="width: 450px; height: 40px;" MaxLength="2000"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="text-align: right;">No Results</th>
            <td>
                <asp:TextBox runat="server" ID="txtTemplateNoResults" TextMode="MultiLine" style="width: 450px; height: 40px;" MaxLength="2000"></asp:TextBox>
            </td>
        </tr>
    </table>

    <div style="margin: 1em 0 2em 0;">
        <a href="#" onclick="jQuery('#tokenHelp').toggle(); return false;" style="font-size: 14px; font-weight: bold; display: block; text-decoration: underline;">Template/Token Reference</a>
        <div id="tokenHelp" style="display: none;">
            <table border="1">
                <tr>
                    <th>[Product:Id]</th>
                    <td>Product Id</td>
                </tr>
                <tr>
                    <th>[Product:Name]</th>
                    <td>Product Name</td>
                </tr>
                <tr>
                    <th>[Product:UrlName]</th>
                    <td>Product Url Name (slug)</td>
                </tr>
                <tr>
                    <th>[Product:Price]</th>
                    <td>Product Price</td>
                </tr>
                <tr>
                    <th>[Product:Sku]</th>
                    <td>Product SKU</td>
                </tr>
                <tr>
                    <th>[Product:Url]</th>
                    <td>Url to the Product Detail</td>
                </tr>
                <tr>
                    <th>[Product:AddToCartUrl]</th>
                    <td>Url to add the product to the customer's cart</td>
                </tr>
                <tr>
                    <th>[Product:AddToCartUrlAndBackToPage]</th>
                    <td>Url to add the product to the customer's cart, and then send the customer back to the page they were on</td>
                </tr>                
                <tr>
                    <th>
                        [Product:Photo]
                        <br />
                        [Product:Photo{Width=120}]
                        <br />
                        [Product:Photo{Width=120,Height=100}]
                    </th>
                    <td>
                        Main Product Photo.
                        <br />
                        Main Product Photo, width 120px.
                        <br />
                        Main Product Photo, width 120px and height 100px.
                    </td>
                </tr>
                <tr>
                    <th>
                        [Product:PhotoUrl]
                        <br />
                        [Product:PhotoUrl{Width=120}]
                        <br />
                        [Product:PhotoUrl{Width=120,Height=100}]
                    </th>
                    <td>
                        Url for Main Product Photo.
                        <br />
                        Url for Main Product Photo, width 120px.
                        <br />
                        Url for Main Product Photo, width 120px and height 100px.
                    </td>
                </tr>
                <tr>
                    <th>
                        [Product:DescriptionX]
                        <br />
                        [Product:DescriptionX{MaxChars=75}]
                    </th>
                    <td>
                        Where X is the Description Tab #. E.g. [Product:Description1] is the first description tab. Optional attribute to specify a max character count.
                    </td>
                </tr>
            </table>
        </div>
    </div>

</div>