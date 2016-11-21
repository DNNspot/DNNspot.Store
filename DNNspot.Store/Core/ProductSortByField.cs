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
    public class ProductSortByField
    {
        public string Field { get; set; }
        public string DisplayName { get; set; }
        public SortDirection SortDirection { get; set; }

        public ProductSortByField()
        {
            this.Field = "";
            this.SortDirection = SortDirection.ASC;
        }

        public string GetValueString()
        {
            return string.Format(@"{0}-{1}", Field, SortDirection);
        }

        public override string ToString()
        {
            return GetValueString();
        }

        public static ProductSortByField FromString(string s)
        {
            if(!string.IsNullOrEmpty(s))
            {
                string[] parts = s.Split('-');
                if(parts.Length == 2)
                {
                    return new ProductSortByField() { Field = parts[0], SortDirection = WA.Enum<SortDirection>.TryParseOrDefault(parts[1], SortDirection.ASC) };
                }
            }
            return null;
        }
    }
}
