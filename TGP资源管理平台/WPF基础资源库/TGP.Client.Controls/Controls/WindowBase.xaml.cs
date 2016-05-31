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
	/// 自定义WindowBase基类
	/// </summary>
	public partial class WindowBase : Window
	{
		/*
		 * 窗体包含元素分析：
		 * 1、窗体图标Icon，窗体图标大小 IconSize
		 * 2、标题栏背景色，字体颜色，以及字体Family，高度，内容
		 * 3、窗体样式按钮最大化、最小化、关闭
		 * 4、是否显示最小化，最大化
		 * 5、
		 */
		public WindowBase()
		{
			this.WindowStyle = WindowStyle.None;
			this.AllowsTransparency = true;
			this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			this.Style = this.FindResource("DefaultWindowStyle") as Style;
		}

		#region 窗体字体图标FIcon

		public static readonly DependencyProperty FIconProperty =
			DependencyProperty.Register("FIcon", typeof(string), typeof(WindowBase), new PropertyMetadata("\ue62e"));

		/// <summary>
		/// 按钮字体图标编码
		/// </summary>
		public string FIcon
		{
			get { return (string)GetValue(FIconProperty); }
			set { SetValue(FIconProperty, value); }
		}

		#endregion

		#region  窗体字体图标大小

		public static readonly DependencyProperty FIconSizeProperty =
			DependencyProperty.Register("FIconSize", typeof(double), typeof(WindowBase), new PropertyMetadata(20D));

		/// <summary>
		/// 按钮字体图标大小
		/// </summary>
		public double FIconSize
		{
			get { return (double)GetValue(FIconSizeProperty); }
			set { SetValue(FIconSizeProperty, value); }
		}

		#endregion

		#region CaptionHeight 标题栏高度

		public static readonly DependencyProperty CaptionHeightProperty =
			DependencyProperty.Register("CaptionHeight", typeof(double), typeof(WindowBase), new PropertyMetadata(26D));

		/// <summary>
		/// 标题高度
		/// </summary>
		public double CaptionHeight
		{
			get { return (double)GetValue(CaptionHeightProperty); }
			set
			{
				SetValue(CaptionHeightProperty, value);
				//this._WC.CaptionHeight = value;
			}
		}

		#endregion

		#region CaptionBackground 标题栏背景色

		public static readonly DependencyProperty CaptionBackgroundProperty = DependencyProperty.Register(
			"CaptionBackground", typeof(Brush), typeof(WindowBase), new PropertyMetadata(null));

		public Brush CaptionBackground
		{
			get { return (Brush)GetValue(CaptionBackgroundProperty); }
			set { SetValue(CaptionBackgroundProperty, value); }
		}

		#endregion

		#region CaptionForeground 标题栏前景景色

		public static readonly DependencyProperty CaptionForegroundProperty = DependencyProperty.Register(
			"CaptionForeground", typeof(Brush), typeof(WindowBase), new PropertyMetadata(null));

		public Brush CaptionForeground
		{
			get { return (Brush)GetValue(CaptionForegroundProperty); }
			set { SetValue(CaptionForegroundProperty, value); }
		}

		#endregion

		#region Header 标题栏内容模板，以提高默认模板，可自定义

		public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
			"Header", typeof(ControlTemplate), typeof(WindowBase), new PropertyMetadata(null));

		public ControlTemplate Header
		{
			get { return (ControlTemplate)GetValue(HeaderProperty); }
			set { SetValue(HeaderProperty, value); }
		}

		#endregion

		#region MaxboxEnable 是否显示最大化按钮

		public static readonly DependencyProperty MaxboxEnableProperty = DependencyProperty.Register(
			"MaxboxEnable", typeof(bool), typeof(WindowBase), new PropertyMetadata(true));

		public bool MaxboxEnable
		{
			get { return (bool)GetValue(MaxboxEnableProperty); }
			set { SetValue(MaxboxEnableProperty, value); }
		}

		#endregion

		#region MinboxEnable 是否显示最小化按钮

		public static readonly DependencyProperty MinboxEnableProperty = DependencyProperty.Register(
			"MinboxEnable", typeof(bool), typeof(WindowBase), new PropertyMetadata(true));

		public bool MinboxEnable
		{
			get { return (bool)GetValue(MinboxEnableProperty); }
			set { SetValue(MinboxEnableProperty, value); }
		}

		#endregion

		public ICommand CloseWindowCommand { get; protected set; }
		public ICommand MaximizeWindowCommand { get; protected set; }
		public ICommand MinimizeWindowCommand { get; protected set; }
	}
}
