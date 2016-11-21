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
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using WA;

namespace DNNspot.Store
{
    public partial class Admin : PortalModuleBase //StoreAdminModuleBase
    {
        protected ModuleDefs.Admin.Views currentView = ModuleDefs.Admin.Views.AdminHome;
        const string breadcrumbSeparator = "<span class='separator'>&raquo;</span>";

        /// <summary>
        /// Specify your "Views" here
        /// </summary>
        /// <returns></returns>
        private string GetCustomControlToLoad()
        {
            currentView = Enum<ModuleDefs.Admin.Views>.TryParseOrDefault(Request.QueryString["v"] ?? "", ModuleDefs.Admin.Views.AdminHome);

            //return string.Format(@"Modules\Admin\{0}.ascx", currentView);
            return string.Format(@"{0}.ascx", currentView);            
        }

        protected override void OnInit(System.EventArgs e)
        {            
            base.OnInit(e);

            if (!IsEditable)
            {
                ContainerControl.Visible = false;
                return;
            }

            string controlPath = GetCustomControlToLoad();
            StoreAdminModuleBase module = (StoreAdminModuleBase)LoadControl(controlPath);
            if (module != null)
            {
                // load the control into the placeholder
                module.ModuleConfiguration = ModuleConfiguration;
                module.ID = System.IO.Path.GetFileNameWithoutExtension(controlPath);

                plhUserControl.Controls.Add(module);

                List<AdminBreadcrumbLink> adminBreadcrumbs = module.GetBreadcrumbs();
                if (adminBreadcrumbs.Count > 0)
                {
                    StringBuilder breadcrumbs = new StringBuilder();
                    StoreContext sc = new StoreContext(Request);

                    StoreUrls urls = new StoreUrls(sc);
                    breadcrumbs.AppendFormat(@"<span><a href=""{0}"">Store Admin</a></span>", urls.Admin(DNNspot.Store.ModuleDefs.Admin.Views.AdminHome));
                    foreach (AdminBreadcrumbLink breadcrumbLink in adminBreadcrumbs)
                    {
                        breadcrumbs.Append(breadcrumbSeparator);
                        string crumb = string.IsNullOrEmpty(breadcrumbLink.Url) ? breadcrumbLink.Text : string.Format(@"<a href=""{0}"">{1}</a>", breadcrumbLink.Url, breadcrumbLink.Text);
                        breadcrumbs.AppendFormat(@"<span>{0}</span>", crumb);
                    }
                    litBreadcrumb.Text = breadcrumbs.ToString();
                }
                else
                {
                    litBreadcrumb.Text = @"<span style=""font-weight: bold; font-size: 14px;"">Store Administration and Management</span>";
                }

            }
        }
    }
}