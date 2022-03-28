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

        public Table Table { get; private set; }

        public void Print(List<TItem> items)
        {
            CalculateFillColumnWidth();

            Table = document.LastSection.AddTable();
            PrepareTable(Table);

            foreach (var column in Columns)
            {
                var tableColumn = Table.AddColumn(column.Width);
                PrepareColumn(column, tableColumn);
            }

            var headerRow = Table.AddRow();
            PrepareHeaderRow(headerRow);

            for (int i = 0; i < Columns.Count; i++)
            {
                var column = Columns[i];
                PrepareHeaderColumn(i, column, headerRow);
            }

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var row = Table.AddRow();
                PrepareRow(i, items, item, row);
            }
        }

        protected virtual void PrepareColumn(GenericTableColumn column, Column tableColumn)
        {
        }

        protected virtual void PrepareHeaderColumn(int i, GenericTableColumn column, Row headerRow)
        {
        }

        protected virtual void PrepareHeaderRow(Row row)
        {
        }

        protected virtual void PrepareRow(int i, List<TItem> items, TItem currentItem, Row row)
        {
        }

        protected virtual void PrepareTable(Table table)
        {
        }

        private void CalculateFillColumnWidth()
        {
            var columnsWidth = Columns.Sum(x => x.Width);
            var columnToStretch = Columns.SingleOrDefault(x => x.Width == default);
            if (columnToStretch != null)
            {
                columnToStretch.Width = document.PageWidth - columnsWidth;
            }
        }
    }
}