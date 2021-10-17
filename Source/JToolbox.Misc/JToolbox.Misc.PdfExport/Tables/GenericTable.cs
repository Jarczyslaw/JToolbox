using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Misc.PdfExport.Tables
{
    public abstract class GenericTable<TItem>
    {
        private readonly BaseDocument document;

        protected GenericTable(BaseDocument document)
        {
            this.document = document;
        }

        public List<GenericTableColumn> Columns { get; set; } = new List<GenericTableColumn>();

        public void Print(List<TItem> items)
        {
            CalculateFillColumnWidth();

            var table = document.LastSection.AddTable();
            PrepareTable(table);

            foreach (var column in Columns)
            {
                var col = table.AddColumn(column.Width);
                PrepareColumn(col);
            }

            var headerRow = table.AddRow();
            PrepareHeaderRow(headerRow);

            for (int i = 0; i < Columns.Count; i++)
            {
                var column = Columns[i];
                var paragraph = headerRow[i].AddParagraph();
                PrepareHeaderColumn(i, column, paragraph);
            }

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var row = table.AddRow();
                PrepareRow(i, item, row);
            }
        }

        protected virtual void PrepareColumn(Column column)
        {
        }

        protected virtual void PrepareHeaderColumn(int i, GenericTableColumn column, Paragraph paragraph)
        {
        }

        protected virtual void PrepareHeaderRow(Row row)
        {
        }

        protected virtual void PrepareRow(int i, TItem item, Row row)
        {
        }

        protected virtual void PrepareTable(Table table)
        {
        }

        private void CalculateFillColumnWidth()
        {
            var columnsWidth = Columns.Sum(x => x.Width);
            Columns.Single(x => x.Width == default).Width = document.PageWidth - columnsWidth;
        }
    }
}