<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="EditProductField.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.EditProductField" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">
    <h1><%= productField.Id.HasValue ? "Edit" : "New" %> Variant / Attribute</h1>
    
    <div class="validationErrors">Please correct the following errors:<ul></ul></div>
    
    <ol class="form labelsLeft">        
        <li id="liWidgetType" runat="server">
            <label>Type:</label>
            <span>              
                <asp:DropDownList ID="ddlWidgetType" runat="server">
                </asp:DropDownList>   
            </span>
        </li>        
        <li>
            <label>Name:<br /><span class="inputHelp">required</span></label>
            <span>
                <asp:TextBox ID="txtName" runat="server" CssClass="required" MaxLength="100" style="width: 200px;" title="Name is required"></asp:TextBox>
            </span>
        </li>
        <% if(productField.Id.HasValue) { %>
        <li>
            <label>System Name:</label>
            <span>
                <%= productField.Slug %>
            </span>
        </li>                            
        <% } %>
        <li>
            <label>Required:</label>
            <span>
                <asp:CheckBox ID="chkIsRequired" runat="server" />
            </span>
        </li>           
        <li id="liPrice" runat="server" class="price">
            <label>Price Adjustment:</label>
            <span>
                <asp:TextBox ID="txtPriceAdjustment" runat="server" style="width: 60px;"></asp:TextBox>
            </span>
        </li> 
        <li id="liWeight" runat="server" class="weight">
            <label>Weight Adjustment:</label>
            <span>
                <asp:TextBox ID="txtWeightAdjustment" runat="server" style="width: 60px;"></asp:TextBox>
            </span>
        </li>                                                                      
        <li id="liOptions" runat="server" class="options">
            <label>Options:</label>
            <span>
                <table id="tblOptionChoices" cellpadding="0" cellspacing="0">
                    <thead>
                        <tr>
                            <th></th>
                            <th>Name</th>
                            <th>Value</th>
                            <th>Price Adjust ($)</th>
                            <th>Weight Adjust (lbs.)</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptFieldChoices" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><img src="<%= ModuleRootImagePath %>icons/move.png" class="moveHandle" alt="move" title="move" /></td>
                                    <td>
                                        <input type="text" name="optionName" maxlength="100" value="<%# (Container.DataItem as ProductFieldChoice).Name.Replace(@"""", @"&quot;") %>" />
                                    </td>        
                                    <td>
                                        <input type="text" name="optionValue" maxlength="100" value="<%# (Container.DataItem as ProductFieldChoice).Value.Replace(@"""", @"&quot;") %>" />
                                    </td>                                            
                                    <td class="price">
                                        <input type="text" name="optionPriceAdjust" value="<%# (Container.DataItem as ProductFieldChoice).PriceAdjustment.HasValue ? (Container.DataItem as ProductFieldChoice).PriceAdjustment.Value.ToString("N2") : "" %>" />
                                    </td>                              
                                    <td class="weight">
                                        <input type="text" name="optionWeightAdjust" value="<%# (Container.DataItem as ProductFieldChoice).WeightAdjustment.HasValue ? (Container.DataItem as ProductFieldChoice).WeightAdjustment.Value.ToString("N2") : "" %>" />
                                    </td>                                        
                                    <td>
                                        <a href="#" onclick="removeOptionRow(jQuery(this).parents('tr').eq(0)); return false;" title="delete"><img src="<%= ModuleRootImagePath %>icons/delete.png" title="delete" alt="delete" /></a>
                                    </td>
                                </tr>                              
                            </ItemTemplate>
                        </asp:Repeater>                    
                                <tr class="newOptionRow">
                                    <td><img src="<%= ModuleRootImagePath %>icons/move.png" class="moveHandle" alt="move" title="move" /></td>
                                    <td>
                                        <input type="text" name="optionName" maxlength="100" />
                                    </td>   
                                    <td>
                                        <input type="text" name="optionValue" maxlength="100" />
                                    </td>                                            
                                    <td class="price">
                                        <input type="text" name="optionPriceAdjust" />
                                    </td>                              
                                    <td class="weight">
                                        <input type="text" name="optionWeightAdjust" />
                                    </td>                                        
                                    <td>
                                        <a href="#" onclick="removeOptionRow(jQuery(this).parents('tr').eq(0)); return false;" title="delete"><img src="<%= ModuleRootImagePath %>icons/delete.png" title="delete" alt="delete" /></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <a href="#" style="font-size: 10px;" onclick="addMoreCustomFieldOptions(jQuery(this).parents('table').eq(0)); return false;">Add more options</a>
                                    </td>
                                </tr>                                                                                                                                                                       
                    </tbody>
                </table>
            </span>
        </li>            
        <li>
            <label>&nbsp;</label>
            <span>
                <asp:Button ID="btnSave" runat="server" CssClass="adminIconBtn ok" Text="Save" OnClick="btnSave_Click" />
                <a href="<%= StoreUrls.AdminEditProduct(product.Id.Value) %>" class="adminIconBtn cancel">Cancel</a>    
                
                <% if (productField.Id.HasValue) { %>
                <a href="<%= StoreUrls.AdminDeleteProductField(productField.Id.Value) %>" onclick="return confirm('Are you sure you want to delete this attribute?');" class="adminIconBtn delete">Delete</a>    
                <% } %>                
            </span>
        </li>          
    </ol>

