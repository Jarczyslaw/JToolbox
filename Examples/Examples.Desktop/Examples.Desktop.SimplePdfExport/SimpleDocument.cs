using JToolbox.Misc.PdfExport;
using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;

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
            AddTitle();
            AddTable();
        }

        private void AddTable()
        {
            var table = new SimpleTable(this);
            table.Print(new List<TableItem>
            {
                new TableItem
                {
                    Title = "Title1",
                    Value = 1
                },
                new TableItem
                {
                    Title = "Title2",
                    Value =2
                }
            });
        }

        private void AddTitle()
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