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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TGP.DataGridDemo.View
{
    /// <summary>
    /// Test5.xaml 的交互逻辑
    /// </summary>
    public partial class Test4 : Page
    {
        //数据集合
        private readonly ICollection<IDictionary<string, object>> DataCollection;

        public Test4()
        {
            InitializeComponent();

            this.DataCollection = new ObservableCollection<IDictionary<string, object>>();

            var row = this.CreateRow();
            row["UserCode"] = "01";
            row["UserName"] = "张三";
            row["Company"] = "张三111";
            row["EnterpriseCulture"] = "张三222";
            row["YearSaleMoney"] = 1231m;
            row["IsBankruptcy"] = true;
            this.DataCollection.Add(row);

            row = this.CreateRow();
            row["USERCODE"] = "02"; //大写试试
            row["UserName"] = "李四";
            row["Company"] = "李四111";
            row["EnterpriseCulture"] = "李四222";
            row["YearSaleMoney"] = 12312m;
            row["IsBankruptcy"] = false;
            this.DataCollection.Add(row);

            row = this.CreateRow();
            row["UserCode"] = "03";
            row["USERNAME"] = "呵呵";
            row["Company"] = "呵呵111";
            row["EnterpriseCulture"] = "呵呵222";
            row["YearSaleMoney"] = 123123m;
            row["IsBankruptcy"] = false;
            this.DataCollection.Add(row);

            row = this.CreateRow();
            row["UserCode"] = "04";
            row["UserName"] = "阿萨德";
            row["Company"] = "阿萨德111";
            row["EnterpriseCulture"] = "阿萨德222";
            row["YearSaleMoney"] = 12312m;
            row["IsBankruptcy"] = false;
            this.DataCollection.Add(row);

            //dicCommonValueConverter、dicChineseBooleanValueConverter 转换器

            string moneyFormat = "{}{0:N2}"; //

            List<ColumnItem> columns = new List<ColumnItem>();

            columns.Add(new ColumnItem("单标题", "UserCode", "dicCommonValueConverter", "", HorizontalAlignment.Left, 100));
            columns.Add(new ColumnItem("单标题", "UserName", "dicCommonValueConverter", "", HorizontalAlignment.Left, 100));

            var company = new ColumnItem("一级标题");
            company.Columns.Add(new ColumnItem("二级标题", "Company", "dicCommonValueConverter", "", HorizontalAlignment.Left, 100));
            company.Columns.Add(new ColumnItem("二级标题", "EnterpriseCulture", "dicCommonValueConverter", "", HorizontalAlignment.Left, 200));
            company.Columns.Add(new ColumnItem("二级标题", "YearSaleMoney", "dicCommonValueConverter", moneyFormat, HorizontalAlignment.Right, 120));
            //字典中文转换
            company.Columns.Add(new ColumnItem("二级标题", "IsBankruptcy", "dicChineseBooleanValueConverter", "", HorizontalAlignment.Center, 80));
            columns.Add(company);

            this.dgList.AddBindingParameterTemplateColumn(columns); //此方法是专用于参数转换绑定

            //  this.dgList.AddBindingPathTemplateColumn(columns); //此方法不能再用，该方法是对象绑定使用

            this.dgList.ItemsSource = this.DataCollection;
        }

        /// <summary>
        /// 创建行
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, object> CreateRow()
        {
            //指定不区分大小写的键转换器,防止因大小写问题无法获取数据
            return new Dictionary<string, object>(new IgnoreCaseEqualityComparer());
        }
    }
}
