<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Discounts.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.Discounts" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">

    <h1>Discounts</h1>
    
    <asp:Literal ID="flash" runat="server"></asp:Literal>
        
    <div class="adminToolbar">
        <a href="<%= StoreUrls.AdminAddDiscount() %>" class="adminIconBtn add positive">Add Discount</a>
    </div>    

    <table class="grid" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th style="width: 24px;"></th>
                <th>Name/Description</th>
                <th>Role</th>                
                <th style="text-align: right;">Discount Amount</th>
                <th style="text-align: center;">Applies To</th>                
                <th style="text-align: right;">Date(s)</th>
                <th style="text-align: center;">Active?</th>
                <th style="width: 24px;"></th>
            </tr>
        </thead>
        <tbody>
        
    <asp:Repeater ID="rptDiscounts" runat="server">
        <ItemTemplate>
                <tr <%# Container.ItemIndex % 2 == 1 ? @"class=""alt""" : "" %>>
                    <td style="width: 24px;"><a href="<%# StoreUrls.AdminEditDiscount((Container.DataItem as Discount).Id.Value) %>" title="edit"><img src="<%= ModuleRootImagePath %>icons/edit.png" alt="edit" title="edit" /></a></td>
                    <td><%# (Container.DataItem as Discount).Name %></td>
                    <td><%# (Container.DataItem as Discount).GetRoleName() %></td>
                    <td style="text-align: right;"><%# (Container.DataItem as Discount).IsPercentOff ? (Container.DataItem as Discount).PercentOff.Value.ToString("N2") + " %" : StoreContext.CurrentStore.FormatCurrency((Container.DataItem as Discount).AmountOff)%></td>                              
                    <td style="text-align: center;"><%# (Container.DataItem as Discount).DiscountTypeName %></td> 
                    <td style="text-align: right;"><%# (Container.DataItem as Discount).ValidDateDisplayString%></td>
                    <td style="text-align: center;"><%# (Container.DataItem as Discount).IsActive.Value.YesNoString()%></td>                                                                                  
                    <td style="width: 24px;"><a href="<%# StoreUrls.AdminDeleteDiscount((Container.DataItem as Discount).Id.Value) %>" title="delete" onclick="return confirm('are you sure you want to delete?');"><img src="<%= ModuleRootImagePath %>icons/delete.png" alt="delete" title="delete" /></a></td>
                </tr>
        </ItemTemplate>
    </asp:Repeater>
        </tbody>
    </table>
    
</div>