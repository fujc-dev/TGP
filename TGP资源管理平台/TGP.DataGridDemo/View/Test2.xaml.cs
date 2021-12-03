using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TGP.DataGridDemo
{
    public partial class Test2 : Page
    {
        private ICollection<Data> SaleDatas;  //绑定数据源
        List<ColumnItem> columns = new List<ColumnItem>();  //构建动态列信息的数据源
        private ObservableCollection<ColumnItem> SettingDatas;  //绑定到Setting的数据源

        private ObservableCollection<String> mComboBoxDatas;

        public ObservableCollection<String> ComboBoxDatas
        {
            get { return mComboBoxDatas; }
            set { mComboBoxDatas = value; }
        }
        public Test2()
        {
            InitializeComponent();

            this.SaleDatas = Data.CreateSaleDatas();
            this.mComboBoxDatas = new ObservableCollection<string>();
            this.mComboBoxDatas.Add("左");
            this.mComboBoxDatas.Add("中");
            this.mComboBoxDatas.Add("右");
            //单一结构表头
            columns.Add(new ColumnItem("入库单号", "入库单号", "", "", HorizontalAlignment.Left, 120));
            columns.Add(new ColumnItem("金额", "金额", "", "", HorizontalAlignment.Left, 120));
            columns.Add(new ColumnItem("供货商", "供货商", "", "", HorizontalAlignment.Left, 160));
            columns.Add(new ColumnItem("单据号", "单据号", "", "", HorizontalAlignment.Left, 140));
            string moneyFormat = "{}{0:N2}";

            //this.dgList.RowHeight 复杂表头时，不能指定这个属性
            //复杂表头
            var col1 = new ColumnItem("验收情况");
            col1.Columns.Add(new ColumnItem("验收人", "验收情况子标题1", "验收人", "", moneyFormat, HorizontalAlignment.Right, 120, Visibility.Collapsed));
            col1.Columns.Add(new ColumnItem("验收日期", "验收情况子标题2", "验收日期", "", moneyFormat, HorizontalAlignment.Right, 120));
            columns.Add(col1);
            var col2 = new ColumnItem("制单情况");
            col2.Columns.Add(new ColumnItem("制单人", "制单情况子标题1", "制单人", "", moneyFormat, HorizontalAlignment.Right, 120));
            col2.Columns.Add(new ColumnItem("入库日期", "制单情况子标题2", "入库日期", "", moneyFormat, HorizontalAlignment.Right, 120));
            columns.Add(col2);
            columns.Add(new ColumnItem("合计", "Total", "", moneyFormat, HorizontalAlignment.Right, 120, Visibility.Collapsed));

            this.dgList.AddBindingPathTemplateColumn(columns); //添加列集合
            this.dgList.ItemsSource = this.SaleDatas;
            //构建Setting控件显示标题以及列绑定信息
            var settingColumns = this.BuilderDataGridSettingColumn();
            this.settingList.AddBindingPathTemplateColumn(settingColumns);
            //构建绑定数据源
            SettingDatas = columns.BuilderDataGridSettingItemSource();
            this.settingList.ItemsSource = SettingDatas;
        }

        //

        /// <summary>
        /// Clones the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List">The list.</param>
        /// <returns>List{``0}.</returns>
        public List<T> Clone<T>(object List)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, List);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as List<T>;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //将Setting更新的数据替换到需要绑定数据列表
            var a = this.SettingDatas.BuilderDataGridFormartColumns(new List<ColumnItem>(columns));
            this.dgList.Columns.Clear();
            this.dgList.ItemsSource = null;
            this.dgList.AddBindingPathTemplateColumn(a); //添加列集合
            this.dgList.ItemsSource = this.SaleDatas;
        }

    }
}