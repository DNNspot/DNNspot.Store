<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendCustomerEmail.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.SendCustomerEmail" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>

<div class="dstore admin">

    <h1>Send Customer Email</h1>
    
    <asp:Literal ID="flash" runat="server"></asp:Literal>
    
    <ol class="form labelsLeft">
        <li>
            <label>From:</label>
            <span>
                <asp:TextBox ID="txtFrom" runat="server" style="width: 250px;"></asp:TextBox>
            </span>
        </li>
        <li>
            <label>To:</label>
            <span>
                <asp:TextBox ID="txtTo" runat="server" style="width: 250px;"></asp:TextBox>
            </span>
        </li>  
        <li>
            <label>Subject:</label>
            <span>
                <asp:TextBox ID="txtSubject" runat="server" style="width: 250px;"></asp:TextBox>
            </span>
        </li>
        <li>
            <label>Body:</label>
            <span>
                <dnn:TextEditor id="txtBody" runat="server" height="400" width="500"></dnn:TextEditor>
            </span>
        </li>  
        <li>
            <label>&nbsp;</label>
            <span>
                <asp:Button ID="btnSendEmail" runat="server" CssClass="adminIconBtn email3" Text="Send Email" OnClick="btnSendEmail_Click" />
            </span>
        </li>                      
    </ol>

</div>