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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DNNspot.Store
{
    public class StoreAdminModuleBase : StoreModuleBase
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //--- Include jQuery plugins
            RegisterJavascriptFileOnceInBody("js/jquery.metadata.js", ModuleRootWebPath + "js/jquery.metadata.js");
            RegisterJavascriptFileOnceInBody("js/jquery.maxlength.js", ModuleRootWebPath + "js/jquery.maxlength.js");
            RegisterJavascriptFileOnceInBody("js/jquery.maskedinput.js", ModuleRootWebPath + "js/jquery.maskedinput.js");
            RegisterJavascriptFileOnceInBody("js/jquery.validate.min.js", ModuleRootWebPath + "js/jquery.validate.min.js");

            //--- jQuery UI
            //RegisterJavascriptFileInHeader(ModuleRootWebPath + "js/jquery-ui-1.7.2.custom.min.js");
            bool includeJqueryUI = WA.Parser.ToBool(DataModel.Store.GetStoreByPortalId(PortalId).GetSetting(StoreSettingNames.IncludeJQueryUi)).GetValueOrDefault(true);
            if (includeJqueryUI && DnnVersionSingleton.Instance.IsDnn5)
            {
                RegisterJavascriptFileOnceInBody("js/jquery-ui-1.9.1.custom.min.js", ModuleRootWebPath + "js/jquery-ui-1.9.1.custom.min.js");
            }
            RegisterCssFileOnceInHeader(ModuleRootWebPath + "css/jquery.ui.css", string.Empty);

            RegisterJavascriptFileOnceInBody("js/jquery.fancybox-1.3.1.min.js", ModuleRootWebPath + "js/jquery.fancybox-1.3.1.min.js");
            RegisterJavascriptFileOnceInBody("js/jquery.tablesorter.min.js", ModuleRootWebPath + "js/jquery.tablesorter.min.js");
            RegisterJavascriptFileOnceInBody("js/jquery.blockUI.js", ModuleRootWebPath + "js/jquery.blockUI.js");            

            //--- jQuery iButton (fancy iphone-like buttons)
            RegisterJavascriptFileOnceInBody("js/jquery.ibutton.min.js", ModuleRootWebPath + "js/jquery.ibutton.min.js");
            RegisterCssFileOnceInHeader(ModuleRootWebPath + "css/jquery.ibutton.css", string.Empty);
        }

        public virtual List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>();
        }
    }

    public class AdminBreadcrumbLink
    {
        public string Url = "";
        public string Text = "";
    }
}