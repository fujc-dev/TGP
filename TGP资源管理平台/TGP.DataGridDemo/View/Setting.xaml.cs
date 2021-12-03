using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Page
    {
        private ICollection<ColumnItem> SettingDatas = new List<ColumnItem>();

        public Setting()
        {
            InitializeComponent();

            List<ColumnItem> columns = new List<ColumnItem>();
            //二级，经此类推，不限级
            var col1 = new ColumnItem("列名");
            col1.Columns.Add(new ColumnItem("默认列名", "Name", "", "", HorizontalAlignment.Right, 120));
            col1.Columns.Add(new ColumnItem("自定义列名", "ExtendName", "", "", HorizontalAlignment.Right, 120));
            columns.Add(col1);
            //一级
            columns.Add(new ColumnItem("列宽", "Width", "", "", HorizontalAlignment.Left, 80));
            columns.Add(new ColumnItem("位置", "Alignment", "horizontalAlignmentValueConverter", "", HorizontalAlignment.Left, 80));
            columns.Add(new ColumnItem("显示", "Visibility", "visibilityValueConverter", "", HorizontalAlignment.Center, 80, ColumnType.CheckBox));

            this.dgList.AddBindingPathTemplateColumn(columns); //添加列集合
            this.GetSettingDatas(ref SettingDatas, GetSettingDatas());
            this.dgList.ItemsSource = this.SettingDatas;
        }

        private List<ColumnItem> GetSettingDatas()
        {

            #region Datagrid绑定信息
            List<ColumnItem> columns = new List<ColumnItem>();
            //单一结构表头
            columns.Add(new ColumnItem("入库单号", "入库单号", "", "", HorizontalAlignment.Left, 100));
            columns.Add(new ColumnItem("金额", "金额", "", "", HorizontalAlignment.Left, 120));
            columns.Add(new ColumnItem("供货商", "供货商", "", "", HorizontalAlignment.Left, 160));
            columns.Add(new ColumnItem("单据号", "单据号", "", "", HorizontalAlignment.Left, 140, Visibility.Collapsed));
            string moneyFormat = "{}{0:N2}";

            //this.dgList.RowHeight 复杂表头时，不能指定这个属性
            //复杂表头
            var col1 = new ColumnItem("验收情况");
            col1.Columns.Add(new ColumnItem("验收情况子标题1", "Quarter1", "", moneyFormat, HorizontalAlignment.Right, 120));
            col1.Columns.Add(new ColumnItem("验收情况子标题2", "Quarter2", "", moneyFormat, HorizontalAlignment.Right, 120));
            columns.Add(col1);
            var col2 = new ColumnItem("制单情况");
            col2.Columns.Add(new ColumnItem("制单情况子标题1", "自定义列名111", "Quarter3", "", moneyFormat, HorizontalAlignment.Right, 120));
            col2.Columns.Add(new ColumnItem("制单情况子标题2", "自定义列名222", "Quarter4", "", moneyFormat, HorizontalAlignment.Right, 120));
            columns.Add(col2);
            columns.Add(new ColumnItem("合计", "Total", "", moneyFormat, HorizontalAlignment.Right, 120));
            #endregion



            return columns;
        }

        private void GetSettingDatas(ref ICollection<ColumnItem> settingDatas, List<ColumnItem> columns)
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
        private void GetSettingDatas(ref ICollection<ColumnItem> settingDatas, ColumnItemCollection columns)
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

    }
}
