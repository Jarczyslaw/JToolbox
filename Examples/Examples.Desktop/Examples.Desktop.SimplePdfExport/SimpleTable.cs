using JToolbox.Misc.PdfExport;
using JToolbox.Misc.PdfExport.Tables;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System.Collections.Generic;

namespace Examples.Desktop.SimplePdfExport
{
    public class SimpleTable : GenericTable<TableItem>
    {
        public SimpleTable(BaseDocument document) : base(document)
        {
            Columns = new List<GenericTableColumn>
            {
                new GenericTableColumn
                {
                    Width = 100,
                    Header = "Header 1"
                },
                new GenericTableColumn
                {
                    Width = 150,
                    Header = "Header 2"
                },
            };
        }

        protected override void PrepareHeaderColumn(int i, GenericTableColumn column, Row headerRow)
        {
            var paragraph = headerRow[i].AddParagraph();
            paragraph.AddFormattedText(column.Header);
        }

        protected override void PrepareHeaderRow(Row row)
        {
            row.HeadingFormat = true;
            row.VerticalAlignment = VerticalAlignment.Center;
            row.Shading.Color = Colors.Gainsboro;
            row.Format.Font.Bold = true;
        }

        protected override void PrepareRow(int i, TableItem item, Row row)
        {
            var paragraph = row.Cells[0].AddParagraph();
            paragraph.AddText(item.Title);

            paragraph = row.Cells[1].AddParagraph();
            paragraph.AddText(item.Value.ToString());
        }

        protected override void PrepareTable(Table table)
        {
            table.Rows.LeftIndent = 0;
            table.TopPadding = 4;
            table.BottomPadding = 4;
            table.LeftPadding = 4;
            table.RightPadding = 4;
            table.Borders.Visible = true;
        }
    }
}