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
using System.Windows.Shapes;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.OpenCommand = new RoutedCommand();
            var bin = new CommandBinding(this.OpenCommand);
            bin.Executed += bin_Executed;
            this.CommandBindings.Add(bin);
        }

        public static readonly DependencyProperty OpenCommandProperty = DependencyProperty.Register("OpenCommand", typeof(RoutedCommand), typeof(MainWindow), new PropertyMetadata(null));

        public RoutedCommand OpenCommand
        {
            get { return (RoutedCommand)GetValue(OpenCommandProperty); }
            set { SetValue(OpenCommandProperty, value); }
        }

        void bin_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var btn = e.Source as Button;
            this.PageContext.Source = new Uri(btn.Tag.ToString(), UriKind.Relative);
        }
    }
}
