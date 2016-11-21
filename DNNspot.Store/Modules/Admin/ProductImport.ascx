<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductImport.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.ProductImport" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">
    <h1>Import Products</h1>
    
    <asp:Literal ID="flash" runat="server"></asp:Literal>

    <p style="color: Red;">
        ** It is STRONGLY recommended that you make a backup of your DNN site BEFORE importing any products. **
    </p>
            
    <table>
        <tr>
            <td>
                <fieldset style="width: 85%;">
                    <legend>IMPORT PRODUCTS</legend>
                    <ol class="form">
                        <li>
                            <label style="font-weight: bold;">CSV File (*.csv):</label>
                            <span>
                                <asp:FileUpload ID="fupImportFile" runat="server" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="fupImportFile" ErrorMessage="*.csv files only" ValidationExpression=".*\.(csv|CSV)$"> *.csv files only</asp:RegularExpressionValidator>
                            </span>
                        </li>
                        <li style="padding-top: 0;">
                            <span>
                                <asp:Button ID="btnUploadFile" runat="server" Text="Upload File" CssClass="adminIconBtn" OnClick="btnUploadFile_Click" />
                            </span>
                        </li>
                    </ol>     
                </fieldset>  
                
                Download Example CSV Files:
                <ul>
                    <li><a href="<%= ModuleRootWebPath %>doc/ProductImport-Sample-Create.csv">Example - Create</a></li>
                    <li><a href="<%= ModuleRootWebPath %>doc/ProductImport-Sample-Update.csv">Example - Update</a></li>
                    <li><a href="<%= ModuleRootWebPath %>doc/ProductImport-Sample-Delete.csv">Example - Delete</a></li>
                </ul>                     
            </td>
            <td>
                <h4>How to Create, Replace, Update or Delete products in your store:</h4>
                <ol class="instructions">
                    <li>
                        Create a CSV file (.csv), 1 line per product, with the product information you wish to create/replace/update 
                        (You can create CSV files from Excel by choosing "Save As" and picking "CSV" in the "Save as type" drop-down).
                    </li>
                    <li>Specify the action to take for each product in the <code>ImportAction</code> column (see table below)</li>
                    <li>Categories are specified in the <code>CategoryNames</code> column, separated by commas</li>
                    <li>Photos are specified in the <code>PhotoFilenames</code> column, separated by commas. Photos are also automatically matched by Product SKU (any filename that starts with the SKU).</li>
                    <li>The digital file (downloadable file) for a product is specified in the <code>DigitalFilename</code> column.</li>                    
                    <li>Photos/Files must exist in the <code>/DesktopModules/DNNspot-Store/ImportFiles/</code> directory BEFORE uploading the CSV file in order to be imported</li>
                    <li>Save your CSV file and upload it!</li>
                </ol>       
                <p>
                    During import, products are found by matching these fields in the following order:
                    <ol class="instructions">
                        <li>Product ID</li>
                        <li>Url Name</li>
                        <li>Name</li>
                        <li>SKU</li>
                    </ol>
                </p>            
                <br />
                <table class="grid gridLight">
                    <thead>
                        <tr>
                            <th>ImportAction</th>
                            <th>Description</th>
                        </tr>
                    </thead>
                    <tr>
                        <th style="font-weight: bold;">C</th>
                        <td>
                            <b>Create</b><br />
                            Creates/inserts a product only if it does not exist.
                        </td>
                    </tr>
                    <tr>
                        <th style="font-weight: bold;">R</th>
                        <td>
                            <b>Replace</b><br />
		                    Replaces fields if product exists, or creates a new product if not found.<br />
		                    ALL matching fields are replaced/overwritten with the values from the CSV file (including empty fields).<br />
		                    Existing Product Categories are first DELETED, and then re-added from the CSV file.<br />
		                    Existing Product Photos are DELETED, and then re-added from the CSV.                            
                        </td>
                    </tr>
                    <tr>
                        <th style="font-weight: bold;">U</th>
                        <td>
                            <b>Update</b><br />
		                    Updates fields if product exists, or creates a new product if not found.<br />
		                    A field is only updated if the CSV field is non-empty.<br />
		                    Existing Product Categories are kept, and new Categories are added from the CSV file.<br />
		                    Existing Product Photos are kept, and new Photos are added.                            
                        </td>
                    </tr>
                    <tr>
                        <th style="font-weight: bold;">D</th>
                        <td>
                            <b>Delete</b><br />
                            Deletes the product completely, including all product photos, descriptions, variants, etc.<br />
                            This action cannot be undone.                            
                        </td>
                    </tr>                                                            
                </table>
            </td>
        </tr>
    </table>
    

    
</div>