</div>


<script type="text/javascript">
var $optionsLi = null;
var $fieldAdjustments = null;
var urlToAjaxHandler = "<%= StoreUrls.AdminAjaxHandler %>";

    // Document Ready
jQuery(function ($) {

    // Widget Type DropDown Handling
    var $ddlWidgetType = jQuery('#<%= ddlWidgetType.ClientID %>');
    $optionsLi = jQuery('.dstore li.options');
    $fieldAdjustments = jQuery('.dstore li.price, .dstore li.weight');
    showHideWidgetFields($ddlWidgetType);
    $ddlWidgetType.change(function () {
        showHideWidgetFields(jQuery(this));
    });

    jQuery("#<%= txtName.ClientID %>").maxlength();
    jQuery("input[name='optionName']").maxlength({ slider: true });


    $sortableProductFields = null;

    // Sortable        
    // Custom Fields - sortable
    var sortableOptions = {};
    sortableOptions.handle = ".moveHandle";
    sortableOptions.axis = "y";
    sortableOptions.placeholder = "ui-drop-placeholder";
    sortableOptions.tolerance = "pointer";
    sortableOptions.opacity = 0.5;
    sortableOptions.sort = function (event, ui) {
        var itemHeightPx = ui.item.css('height');
        if (itemHeightPx) {
            ui.placeholder.css('height', itemHeightPx);
        }
    };
    sortableOptions.update = function (event, ui) {
//        var sortedArray = jQuery("#tblOptionChoices tbody tr").sortable('toArray');
//        console.log(sortedArray);
//        var productFieldIdArray = [];
//        jQuery.each(sortedArray, function (i, val) {
//            productFieldIdArray.push(parseInt(i));
//        });
//        if (productFieldIdArray.length > 1) {
//            jQuery.post(urlToAjaxHandler, { 'action': 'updateProductFieldSortOrder', 'sortedProductFieldIds': productFieldIdArray }, function (data) {
//                if (data.success) {
//                    $sortableProductFields.effect('highlight', { backgroundColor: '#FFFF40' }, 1500);
//                }
//                else {
//                    //console.log('error');
//                    //if (data.error) console.log(data.error);
//                }
//            }
//                , "json");
//        }
    };
    $sortableProductFields = jQuery("#tblOptionChoices tbody");
    $sortableProductFields.sortable(sortableOptions);




    // Form Validation
    var $validator = $('form').validate({
        errorContainer: ".validationErrors",
        errorLabelContainer: ".validationErrors > ul",
        wrapper: "li",
        onfocusout: false,
        onkeyup: false
    });
});

    function showHideWidgetFields(widgetTypeDropdown) {
        //check for types that have no options
        var val = widgetTypeDropdown.val();
        if (val == '<%= ProductFieldWidgetType.Textbox %>'
                || val == '<%= ProductFieldWidgetType.Textarea %>'
                || val == '<%= ProductFieldWidgetType.Checkbox %>') {
            $optionsLi.hide();
            $fieldAdjustments.show();
        }
        else {
            $optionsLi.show();
            $fieldAdjustments.hide();
        }
    }

    function addMoreCustomFieldOptions(tableElm) {
        // clone the lastOptionRow, then append the html to the end of the table
        var lastOptionRow = jQuery('tr.newOptionRow:last', tableElm);

        // this is a hack to simulate the 'outerHtml' property that we need
        lastOptionRow.after(jQuery('<div>').append(lastOptionRow.clone()).html());
        lastOptionRow.removeClass('newOptionRow');
    }

    function removeOptionRow(trToRemove) {        
        var showPrompt = false;
        jQuery(':input', trToRemove).each(function(i, obj) {            
            if (jQuery(obj).val()) {
                showPrompt = true;
                return false;
            }
        });

        if (showPrompt && confirm("Are you sure you want to remove this option?")) {
            trToRemove.remove();
        }
        else if(!showPrompt) {
            trToRemove.remove();
        }
    }
    
</script>

