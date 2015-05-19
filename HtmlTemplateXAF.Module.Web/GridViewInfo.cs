using DevExpress.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.Web
{
    public class GridViewInfo
    {
        public GridViewInfo()
        {
        }
        public GridViewInfo(ASPxGridView grid)
        {
            Columns = new List<ColumnInfoBase>(grid.Columns.Count);
            FilterExpression = grid.FilterExpression;
            PageIndex = grid.PageIndex;
            Criteria = "";
        }

        public List<ColumnInfoBase> Columns { get; set; }
        public string FilterExpression { get; set; }
        public int PageIndex { get; set; }
        public string Criteria { get; set; }

        public void RestoreGridViewLayout(ASPxGridView grid)
        {
            foreach (ColumnInfoBase column in Columns)
            {
                DataColumnInfo dataColumnInfo = column as DataColumnInfo;
                if (dataColumnInfo != null)
                {
                    column.RestoreGridViewColumn(grid.Columns[dataColumnInfo.FieldName]);
                    continue;
                }
                if (column is CommandColumnInfo)
                {
                    column.RestoreGridViewColumn(GetSpecificColumn<GridViewCommandColumn>(grid));
                    continue;
                }
                if (column is BandColumnInfo)
                {
                    column.RestoreGridViewColumn(GetSpecificColumn<GridViewBandColumn>(grid));
                    continue;
                }
            }
            grid.FilterExpression = FilterExpression;
            grid.PageIndex = PageIndex;
        }

        private T GetSpecificColumn<T>(ASPxGridView grid) where T : GridViewColumn
        {
            foreach (GridViewColumn c in grid.Columns)
                if (c is T) return (T)c;

            return null;
        }
    }
}
