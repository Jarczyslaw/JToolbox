﻿using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MigraDoc.Rendering.Printing;
using PdfSharp.Pdf;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace JToolbox.Misc.PdfExport
{
    public abstract class BaseDocument
    {
        protected BaseDocument(DocumentInfo documentInfo, double leftRightMargin = 1, double topBottomMargin = 1)
        {
            InitializeDocument(documentInfo, leftRightMargin, topBottomMargin);
        }

        protected Section LastSection => Document.LastSection;
        protected Document Document { get; private set; }

        public void Print()
        {
            var printerSettings = ShowPrintDialog();
            if (printerSettings != null)
            {
                using (var printDocument = new MigraDocPrintDocument(Document))
                {
                    printDocument.PrinterSettings = printerSettings;
                    printDocument.Print();
                }
            }
        }

        public void Save(string filePath)
        {
            var pdfDocument = Render();
            pdfDocument.Save(filePath);
            pdfDocument.Close();
        }

        private void InitializeDocument(DocumentInfo documentInfo, double leftRightMargin, double topBottomMargin)
        {
            Document = new Document();
            Document.Info = documentInfo;

            var pageSetup = Document.DefaultPageSetup.Clone();
            pageSetup.TopMargin = Unit.FromCentimeter(topBottomMargin);
            pageSetup.BottomMargin = Unit.FromCentimeter(topBottomMargin);
            pageSetup.LeftMargin = Unit.FromCentimeter(leftRightMargin);
            pageSetup.RightMargin = Unit.FromCentimeter(leftRightMargin);

            Document.AddSection();
            Document.LastSection.PageSetup = pageSetup;
        }

        private PdfDocument Render()
        {
            var renderer = new PdfDocumentRenderer(true)
            {
                Document = Document
            };
            renderer.RenderDocument();
            return renderer.PdfDocument;
        }

        private PrinterSettings ShowPrintDialog()
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