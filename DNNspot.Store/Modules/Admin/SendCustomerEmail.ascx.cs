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

namespace DNNspot.Store.Modules.Admin
{
    public partial class SendCustomerEmail : StoreAdminModuleBase
    {
        protected DotNetNuke.UI.UserControls.TextEditor txtBody;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string from = GetParam("from");
                if (string.IsNullOrEmpty(from))
                {
                    from = StoreContext.CurrentStore.GetSetting(StoreSettingNames.CustomerServiceEmailAddress);
                }
                string to = GetParam("to");
                string subject = GetParam("subject");
                string body = GetParam("body");

                txtFrom.Text = from;
                txtTo.Text = to;
                txtSubject.Text = subject;
                txtBody.Text = body;
            }
        }

        private string GetParam(string name)
        {
            if(Request.Params[name] != null)
            {
                return Request.Params[name];
            }
            if(Session[name] != null)
            {
                return Session[name].ToString();
            }
            if(Context.Items[name] != null)
            {
                return Context.Items[name].ToString();
            }
            return "";
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            EmailController emailer = new EmailController();
            string response = emailer.SendEmail(txtFrom.Text, txtTo.Text, txtSubject.Text, HttpUtility.HtmlDecode(txtBody.Text), true);
            if(string.IsNullOrEmpty(response))
            {
                ShowFlash("Email sent");
            }
            else
            {
                ShowFlash(string.Format(@"Error sending email: ""{0}""", response));
            }

            Session.Remove("from");
            Session.Remove("to");
            Session.Remove("subject");
            Session.Remove("body");
        }
    }
}