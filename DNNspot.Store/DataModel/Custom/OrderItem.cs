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

/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2012.1.0930.0
EntitySpaces Driver  : SQL
Date Generated       : 4/12/2013 3:32:33 PM
===============================================================================
*/

using System;
using System.Collections.Generic;
using System.Text;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;
using WA.Extensions;

namespace DNNspot.Store.DataModel
{
	public partial class OrderItem : esOrderItem
	{
		public OrderItem()
		{

        }

        public bool HasDigitalFile
        {
            get { return !string.IsNullOrEmpty(this.DigitalFilename); }
        }

        public string DigitalFileExtension
        {
            get { return HasDigitalFile ? System.IO.Path.GetExtension(this.DigitalFilename).ToLower() : ""; }
        }

        public decimal PriceForSingleItem
        {
            get { return this.PriceTotal.GetValueOrDefault(0) / this.Quantity.GetValueOrDefault(1); }
        }

        public List<JsonProductFieldData> GetProductFieldData()
        {
            if (!string.IsNullOrEmpty(this.ProductFieldData))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<JsonProductFieldData>>(this.ProductFieldData);
            }
            return new List<JsonProductFieldData>();
        }

        public string GetProductFieldDataPlainTextDisplayString()
        {
            List<JsonProductFieldData> fieldData = this.GetProductFieldData();
            if (fieldData.Count > 0)
            {
                StringBuilder text = new StringBuilder();
                fieldData.ForEach(f => text.AppendFormat(@"{0} : {1} | ", f.Name, f.ChoiceValues.ToDelimitedString(", ")));

                return text.ToString().TrimEnd(" | ");
            }
            return "";
        }

        public string GetProductFieldDataHtmlDisplayString()
        {
            List<JsonProductFieldData> fieldData = this.GetProductFieldData();
            if (fieldData.Count > 0)
            {
                StringBuilder html = new StringBuilder("<span class=\"productFields\">");
                foreach (JsonProductFieldData field in fieldData)
                {
                    html.AppendFormat(@"<span>{0}: {1}</span>", field.Name, field.ChoiceValues.ToDelimitedString(", "));
                }
                html.Append("</span>");
                return html.ToString();
            }
            return "";
        }
	}
}
