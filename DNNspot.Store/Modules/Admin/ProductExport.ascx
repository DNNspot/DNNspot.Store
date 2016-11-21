<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductExport.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.ProductExport" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">

    <h1>Export Products</h1>
    
    <asp:Button ID="btnDownloadCsv" runat="server" CssClass="adminIconBtn" Text="Download CSV File" OnClick="btnDownloadCsv_Click" />

</div>