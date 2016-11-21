<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Orders.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.Orders" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">
    
    <h1>Orders</h1>              
            
    <asp:Panel ID="pnlSearch" runat="server" style="overflow: auto;" DefaultButton="btnFilter">
        <ol class="form labelsLeft">
            <li>
                <label>Order Date:</label>
                <span>
                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="datepick"></asp:TextBox>
                    &nbsp;
                    to
                    &nbsp;
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="datepick"></asp:TextBox>                
                </span>
            </li>
            <li>
                <label>Customer:</label>
                <span>
                    <asp:TextBox ID="txtCustomerFirstName" runat="server" style="width: 150px;"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtCustomerLastName" runat="server" style="width: 150px;"></asp:TextBox>
                </span>
            </li>              
            <li>
                <label>&nbsp;</label>
                <span>
                    <asp:TextBox ID="txtCustomerEmail" runat="server" style="width: 235px;"></asp:TextBox>
                </span>
            </li>            
            <li>
                <label>Order Status:</label>
                <span>
                    <asp:CheckBoxList ID="chklOrderStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="3">
                    </asp:CheckBoxList>
                </span>
            </li>                      
            <li>
                <label>&nbsp;</label>
                <span>
                    <asp:Button ID="btnFilter" runat="server" CssClass="adminIconBtn" Text="Search" OnClick="btnFilter_Click" />                    
                </span>
            </li>
        </ol>
    </asp:Panel>          
    
    <div id="flash" class="flash" runat="server" visible="false"></div>      
    
    <div id="processShipmentMessages" class="flash" runat="server" visible="false"></div>
    
    <asp:Panel ID="pnlOrderResults" runat="server">
    
        <asp:Panel ID="pnlBulkActions" runat="server" Visible="false">        
            <div class="bulkActions">
                <span>Bulk Actions:</span>
                <asp:Button ID="btnBulkProcessShipments" runat="server" CssClass="adminIconBtn" Text="Process Shipments" OnClick="btnBulkProcessShipments_Click" />
                <asp:Button ID="btnBulkPrintShippingLabels" runat="server" CssClass="adminIconBtn" Text="Print Shipping Labels" OnClick="btnBulkPrintShippingLabels_Click" />
            </div>
        </asp:Panel>
    
        <asp:Repeater ID="rptOrders" runat="server">
            <HeaderTemplate>
                <table id="ordersGrid" cellpadding="0" cellspacing="0" class="grid tablesorter" style="margin-top: 1em; ">
                    <thead>
                        <tr>             
                            <% if(bulkActionsEnabled) { %>   
                            <th class="{ sorter: false }"><input type="checkbox" id="checkAllOrders" /></th>        
                            <% } %>
                            <th>Order #</th>
                            <th class="leftAlign">Date</th>                        
                            <th class="rightAlign">Total</th>
                            <th>Status</th>
                            <th>Payment</th>       
                            <th>Shipper Tracking #</th>                 
                            <th>Customer</th>                  
                            <th class="{ sorter: false }"></th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                        <tr>
                            <% if(bulkActionsEnabled) { %>   
                            <td class="{ sorter: false }"><input type="checkbox" name="orderNumber" value="<%# (Container.DataItem as Order).OrderNumber %>" /></td>
                            <% } %>
                            <td><a href="<%# StoreUrls.AdminViewOrder((Container.DataItem as Order).Id.Value) %>"><%# (Container.DataItem as Order).OrderNumber %></a></td>
                            <td class="leftAlign"><%# (Container.DataItem as Order).CreatedOn.Value.ToString() %></td>                        
                            <td class="rightAlign"><%# (Container.DataItem as Order).Total.Value.ToString("C2") %></td>
                            <td><%# (Container.DataItem as Order).OrderStatus %></td>
                            <td><%# (Container.DataItem as Order).PaymentStatus %></td>  
                            <td>
                                <%# (Container.DataItem as Order).TrackingNumbers.ToDelimitedString("<br />") %>                                
                            </td>                      
                            <td>
                                <%# (Container.DataItem as Order).CustomerLastName %>, <%# (Container.DataItem as Order).CustomerFirstName %>
                                <br />
                                <%# (Container.DataItem as Order).CustomerEmail %>
                            </td>                        
                            <td class="{ sorter: false }"><a href="<%# StoreUrls.AdminDeleteOrder((Container.DataItem as Order).Id.Value) %>" onclick="return confirm('Are you sure you want to delete this order?')"><img src="<%= ModuleRootImagePath %>icons/delete.png" title="delete order" /></a></td>
                        </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>      
    </asp:Panel>  
    
</div>


<script type="text/javascript">
    var $orderCheckboxes = null;

    jQuery(function($) {

        $orderCheckboxes = jQuery('#ordersGrid :checkbox');

        jQuery(".dstore .datepick").datepicker({
            numberOfMonths: 3
        });

        jQuery('#checkAllOrders').click(function() {
            var $this = jQuery(this);
            if ($this.is(':checked')) {
                $orderCheckboxes.attr('checked', 'checked');
            }
            else {
                $orderCheckboxes.attr('checked', '');
            }
        });

        jQuery('#<%= txtCustomerFirstName.ClientID %>').watermark('First Name');
        jQuery('#<%= txtCustomerLastName.ClientID %>').watermark('Last Name');
        jQuery('#<%= txtCustomerEmail.ClientID %>').watermark('Email');

        jQuery('#ordersGrid').tablesorter({
            widgets: ['zebra']
        });

    });
</script>

