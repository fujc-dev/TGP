using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// 列<see cref="DataTemplate"/>构建类型
    /// </summary>
    public enum ColumnType
    {
        /// <summary>
        /// 文本
        /// </summary>
        TextBlock,
        /// <summary>
        /// 多选框
        /// </summary>
        CheckBox,
        /// <summary>
        /// 文本输入框
        /// </summary>
        TextBox,
        /// <summary>
        /// 下拉选项卡
        /// </summary>
        ComboBox
    }
}
