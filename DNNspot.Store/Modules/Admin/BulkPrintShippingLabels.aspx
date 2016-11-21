<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkPrintShippingLabels.aspx.cs" Inherits="DNNspot.Store.Modules.Admin.BulkPrintShippingLabels1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <asp:Repeater ID="rptLabels" runat="server">
        <ItemTemplate>
            <div style="page-break-after: always;">
                <img src="<%# storeUrls.ShippingLabelFolderUrlRoot + (Container.DataItem as Order).ShippingServiceLabelFile %>" alt="label" />
            </div>
        </ItemTemplate>
    </asp:Repeater>


    </form>
</body>
</html>
