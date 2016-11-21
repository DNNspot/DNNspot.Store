<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin.ascx.cs" Inherits="DNNspot.Store.Admin" %>


<div class="dstore admin">

    <div class="breadcrumb">
        <asp:Literal ID="litBreadcrumb" runat="server"></asp:Literal>
    </div>

    <asp:PlaceHolder ID="plhUserControl" runat="server"></asp:PlaceHolder>
</div>