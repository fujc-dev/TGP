using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TGP.DataGridDemo.View
{
    /// <summary>
    /// Convert转换器使用案例
    /// </summary>
    public partial class Test3 : Page
    {
        private ICollection<Data3> SaleDatas;

        public Test3()
        {
            InitializeComponent();

            this.SaleDatas = Data3.CreateSaleData3s();

            List<ColumnItem> columns = new List<ColumnItem>();

            columns.Add(new ColumnItem("入库单号", "入库单号", "", "", HorizontalAlignment.Left, 100));
            columns.Add(new ColumnItem("金额", "金额", "", "", HorizontalAlignment.Left, 100));

            string moneyFormat = "{}{0:N2}";

            var col = new ColumnItem("顶级标题");

            var quarter = new ColumnItem("二级标题");
            quarter.Columns.Add(new ColumnItem("三级标题", "Quarter1", "", moneyFormat, HorizontalAlignment.Right, 90));
            quarter.Columns.Add(new ColumnItem("三级标题", "Quarter2", "", moneyFormat, HorizontalAlignment.Right, 90));

            col.Columns.Add(quarter);
            col.Columns.Add(new ColumnItem("特殊二级标题", "Total", "", moneyFormat, HorizontalAlignment.Right, 120));

            columns.Add(col);

            //将 Boolean 类型转换为中文
            columns.Add(new ColumnItem("Boolean转换测试", "CompleteState", "chineseCommonValueConverter", "", HorizontalAlignment.Center, 60));


            this.dgList.AddBindingPathTemplateColumn(columns); //添加列集合

            this.dgList.ItemsSource = this.SaleDatas;
        }
    }
}
