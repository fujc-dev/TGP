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
    /// <see cref="DataGrid"/>控件数据模板<see cref="DataTemplate"/>构建服务，
    /// 根据<see cref="DataTemplate"/>的内容多样性构建不同的显示控件，主要针对单一控件显示
    /// </summary>
    public interface IDataGridDataTemplateService
    {
        /// <summary>
        /// 动态构内容绑定模板
        /// </summary>
        /// <param name="column">自定义列信息</param>
        /// <param name="bindingParameter">是否参数转换</param>
        /// <param name="gridColum">绑定的子列索引 </param>
        /// <returns></returns>
        String CreateCellXaml(ColumnItem column, bool bindingParameter, int? gridColum);
    }
}
