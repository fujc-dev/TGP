using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
    /// 三级多行标题案例 
    /// </summary>
    public partial class Test1 : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        private ICollection<Data2> SaleDatas;
        private List<ColumnItem> columns = new List<ColumnItem>();
        private ObservableCollection<ColumnItem> mSettingDatas = new ObservableCollection<ColumnItem>();

        /// <summary>
        /// 绑定到Setting的ItemSource
        /// </summary>
        public ObservableCollection<ColumnItem> SettingDatas
        {
            get { return mSettingDatas; }
            set { mSettingDatas = value; OnPropertyChanged("SettingDatas"); }
        }

        private ObservableCollection<A> mComboBoxDatas;

        public ObservableCollection<A> ComboBoxDatas
        {
            get { return mComboBoxDatas; }
            set { mComboBoxDatas = value; OnPropertyChanged("ComboBoxDatas"); }
        }


        private A mSelectItemA;

        public A SelectItemA
        {
            get { return mSelectItemA; }
            set { mSelectItemA = value; }
        }




        public Test1()
        {
            InitializeComponent();

            //绑定一个ComboBox数据源
            ObservableCollection<A> itemSource = new ObservableCollection<A>() {
                new A() { Name="左" },
                new A() { Name="中" },
                new A() { Name="右" }
            };
            ComboBoxDatas = itemSource;

            this.SaleDatas = Data2.CreateSaleData2s();


            //一级
            columns.Add(new ColumnItem("入库单号", "入库单号", "", "", HorizontalAlignment.Left, 130));
            columns.Add(new ColumnItem("金额", "金额", "", "", HorizontalAlignment.Left, 80));

            string moneyFormat = "{}{0:N2}";

            //一级
            var col = new ColumnItem("顶级标题");

            //二级，经此类推，不限级
            var quarter = new ColumnItem("二级标题", "dad");
            quarter.Columns.Add(new ColumnItem("三级标题", "Month1", "", moneyFormat, HorizontalAlignment.Right, 60));
            quarter.Columns.Add(new ColumnItem("三级标题", "Month2", "", moneyFormat, HorizontalAlignment.Right, 60));
            quarter.Columns.Add(new ColumnItem("三级标题", "Month3", "", moneyFormat, HorizontalAlignment.Right, 60));
            quarter.Columns.Add(new ColumnItem("三级标题", "Quarter1", "", moneyFormat, HorizontalAlignment.Right, 80));
            col.Columns.Add(quarter);

            quarter = new ColumnItem("二级标题", "asdas");
            quarter.Columns.Add(new ColumnItem("三级标题", "Month4", "", moneyFormat, HorizontalAlignment.Right, 60));
            quarter.Columns.Add(new ColumnItem("三级标题", "Month5", "", moneyFormat, HorizontalAlignment.Right, 60));
            quarter.Columns.Add(new ColumnItem("三级标题", "Month6", "", moneyFormat, HorizontalAlignment.Right, 60));
            quarter.Columns.Add(new ColumnItem("三级标题", "Quarter2", "", moneyFormat, HorizontalAlignment.Right, 80));
            col.Columns.Add(quarter);


            columns.Add(col);

            columns.Add(new ColumnItem("CheckBox类型", "Test", "", moneyFormat, HorizontalAlignment.Center, 80, ColumnType.CheckBox));

            this.dgList.AddBindingPathTemplateColumn(columns); //添加列集合

            OnLoadSetting(columns);
            this.dgList.ItemsSource = this.SaleDatas;
            //DataRowView rowView = this.dgList.SelectedItem as DataRowView;
            //if (rowView != null)
            //{
            //    var columnA = rowView.Row[0].ToString();  //传递索引
            //    var columnB = rowView.Row["columnname"].ToString(); //传递列名
            //}
        }

        #region 将指定的的DataGrid转换为Setting设置对象
        private void OnLoadSetting(List<ColumnItem> settingDatas)
        {
            List<ColumnItem> columns = new List<ColumnItem>();
            //二级，经此类推，不限级
            var col1 = new ColumnItem("列名");
            col1.Columns.Add(new ColumnItem("默认列名", "Name", "", "", HorizontalAlignment.Right, 120));
            col1.Columns.Add(new ColumnItem("自定义列名", "ExtendName", "", "", HorizontalAlignment.Stretch, 120, ColumnType.TextBox));
            columns.Add(col1);
            //一级
            columns.Add(new ColumnItem("列宽", "Width", "", "", HorizontalAlignment.Stretch, 80, ColumnType.TextBox));
            var _ = new ColumnItem("位置", "Alignment", "horizontalAlignmentValueConverter", "", HorizontalAlignment.Stretch, 80, ColumnType.ComboBox);

            _.SetColumnComboBox(new ColumnComboBox("ComboBoxDatas,RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Page}}",
                "", "Name", "Name"));  //SelectItemA,RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Page}}
            columns.Add(_);

            columns.Add(new ColumnItem("显示", "Visibility", "visibilityValueConverter", "", HorizontalAlignment.Center, 80, ColumnType.CheckBox));

            this.settingList.AddBindingPathTemplateColumn(columns); //添加列集合
            this.GetSettingDatas(ref mSettingDatas, settingDatas);
            this.settingList.ItemsSource = this.mSettingDatas;
        }

        private void GetSettingDatas(ref ObservableCollection<ColumnItem> settingDatas, List<ColumnItem> columns)
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
        private void GetSettingDatas(ref ObservableCollection<ColumnItem> settingDatas, ColumnItemCollection columns)
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

        #region 将设置的Setting对象格式化为绑定的DataGrid
        private List<ColumnItem> FormartColumns(ObservableCollection<ColumnItem> SettingDatas, ICollection<ColumnItem> columns)
        {
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

        private void FormartColumnsWithChild(ObservableCollection<ColumnItem> SettingDatas, ICollection<ColumnItem> parentColumns, ColumnItem parent)
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

        private void FormartColumnsWithCheckHasParent(ICollection<ColumnItem> parents, ColumnItem column)
        {
            if (column != null && column.Parent != null)
            {
                if (column.Parent.Columns.Count == 0)
                {
                    parents.Remove(column);
                }
            }
        }

        #endregion

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //
            this.dgList.Columns.Clear();
            this.dgList.ItemsSource = null;
            //columns[2].Columns[1].Columns.RemoveAt(3);
            //List<ColumnItem> myColumns = new List<ColumnItem>(columns);
            //ICollection<ColumnItem> clones = new List<ColumnItem>();
            //columns.ColumnItemsToClone(clones);
            var a = this.SettingDatas.BuilderDataGridFormartColumns(new List<ColumnItem>(columns));
            this.dgList.AddBindingPathTemplateColumn(new List<ColumnItem>(a)); //添加列集合
            this.dgList.ItemsSource = this.SaleDatas;
        }
    }

    public class A
    {
        public String Name { get; set; }
    }
}
