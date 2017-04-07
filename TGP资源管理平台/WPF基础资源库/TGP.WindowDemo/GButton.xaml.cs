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

namespace TGP.WindowDemo
{
	/// <summary>
	/// 自定义按钮
	/// </summary>
	public partial class GButton : Button
	{
		public GButton()
		{

		}



		#region 图标
		public ImageSource GIcon
		{
			get { return (ImageSource)GetValue(GIconProperty); }
			set { SetValue(GIconProperty, value); }
		}
		public static readonly DependencyProperty GIconProperty = DependencyProperty.Register("GIcon", typeof(ImageSource), typeof(GButton), new PropertyMetadata(null)); 
		#endregion


	}
}
