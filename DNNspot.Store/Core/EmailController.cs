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
using System.Text;
using System.Web;
using DNNspot.Store.DataModel;
using DotNetNuke.Entities.Host;
using DotNetNuke.Services.Mail;
using WA.Components;
using WA.Extensions;

namespace DNNspot.Store
{
    public class EmailController
    {        
        WA.Components.TokenProcessor tokenizer;

        public EmailController()
        {
            tokenizer = new TokenProcessor(Constants.TemplateTokenStart, Constants.TemplateTokenEnd);
            tokenizer.StripNonMatchingTokens = true;
        }

        public string SendEmail(string from, string to, string subject, string body, bool isHtml)
        {
            return SendEmail(from, to, "", "", subject, body, isHtml);
        }

        public string SendEmail(string from, string to, string cc, string bcc, string subject, string body, bool isHtml)
        {
            MailFormat mailFormat = isHtml ? MailFormat.Html : MailFormat.Text;
            body = InjectEmailCssIntoBody(body);

            return DotNetNuke.Services.Mail.Mail.SendMail(from, to, cc, bcc, MailPriority.Normal, subject, mailFormat, Encoding.UTF8, body, "", "", "", "", "");
        }

        public string SendEmailTemplate(EmailTemplateNames templateName, Dictionary<string,string> tokens, string sendToEmail, DataModel.Store store)
        {
            //string storeEmail = storeContext.CurrentStore.GetSetting(StoreSettingNames.OrderCompletedEmailRecipient);
            string storeEmail = store.GetSetting(StoreSettingNames.OrderCompletedEmailRecipient);
            string customerServiceEmail = store.GetSetting(StoreSettingNames.CustomerServiceEmailAddress);
            string hostEmail = HostSettings.GetHostSetting("HostEmail");
            string from = !string.IsNullOrEmpty(customerServiceEmail) ? customerServiceEmail : hostEmail;
            
            vStoreEmailTemplate emailTemplate = store.GetStoreEmailTemplate(templateName);
            
            string subject = tokenizer.ReplaceTokensInString(HttpUtility.HtmlDecode(emailTemplate.SubjectTemplate), tokens);
            string body = tokenizer.ReplaceTokensInString(HttpUtility.HtmlDecode(emailTemplate.BodyTemplate), tokens);
            body = InjectEmailCssIntoBody(body);

            return SendEmail(from, sendToEmail, subject, body, true);
        }

        private string GetModuleBaseFilePath()
        {
            string rootFilePath = HttpContext.Current.Request.PhysicalApplicationPath.EnsureEndsWith(Path.DirectorySeparatorChar.ToString());

            return rootFilePath + "DesktopModules" + Path.DirectorySeparatorChar + Constants.ModuleFolderName + Path.DirectorySeparatorChar;
        }

        private string InjectEmailCssIntoBody(string body)
        {
            string emailCssString = "";
            string emailCssPath = GetModuleBaseFilePath() + string.Format("css{0}email.css", Path.DirectorySeparatorChar);
            if (File.Exists(emailCssPath))
            {
                emailCssString = File.ReadAllText(emailCssPath);
            }
            if (!string.IsNullOrEmpty(emailCssString))
            {
                // inject our "email.css" styles into the body
                body = string.Format("<style>{0}{1}{0}</style>{0}{2}", Environment.NewLine, emailCssString, body);
            }
            return body;
        }
    }
}
