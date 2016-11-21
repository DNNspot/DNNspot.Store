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
using System.IO;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Log.EventLog;
using WA.Extensions;

namespace DNNspot.Store
{
    //public static class Debug
    //{
    //    public static void Write(string msg)
    //    {
    //        #if DEBUG
    //            string debugFilePath = HttpContext.Current.Request.PhysicalApplicationPath.EnsureEndsWith(@"\") + @"DesktopModules\" + Constants.ModuleFolderName + @"\debug.txt";        
    //            try
    //            {                                
    //                File.AppendAllText(debugFilePath, string.Format(@"{0} ::: {1} ::: {2} {3}", DateTime.Now, HttpContext.Current.Request.RawUrl, msg, Environment.NewLine));
    //            }
    //            catch(Exception ex)
    //            {
    //                EventLogController dnnLog = new EventLogController();
    //                dnnLog.AddLog("DNNspot-Store-Debug", msg, PortalSettings.Current, UserController.GetCurrentUserInfo().UserID, EventLogController.EventLogType.ADMIN_ALERT);

    //                throw new ApplicationException(string.Format("Unable to write to debug file '{0}'.", debugFilePath), ex);
    //            }
    //        #endif
    //    }

    //    public static void WriteFormat(string format, params object[] args)
    //    {
    //        Debug.Write(string.Format(format, args));
    //    }
    //}
}
