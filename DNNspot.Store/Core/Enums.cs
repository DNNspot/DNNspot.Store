/*
* This software is licensed under the GNU General Public License, version 2
* You may copy, distribute and modify the software as long as you track changes/dates of in source files and keep all modifications under GPL. You can distribute your application using a GPL library commercially, but you must also provide the source code.

* DNNspot Software (http://www.dnnspot.com)
* Copyright (C) 2013 Atriage Software LLC
* Authors: Kevin Southworth, Matthew Hall, Ryan Doom

* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; either version 2
* of the License, or (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program; if not, write to the Free Software
* Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

* Full license viewable here: http://www.gnu.org/licenses/gpl-2.0.txt
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DNNspot.Store
{
    public enum StoreSettingNames
    {
        OrderNumberPrefix,
        OrderCompletedEmailRecipient,
        DefaultCountryCode,
        CustomerServiceEmailAddress,
        ForceSslCheckout,
        TaxShipping,
        UrlToPostCompletedOrder,
        CheckoutAllowAnonymous,
        CheckoutShowCreateAccountLink,
        DisplaySiteCredit,
        StoreAddressStreet,
        StoreAddressCity,
        StoreAddressRegion,
        StoreAddressPostalCode,
        StoreAddressCountryCode,        
        StorePhoneNumber,
        CatalogDefaultSortOrder,
        CatalogMaxResultsPerPage,
        CurrencyCode,
        SalesTaxAddressType,
        AcceptedCreditCards,
        EnableCheckout,
        ShowCouponBox,
        ShowShippingEstimate,
        ShowPrices,
        RequireOrderNotes,
        ShipmentPackagingStrategy,
        IncludeJQueryUi,
        ShowPriceAndQuantityInCatalog,
        SendOrderReceivedEmail,
        SendPaymentCompleteEmail
    }

    public enum CouponStatus
    {
        Valid,
        NotFound,
        NotActive,
        AlreadyApplied,
        NotCombinable,
        NonCombinableCouponAlreadyInUse,
        NoEligibleProduct,
        NoEligibleShipping,
        ActiveDateInvalidFrom,
        ActiveDateInvalidTo,
        MinOrderAmountNotReached,
        ExceededMaxLifetimeRedemptions        
    }

    public enum CouponDiscountType
    {
        UNKNOWN,
        SubTotal,
        SubTotalAndShipping,
        Product,
        Shipping
    }

    public enum DiscountDiscountType
    {
        UNKNOWN,
        AllProducts,
        Product,
        Category
    }

    public enum ProductDeliveryMethod : short
    {
        UNKNOWN = -1,
        Shipped = 1,
        Downloaded = 2
    }

    public enum ProductFieldWidgetType
    {
        Textbox,
        Textarea,
        DropdownList,
        RadioButtonList,
        Checkbox,
        CheckboxList,        
    }

    public enum EmailTemplateNames
    {        
        OrderReceived,
        OrderReceivedAdmin,               
        PaymentCompleted,
        ShippingUpdate
    }

    public enum SortDirection
    {
        Unassigned, ASC, DESC
    }

    public enum OrderStatusName : short
    {
        PendingOffsite = 20,
        Processing = 30,
        Completed = 1,        
        Deleted = 98,
        Failed = 99
    }    

    public enum PaymentStatusName : short
    {
        Completed = 1,
        Pending = 20,
        Denied = 30,
        ProviderError = 99
    }

    public enum PaymentProviderName : short
    {
        UNKNOWN = 0,
        CardCaptureOnly = 1,
        PayLater = 2,
        PayPalStandard = 20,
        PayPalDirectPayment = 21,
        PayPalExpressCheckout = 22,
        AuthorizeNetAim = 30,
        None = 99
    }

    public enum ShippingProviderType : short
    {
        UNKNOWN = 0,
        CustomShipping = 1,
        FedEx = 20,
        UPS = 30
    }

    public enum ShipmentPackagingStrategy
    {
        SingleBox,
        BoxPerProductType,
        BoxPerItem
    }
}
