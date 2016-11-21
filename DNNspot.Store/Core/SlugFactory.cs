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
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using DNNspot.Store.DataModel;
using WA.Extensions;

namespace DNNspot.Store
{
    public static class SlugFactory
    {
        static Regex rxSpaces = new Regex(@"\s", RegexOptions.CultureInvariant);
        static Regex rxMultiHyphens = new Regex(@"-+", RegexOptions.CultureInvariant);
        static Regex rxNonSlug = new Regex(@"[^a-zA-Z0-9_-]", RegexOptions.CultureInvariant);

        static List<string> reservedSlugs = new List<string>
                                                {
                                                    "Cart", "cart",
                                                    "Checkout", "checkout",
                                                    "CheckoutBilling", "checkoutbilling",
                                                    "CheckoutShipping", "checkoutshipping",
                                                    "CheckoutPayment", "checkoutpayment",
                                                    "CheckoutReview", "checkoutreview",
                                                    "CheckoutComplete", "checkoutcomplete",
                                                    "LoginPrompt"
                                                };

        /// <summary>
        /// Can also be called as an Extension Method
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string CreateSlug(this string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                // trim and lowercase the input         
                string temp = source.Trim().ToLower();

                // replace spaces with hyphens
                temp = rxSpaces.Replace(temp, "-");

                // remove any non slug characters
                temp = rxNonSlug.Replace(temp, "");

                // replace multiple hyphens with single hyphen
                temp = rxMultiHyphens.Replace(temp, "-");

                temp = temp.TrimEnd('-');
                temp = temp.ChopAt(50);
                temp = temp.TrimEnd('-');

                return temp;
            }
            return "";
        }

        public static string CreateSpecialSlug(this string source, bool forceLowercase, int length)
        {
            if (!string.IsNullOrEmpty(source))
            {                
                string temp = source.Trim();

                if (forceLowercase)
                {
                    temp = temp.ToLower();
                }

                // replace spaces with hyphens
                temp = rxSpaces.Replace(temp, "-");

                // remove any non slug characters
                temp = rxNonSlug.Replace(temp, "");

                // replace multiple hyphens with single hyphen
                temp = rxMultiHyphens.Replace(temp, "-");

                temp = temp.TrimEnd('-');
                temp = temp.ChopAt(length);
                temp = temp.TrimEnd('-');

                return temp;
            }
            return "";
        }

        public static bool IsSlugAvailable(int storeId, string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return false;
            }

            if (Category.SlugExists(storeId, slug))
            {
                return false;
            }
            if (Product.SlugExists(storeId, slug))
            {
                return false;
            }
            if(reservedSlugs.Contains(slug))
            {
                return false;
            }

            return true;
        }
    }
}
