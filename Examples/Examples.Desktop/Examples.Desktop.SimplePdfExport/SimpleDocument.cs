using JToolbox.Misc.PdfExport;
using MigraDoc.DocumentObjectModel;
using System;

namespace Examples.Desktop.SimplePdfExport
{
    public class SimpleDocument : BaseDocument
    {
        public SimpleDocument(DocumentInfo documentInfo, double leftRightMargin = 1, double topBottomMargin = 1)
            : base(documentInfo, leftRightMargin, topBottomMargin)
        {
        }

        public void Fill()
        {
            var paragraph = LastSection.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Size = 16;
            paragraph.Format.SpaceAfter = Unit.FromCentimeter(1d);
            paragraph.AddText($"Document generated at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }
    }
}