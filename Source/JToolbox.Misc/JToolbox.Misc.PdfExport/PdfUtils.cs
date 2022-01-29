using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering.Printing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace JToolbox.Misc.PdfExport
{
    public static class PdfUtils
    {
        public static PdfDocument CombinePdfDocuments(List<PdfDocument> documents)
        {
            var importedDocuments = new List<PdfDocument>();
            foreach (var document in documents)
            {
                importedDocuments.Add(ImportDocument(document));
            }

            var output = new PdfDocument();
            foreach (var document in importedDocuments)
            {
                int count = document.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    PdfPage page = document.Pages[idx];
                    output.AddPage(page);
                }
            }
            return output;
        }

        public static bool CombinePdfFiles(List<string> filePaths, string resultFile)
        {
            try
            {
                var documents = new List<PdfDocument>();
                foreach (string file in filePaths)
                {
                    documents.Add(CompatiblePdfReader.Open(file, PdfDocumentOpenMode.Import));
                }
                var output = CombinePdfDocuments(documents);
                output.Save(resultFile);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void PrintDocument(Document document)
        {
            var printSettings = ShowPrintDialog();
            if (printSettings == null)
            {
                return;
            }

            var printDocument = new MigraDocPrintDocument(document)
            {
                PrinterSettings = printSettings
            };
            printDocument.Print();
        }

        private static PdfDocument ImportDocument(PdfDocument document)
        {
            if (document.IsImported)
            {
                return document;
            }

            using (var stream = new MemoryStream())
            {
                document.Save(stream, false);
                return CompatiblePdfReader.Open(stream, PdfDocumentOpenMode.Import);
            }
        }

        private static PrinterSettings ShowPrintDialog()
        {
            var printerSettings = new PrinterSettings();
            using (var dialog = new PrintDialog())
            {
                dialog.PrinterSettings = printerSettings;
                dialog.AllowSomePages = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return printerSettings;
                }
            }
            return null;
        }
    }
}