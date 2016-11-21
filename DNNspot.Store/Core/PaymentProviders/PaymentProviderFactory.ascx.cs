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

namespace DNNspot.Store.PaymentProviders
{
    internal static class PaymentProviderFactory
    {
        public static PaymentProvider GetProvider(PaymentProviderName providerName, DataModel.Store store)
        {
            switch(providerName)
            {
                case PaymentProviderName.PayLater:
                    return new PayLaterPaymentProvider(store.GetPaymentProviderConfig(providerName));

                case PaymentProviderName.CardCaptureOnly:
                    return new CardCaptureOnlyPaymentProvider(store.GetPaymentProviderConfig(providerName));

                case PaymentProviderName.PayPalStandard:
                    return new PayPalStandardProvider(store.GetPaymentProviderConfig(providerName));

                case PaymentProviderName.PayPalDirectPayment:
                    return new PayPalDirectPaymentProvider(store.GetPaymentProviderConfig(providerName));

                case PaymentProviderName.PayPalExpressCheckout:
                    return new PayPalExpressCheckoutPaymentProvider(store.GetPaymentProviderConfig(providerName));

                case PaymentProviderName.AuthorizeNetAim:
                    return new AuthorizeNetAimProvider(store.GetPaymentProviderConfig(providerName));

                default:                
                    throw new ArgumentException("PaymentProvider Not Found: " + providerName.ToString());
            }            
        }
    }
}