<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCoupon.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.EditCoupon" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">

    <h1>Edit Coupon</h1>
    
    <asp:Literal ID="flash" runat="server"></asp:Literal>
        
    <div class="validationErrors">Please correct the following errors:<ul></ul></div>
    
    <ol class="form labelsLeft">
        <li>
            <label>Active:</label>
            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
        </li>
        <li>
            <label>Coupon Code: *</label>
            <span>
                <asp:TextBox ID="txtCode" runat="server" MaxLength="50" style="width: 250px;" CssClass="required" title="Coupon Code is required"></asp:TextBox>
                <span class="inputHelp" style="display: block;">(What the customer will type in to activate the discount)</span>
            </span>
        </li>      
        <li>
            <label>Description for Customer:</label>
            <span>
                <asp:TextBox ID="txtDescriptionForCustomer" runat="server" TextMode="MultiLine" MaxLength="250" style="width: 250px; height: 40px;"></asp:TextBox>
            </span>
        </li>
        <li>
            <label>Allow combinations with other coupons:</label>
            <asp:CheckBox ID="chkIsCombinable" runat="server" />
        </li>        
        <li>
            <label>Discount: *</label>
            <span>
                <asp:TextBox ID="txtDiscountNumber" runat="server" MaxLength="6" style="width: 60px;" CssClass="required number" title="Please enter a discount value"></asp:TextBox>
                
                <asp:RadioButtonList ID="rdoDiscountNumberType" runat="server" RepeatLayout="Flow" RepeatColumns="2" RepeatDirection="Horizontal">
                    <asp:ListItem Value="percent" Selected="True">percent (%)</asp:ListItem>
                    <asp:ListItem Value="amount">dollar amount ($)</asp:ListItem>
                </asp:RadioButtonList>
            </span>
        </li>       
        <li>
            <label>Applies To:</label>
            <span class="appliesTo">
                <span>
                    <input type="radio" id="rdoAppliesToSubTotal" name="rdoAppliesTo" value="<%= CouponDiscountType.SubTotal %>" <%= (coupon.DiscountTypeName == CouponDiscountType.SubTotal || coupon.DiscountTypeName == CouponDiscountType.UNKNOWN) ? "checked='checked'" : "" %> />
                    <label for="rdoAppliesToSubTotal">Order SubTotal</label>
                </span>
                <span>                    
                    <input type="radio" id="rdoAppliesToSubTotalAndShipping" name="rdoAppliesTo" value="<%= CouponDiscountType.SubTotalAndShipping %>" <%= (coupon.DiscountTypeName == CouponDiscountType.SubTotalAndShipping) ? "checked='checked'" : "" %> />
                    <label for="rdoAppliesToSubTotalAndShipping">Order SubTotal + Shipping &amp; Handling</label>                                     
                </span>                
                <span>                    
                    <input type="radio" id="rdoAppliesToProduct" name="rdoAppliesTo" value="<%= CouponDiscountType.Product %>" <%= (coupon.DiscountTypeName == CouponDiscountType.Product) ? "checked='checked'" : "" %> />
                    <label for="rdoAppliesToProduct">Only these Products</label>
                    <div style="display: none;">
                        <asp:CheckBoxList ID="chkAppliesToProducts" runat="server">
                        </asp:CheckBoxList>
                    </div>
                </span>
                <span>                    
                    <input type="radio" id="rdoAppliesToShipping" name="rdoAppliesTo" value="<%= CouponDiscountType.Shipping %>" <%= (coupon.DiscountTypeName == CouponDiscountType.Shipping) ? "checked='checked'" : "" %> />
                    <label for="rdoAppliesToShipping">Only these Shipping &amp; Handling Option(s)</label>                    
                    <div style="display: none;">
                        <asp:CheckBoxList ID="chkAppliesToShipping" runat="server">
                        </asp:CheckBoxList>                
                    </div>
                </span>                
            </span>
        </li>
        <li>
            <label>Active Dates:</label>
            <span>                
                <asp:TextBox ID="txtValidDateFrom" runat="server" style="width: 100px;" autocomplete="off"></asp:TextBox>
                
                <asp:Label ID="Label2" AssociatedControlID="txtValidDateTo" runat="server" Text="to:"></asp:Label>
                <asp:TextBox ID="txtValidDateTo" runat="server" style="width: 100px;"  autocomplete="off"></asp:TextBox>
            </span>
        </li>
        <li>
            <label>Min. Cart Total:</label>
            <span>
                <asp:TextBox ID="txtMinOrderAmount" runat="server" style="width: 60px;"></asp:TextBox>
                <span class="inputHelp">($ amount, user must order at least this amount to use this coupon)</span>
            </span>
        </li>        
        <!--
        <li>
            <label>Max. # Redemptions - Per User:</label>
            <span>
                <asp:TextBox ID="txtMaxUsesPerUser" runat="server" style="width: 60px;"></asp:TextBox>
            </span>
        </li>
        -->
        <li>
            <label>Max. # Redemptions - Lifetime:</label>
            <span>
                <asp:TextBox ID="txtMaxUsesLifetime" runat="server" style="width: 60px;"></asp:TextBox>
                <span class="inputHelp">(# of times this coupon can be used before it become invalid)</span>
            </span>
        </li>      
        <li>
            <label>Max. Discount Amount Per Order:</label>
            <span>
                <asp:TextBox ID="txtMaxDiscountAmountPerOrder" runat="server" style="width: 60px;"></asp:TextBox>
                <span class="inputHelp">(Set the maximum discount ($ amount) that this coupon can give the customer)</span>
            </span>
        </li>      
        <li>
            <label>&nbsp;</label>
            <span>
                <asp:Button ID="btnSave" runat="server" CssClass="adminIconBtn ok" Text="Save" OnClick="btnSave_Click" />
                <input type="button" onclick="location.href='<%= StoreUrls.Admin(ModuleDefs.Admin.Views.Coupons) %>'; return false;" class="adminIconBtn cancel" value="Cancel" />
            </span>
        </li>          
    </ol>    
</div>    


<script type="text/javascript">    
    var $appliesToProductList = null;
    var $appliesToShippingList = null;

    jQuery(function($) {

        jQuery('#<%= txtValidDateFrom.ClientID %>, #<%= txtValidDateTo.ClientID %>').datepicker({
            numberOfMonths: 2
        });

        //--- "Applies To" widgets
        $appliesToProductList = jQuery('#appliesToProductList');
        $appliesToShippingList = jQuery('#appliesToShippingList');
        jQuery('.appliesTo :radio:checked').nextAll('div').show();
        jQuery('.appliesTo :radio').click(function() {
            jQuery('.appliesTo span > div').hide();
            var $this = jQuery(this);
            $this.nextAll('div').show();
        });

        //--- Form Validation
        jQuery('form').validate({
            errorContainer: ".validationErrors",
            errorLabelContainer: ".validationErrors > ul",
            wrapper: "li",
            onfocusout: false,
            onkeyup: false
        });

        jQuery('#<%= txtCode.ClientID %>').focus();
    });
</script>

