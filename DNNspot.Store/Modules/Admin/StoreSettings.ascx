<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StoreSettings.ascx.cs"
    Inherits="DNNspot.Store.Modules.Admin.StoreSettings" %>
<div class="dstore <%=this.GetType().BaseType.Name%>">
    <h1>
        Store Settings</h1>
    <asp:Literal ID="flash" runat="server"></asp:Literal>
    <ol class="form labelsLeft">
        <li>
            <label>
                Store Name</label>
            <asp:TextBox ID="txtStoreName" runat="server" Width="400px"></asp:TextBox>
        </li>
        <li>
            <label>
                Store GUID</label>
            <span>
                <%=StoreContext.CurrentStore.StoreGuid%></span> </li>
            <% if (DnnVersionSingleton.Instance.IsDnn5) {%>
                <li>
            <% } else {%>
                <li style="display:none;">
            <% } %>
            <label>
                Load jQuery UI:</label>
            <span>
                <asp:CheckBox ID="chkLoadJQueryUi" runat="server" Checked="true" />
                <br />
                <span class="inputHelp">Disable jQuery UI Javascript</span> </span></li>
        <li>
            <label>
                Customer Service Email</label>
            <span>
                <asp:TextBox ID="txtCustomerServiceEmail" runat="server" Width="400px"></asp:TextBox>
                <br />
                <span class="inputHelp">customer service/contact email address to be included in order
                    emails</span> </span></li>
        <li>
            <label>
                Order # Prefix</label>
            <span>
                <asp:TextBox ID="txtOrderNumberPrefix" runat="server" Width="140px"></asp:TextBox>
                <br />
                <span class="inputHelp">custom prefix for Order Numbers, e.g. "DSTORE-"</span>
            </span></li>
        <li>
            <label>
                Default Country</label>
            <span>
                <asp:DropDownList ID="ddlDefaultCountryCode" runat="server">
                </asp:DropDownList>
                <br />
                <span class="inputHelp">This will pre-select the Country in the checkout Billing/Shipping
                    forms.</span> </span></li>
        <li>
            <label>
                Show product prices:</label>
            <span>
                <asp:CheckBox ID="chkShowPrices" runat="server" Checked="true" />
                <br />
                <span class="inputHelp">Disabling this option will hide product prices store-wide</span>
            </span></li>
        <li>
            <label>
                Show quantity and price in catalog listing:</label>
            <span>
                <asp:CheckBox ID="chkQuantityAndPrice" runat="server" Checked="true" />
                <br />
                <span class="inputHelp">Enabling this option will show the quantity and price when viewing products in the category listing.</span>
            </span></li>
    </ol>
    <ol class="form labelsLeft">
        <li>
            <label style="font-weight: bold;">
                My Store Location</label>
            <span><span class="inputHelp">The store location will be used to calculate Shipping
                Estimates.</span> </span></li>
        <li>
            <label>
                Phone #</label>
            <span>
                <asp:TextBox ID="txtStorePhone" runat="server" Style="width: 160px;"></asp:TextBox>
            </span></li>
        <li>
            <label>
                Street</label>
            <span>
                <asp:TextBox ID="txtStoreStreet" runat="server" Style="width: 160px;"></asp:TextBox>
            </span></li>
        <li>
            <label>
                City</label>
            <span>
                <asp:TextBox ID="txtStoreCity" runat="server" Style="width: 160px;"></asp:TextBox>
            </span></li>
        <li>
            <label>
                Region Code:<br />
                <span style="font-size: 10px;">Example: NY</span></label>
            <span>
                <asp:TextBox ID="txtStoreRegion" runat="server" Style="width: 40px;"></asp:TextBox>
            </span></li>
        <li>
            <label>
                ZIP / Postal Code</label>
            <span>
                <asp:TextBox ID="txtStorePostalCode" runat="server" Style="width: 70px;"></asp:TextBox>
            </span></li>
        <li>
            <label>
                Country</label>
            <span>
                <asp:DropDownList ID="ddlStoreCountryCode" runat="server">
                </asp:DropDownList>
            </span></li>
    </ol>
    <ol class="form labelsLeft">
        <li>
            <label style="font-weight: bold;">
                Cart Options</label>
        </li>
        <li>
            <label>
                Enable Shipping Estimate:</label>
            <span>
                <asp:CheckBox ID="chkShowShippingEstimateBox" runat="server" Checked="true" />
                <br />
                <span class="inputHelp">Disabling this option will hide the Shipping Estimate box on
                    the cart page</span> </span></li>
        <li>
            <label>
                Enable Coupons:</label>
            <span>
                <asp:CheckBox ID="chkShowCouponBox" runat="server" Checked="true" />
                <br />
                <span class="inputHelp">Disabling this option will hide the Coupon box on the cart page</span>
            </span></li>
    </ol>
    <ol class="form labelsLeft">
        <li>
            <label style="font-weight: bold;">
                Checkout Actions</label>
        </li>
        <li>
            <label>
                Enable checkout:</label>
            <span>
                <asp:CheckBox ID="chkEnableCheckout" runat="server" Checked="true" />
                <br />
                <span class="inputHelp">Disabling this option will hide all "Add to Cart" buttons and
                    disable checkout</span> </span></li>
        <li>
            <label>
                Allow checkout as guest<span class="inputHelp">(anonymous checkout)</span></label>
            <span>
                <asp:CheckBox ID="chkCheckoutAllowAnonymous" runat="server" Checked="true" />
            </span></li>
        <li>
            <label>
                Require Order Notes:</label>
            <span>
                <asp:CheckBox ID="chkRequireOrderNotes" runat="server" Checked="false" />
                <br />
                <span class="inputHelp">Checking this option will force customers to enter order notes
                    on the Checkout Review screen.</span> </span></li>
        <li>
            <label>
                Display "create account" link<span class="inputHelp">(register link)</span></label>
            <span>
                <asp:CheckBox ID="chkCheckoutShowCreateAccountLink" runat="server" Checked="true" />
            </span></li>
        <li>
            <label>
                Secure (SSL) Checkout</label>
            <span>
                <asp:CheckBox ID="chkForceSslCheckout" runat="server" />
                <br />
                <span class="inputHelp">If checked, all checkout pages will be directed to https://
                    (secure) instead of http://.
                    <br />
                    Only use this if you have installed an SSL certificate on your site/server.
                </span></span></li>
        <li>
            <label>
                Currency</label>
            <span>
                <asp:DropDownList ID="ddlCurrency" runat="server">
                </asp:DropDownList>
            </span></li>
        <li>
            <label>
                Tax Shipping:</label>
            <span>
                <asp:CheckBox ID="chkTaxShipping" runat="server" Checked="true" />
                <br />
                <span class="inputHelp">Uncheck this box if you do not want to tax shipping</span>
            </span></li>
        <li>
            <label>
                Accepted Credit Cards</label>
            <span>
                <asp:CheckBoxList runat="server" ID="chklAcceptedCreditCards" RepeatDirection="Horizontal"
                    RepeatColumns="2">
                    <asp:ListItem Value="Visa" Text="VISA"></asp:ListItem>
                    <asp:ListItem Value="MasterCard" Text="MasterCard"></asp:ListItem>
                    <asp:ListItem Value="Discover" Text="Discover"></asp:ListItem>
                    <asp:ListItem Value="Amex" Text="American Express"></asp:ListItem>
                </asp:CheckBoxList>
            </span></li>
    </ol>
    <ol class="form labelsLeft">
        <li>
            <label style="font-weight: bold;">
                Post-Checkout Actions</label>
        </li>
        <li>
            <label>
                Send order received email to customer</label>
            <span>
                <asp:CheckBox ID="chkSendOrderReceivedEmail" runat="server" Checked="true" />
            </span></li>
        <li>
            <label>
                Send payment complete email to customer</label>
            <span>
                <asp:CheckBox ID="chkSendPaymentCompleteEmail" runat="server" Checked="true" />
            </span></li>
        <li>
            <label>
                Send email to store owner</label>
            <span>
                <asp:TextBox ID="txtOrderCompletedEmailRecipient" runat="server" Width="400px"></asp:TextBox>
                <br />
                <span class="inputHelp">email addresses that should receive order notices for the store
                    (separate multiples with commas)</span> </span></li>
        <li>
            <label>
                POST the order to a URL</label>
            <span>
                <asp:TextBox ID="txtUrlToPostOrder" runat="server" Width="400px"></asp:TextBox>
                <span class="inputHelp">Completed order details will be sent to this url via HTTP POST</span>
            </span></li>
        <li>
            <label>
                &nbsp;</label>
            <span>
                <asp:CheckBox ID="chkDisplaySiteCredit" runat="server" Checked="true" />
                <span class="inputHelp" style="display: inline;">I'm a huge fan of DNNspot, include
                    a small credit under my store. </span></span></li>
    </ol>
    <div class="adminToolbar">
        <asp:Button ID="btnSaveSettings" runat="server" CssClass="adminIconBtn ok" Text="Save"
            OnClick="btnSaveSettings_Click" />
        <a href="<%= StoreUrls.Admin(ModuleDefs.Admin.Views.AdminHome) %>" class="adminIconBtn cancel">
            Cancel</a>
    </div>
</div>
