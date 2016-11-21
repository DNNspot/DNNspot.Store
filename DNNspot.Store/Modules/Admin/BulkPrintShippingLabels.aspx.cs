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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DNNspot.Store.PaymentProviders;
using EntitySpaces.Interfaces;
using System.IO;
using iTextSharp.text.pdf;
using WA.Extensions;
using iTextSharp;
using iTextSharp.text;


namespace DNNspot.Store.Modules.Admin
{
    public partial class BulkPrintShippingLabels1 : System.Web.UI.Page
    {
        protected StoreContext storeContext;
        protected StoreUrls storeUrls;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeEntitySpaces();

            storeContext = new StoreContext(Request);
            storeUrls = new StoreUrls(storeContext);

            string ids = Request.Params["ids"];
            if (!string.IsNullOrEmpty(ids))
            {
                List<int> orderIds = ids.ToListOfInt(",");

                List<Order> orders = OrderCollection.GetOrdersByIds(orderIds);
                orders = orders.FindAll(o => o.HasShippingLabel);

                //rptLabels.DataSource = orders;
                //rptLabels.DataBind();

                ConcatenateLabelPdfs(orders.ConvertAll(o => o.ShippingServiceLabelFile));
            }
        }

        private void ConcatenateLabelPdfs(List<string> filenames)
        {
         //int pageOffset = 0;
         //ArrayList master = new ArrayList();
         //int f = 0;

         //String outFile = args[args.length - 1];

            List<string> inputFilepaths = filenames.ConvertAll(x => storeUrls.ShippingLabelFolderFileRoot + x);

            Document document = null;
            PdfCopy writer = null;

            //int pageOffset = 0;
            int fileIndex = 0;
            foreach (string inputFile in inputFilepaths)
            {
                PdfReader reader = new PdfReader(inputFile);
                reader.ConsolidateNamedDestinations();
                int pageCount = reader.NumberOfPages;
                //pageOffset += pageCount;

                if (fileIndex == 0)
                {
                    document = new Document(reader.GetPageSizeWithRotation(1));
                    writer = new PdfCopy(document, new FileStream(@"C:\WEB\DNNspot_DEV\DesktopModules\DNNspot-Store\ShippingLabels\bulk.pdf", FileMode.Create));
                    document.Open();
                }

                PdfImportedPage page;
                for (int p = 0; p < pageCount; p++)
                {
                    ++p;
                    page = writer.GetImportedPage(reader, p);
                    writer.AddPage(page);
                }
                PRAcroForm form = reader.AcroForm;
                if (form != null)
                {
                    writer.CopyAcroForm(reader);
                }
                fileIndex++;
            }
            document.Close();


         //while (f < args.length - 1) {
         //  PdfReader reader = new PdfReader(args[f]);
         //  reader.consolidateNamedDestinations();
         //  int n = reader.getNumberOfPages();
         //  List bookmarks = SimpleBookmark.getBookmark(reader);
         //  if (bookmarks != null) {
         //    if (pageOffset != 0) {
         //      SimpleBookmark.shiftPageNumbers(bookmarks, pageOffset,
         //         null);
         //    }
         //    master.addAll(bookmarks);
         //   }
         //   pageOffset += n;

         //   if (f == 0) {
         //     document = new Document(reader.getPageSizeWithRotation(1));
         //     writer = new PdfCopy(document,
         //         new FileOutputStream(outFile));
         //     document.open();
         //   }
         //   PdfImportedPage page;
         //   for (int i = 0; i < n;) {
         //     ++i;
         //     page = writer.getImportedPage(reader, i);
         //     writer.addPage(page);
         //   }
         //   PRAcroForm form = reader.getAcroForm();
         //   if (form != null) {
         //     writer.copyAcroForm(reader);
         //   }
         //   f++;
         //}
         //if (!master.isEmpty()) {
         //  writer.setOutlines(master);
         //}
         //document.close();            
        }

        private static void InitializeEntitySpaces()
        {
            if (esConfigSettings.ConnectionInfo.Default != "SiteSqlServer")
            {
                esConfigSettings connectionInfoSettings = esConfigSettings.ConnectionInfo;
                foreach (esConnectionElement connection in connectionInfoSettings.Connections)
                {
                    //if there is a SiteSqlServer in es connections set it default
                    if (connection.Name == "SiteSqlServer")
                    {
                        esConfigSettings.ConnectionInfo.Default = connection.Name;
                        return;
                    }
                }

                //no SiteSqlServer found grab dnn cnn string and create
                string dnnConnection = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;

                // Manually register a connection
                esConnectionElement conn = new esConnectionElement();
                conn.ConnectionString = dnnConnection;
                conn.Name = "SiteSqlServer";
                conn.Provider = "EntitySpaces.SqlClientProvider";
                conn.ProviderClass = "DataProvider";
                conn.SqlAccessType = esSqlAccessType.DynamicSQL;
                conn.ProviderMetadataKey = "esDefault";
                conn.DatabaseVersion = "2005";

                // Assign the Default Connection
                esConfigSettings.ConnectionInfo.Connections.Add(conn);
                esConfigSettings.ConnectionInfo.Default = "SiteSqlServer";

                // Register the Loader
                esProviderFactory.Factory = new EntitySpaces.LoaderMT.esDataProviderFactory();
            }
        }
    }
}
