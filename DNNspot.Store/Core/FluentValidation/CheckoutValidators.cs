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
using System.Linq;
using System.Web;
using WA.Extensions;
using DNNspot.Store.DataModel;
using FluentValidation;

namespace DNNspot.Store
{
    /// <summary>
    /// Validation for Orders being submitted for checkout/completion
    /// </summary>
    public class CheckoutOrderValidator : AbstractValidator<CheckoutOrderInfo>
    {
        public CheckoutOrderValidator()
        {
            RuleFor(o => o.BillingAddress).NotNull();
            RuleFor(o => o.BillingAddress.FirstName).NotEmpty();
            RuleFor(o => o.BillingAddress.LastName).NotEmpty();
            RuleFor(o => o.BillingAddress.Email).NotEmpty();
            RuleFor(o => o.BillingAddress.Address1).NotEmpty();
            RuleFor(o => o.BillingAddress.City).NotEmpty();
            RuleFor(o => o.BillingAddress.Region).NotEmpty();
            RuleFor(o => o.BillingAddress.PostalCode).NotEmpty();
            RuleFor(o => o.BillingAddress.Country).NotEmpty();

            RuleFor(o => o.ShippingAddress).NotNull().When(o => !o.HasOnlyDownloadableProducts);
            RuleFor(o => o.ShippingAddress.Address1).NotEmpty().When(o => !o.HasOnlyDownloadableProducts);
            RuleFor(o => o.ShippingAddress.City).NotEmpty().When(o => !o.HasOnlyDownloadableProducts);
            RuleFor(o => o.ShippingAddress.Region).NotEmpty().When(o => !o.HasOnlyDownloadableProducts);
            RuleFor(o => o.ShippingAddress.PostalCode).NotEmpty().When(o => !o.HasOnlyDownloadableProducts);
            RuleFor(o => o.ShippingAddress.Country).NotEmpty().When(o => !o.HasOnlyDownloadableProducts);

            RuleFor(o => o.ShippingRate).NotNull().WithMessage("Please choose a shipping option").When(o => !o.HasOnlyDownloadableProducts);
            RuleFor(o => o.ShippingProvider).NotEqual(ShippingProviderType.UNKNOWN).WithMessage("Unknown Shipping Provider").When(o => !o.HasOnlyDownloadableProducts);

            RuleFor(o => o.PaymentProvider).NotEqual(PaymentProviderName.UNKNOWN).WithMessage("Unknown Payment Processor");

            RuleFor(o => o.Cart).NotNull().WithMessage("Cart not found");
            RuleFor(o => o.Cart).SetValidator(new CheckoutCartValidator());
        }
    }

    public class CheckoutCartValidator : AbstractValidator<DataModel.Cart>
    {
        public CheckoutCartValidator()
        {
            RuleFor(c => c.CartItemCollectionByCartId.Count).GreaterThan(0).WithMessage("Cannot checkout with an empty cart");

            RuleFor(c => c.CartItemCollectionByCartId).SetValidator(new CheckoutCartItemValidator());
        }
    }

    public class CheckoutCartItemValidator : AbstractValidator<DataModel.CartItem>
    {
        public CheckoutCartItemValidator()
        {
            RuleFor(ci => ci.UpToProductByProductId).NotNull().WithMessage("Product not found or is no longer available");
            RuleFor(ci => ci.UpToProductByProductId.IsInStock).Equal(true).WithMessage(@"""{0}"" is out of stock", ci => ci.UpToProductByProductId.Name);

            RuleFor(ci => ci.Quantity).Must((ci, qty) => (qty <= ci.UpToProductByProductId.InventoryQtyInStock)).When(ci => ci.UpToProductByProductId.InventoryIsEnabled == true).WithMessage(@"""{0}"" is out of stock", ci => ci.UpToProductByProductId.Name);
        }
    }
}