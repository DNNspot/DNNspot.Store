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
using System.Reflection;
using System.Text;

namespace DNNspot.Store
{
    public sealed class DnnVersionSingleton
    {
        private readonly Version dnnVersion = GetDnnVersion();
        private static DnnVersionSingleton instance = null;
        private readonly bool isDnn5 = false;
        private readonly bool isDnn6 = false;
        private readonly bool isDnn7 = false;
        private static readonly object padlock = new object();

        private DnnVersionSingleton()
        {
            if (this.dnnVersion.Major == 5)
            {
                this.isDnn5 = true;
            }
            if (this.dnnVersion.Major == 6)
            {
                this.isDnn6 = true;
            }
            if (this.dnnVersion.Major == 7)
            {
                this.isDnn7 = true;
            }
        }

        private static Version GetDnnVersion()
        {
            Version version = Assembly.GetAssembly(typeof(DotNetNuke.Common.Globals)).GetName().Version;
            if (version != null)
            {
                return version;
            }
            return null;
        }

        public Version DnnVersion
        {
            get
            {
                return this.dnnVersion;
            }
        }

        public static DnnVersionSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DnnVersionSingleton();
                    }
                    return instance;
                }
            }
        }

        public bool IsDnn5
        {
            get
            {
                return this.isDnn5;
            }
        }

        public bool IsDnn6
        {
            get
            {
                return this.isDnn6;
            }
        }
    }



}
