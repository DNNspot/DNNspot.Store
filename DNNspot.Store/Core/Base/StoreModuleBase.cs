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
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Log.EventLog;
using EntitySpaces.Interfaces;
using WA;
using WA.Extensions;
using DNNspot.Store.DataModel;

namespace DNNspot.Store
{
    public class StoreModuleBase : PortalModuleBase
    {
        private StoreUrls storeUrls;
        private StoreContext storeContext;
        readonly EventLogController eventLog = new EventLogController();
        protected string GlobalResourceFile;

        protected StoreUrls StoreUrls
        {
            get { return storeUrls; }
        }

        protected internal StoreContext StoreContext
        {
            get { return storeContext; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            DataModel.DataModel.Initialize();

            //GlobalResourceFile = ModuleRootWebPath + "App_GlobalResources/Global.resx";
            GlobalResourceFile = string.Format("/DesktopModules/{0}/App_GlobalResources/Global.resx", ModuleConfiguration.FolderName);

            storeContext = new StoreContext(Request);
            storeUrls = new StoreUrls(storeContext);

            RegisterJQuery();

            CheckDnnSiteUrlsConfig();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // include any needed javascript files here...   
            RegisterJavascriptFileOnceInBody("js/jquery.watermark.min.js", ModuleRootWebPath + "js/jquery.watermark.min.js");

            IncludeConditionalCss();

            string str = base.Request.Params["flash"] ?? "";
            if (!string.IsNullOrEmpty(str))
            {
                ShowFlash(str);
            }

        }

        public static List<int> GetCurrentUserRoleIds()
        {
            UserInfo userInfo = UserController.GetCurrentUserInfo();

            RoleController roleController = new RoleController();
            List<RoleInfo> userRoles = roleController.GetUserRoles(userInfo.PortalID, userInfo.UserID).ToList<RoleInfo>();

            return userRoles.ConvertAll(r => r.RoleID);
        }

        private void IncludeConditionalCss()
        {
            string browser = Request.Browser.Browser;
            string browserVersion = Request.Browser.Version;
            string browserPlatform = Request.Browser.Platform;

            if (browser == "IE")
            {
                string filename = "";

                if (browserVersion.StartsWith("6"))
                    filename = "ie6.css";
                else if (browserVersion.StartsWith("7"))
                    filename = "ie7.css";
                else if (browserVersion.StartsWith("8"))
                    filename = "ie8.css";

                if (!string.IsNullOrEmpty(filename))
                {
                    RegisterCssFileOnceInHeader(ModuleRootWebPath + filename, "");
                }
            }
        }

        private void RegisterJQuery()
        {
            // DNN 5 only :(
            DotNetNuke.Framework.jQuery.RequestRegistration();                        

            // we need jQuery 1.4+ for our jQuery UI stuff to work, DNN 4 only bundles 1.2.6 and DNN 5.x only bundles 1.3.2, so we'll include our own            
            //if (HttpContext.Current.Items["jquery_registered"] == null || HttpContext.Current.Items["jquery14_registered"] == null)
            //{
            //    string pathToJquery = storeUrls.ModuleFolderUrlRoot + "js/jquery.min.js";

            //    if (DnnVersionSingleton.Instance.IsDnn5)
            //    {
            //        // NOTE - this won't execute when run against the DotNetNuke 4 dll :(
            //        //if (DotNetNuke.Framework.jQuery.IsInstalled && DotNetNuke.Framework.jQuery.IsRequested)
            //        //{
            //        //    string[] versionParts = DotNetNuke.Framework.jQuery.Version.Split('.');
            //        //    if (Convert.ToInt32(versionParts[1]) >= 4)
            //        //    {
            //        //        // the bundled DNN jQuery will suffice!
            //        //        return;
            //        //    }
            //        //}
            //    }

            //    if (!string.IsNullOrEmpty(pathToJquery))
            //    {
            //        RegisterJavascriptFileInHeader(pathToJquery);

            //        HttpContext.Current.Items.Add("jquery_registered", "true");
            //        HttpContext.Current.Items.Add("jquery14_registered", "true");
            //    }
            //}



            //DotNetNuke.Framework.jQuery.Version
        }

        protected string BrowserName
        {
            get { return Request.Browser.Browser; }
        }

        protected string BrowserVersion
        {
            get { return Request.Browser.Version; }
        }

        protected new bool IsEditable
        {
            get
            {
                return base.IsEditable || UserInfo.IsSuperUser || UserInfo.IsInRole(PortalSettings.AdministratorRoleName);
            }
        }

        /// <summary>
        /// The DotNetNuke "Page" object. Useful for setting the Page Title dynamically. 
        /// </summary>
        protected DotNetNuke.Framework.CDefault DnnPage
        {
            get { return (DotNetNuke.Framework.CDefault)Page; }
        }

        public string ModuleName
        {
            get
            {
                // DNN 5 only
                //return ModuleConfiguration.DesktopModule.ModuleName;

                return ModuleConfiguration.ModuleName;
            }
        }

        protected string ModuleRootWebPath
        {
            get
            {
                // DNN 5 only
                //return string.Format("/DesktopModules/{0}/", ModuleConfiguration.DesktopModule.FolderName);

                //return string.Format("/DesktopModules/{0}/", ModuleConfiguration.FolderName);

                return (storeUrls != null) ? storeUrls.ModuleFolderUrlRoot : StoreUrls.GetModuleFolderUrlRoot();
            }
        }

        protected string ModuleRootImagePath
        {
            get { return ModuleRootWebPath + "images/"; }
        }


        //protected string ModuleWebPath
        //{
        //    get
        //    {
        //        // DNN 5 only
        //        //return ControlPath.EnsureEndsWith("/");

        //        return ControlPath;
        //    }
        //}

        //protected string ModuleFilePath
        //{
        //    get
        //    {
        //        string basePhysicalPath = GetBasePhysicalPathFromRequest();
        //        string controlPath = ControlPath.Replace("/", Path.DirectorySeparatorChar.ToString()).TrimStart(Path.DirectorySeparatorChar).EnsureEndsWith(Path.DirectorySeparatorChar.ToString());

        //        return string.Format("{0}{1}", basePhysicalPath, controlPath);
        //    }
        //}

        //protected string ModuleRootFilePath
        //{
        //    get
        //    {
        //        string basePhysicalPath = GetBasePhysicalPathFromRequest().EnsureEndsWith(Path.DirectorySeparatorChar.ToString());
        //        // DNN 5 only
        //        //string moduleRootPath = string.Format(@"DesktopModules{0}{1}{0}", Path.DirectorySeparatorChar, ModuleConfiguration.DesktopModule.FolderName);
        //        string moduleRootPath = string.Format(@"DesktopModules{0}{1}{0}", Path.DirectorySeparatorChar, ModuleConfiguration.FolderName);

        //        return basePhysicalPath + moduleRootPath;
        //    }
        //}

        //protected new string ControlPath
        //{
        //    get { return this.TemplateSourceDirectory.EnsureEndsWith("/"); }
        //}

        /// <summary>
        /// Check if SiteUrls.config has been updated
        /// </summary>
        private void CheckDnnSiteUrlsConfig()
        {
            string cacheKeySiteUrls = StoreContext.CacheKeys.FriendlyStoreUrlsEnabled;

            //Debug.Write("Checking for DNN Site Urls Config...");

            bool? urlsEnabledFromCache = WA.Parser.ToBool(DataCache.GetCache(cacheKeySiteUrls));
            if (!urlsEnabledFromCache.GetValueOrDefault(false))
            {
                //Debug.Write("DNN Site Urls Config SHOULD be updated...");
                // cache has expired, or urls are not enabled
                // let's try to enabled them
                if (StoreContext.UpdateDnnHostSiteUrlConfig())
                {
                    //Debug.Write("DNN Site Urls Config was updated successfully.");
                    // set the cache so we don't check again for a while
                    DataCache.SetCache(cacheKeySiteUrls, "true", TimeSpan.FromHours(23));
                }
                else
                {
                    //Debug.Write("ERROR - Unable to update DNN Site Urls Config!");
                }
            }
            else
            {
                //Debug.Write("DNN Site Urls Config has already been updated");
            }
        }

        private string GetBasePhysicalPathFromRequest()
        {
            return Request.PhysicalApplicationPath.EnsureEndsWith(Path.DirectorySeparatorChar.ToString());
        }

        /// <summary>
        /// Shows a flash message on the screen.
        /// A Literal control with id "flash" must exist for the message to be displayed.
        /// </summary>
        /// <param name="msg"></param>        
        protected void ShowFlash(string msg)
        {
            const string flashControlId = "flash";

            if (!string.IsNullOrEmpty(msg))
            {
                string flashHtml = string.Format(@"<div class=""flash"">{0}</div>", HttpUtility.UrlDecode(msg));
                foreach (Control c in Controls)
                {
                    if (c.ID == flashControlId || c.ID == flashControlId + "2")
                    {
                        Literal flashControl = c as Literal;
                        if (flashControl != null)
                        {
                            flashControl.Text = flashHtml;
                            flashControl.Visible = true;

                            //const string jQueryFadeOut = "jQuery(function($) { setTimeout( function() { jQuery('.admin div.flash').fadeTo('slow', 0) }, 7000 ); });";
                            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), ModuleId + "-flash", jQueryFadeOut, true);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Registers a Javascript <script src=""> block inside the body tag.
        /// Multiple calls with the same keyName will only register the script once.
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="pathToJsFile"></param>
        protected void RegisterJavascriptFileOnceInBody(string keyName, string pathToJsFile)
        {
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(keyName))
            {
                Page.ClientScript.RegisterClientScriptInclude(keyName, pathToJsFile);
            }
        }

        /// <summary>
        /// Registers a JS file in the page <head> tag (does NOT check if it's already registered)        
        /// </summary>
        /// <param name="pathToJsFile"></param>
        protected void RegisterJavascriptFileInHeader(string pathToJsFile)
        {
            if (DnnVersionSingleton.Instance.IsDnn6 && DnnVersionSingleton.Instance.DnnVersion.Minor > 0)
            {
                return;
            }
            else
            {
                HtmlGenericControl script = new HtmlGenericControl("script");
                script.Attributes.Add("type", "text/javascript");
                script.Attributes.Add("src", pathToJsFile);
                Page.Header.Controls.Add(script);
            }
            //string controlId = "DNNspot_Store_js_" + Path.GetFileNameWithoutExtension(pathToJsFile).Replace(".", "_");            
            //System.Web.UI.Control c = Page.Header.FindControl(controlId);
            //if (c == null)
            //{
            //    HtmlGenericControl script = new HtmlGenericControl("script");
            //    script.ID = controlId;
            //    script.Attributes.Add("type", "text/javascript");
            //    script.Attributes.Add("src", pathToJsFile);
            //    Page.Header.Controls.Add(script);                
            //}
        }

        protected void RegisterCssFileInHeader(string fullPath)
        {
            RegisterCssFileInHeader(fullPath, "");
        }

        protected void RegisterCssFileInHeader(string fullPath, string mediaType)
        {
            HtmlGenericControl cssLink = new HtmlGenericControl("link");
            cssLink.ID = "DNNspot_Store_css_" + Path.GetFileNameWithoutExtension(fullPath);
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");
            if (!string.IsNullOrEmpty(mediaType))
            {
                cssLink.Attributes.Add("media", mediaType);
            }
            cssLink.Attributes.Add("href", fullPath);

            Page.Header.Controls.Add(cssLink);
        }

        protected void RegisterCssFileOnceInHeader(string fullPath, string mediaType)
        {
            System.Web.UI.Control c = Page.Header.FindControl("DNNspot_Store_css_" + Path.GetFileNameWithoutExtension(fullPath));
            if (c == null)
            {
                RegisterCssFileInHeader(fullPath, mediaType);
            }
        }

        protected void LogToDnnEventLog(string msg)
        {
            LogToDnnEventLog(msg, EventLogController.EventLogType.ADMIN_ALERT);
        }

        protected void LogToDnnEventLog(string msg, EventLogController.EventLogType logType)
        {
            eventLog.AddLog("DNNspot-Store", msg, PortalSettings, UserId, logType);
        }

        protected void LogToDnnEventLog(object o)
        {
            eventLog.AddLog(o, PortalSettings, UserId, UserInfo.Username, EventLogController.EventLogType.ADMIN_ALERT);
        }

        /// <summary>
        /// Return the localization string from a resource (.resx) file, searching the Local and Global resource folders.
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        protected string ResourceString(string resourceKey)
        {
            string s = LocalString(resourceKey);
            if (string.IsNullOrEmpty(s))
            {
                s = GlobalString(resourceKey);
            }
            if (string.IsNullOrEmpty(s))
            {
                s = "";
            }
            return s;
        }

        /// <summary>
        /// Return the Localization String for the given key from the LOCAL resource (.resx) file.
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        private string LocalString(string resourceKey)
        {
            return DotNetNuke.Services.Localization.Localization.GetString(resourceKey, LocalResourceFile);
        }

        /// <summary>
        /// Return the Localization String for the given key from the GLOBAL resource (.resx) file.
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        private string GlobalString(string resourceKey)
        {
            return DotNetNuke.Services.Localization.Localization.GetString(resourceKey, GlobalResourceFile);
        }
    }
}
