<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditEmailTemplate.ascx.cs"
    Inherits="DNNspot.Store.Modules.Admin.EditEmailTemplate" %>
<%@ Import Namespace="DNNspot.Store" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<div class="dstore <%= this.GetType().BaseType.Name %>">
    <h1>
        Edit Email Template: "<asp:Literal ID="litTemplateName" runat="server"></asp:Literal>"</h1>
    <ol class="form">
        <li>
            <label>
                Subject:</label>
            <span>
                <asp:TextBox ID="txtSubjectTemplate" runat="server" MaxLength="200" Width="400px"></asp:TextBox>
            </span></li>
        <li>
            <label>
                Body:</label>
            <span>
                <dnn:TextEditor id="txtBodyTemplate" runat="server" height="400" width="500">
                </dnn:TextEditor>
            </span></li>
        <li>
            <label>
                <a href="#" onclick="jQuery('#tokenHelp').toggle(); return false;">Token Reference &raquo;</a></label>
            <span>
                <div id="tokenHelp" style="display: none;">
                    <ul>
                        <li>{{store.name}}</li>
                        <li>{{store.contactemail}}</li>
                    </ul>
                    <ul>
                        <li>{{customer.userid}}</li>
                        <li>{{customer.firstname}}</li>
                        <li>{{customer.lastname}}</li>
                        <li>{{customer.email}}</li>
                    </ul>
                    <ul>
                        <li>{{order.number}}</li>
                        <li>{{order.status}}</li>
                        <li>{{order.payment.status}}</li>
                        <li>{{order.payment.summary}}</li>
                        <li>{{order.date}}</li>
                        <li>{{order.date.monthname}}</li>
                        <li>{{order.date.day}}</li>
                        <li>{{order.date.year}}</li>
                        <li>{{order.subtotal}}</li>
                        <li>{{order.shippingcost}}</li>
                        <li>{{order.couponcodes}}</li>
                        <li>{{order.tax}}</li>
                        <li>{{order.total}}</li>
                    </ul>
                    <ul>
                        <li>{{order.billing.address1}}</li>
                        <li>{{order.billing.address2}}</li>
                        <li>{{order.billing.city}}</li>
                        <li>{{order.billing.region}}</li>
                        <li>{{order.billing.postalcode}}</li>
                        <li>{{order.billing.countrycode}}</li>
                        <li>{{order.billing.telephone}}</li>
                    </ul>
                    <ul>
                        <li>{{order.billing.creditcardtype}}</li>
                        <li>{{order.billing.creditcardlast4}}</li>
                        <li>{{order.billing.creditcardexpiration}}</li>
                    </ul>
                    <ul>
                        <li>{{order.payment.status}}</li>
                    </ul>
                    <ul>
                        <li>{{order.shipping.recipientname}}</li>
                        <li>{{order.shipping.address1}}</li>
                        <li>{{order.shipping.address2}}</li>
                        <li>{{order.shipping.city}}</li>
                        <li>{{order.shipping.region}}</li>
                        <li>{{order.shipping.postalcode}}</li>
                        <li>{{order.shipping.countrycode}}</li>
                        <li>{{order.shipping.telephone}}</li>
                    </ul>
                    <ul>
                        <li>{{order.shipping.option}}</li>
                        <li>{{order.shipping.cost}}</li>
                        <li>{{order.shipping.trackingnumber}}</li>
                    </ul>
                    <ul>
                        <li>{{order.itemstable}}</li>
                        <li>{{order.itemstablenoprice}}</li>
                        <li>{{order.itemsdelim}}</li>
                        <li>{{order.itemsjson}}</li>
                    </ul>
                    <ul>
                        <li>{{order.ordernotes}}</li>                     
                    </ul>
                </div>
            </span></li>
    </ol>
    <br />
    <div class="adminToolbar">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="adminIconBtn ok" OnClick="btnSave_Click" />
        <a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.EmailTemplates) %>" class="adminIconBtn cancel">
            Cancel</a>
    </div>
</div>
