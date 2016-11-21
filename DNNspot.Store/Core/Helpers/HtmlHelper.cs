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
using System.Web;
using System.Web.UI.WebControls;
using WA.Extensions;

namespace DNNspot.Store
{
    public static class HtmlHelper
    {       
        public static List<ListItem> GetShippingOptionsAsListItems(List<ShippingOption> shippingOptions)
        {                        
            List<ListItem> options = new List<ListItem>();
            var store = StoreContext.GetCurrentStore(HttpContext.Current.Request);

            shippingOptions.ForEach(rt => options.Add(new ListItem()
                {
                    Value = rt.ListKey,
                    Text = rt.DisplayName + (rt.Cost.HasValue ? string.Format(@" ({0})", store.FormatCurrency(rt.Cost)) : "")
                }
            ));

            return options;
        }

        public static string ListItemsToOptionString(List<ListItem> listItems)
        {
            return ListItemsToOptionString(listItems, string.Empty);
        }

        public static string ListItemsToOptionString(List<ListItem> listItems, string selectedValue)
        {
            StringBuilder html = new StringBuilder();

            if(!string.IsNullOrEmpty(selectedValue))
            {
                listItems.ForEach(item => html.AppendFormat(@"<option value=""{0}""{2}>{1}</option>", item.Value, item.Text, (item.Value == selectedValue) ? " selected=\"selected\"" : ""));
            }
            else
            {
                listItems.ForEach(item => html.AppendFormat(@"<option value=""{0}"">{1}</option>", item.Value, item.Text));                
            }            

            return html.ToString();
        }

        public static string ConfirmDeleteImage(string urlForDeleteAction)
        {
            return ConfirmDeleteImage(urlForDeleteAction, "Are you sure you want to delete?");
        }

        public static string ConfirmDeleteImage(string urlForDeleteAction, string confirmMessage)
        {
            return string.Format(@"<a href=""{0}"" onclick=""return confirm('{1}');"" title=""delete""><img src=""{2}images/icons/delete.png"" alt=""delete"" /></a>", urlForDeleteAction, confirmMessage, StoreUrls.GetModuleFolderUrlRoot());
        }

        public static string AddressFieldsToHumanFriendlyString(
            string firstName,
            string lastName,
            string businessName,
            string address1,
            string address2,
            string city,
            string region,
            string postalCode,
            string country,
            string telephone,
            string email)
        {
            const string lineSeparator = "<br />";

            List<string> lines = new List<string>();

            bool firstNameEmpty = string.IsNullOrEmpty(firstName);
            bool lastNameEmpty = string.IsNullOrEmpty(lastName);
            if (!firstNameEmpty && !lastNameEmpty)
            {
                lines.Add(firstName + " " + lastName);
            }
            else if (!firstNameEmpty)
            {
                lines.Add(firstName);
            }
            else if (!lastNameEmpty)
            {
                lines.Add(lastName);
            }

            AddLineIfNotEmpty(businessName, ref lines);

            AddLineIfNotEmpty(address1, ref lines);
            AddLineIfNotEmpty(address2, ref lines);
            bool cityEmpty = string.IsNullOrEmpty(city);
            bool regionEmpty = string.IsNullOrEmpty(region);
            bool postalCodeEmpty = string.IsNullOrEmpty(postalCode);
            if (!cityEmpty && !regionEmpty && !postalCodeEmpty)
            {
                lines.Add(city + ", " + region + " " + postalCode);
            }
            else if (!cityEmpty && !regionEmpty)
            {
                lines.Add(city + ", " + region);
            }
            else if (!cityEmpty)
            {
                lines.Add(city);
            }
            else if (!regionEmpty)
            {
                lines.Add(region);
            }
            else if (!postalCodeEmpty)
            {
                lines.Add(postalCode);
            }
            AddLineIfNotEmpty(country, ref lines);

            AddLineIfNotEmpty(telephone, ref lines);
            AddLineIfNotEmpty(email, ref lines);

            return lines.ToDelimitedString(lineSeparator);            
        }

        private static void AddLineIfNotEmpty(string s, ref List<string> lines)
        {
            if (!string.IsNullOrEmpty(s))
            {
                lines.Add(s);
            }
        }
    }
}