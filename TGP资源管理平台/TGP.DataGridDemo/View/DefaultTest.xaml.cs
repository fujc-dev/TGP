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
    /// DefaultTest.xaml 的交互逻辑
    /// </summary>
    public partial class DefaultTest : Page
    {
        public DefaultTest()
        {
            InitializeComponent();
            this.DataContext = this;
            ObservableCollection<SimpleTest> SaleDatas = new ObservableCollection<SimpleTest>();

            var SimpleTest = new SimpleTest()
            {
                Name = "测试",
                Description = "测试描述信息",
                ShowItem = HorizontalAlignment.Left,
                ComboDatas = new ObservableCollection<ComboBoxTest>()
                {
                     new ComboBoxTest() { ComboValue ="左" } ,
                     new ComboBoxTest() { ComboValue ="中" },
                     new ComboBoxTest() { ComboValue ="右" },
                     new ComboBoxTest() { ComboValue ="自适应" }

                }
            };
            SaleDatas.Add(SimpleTest);

            SimpleTest = new SimpleTest()
            {
                Name = "测试2",
                Description = "测试描述信息2",
                ShowItem = HorizontalAlignment.Center,
                ComboDatas = new ObservableCollection<ComboBoxTest>()
                {
                     new ComboBoxTest() { ComboValue ="左" } ,
                     new ComboBoxTest() { ComboValue ="中" },
                     new ComboBoxTest() { ComboValue ="右" },
                     new ComboBoxTest() { ComboValue ="自适应" }

                }
            };
            SaleDatas.Add(SimpleTest);
            dgList.ItemsSource = SaleDatas;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class SimpleTest
    {
        private String mName;
        private String mDescription;
        private HorizontalAlignment mShowItem;
        private ObservableCollection<ComboBoxTest> mComboDatas;

        public ObservableCollection<ComboBoxTest> ComboDatas
        {
            get { return mComboDatas; }
            set { mComboDatas = value; }
        }


        public String Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }


        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public HorizontalAlignment ShowItem
        {
            get
            {
                return mShowItem;
            }

            set
            {
                mShowItem = value;
            }
        }
    }
    public class ComboBoxTest
    {
        public String ComboValue { get; set; }
    }
}
