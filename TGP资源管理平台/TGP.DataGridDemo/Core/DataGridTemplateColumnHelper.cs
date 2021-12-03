using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// 自定义模板列构建帮助类
    /// </summary>
    internal static class DataGridTemplateColumnHelper
    {
        /// <summary>
        /// 设置标题Grid列宽度，组织出一个复杂的标题格式
        /// </summary>
        /// <param name="sbStr">标题字符串</param>
        /// <param name="column">列</param>
        /// <param name="index">列索引，用于控制标题内容显示的边线</param>
        private static void SetColumnDefinition(StringBuilder sbStr, ColumnItem column, ref int index)
        {
            if (column.Columns.Count > 0)
            {
                foreach (var item in column.Columns)
                {
                    SetColumnDefinition(sbStr, item, ref index);
                }
            }
            else  //默认包含一列
            {
                if (index > 0) //当index>0时，添加一个右侧的Rectangle矩形图形边线位置，默认是左侧和右侧是不绘制边线
                {
                    sbStr.AppendLine("<ColumnDefinition Width=\"1\" />");
                }
                //添加一个标题的显示内容列，并设置固定宽度
                sbStr.AppendLine(String.Format("<ColumnDefinition Width=\"{0}*\"/>", column.Width));
                index++;
            }
        }

        /// <summary>
        /// 设置标题内容
        /// </summary>
        /// <param name="sbStr"></param>
        /// <param name="column">当前列</param>
        /// <param name="totalcolumnCount">总列数</param>
        /// <param name="colIndex">内容索引列</param>
        private static void SetContentPresenter(StringBuilder sbStr, ColumnItem column, int totalcolumnCount, ref int colIndex)
        {
            //计算出当前列所占的Grid的ColumnDefinition数目
            int columnOffset = column.LastLevelColumnCount() * 2 - 1;
            //计算当前列在整个标题中的标题行深度
            int LevelDepth = column.Parent.ChildLevelDepth();
            //用于控制绘制标题内容下面显示下线，以及当前显示标题的Grid.RowSpan
            int rowOffset;  //一般情况默认为1
            if (column.Columns.Count == 0)//
            {
                rowOffset = LevelDepth * 2 - 1;
            }
            else
            {
                rowOffset = 1; //
            }
            //计算出当前标题在第几个RowDefinition画内容下边线
            int lineRow = (column.Level * 2 + 1) + (rowOffset - 1);  
            //计算出当前标题在第几个ColumnDefinition画内容右边线
            int lineColumn = (colIndex + 1) + (columnOffset - 1);
            //画标题，并设置当前标题在Grid中的定位
            sbStr.AppendLine(
                CreateDataGridTemplateColumnHeaderContent(
                    String.IsNullOrWhiteSpace(column.ExtendName) ? column.Name : column.ExtendName,  //标题显示内容
                    column.Level * 2,  //所属行，标题内容显示的Grid.Row
                    rowOffset,  //标题内容显示的Grid.RowSpan
                    colIndex,  //标题内容显示的Grid.Column列
                    columnOffset //标题内容显示的Grid.ColumnSpan
                ));
            //存在子级，时添加下线
            if (column.Columns.Count > 0)
            {
                sbStr.AppendLine(String.Format("<Rectangle Fill=\"#FFC9CACA\" VerticalAlignment=\"Stretch\" Height=\"1\" Grid.Row=\"{0}\" Grid.Column=\"{1}\" Grid.RowSpan=\"{2}\" Grid.ColumnSpan=\"{3}\" />", lineRow, colIndex, 1, columnOffset));
            }
            //标题右侧下线
            if (lineColumn < (totalcolumnCount * 2 - 1))
            {
                sbStr.AppendLine(String.Format("<Rectangle Fill=\"#FFC9CACA\" VerticalAlignment=\"Stretch\" Width=\"1\" Visibility=\"Visible\" Grid.Row=\"{0}\" Grid.Column=\"{1}\" Grid.RowSpan=\"{2}\" Grid.ColumnSpan=\"{3}\" />", column.Level * 2, lineColumn, rowOffset, 1));
            }
            //存在子级，先从子级起画
            if (column.Columns.Count > 0)
            {
                foreach (var item in column.Columns)
                {
                    SetContentPresenter(sbStr, item, totalcolumnCount, ref colIndex);
                }
            }
            else
            {
                colIndex += 2; //含分隔线
            }
        }

        /// <summary>
        /// 设置单元格绑定
        /// </summary>
        /// <param name="sbStr"></param>
        /// <param name="column"></param>
        /// <param name="bindingParameter"></param>
        /// <param name="index"></param>
        private static void SetCellBinding(StringBuilder sbStr, ColumnItem column, bool bindingParameter, ref int index)
        {
            if (column.Columns.Count > 0)
            {
                foreach (var item in column.Columns)
                {
                    SetCellBinding(sbStr, item, bindingParameter, ref index);
                }
            }
            else
            {
                if (index > 0)
                {
                    sbStr.AppendLine(String.Format("<Rectangle Fill=\"#FFC9CACA\" VerticalAlignment=\"Stretch\" Grid.Column=\"{0}\"/>", index));
                    index++;
                }
                //构建指定类型的项绑定
                IDataGridDataTemplateFactory templateFactory = ServiceFactory.GetService<IDataGridDataTemplateFactory>();
                IDataGridDataTemplateService templateService = templateFactory.GetService(column.Type);
                sbStr.AppendLine(templateService.CreateCellXaml(column, bindingParameter, index));
                index++;
            }
        }

        /// <summary>
        /// 设置单元格列宽
        /// </summary>
        /// <param name="sbStr"></param>
        /// <param name="column"></param>
        /// <param name="index"></param>
        private static void SetCellColumnDefinition(StringBuilder sbStr, ColumnItem column, ref int index)
        {
            if (column.Columns.Count > 0)
            {
                foreach (var item in column.Columns)
                {
                    SetCellColumnDefinition(sbStr, item, ref index);
                }
            }
            else
            {
                if (index > 0)
                {
                    sbStr.AppendLine("<ColumnDefinition Width=\"1\" />");
                }
                sbStr.AppendLine(String.Format("<ColumnDefinition Width=\"{0}*\"/>", column.Width));
                index++;
            }
        }

        /// <summary>
        /// 创建组列
        /// </summary>
        /// <param name="column">列</param>
        /// <param name="bindingParameter">是否是参数</param>
        /// <returns></returns>
        public static DataGridTemplateColumn CreateTemplateGroupColumn(ColumnItem column, bool bindingParameter)
        {
            var templateColumn = new DataGridTemplateColumn();
            //当前列包含的子级深度
            int LevelDepth = column.ChildLevelDepth();
            //获取当前列子级的总列数
            int totalcolumnCount = column.LastLevelColumnCount();    //
            //设置当前列的宽度
            templateColumn.Width = new DataGridLength(column.TotalGroupWidth() + LevelDepth);
            //构建HeaderStyle以及CellTemplate模板
            //动态构建标题样式 DataGridTemplateColumn.HeaderStyle
            #region 构建多行标题
            var sbHeaderStr = new StringBuilder();
            sbHeaderStr.AppendLine("<Grid.RowDefinitions>");
            for (int i = 0; i <= LevelDepth; i++)
            {
                if (i > 0)
                {
                    sbHeaderStr.AppendLine("<RowDefinition Height=\"1\" />");  //构建分割线，
                }
                if (i < LevelDepth)
                {
                    sbHeaderStr.AppendLine("<RowDefinition Height=\"25\" />");  //内容区域
                }
                else
                {
                    sbHeaderStr.AppendLine("<RowDefinition Height=\"*\" MinHeight=\"25\"/>"); //内容区域
                }
            }
            sbHeaderStr.AppendLine("</Grid.RowDefinitions>");
            sbHeaderStr.AppendLine("<Grid.ColumnDefinitions>");
            int index = 0;
            foreach (var item in column.Columns)
            {
                SetColumnDefinition(sbHeaderStr, item, ref index);
            }
            sbHeaderStr.AppendLine("</Grid.ColumnDefinitions>");

            int columnOffset = totalcolumnCount * 2 - 1;

            sbHeaderStr.AppendLine(CreateDataGridTemplateColumnHeaderContent(String.IsNullOrWhiteSpace(column.ExtendName) ? column.Name : column.ExtendName, -1, -1, -1, columnOffset));

            sbHeaderStr.AppendLine(String.Format("<Rectangle Fill=\"#FFC9CACA\" VerticalAlignment=\"Stretch\" Height=\"1\" Grid.Row=\"1\" Grid.ColumnSpan=\"{0}\" />", columnOffset));

            index = 0;
            foreach (var item in column.Columns)
            {
                SetContentPresenter(sbHeaderStr, item, totalcolumnCount, ref index);
            }
            var headerStyleStr = HeaderStyleString();
            headerStyleStr = headerStyleStr.Replace("{#content#}", sbHeaderStr.ToString());
            templateColumn.HeaderStyle = (Style)XamlReader.Parse(headerStyleStr);
            #endregion
            //动态构建绑定DataTemplate DataGridTemplateColumn.CellTemplate
            #region 构建多行标题数据绑定模板
            var sbCellTempStr = new StringBuilder();
            sbCellTempStr.AppendLine("<Grid.ColumnDefinitions>");
            index = 0;
            foreach (var item in column.Columns)
            {
                SetCellColumnDefinition(sbCellTempStr, item, ref index);
            }
            sbCellTempStr.AppendLine("</Grid.ColumnDefinitions>");
            index = 0;
            foreach (var item in column.Columns)
            {
                SetCellBinding(sbCellTempStr, item, bindingParameter, ref index);
            }
            var cellTemplateStr = CellDataTemplateString();
            cellTemplateStr = cellTemplateStr.Replace("{#content#}", sbCellTempStr.ToString());
            templateColumn.CellTemplate = (DataTemplate)XamlReader.Parse(cellTemplateStr);
            #endregion
            return templateColumn;
        }

        /// <summary>
        /// 动态构建标题模板
        /// </summary>
        /// <param name="sbStr">目标结果</param>
        /// <param name="title">标题名称</param>
        /// <param name="row">所属Grid行Grid.Row</param>
        /// <param name="rowSpan">所属合并行Grid.RowSpan</param>
        /// <param name="column">所属Grid列Grid.Column</param>
        /// <param name="columnSpan">所属合并列Grid.ColumnSpan</param>
        private static String CreateDataGridTemplateColumnHeaderContent(String title, int row, int rowSpan, int column, int columnSpan)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendLine("<ContentPresenter VerticalAlignment=\"Center\" HorizontalAlignment=\"Center\"");
            if (row > 0)
            {
                sbStr.AppendFormat(" Grid.Row=\"{0}\"", row);
            }
            if (rowSpan > 0)
            {
                sbStr.AppendFormat(" Grid.RowSpan=\"{0}\"", rowSpan);
            }
            if (column > 0)
            {
                sbStr.AppendFormat(" Grid.Column=\"{0}\"", column);
            }
            if (columnSpan > 0)
            {
                sbStr.AppendFormat(" Grid.ColumnSpan=\"{0}\"", columnSpan);
            }
            sbStr.Append(">");
            sbStr.AppendLine("<ContentPresenter.Content>");
            sbStr.AppendLine(String.Format("<TextBlock Text=\"{0}\" TextAlignment=\"Center\" TextWrapping=\"Wrap\" />", title));
            sbStr.AppendLine("</ContentPresenter.Content>");
            sbStr.AppendLine("</ContentPresenter>");
            return sbStr.ToString();
        }


        /// <summary>
        /// 创建单列
        /// </summary>
        /// <param name="column"></param>
        /// <param name="bindingParameter"></param>
        /// <returns></returns>
        public static DataGridTemplateColumn CreateTemplateSingleColumn(ColumnItem column, bool bindingParameter)
        {
            if (column == null)
            {
                throw new ArgumentNullException("column");
            }
            if (String.IsNullOrWhiteSpace(column.BindingName))
            {
                throw new ArgumentNullException("column.BindingName", "末级列的绑定名称不能为 null 或空白字符串。");
            }
            //创建DataGrid的列
            var templateColumn = new DataGridTemplateColumn();
            //设置列的宽度
            templateColumn.Width = new DataGridLength(column.Width);
            //构建模板字符串
            var sbStr = new StringBuilder();
            //根据模板创建标题
            sbStr.AppendLine(CreateDataGridTemplateColumnHeaderContent(String.IsNullOrWhiteSpace(column.ExtendName) ? column.Name : column.ExtendName, -1, -1, -1, -1));
            //动态构建标题样式 DataGridTemplateColumn.HeaderStyle
            #region DataGridTemplateColumn.HeaderStyle
            var headerStyleStr = HeaderStyleString();
            headerStyleStr = headerStyleStr.Replace("{#content#}", sbStr.ToString());
            templateColumn.HeaderStyle = (Style)XamlReader.Parse(headerStyleStr);
            sbStr.Clear();
            #endregion
            //动态构建绑定DataTemplate DataGridTemplateColumn.CellTemplate
            #region DataGridTemplateColumn.CellTemplate
            //构建绑定模板
            IDataGridDataTemplateFactory templateFactory = ServiceFactory.GetService<IDataGridDataTemplateFactory>();
            IDataGridDataTemplateService templateService = templateFactory.GetService(column.Type);
            sbStr.AppendLine(templateService.CreateCellXaml(column, bindingParameter, null));
            String cellTemplateStr = CellDataTemplateString();
            cellTemplateStr = cellTemplateStr.Replace("{#content#}", sbStr.ToString());
            templateColumn.CellTemplate = (DataTemplate)XamlReader.Parse(cellTemplateStr);
            #endregion
            return templateColumn;
        }

        #region 本地资源模板处理

        private static object lockObj = new object();
        private static String _HeaderStyleString = null;  //
        private static String _CellDataTemplateString = null;//

        /// <summary>
        /// 获取标题样式
        /// </summary>
        /// <returns></returns>
        public static String HeaderStyleString()
        {
            lock (lockObj)
            {
                if (_HeaderStyleString == null)
                {
                    _HeaderStyleString = GetResourceXamlString("HeaderStyle.xaml");
                }
                return _HeaderStyleString;
            }
        }

        /// <summary>
        /// 获取单元格模板字符串
        /// </summary>
        /// <returns></returns>
        public static String CellDataTemplateString()
        {
            lock (lockObj)
            {
                if (_CellDataTemplateString == null)
                {
                    _CellDataTemplateString = GetResourceXamlString("CellDataTemplate.xaml");
                }
                return _CellDataTemplateString;
            }
        }

        /// <summary>
        /// 获取资源Xaml字符串
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        private static String GetResourceXamlString(String fileName)
        {
            var res = Application.GetResourceStream(new Uri(String.Format("pack://application:,,,/TGP.DataGridDemo;component/Template/{0}", fileName), UriKind.RelativeOrAbsolute));
            using (var sr = new StreamReader(res.Stream))
            {
                return sr.ReadToEnd();
            }
        }

        #endregion
    }
}
