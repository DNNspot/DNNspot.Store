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

namespace DNNspot.Store
{
    public class JsonPhoto
    {
        public int Id;
        public string OriginalUrl;
        public string ThumbnailUrl;
    }

    public class JsonProductField
    {
        public int Id;
        public string WidgetType = "";
        public string Name = "";
        public string Slug = "";
        public bool IsRequired;
        public decimal PriceAdjustment;
        public decimal WeightAdjustment;
        public short SortOrder;

        public List<JsonProductFieldChoice> Choices;

        public JsonProductField()
        {
            Choices = new List<JsonProductFieldChoice>();
        }
    }

    public class JsonProductFieldChoice
    {
        public int Id;
        public int ProductFieldId;
        public string Name;
        public string Value;
        public decimal PriceAdjustment;
        public decimal WeightAdjustment;
        public short SortOrder;        
    }

    public class JsonProductFieldData
    {
        public int ProductFieldId;
        public string Name;        
        public string Slug;
        public decimal PriceAdjustment;
        public decimal WeightAdjustment;
        
        public List<string> ChoiceValues;
    }

    public class JsonOrderItem
    {
        public int Id;
        public int OrderId;
        public int? ProductId;
        public string Name;
        public string Sku;
        public int Quantity;
        public List<JsonProductFieldData> ProductFieldData;
        public string DigitalFileDisplayName;
        public string DigitalFilename;
        public decimal WeightTotal;
        public decimal PriceTotal;
    }

    public class JsonShippingServiceRate
    {
        public int RateTypeId;
        public string CountryCode = "";
        public string Region = "";        
        public decimal? MinWeight = null;
        public decimal? MaxWeight = null;
        public decimal? Cost = null;

        public string CountryRegionValue
        {
            get { return CountryCode + (!string.IsNullOrEmpty(Region) ? "|" + Region : ""); }
        }

        public string CountryRegionName
        {
            get { return CountryCode + (!string.IsNullOrEmpty(Region) ? " | " + Region : ""); }
        }
    }
}