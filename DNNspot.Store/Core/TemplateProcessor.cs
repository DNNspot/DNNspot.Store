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
using System.Text.RegularExpressions;
using System.Web;

namespace DNNspot.Store
{
    /// <summary>
    /// Processes Templates containing tokens of the forms:
    /// [Product:Name]
    /// [Product:Price]
    /// [Product:Photo{Width=150}]
    /// [Product:Photo{Width=150,Height=50}]
    /// </summary>
    public class TemplateProcessor
    {
        ITokenValueProvider valueProvider;

        public TemplateProcessor(ITokenValueProvider tokenValueProvider)
        {
            this.valueProvider = tokenValueProvider;
        }

        public string ProcessTemplate(string template)
        {
            string output = string.Empty;
            try
            {

                output = rxToken.Replace(template, delegate(Match match)
                {
                    string token = match.Groups["token"].Value;
                    string property = match.Groups["property"].Value;
                    string attribString = match.Groups["attributes"].Value ?? string.Empty;
                    Dictionary<string, string> dict = null;
                    if (!string.IsNullOrEmpty(attribString))
                    {
                        dict = new Dictionary<string, string>();
                        string[] attribs = attribString.Split(',');
                        foreach (var a in attribs)
                        {
                            string[] pair = a.Split('=');
                            if (pair.Length == 2)
                            {
                                dict[pair[0]] = pair[1];
                            }
                        }
                    }

                    return valueProvider.GetTokenValue(token, property, dict);
                });
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return output;
        }

        /// <summary>
        ///  Regular expression built for C# on: Mon, Sep 13, 2010, 03:02:06 PM
        ///  Using Expresso Version: 3.0.3634, http://www.ultrapico.com
        ///  
        ///  A description of the regular expression:
        ///  
        ///  Select from 2 alternatives
        ///      \[(?<token>[\w/]+):(?<property>\w+)\]
        ///          Literal [
        ///          [token]: A named capture group. [[\w/]+]
        ///              Any character in this class: [\w/], one or more repetitions
        ///          :
        ///          [property]: A named capture group. [\w+]
        ///              Alphanumeric, one or more repetitions
        ///          Literal ]
        ///      \[(?<token>\w+):(?<property>\w+){(?<attributes>.+)}\]
        ///          Literal [
        ///          [token]: A named capture group. [\w+]
        ///              Alphanumeric, one or more repetitions
        ///          :
        ///          [property]: A named capture group. [\w+]
        ///              Alphanumeric, one or more repetitions
        ///          {
        ///          [attributes]: A named capture group. [.+]
        ///              Any character, one or more repetitions
        ///          }
        ///          Literal ]
        ///  
        ///
        /// </summary>
        Regex rxToken = new Regex(@"\[(?<token>[\w/]+):(?<property>\w+)\]|\[(?<token>\w+):(?<property>\w+){(?<attributes>.+?)}\]",
            RegexOptions.CultureInvariant
            //| RegexOptions.Compiled
            );
    }

    public interface ITokenValueProvider
    {
        string GetTokenValue(string token, string property, Dictionary<string, string> attributes);
    }
}