using MigraDoc.DocumentObjectModel;
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

        public Document Document { get; private set; }
        public Section LastSection => Document.LastSection;
        public PageSetup PageSetup { get; private set; }
        public double PageWidth => PageSetup.PageWidth - PageSetup.LeftMargin - PageSetup.RightMargin;

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

            PageSetup = Document.DefaultPageSetup.Clone();
            PageSetup.TopMargin = Unit.FromCentimeter(topBottomMargin);
            PageSetup.BottomMargin = Unit.FromCentimeter(topBottomMargin);
            PageSetup.LeftMargin = Unit.FromCentimeter(leftRightMargin);
            PageSetup.RightMargin = Unit.FromCentimeter(leftRightMargin);

            Document.AddSection();
            Document.LastSection.PageSetup = PageSetup;
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