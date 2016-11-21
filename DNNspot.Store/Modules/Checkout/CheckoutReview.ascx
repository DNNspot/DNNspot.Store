<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckoutReview.ascx.cs"
    Inherits="DNNspot.Store.Modules.Checkout.CheckoutReview" %>
<%@ Register TagPrefix="dstore" TagName="AddressForm" Src="../../UserControls/AddressForm.ascx" %>
<div class="dstore checkout checkoutReview">
    <h2><asp:Literal ID="litReviewOrder" runat="server"></asp:Literal></h2>
    <div id="msgFlash" runat="server" class="flash" visible="false">
    </div>
    <div class="stepsNav review" id="stepsNav" runat="server">
        <ol>
            <li class="pnlBilling"><a href="<%= StoreUrls.CheckoutBilling() %>"><span><asp:Literal ID="litBillingBreadcrumb" runat="server"></asp:Literal></span></a></li>
            <li class="pnlShipping"><a href="<%= StoreUrls.CheckoutShipping() %>"><span><asp:Literal ID="litShippingBreadcrumb" runat="server"></asp:Literal></span></a></li>
            <li class="pnlShippingMethod"><a href="<%= StoreUrls.CheckoutShippingMethod() %>"><span><asp:Literal ID="litShippingMethodBreadcrumb" runat="server"></asp:Literal></span></a></li>
            <li class="pnlPayment"><a href="<%= StoreUrls.CheckoutPayment() %>"><span><asp:Literal ID="litPaymentBreadcrumb" runat="server"></asp:Literal></span></a></li>
            <li><span class="active"><asp:Literal ID="litReviewOrderBreadcrumb" runat="server"></asp:Literal></span></li>
        </ol>
    </div>
    <fieldset class="panel">
        <asp:Repeater ID="rptCheckoutItems" runat="server">
            <HeaderTemplate>
                <table class="checkoutItems" cellpadding="0" cellspacing="0">
                    <thead>
                        <tr>
                            <th class="colProduct">
                                Product
                            </th>
                            <th class="colQty">
                                Qty.
                            </th>
                            <th class="colSubtotal">
                                Subtotal
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="colProduct">
                        <%# (Container.DataItem as vCartItemProductInfo).ProductName %>
                        <%# (Container.DataItem as vCartItemProductInfo).GetProductFieldDataDisplayString() %>
                    </td>
                    <td class="colQty">
                        <span class="quantityPerProduct"><%# (Container.DataItem as vCartItemProductInfo).Quantity%></span>
                        <span class="pricePerProductX">x</span>
                        <span class="pricePerProduct"><%# StoreContext.CurrentStore.FormatCurrency((Container.DataItem as vCartItemProductInfo).GetPriceForSingleItem())%></span>
                    </td>
                    <td class="colSubtotal">
                        <%# StoreContext.CurrentStore.FormatCurrency((Container.DataItem as vCartItemProductInfo).GetPriceForQuantity())%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
        <div class="leftCol">
            <div class="billingSummary box">
                <label><asp:Literal ID="litBillTo" runat="server"></asp:Literal></label>
                <p>
                    <asp:Literal ID="litBillToSummary" runat="server"></asp:Literal>
                </p>
            </div>
            <div class="paymentSummary box">
                <label>
                    Payment:</label>
                <p>
                    <asp:Literal ID="litPaymentSummary" runat="server"></asp:Literal>
                </p>
            </div>
            <div class="shippingSummary box">
                <label>
                    Ship To:</label>
                <p>
                    <asp:Literal ID="litShipToSummary" runat="server"></asp:Literal>
                </p>
            </div>
        </div>
        <div class="rightCol">
            <div class="priceSummary">
                <table cellpadding="0" cellspacing="0">
                    <tr class="subtotal">
                        <td>
                            Subtotal
                        </td>
                        <td>
                            <%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.SubTotal)%>
                        </td>
                    </tr>
                    <tr class="shipping">
                        <td>
                            Shipping &amp; Handling
                            <br />
                            <%= checkoutOrderInfo.ShippingRate.ServiceTypeDescription %>
                        </td>
                        <td>
                            <%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.ShippingRate.Rate)%>
                        </td>
                    </tr>
                    <% if (checkoutOrderInfo.DiscountAmount > 0)
                       { %>
                    <tr class="discount">
                        <td>
                            Discount
                        </td>
                        <td>
                            (<%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.DiscountAmount)%>)
                        </td>
                    </tr>
                    <% } %>
                    <tr class="tax">
                        <td>
                            Tax
                        </td>
                        <td>
                            <%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.TaxAmount)%>
                        </td>
                    </tr>
                    <tr class="total">
                        <td>
                            Total
                        </td>
                        <td>
                            <%= StoreContext.CurrentStore.FormatCurrency(checkoutOrderInfo.Total)%>
                        </td>
                    </tr>
                </table>
            </div>
            <p class="orderNotes">
                <asp:Label ID="lblOrderNotes" runat="server" AssociatedControlID="txtOrderNotes"></asp:Label>
                <asp:TextBox ID="txtOrderNotes" CssClass="required" TextMode="MultiLine" Rows="3" Columns="28" runat="server"></asp:TextBox></p>
            <asp:Button ID="btnPlaceOrder" runat="server" CssClass="btnPlaceOrder next dnnPrimaryAction" OnClick="btnPlaceOrder_Click" />
            <div id="processingOrderSpinner" style="display: none;">
                <img src="<%= ModuleRootImagePath %>ajax-loader-bar.gif" alt="processing order..."
                    title="processing order..." />
                <div><asp:Literal ID="litProcessingOrder" runat="server"></asp:Literal></div>
            </div>
        </div>
    </fieldset>
    <div class="validationErrors">
        Please correct the following errors:<ul>
        </ul>
    </div>
</div>
<script type="text/javascript">

    jQuery(function ($) {
        jQuery('#<%=btnPlaceOrder.ClientID %>').click(function () {
            <% if(RequireOrderNotes) { %>
            if($("form").valid() != false)
            {
                jQuery(this).hide();
                jQuery('#processingOrderSpinner').show();
            }
            <% } else { %>
                jQuery(this).hide();
                jQuery('#processingOrderSpinner').show();
            <% } %>
        });

        <% if(RequireOrderNotes) { %>
        // Form Validation
        var $billingValidator = $('form').validate({
            errorContainer: ".validationErrors",
            errorLabelContainer: ".validationErrors > ul",
            wrapper: "li",
            onfocusout: false,
            onkeyup: false,
            onclick: false
        });
        <% } %>
    });

</script>
            
