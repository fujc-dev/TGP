using System.Collections.Generic;
using System.Windows.Controls;
using System;
using System.Windows;
using System.Collections.ObjectModel;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// 列扩展
    /// </summary>
    public static class ColumnExtension
    {
        /// <summary>
        /// 添加绑定参数模板列
        /// </summary>
        /// <param name="dataGrid">表格</param>
        /// <param name="columns">列集合</param>
        public static void AddBindingParameterTemplateColumn(this DataGrid dataGrid, List<ColumnItem> columns)
        {
            if (dataGrid == null)
            {
                throw new ArgumentNullException("dataGrid");
            }
            var templateColumn = ServiceFactory.GetService<IDataGridMultiLineService>().CreateDataGridTemplateColumns(columns, true);
            foreach (var col in templateColumn)
            {
                dataGrid.Columns.Add(col);
            }
        }

        /// <summary>
        /// 添加绑定路径模板列
        /// </summary>
        /// <param name="dataGrid">表格</param>
        /// <param name="columns">列集合</param>
        public static void AddBindingPathTemplateColumn(this DataGrid dataGrid, List<ColumnItem> columns)
        {
            //格式化columns集合中的列信息 2017.5.2
            //List<ColumnItem> clones = new List<ColumnItem>();
            //columns.ColumnItemsToClone(clones);
            //移除当前被影藏的Items
            //*****
            if (dataGrid == null)
            {
                throw new ArgumentNullException("dataGrid");
            }
            //构建DataGrid.Columns的绑定信息
            var templateColumn = ServiceFactory.GetService<IDataGridMultiLineService>().CreateDataGridTemplateColumns(columns);
            foreach (var col in templateColumn)
            {
                dataGrid.Columns.Add(col);
            }
        }

        /// <summary>
        /// 创建用于构建设置DataGrid的列的数据源
        /// </summary>
        /// <param name="element">目标源，Page、Window等</param>
        /// <returns></returns>
        public static List<ColumnItem> BuilderDataGridSettingColumn(this FrameworkElement element)
        {
            if (!(typeof(Page).IsAssignableFrom(element.GetType()) || typeof(Window).IsAssignableFrom(element.GetType())))
            {
                throw new ArgumentNullException("当前窗体必须派生自Page、Window");
            }
            //创建用于构建设置DataGrid的列的数据源
            List<ColumnItem> columns = new List<ColumnItem>();
            //二级
            var col1 = new ColumnItem("列名");
            col1.Columns.Add(new ColumnItem("默认列名", "Name", "", "", HorizontalAlignment.Right, 120));
            col1.Columns.Add(new ColumnItem("自定义列名", "ExtendName", "", "", HorizontalAlignment.Stretch, 120, ColumnType.TextBox));
            columns.Add(col1);
            //一级
            columns.Add(new ColumnItem("列宽", "Width", "", "", HorizontalAlignment.Stretch, 80, ColumnType.TextBox));
            //绑定一个ComboBox数据源
            //位置比较特殊，此处绑定的是另外一个数据源，设置不同的数据源需要设置不同的绑定参数
            var horizontalAlignment = new ColumnItem("位置", "Alignment", "horizontalAlignmentValueConverter", "", HorizontalAlignment.Stretch, 80, ColumnType.ComboBox);

            horizontalAlignment.SetColumnComboBox(new ColumnComboBox(
                "ComboBoxDatas,RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Page}}",  //
                "",
                "",
                ""));  //SelectItemA,RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Page}}
            columns.Add(horizontalAlignment);
            columns.Add(new ColumnItem("显示", "Visibility", "visibilityValueConverter", "", HorizontalAlignment.Center, 80, ColumnType.CheckBox));
            return columns;
        }

        /// <summary>
        /// 创建用于绑定Setting的DataGrid的ItemSource
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static ObservableCollection<ColumnItem> BuilderDataGridSettingItemSource(this List<ColumnItem> columns)
        {

            ObservableCollection<ColumnItem> collection = new ObservableCollection<ColumnItem>();
            foreach (ColumnItem column in columns)
            {
                if (column.Columns.Count > 0)
                {
                    GetSettingDatas(ref collection, column.Columns);
                }
                else
                {
                    collection.Add(column);
                }
            }
            return collection;
        }

        /// <summary>
        /// 集合深度复制
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static void ColumnItemsToClone(this ICollection<ColumnItem> columns, ICollection<ColumnItem> clones)
        {
            foreach (var item in columns)
            {
                var clone = (ColumnItem)item.Clone();
                clones.Add(clone);
            }
        }

        /// <summary>
        /// 格式列，将自定义后的Setting明细格式化到目标列
        /// </summary>
        /// <param name="SettingDatas">自定义后的列设置信息</param>
        /// <param name="columns">原始的列信息</param>
        public static List<ColumnItem> BuilderDataGridFormartColumns(this ObservableCollection<ColumnItem> SettingDatas, ICollection<ColumnItem> baseColumns)
        {
            var columns = new List<ColumnItem>();
            baseColumns.ColumnItemsToClone(columns);
            var parentColumns = new List<ColumnItem>(columns);
            //
            foreach (ColumnItem column in new List<ColumnItem>(parentColumns))
            {
                if (column.Columns.Count > 0)
                {
                    FormartColumnsWithChild(SettingDatas, parentColumns, column);
                }
                foreach (ColumnItem o in SettingDatas)
                {
                    if (o.ID == column.ID)
                    {
                        if (o.Visibility == Visibility.Visible)
                        {
                            column.ExtendName = o.ExtendName;
                            column.Width = o.Width;
                            column.Alignment = o.Alignment;
                            column.Visibility = o.Visibility;
                        }
                        else
                        {
                            parentColumns.Remove(column);
                        }
                    }
                }
            }
            //移除无效的子节点
            return parentColumns;
        }

        #region ================================静态私有方法================================

        //格式绑定项节点
        private static void FormartColumnsWithChild(ObservableCollection<ColumnItem> SettingDatas, ICollection<ColumnItem> parentColumns, ColumnItem parent)
        {
            //获取当前节点parent的子节点
            foreach (ColumnItem column in new List<ColumnItem>(parent.Columns))  //parent.Columns
            {
                //检测节点是否发生变化
                foreach (ColumnItem o in SettingDatas)
                {
                    //当前节点的子节点包含子节点，再继续解析
                    if (column.Columns.Count > 0)
                    {
                        FormartColumnsWithChild(SettingDatas, parent.Columns, column);
                    }
                    if (o.ID == column.ID)
                    {
                        if (o.Visibility == Visibility.Visible)
                        {
                            column.ExtendName = o.ExtendName;
                            column.Width = o.Width;
                            column.Alignment = o.Alignment;
                            column.Visibility = o.Visibility;
                        }
                        else
                        {
                            parent.Columns.Remove(column);
                        }
                    }
                }
            }
            //当解析后，当前节点没有子节点时，移除父节点
            if (parent.Columns.Count == 0)
            {
                parentColumns.Remove(parent);
            }
        }

        /// <summary>
        /// 获取Setting的DataGrid的数据源
        /// </summary>
        /// <param name="settingDatas"></param>
        /// <param name="columns"></param>
        private static void GetSettingDatas(ref ObservableCollection<ColumnItem> settingDatas, ColumnItemCollection columns)
        {
            foreach (ColumnItem column in columns)
            {
                if (column.Columns.Count > 0)
                {
                    GetSettingDatas(ref settingDatas, column.Columns);
                }
                else
                {
                    settingDatas.Add(column);
                }
            }
        }

        #endregion

    }
}
