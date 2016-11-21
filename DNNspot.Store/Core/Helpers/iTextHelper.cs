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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DNNspot.Store
{
    public static class iTextHelper
    {
        public static void ConcatenatePdfs(List<string> inputPdfFilepaths, Stream outputStream)
        {
            Document document = null;
            PdfCopy writer = null;
            int fileIndex = 0;
            foreach (string inputFile in inputPdfFilepaths)
            {
                PdfReader reader = new PdfReader(inputFile);                
                reader.ConsolidateNamedDestinations();
                int pageCount = reader.NumberOfPages;

                if (fileIndex == 0)
                {
                    document = new Document(reader.GetPageSizeWithRotation(1));
                    writer = new PdfCopy(document, outputStream);
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
        }
    }
}
