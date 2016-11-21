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
    public class CsvProductInfo
    {
        // StoreId not needed since we can determine it at import-time

        public string ImportAction { get; set; }

        public int? ProductId { get; set; }

        public string Name { get; set; }

        public string Sku { get; set; }

        public string UrlName { get; set; }       

        [TypeConverter(typeof(MyDecimalTypeConverter))]
        public decimal? Price { get; set; }

        [TypeConverter(typeof(MyDecimalTypeConverter))]
        public decimal? Weight { get; set; }

        public string SeoTitle { get; set; }

        public string SeoDescription { get; set; }

        public string SeoKeywords { get; set; }

        [TypeConverter(typeof(MyBooleanTypeConverter))]
        public bool? IsActive { get; set; }

        [TypeConverter(typeof(MyBooleanTypeConverter))]
        public bool? EnableInventoryManagement { get; set; }

        public int? StockLevel { get; set; }

        [TypeConverter(typeof(MyBooleanTypeConverter))]
        public bool? AllowNegativeStock { get; set; }

        public string CategoryNames { get; set; }

        public string PhotoFilenames { get; set; }

        public string DigitalFilename { get; set; }

        [TypeConverter(typeof(MyBooleanTypeConverter))]
        public bool? TaxableItem { get; set; }

        [TypeConverter(typeof(MyBooleanTypeConverter))]
        public bool? ShowPrice { get; set; }

        [TypeConverter(typeof(MyBooleanTypeConverter))]
        public bool? AvailableForPurchase { get; set; }

        public string Desc1Name { get; set; }
        public string Desc1Html { get; set; }

        public string Desc2Name { get; set; }
        public string Desc2Html { get; set; }

        public string Desc3Name { get; set; }
        public string Desc3Html { get; set; }

        public string Desc4Name { get; set; }
        public string Desc4Html { get; set; }

        public string Desc5Name { get; set; }
        public string Desc5Html { get; set; }
    }

    public class MyBooleanTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return WA.Parser.ToBool(value);
        }
    }

    public class MyDecimalTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return WA.Parser.ToDecimal(value);
        }
    }
}
