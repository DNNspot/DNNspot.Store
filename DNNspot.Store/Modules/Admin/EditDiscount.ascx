<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditDiscount.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.EditDiscount" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">

    <h1>Add / Edit Discount</h1>
    
    <asp:Literal ID="flash" runat="server"></asp:Literal>
    
    <p>
        Discounts are automatically applied based on the Role of the logged-in user and the criteria below.
    </p>
        
    <div class="validationErrors">Please correct the following errors:<ul></ul></div>
    
    <ol class="form labelsLeft">
        <li>
            <label>Active:</label>
            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
        </li>
        <li>
            <label>Name/Description: *</label>
            <span>
                <asp:TextBox ID="txtName" runat="server" MaxLength="500" style="width: 400px;" CssClass="required" title="Please enter a name"></asp:TextBox>
            </span>
        </li>   
        <li>
            <label>DNN Role: *</label>
            <span>
                <asp:DropDownList ID="ddlDnnRole" runat="server" CssClass="required" title="Please choose a DNN Role">
                </asp:DropDownList>
            </span>
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
            <label>Allow combinations with other discounts:</label>
            <asp:CheckBox ID="chkIsCombinable" runat="server" />
        </li>   
        <li>
            <label>Active Dates:</label>
            <span>                
                <asp:TextBox ID="txtValidDateFrom" runat="server" style="width: 100px;" autocomplete="off"></asp:TextBox>
                
                <asp:Label ID="Label2" AssociatedControlID="txtValidDateTo" runat="server" Text="to:"></asp:Label>
                <asp:TextBox ID="txtValidDateTo" runat="server" style="width: 100px;" autocomplete="off"></asp:TextBox>
            </span>
        </li>
        <li>
            <label>Applies To:</label>
            <span class="appliesTo">
                <span>
                    <input type="radio" id="rdoAppliesToAllProducts" name="rdoAppliesTo" value="<%= DiscountDiscountType.AllProducts %>" <%= (discount.DiscountTypeName == DiscountDiscountType.AllProducts || discount.DiscountTypeName == DiscountDiscountType.UNKNOWN) ? "checked='checked'" : "" %> />
                    <label for="rdoAppliesToAllProducts">ALL Products</label>
                </span>
                <span>                    
                    <input type="radio" id="rdoAppliesToProduct" name="rdoAppliesTo" value="<%= DiscountDiscountType.Product %>" <%= (discount.DiscountTypeName == DiscountDiscountType.Product) ? "checked='checked'" : "" %> />
                    <label for="rdoAppliesToProduct">Only these Products</label>
                    <div style="display: none;">
                        <asp:CheckBoxList ID="chkAppliesToProducts" runat="server">
                        </asp:CheckBoxList>
                    </div>
                </span>   
                <span>                    
                    <input type="radio" id="rdoAppliesToCategory" name="rdoAppliesTo" value="<%= DiscountDiscountType.Category %>" <%= (discount.DiscountTypeName == DiscountDiscountType.Category) ? "checked='checked'" : "" %> />
                    <label for="rdoAppliesToCategory">Only these Categories</label>
                    <div style="display: none;">
                        <asp:CheckBoxList ID="chkAppliesToCategories" runat="server">
                        </asp:CheckBoxList>
                    </div>
                </span>                                            
            </span>
        </li>  
        <li>
            <label>&nbsp;</label>
            <span>
                <asp:Button ID="btnSave" runat="server" CssClass="adminIconBtn ok" Text="Save" OnClick="btnSave_Click" />
                <input type="button" onclick="location.href='<%= StoreUrls.Admin(ModuleDefs.Admin.Views.Discounts) %>'; return false;" class="adminIconBtn cancel" value="Cancel" />
            </span>
        </li>          
    </ol>    
</div>    

<script type="text/javascript">
    var $appliesToProductList = null;
    var $appliesToCategoryList = null;

    jQuery(function ($) {

        jQuery('#<%= txtValidDateFrom.ClientID %>, #<%= txtValidDateTo.ClientID %>').datepicker({
            numberOfMonths: 2
        });

        //--- "Applies To" widgets
        $appliesToProductList = jQuery('#appliesToProductList');
        $appliesToCategoryList = jQuery('#appliesToShippingList');
        jQuery('.appliesTo :radio:checked').nextAll('div').show();
        jQuery('.appliesTo :radio').click(function () {
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

    });
</script>