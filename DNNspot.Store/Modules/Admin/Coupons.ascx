<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Coupons.ascx.cs" Inherits="DNNspot.Store.Modules.Admin.Coupons" %>

<div class="dstore admin<%= this.GetType().BaseType.Name %>">

    <h1>Coupons</h1>
    
    <asp:Literal ID="flash" runat="server"></asp:Literal>
        
    <div class="adminToolbar">
        <a href="<%= StoreUrls.AdminAddCoupon() %>" class="adminIconBtn add positive">Add Coupon</a>
    </div>    

    <table class="grid" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th style="width: 24px;"></th>
                <th>Coupon</th>                
                <th style="text-align: center;">Applies To</th>
                <th style="text-align: right;">Discount</th>
                <th style="text-align: right;">Date(s)</th>
                <th style="text-align: center;">Active?</th>
                <th style="width: 24px;"></th>
            </tr>
        </thead>
        <tbody>
        
    <asp:Repeater ID="rptCoupons" runat="server">
        <ItemTemplate>
                <tr <%# Container.ItemIndex % 2 == 1 ? @"class=""alt""" : "" %>>
                    <td style="width: 24px;"><a href="<%# StoreUrls.AdminEditCoupon((Container.DataItem as Coupon).Id.Value) %>" title="edit"><img src="<%= ModuleRootImagePath %>icons/edit.png" alt="edit" title="edit" /></a></td>
                    <td>
                        <span style="font-weight: bold;"><%# (Container.DataItem as Coupon).Code %></span>
                        <div style="padding-left: 2em;">
                            <%# (Container.DataItem as Coupon).DescriptionForCustomer %>
                        </div>
                    </td>        
                    <td style="text-align: center;"><%# (Container.DataItem as Coupon).DiscountTypeName %></td>   
                    <td style="text-align: right;"><%# (Container.DataItem as Coupon).IsPercentOff != null && (Container.DataItem as Coupon).IsPercentOff ? (Container.DataItem as Coupon).PercentOff.Value.ToString("N2") + " %" : StoreContext.CurrentStore.FormatCurrency((Container.DataItem as Coupon).AmountOff)%></td>       
                    <td style="text-align: right;"><%# (Container.DataItem as Coupon).ValidDateDisplayString %></td>
                    <td style="text-align: center;">
                        <%# (Container.DataItem as Coupon).IsActive.Value.YesNoString() %>
                    </td>                                        
                    <td style="width: 24px;"><a href="<%# StoreUrls.AdminDeleteCoupon((Container.DataItem as Coupon).Id.Value) %>" title="delete" onclick="return confirm('are you sure you want to delete?');"><img src="<%= ModuleRootImagePath %>icons/delete.png" alt="delete" title="delete" /></a></td>
                </tr>
        </ItemTemplate>
    </asp:Repeater>
        </tbody>
    </table>
    
</div>