using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;

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
    }
}