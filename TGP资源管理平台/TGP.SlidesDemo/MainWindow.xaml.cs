using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TGP.SlidesDemo
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
	}

	public class DataGridExtession
	{


		public static Object GetMyHeader(DependencyObject obj)
		{
			return (Object)obj.GetValue(MyHeaderProperty);
		}

		public static void SetMyHeader(DependencyObject obj, Object value)
		{
			obj.SetValue(MyHeaderProperty, value);
		}


		public static readonly DependencyProperty MyHeaderProperty = DependencyProperty.RegisterAttached("MyHeader", typeof(Object), typeof(DataGridExtession), new PropertyMetadata("111"));


	}
}
