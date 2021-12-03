using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TGP.DataGridDemo
{
    internal class DataGridMultiLineServiceImp : IDataGridMultiLineService
    {
        public List<DataGridTemplateColumn> CreateDataGridTemplateColumns(List<ColumnItem> columns, bool bindingParameter = false)
        {
            if (columns == null)
            {
                throw new ArgumentNullException("columns");
            }
            if (columns.Count == 0)
            {
                throw new ArgumentException("columns", "至少需要一列以上。");
            }
            if (columns.Count(a => a == null) > 0)
            {
                throw new ArgumentNullException("columns", "数组包含为 null 的列项目。");
            }
            List<DataGridTemplateColumn> TemplateColumns = new List<DataGridTemplateColumn>();
            foreach (var column in columns)
            {
                if (column.Parent != null)  //
                {
                    throw new ArgumentException(String.Format("{0} 是列 {1} 的子级，不能直接创建。", column.Name, column.Parent.Name));
                }
                column.CreateVerify();
                if (column.Columns.Count == 0)
                {
                    if (column.Visibility == System.Windows.Visibility.Visible)
                        TemplateColumns.Add(DataGridTemplateColumnHelper.CreateTemplateSingleColumn(column, bindingParameter));
                }
                else
                {
                    if (column.Visibility == System.Windows.Visibility.Visible)  //父级一般永远都是显示的
                    {
                        TemplateColumns.Add(DataGridTemplateColumnHelper.CreateTemplateGroupColumn(column, bindingParameter));
                    }

                }
            }
            return TemplateColumns;
        }
    }
}
