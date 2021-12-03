using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// <see cref="DataGrid"/>多行标题服务
    /// </summary>
    public interface IDataGridMultiLineService
    {

        /// <summary>
        /// 创建<see cref="DataGridTemplateColumn"/>列集合，
        /// 每个列中包含自定义个样式<see cref="Style"/>和数据模板<see cref="DataTemplate"/>，
        /// 其中列包含单列和多行标题列
        /// </summary>
        /// <param name="columns">列集合</param>
        /// <param name="bindingParameter">是否绑定参数(参数转换绑定)，默认对象绑定</param>
        /// <returns><see cref="DataGrid"/>单元格模板指定目录的列集合</returns>
        List<DataGridTemplateColumn> CreateDataGridTemplateColumns(List<ColumnItem> columns, bool bindingParameter = false);
    }
}
