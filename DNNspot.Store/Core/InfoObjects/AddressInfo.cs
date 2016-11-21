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
using DNNspot.Store.Shipping;
using WA.Extensions;

namespace DNNspot.Store
{
    public class AddressInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool EnablePropertyChangeEvent { get; set; }        

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string BusinessName { get; set; }

        private bool isResidential = true;
        public bool IsResidential
        {
            get { return isResidential; }
            set { isResidential = value; OnPropertyChanged("IsResidential"); }
        }

        private string address1;
        public string Address1
        {
            get { return address1; }
            set { address1 = value; OnPropertyChanged("Address1"); }
        }

        private string address2;
        public string Address2
        {
            get { return address2; }
            set { address2 = value; OnPropertyChanged("Address2"); }
        }

        private string city;
        public string City
        {
            get { return city; }
            set { city = value; OnPropertyChanged("City"); }
        }

        private string region;
        public string Region
        {
            get { return region; }
            set { region = value; OnPropertyChanged("Region"); }
        }

        private string postalCode;
        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; OnPropertyChanged("PostalCode"); }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set { country = value; OnPropertyChanged("Country"); }
        }

        public bool NameFieldsAreEmpty
        {
            get
            {                
                if (!string.IsNullOrEmpty(FirstName)) return false;

                if (!string.IsNullOrEmpty(LastName)) return false;

                return true;
            } 
        }

        public bool AddressFieldsAreEmpty
        {
            get
            {
                if (!string.IsNullOrEmpty(Address1)) return false;
                if (!string.IsNullOrEmpty(Address2)) return false;
                if (!string.IsNullOrEmpty(City)) return false;
                if (!string.IsNullOrEmpty(Region)) return false;
                if (!string.IsNullOrEmpty(PostalCode)) return false;
                if (!string.IsNullOrEmpty(Country)) return false;

                return true;
            }
        }

        public AddressInfo()
        {
            EnablePropertyChangeEvent = false;

            IsResidential = true;

            FirstName = "";
            LastName = "";
            Email = "";
            BusinessName = "";
            Telephone = "";
            Address1 = "";
            Address2 = "";
            City = "";
            Region = "";
            PostalCode = "";
            Country = "";
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if(EnablePropertyChangeEvent && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }        

        public string ToHumanFriendlyString(string lineSeparator)
        {
            return ToHumanFriendlyString(lineSeparator, true, true);
        }

        public string ToHumanFriendlyString(string lineSeparator, bool includeTelephone, bool includeEmail)
        {
            List<string> lines = new List<string>();

            AddLineIfNotEmpty(BusinessName, ref lines);

            bool firstNameEmpty = string.IsNullOrEmpty(FirstName);
            bool lastNameEmpty = string.IsNullOrEmpty(LastName);
            if(!firstNameEmpty && !lastNameEmpty)
            {
                lines.Add(FirstName + " " + LastName);
            }
            else if(!firstNameEmpty)
            {
                lines.Add(FirstName);
            }
            else if (!lastNameEmpty)
            {
                lines.Add(LastName);
            }            

            AddLineIfNotEmpty(Address1, ref lines);
            AddLineIfNotEmpty(Address2, ref lines);
            bool cityEmpty = string.IsNullOrEmpty(City);
            bool regionEmpty = string.IsNullOrEmpty(Region);
            bool postalCodeEmpty = string.IsNullOrEmpty(PostalCode);
            if(!cityEmpty && !regionEmpty && !postalCodeEmpty)
            {
                lines.Add(City + ", " + Region + " " + PostalCode);
            }
            else if (!cityEmpty && !regionEmpty)
            {
                lines.Add(City + ", " + Region);
            }
            else if(!cityEmpty)
            {
                lines.Add(City);
            }
            else if(!regionEmpty)
            {
                lines.Add(Region);
            }
            else if(!postalCodeEmpty)
            {
                lines.Add(PostalCode);
            }
            AddLineIfNotEmpty(Country, ref lines);

            if(includeTelephone)
                AddLineIfNotEmpty(Telephone, ref lines);
            if (includeEmail)
                AddLineIfNotEmpty(Email, ref lines);

            return lines.ToDelimitedString(lineSeparator);
        }

        private void AddLineIfNotEmpty(string s, ref List<string> lines)
        {
            if(!string.IsNullOrEmpty(s))
            {
                lines.Add(s);
            }
        }

        public IPostalAddress ToPostalAddress()
        {
            return new PostalAddress()
                       {
                           IsResidential = this.IsResidential,
                           Address1 = this.Address1,
                           Address2 =  this.Address2,
                           City = this.City,
                           Region = this.Region,
                           PostalCode = this.PostalCode,
                           CountryCode = this.Country
                       };
        }
    }
}
