<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayPalStandardPostCart.aspx.cs" Inherits="DNNspot.Store.PayPal.PayPalStandardPostCart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Please wait...</title>
    <script type="text/javascript" src="../js/jquery.min.js"></script>
</head>
<body>
    <div style="text-align: center; margin-top: 50px;">
        <h2 style="font-family: Verdana; font-size: 16px;">Please wait while we communicate with PayPal &hellip;</h2>
        <img src="../images/ajax-loader-bar.gif" alt="progress indicator" />
        <p>
            <img src="../images/paypal/bnr_paymentsBy_150x60.gif" alt="PayPal" />
        </p>
    </div>
    <form name="payPalForm" action="<%= formPostAction %>" method="post">
        <asp:Literal ID="litFormFields" runat="server"></asp:Literal> 
        
        <input type="submit" id="btnSubmit" value="GO" style="display: none;" />
    </form>            
    

    <script type="text/javascript">
        jQuery(function($) {
            setTimeout(function() { jQuery('#btnSubmit').click(); }, 500);
        });
    </script>
    
</body>
</html>
