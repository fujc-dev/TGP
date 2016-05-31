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
	///  表示 GTabControl 内某个可选择的项
	/// </summary>
	public partial class GTabItem : TabItem
	{
		static GTabItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(GTabItem), new FrameworkPropertyMetadata(typeof(GTabItem)));
		}

		public String FIcon
		{
			get { return (String)GetValue(FIconProperty); }
			set { SetValue(FIconProperty, value); }
		}
		public static readonly DependencyProperty FIconProperty = DependencyProperty.Register("FIcon", typeof(String), typeof(GTabItem), new PropertyMetadata("&#xe618;"));



		public Int32 FIconSize
		{
			get { return (Int32)GetValue(FIconSizeProperty); }
			set { SetValue(FIconSizeProperty, value); }
		}
		public static readonly DependencyProperty FIconSizeProperty = DependencyProperty.Register("FIconSize", typeof(Int32), typeof(GTabItem), new PropertyMetadata(12));




		public Boolean AllowsAnimation
		{
			get { return (Boolean)GetValue(AllowsAnimationProperty); }
			set { SetValue(AllowsAnimationProperty, value); }
		}


		public static readonly DependencyProperty AllowsAnimationProperty = DependencyProperty.Register("AllowsAnimation", typeof(Boolean), typeof(GTabItem), new PropertyMetadata(false));



		public CornerRadius CornerRadius
		{
			get { return (CornerRadius)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}
		public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(GTabItem), new PropertyMetadata(new CornerRadius(0)));




		public Thickness FIconMargin
		{
			get { return (Thickness)GetValue(FIconMarginProperty); }
			set { SetValue(FIconMarginProperty, value); }
		}
		public static readonly DependencyProperty FIconMarginProperty = DependencyProperty.Register("FIconMargin", typeof(Thickness), typeof(GTabItem), new PropertyMetadata(new Thickness(0, 0, 2, 0)));




		public Brush FocusBackground
		{
			get { return (Brush)GetValue(FocusBackgroundProperty); }
			set { SetValue(FocusBackgroundProperty, value); }
		}
		public static readonly DependencyProperty FocusBackgroundProperty = DependencyProperty.Register("FocusBackground", typeof(Brush), typeof(GTabItem), new PropertyMetadata(Brushes.DarkOrange));



		public Brush FocusForeground
		{
			get { return (Brush)GetValue(FocusForegroundProperty); }
			set { SetValue(FocusForegroundProperty, value); }
		}
		public static readonly DependencyProperty FocusForegroundProperty = DependencyProperty.Register("FocusForeground", typeof(Brush), typeof(GTabItem), new PropertyMetadata(Brushes.White));




		public Brush MouseOverBackground
		{
			get { return (Brush)GetValue(MouseOverBackgroundProperty); }
			set { SetValue(MouseOverBackgroundProperty, value); }
		}
		public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(GTabItem), new PropertyMetadata(Brushes.Orange));

		public Brush MouseOverForeground
		{
			get { return (Brush)GetValue(MouseOverForegroundProperty); }
			set { SetValue(MouseOverForegroundProperty, value); }
		}
		public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(GTabItem), new PropertyMetadata(Brushes.White));



	}
}
