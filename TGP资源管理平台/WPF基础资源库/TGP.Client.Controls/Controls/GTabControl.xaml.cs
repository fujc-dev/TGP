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

namespace TGP.UI.Core.Controls
{
	/// <summary>
	/// GTabControl.xaml 的交互逻辑
	/// </summary>
	public partial class GTabControl : TabControl
	{


		public Int32 GHeight
		{
			get { return (Int32)GetValue(GHeightProperty); }
			set { SetValue(GHeightProperty, value); }
		}

		public static readonly DependencyProperty GHeightProperty = DependencyProperty.Register("GHeight", typeof(Int32), typeof(GTabControl), new PropertyMetadata(40));



		public Brush FocusBackground
		{
			get { return (Brush)GetValue(FocusBackgroundProperty); }
			set { SetValue(FocusBackgroundProperty, value); }
		}

	
		public static readonly DependencyProperty FocusBackgroundProperty =
			DependencyProperty.Register("FocusBackground", typeof(Brush), typeof(GTabControl), new PropertyMetadata(null));

		

	}
}
