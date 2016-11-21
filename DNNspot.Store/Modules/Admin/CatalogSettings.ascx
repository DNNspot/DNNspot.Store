<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CatalogSettings.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.CatalogSettings" %>

<div class="dstore <%= this.GetType().BaseType.Name %>">
    <h1>Catalog Settings</h1>
    
    <asp:Literal ID="flash" runat="server"></asp:Literal>
    
    <ol class="form labelsLeft">
        <li>                
            <label>Default Sort Order:</label>
            <span>
                <asp:DropDownList ID="ddlDefaultSortOrder" runat="server">
                </asp:DropDownList>
            </span>
        </li>      
        <li>
            <label>Max Products Per Page:</label>
            <asp:TextBox runat="server" ID="txtMaxResultsPerPage" style="width: 80px;" Text="100"></asp:TextBox>
        </li>             
    </ol>
    <div style="clear: both; overflow: auto;">&nbsp;</div>
    <div class="adminToolbar">
        <asp:Button ID="btnSaveSettings" runat="server" CssClass="adminIconBtn ok" Text="Save" OnClick="btnSaveSettings_Click" />
        <a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.AdminHome) %>" class="adminIconBtn cancel">Cancel</a>    
    </div>     

</div>