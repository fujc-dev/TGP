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
	/// GButton.xaml 的交互逻辑
	/// </summary>
	public partial class GButton : Button
	{
		static GButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(GButton), new FrameworkPropertyMetadata(typeof(GButton)));
		}

		#region 鼠标进入背景样式 -- MouseOverBackground
		public Brush MouseOverBackground
		{
			get { return (Brush)GetValue(MouseOverBackgroundProperty); }
			set { SetValue(MouseOverBackgroundProperty, value); }
		}
		public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(GButton), new PropertyMetadata(Brushes.RoyalBlue));
		#endregion

		#region 鼠标进入前景样式 -- MouseOverForeground
		public Brush MouseOverForeground
		{
			get { return (Brush)GetValue(MouseOverForegroundProperty); }
			set { SetValue(MouseOverForegroundProperty, value); }
		}
		public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(GButton), new PropertyMetadata(Brushes.White));
		#endregion

		#region 鼠标按下背景样式 -- PressedBackground
		public Brush PressedBackground
		{
			get { return (Brush)GetValue(PressedBackgroundProperty); }
			set { SetValue(PressedBackgroundProperty, value); }
		}
		public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(GButton), new PropertyMetadata(Brushes.DarkBlue));
		#endregion

		#region 鼠标按下前景样式（图标、文字） -- PressedForeground
		public Brush PressedForeground
		{
			get { return (Brush)GetValue(PressedForegroundProperty); }
			set { SetValue(PressedForegroundProperty, value); }
		}
		public static readonly DependencyProperty PressedForegroundProperty = DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(GButton), new PropertyMetadata(Brushes.White));
		#endregion

		#region 字体图标间距 -- IconMargin
		public Thickness IconMargin
		{
			get { return (Thickness)GetValue(IconMarginProperty); }
			set { SetValue(IconMarginProperty, value); }
		}
		public static readonly DependencyProperty IconMarginProperty = DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(GButton), new PropertyMetadata(new Thickness(0, 1, 3, 1)));
		#endregion

		#region 是否启用Icon动画 -- AllowsAnimation
		public Boolean AllowsAnimation
		{
			get { return (Boolean)GetValue(AllowsAnimationProperty); }
			set { SetValue(AllowsAnimationProperty, value); }
		}
		public static readonly DependencyProperty AllowsAnimationProperty = DependencyProperty.Register("AllowsAnimation", typeof(Boolean), typeof(GButton), new PropertyMetadata(true));
		#endregion

		#region 按钮圆角大小,左上，右上，右下，左下 -- CornerRadius
		public CornerRadius CornerRadius
		{
			get { return (CornerRadius)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}
		public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(GButton), new PropertyMetadata(new CornerRadius(2)));
		#endregion

		#region 按钮字体图标大小 -- IconSize
		public Int32 IconSize
		{
			get { return (int)GetValue(IconSizeProperty); }
			set { SetValue(IconSizeProperty, value); }
		}
		public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof(Int32), typeof(GButton), new PropertyMetadata(20));
		#endregion

		#region 按钮字体图标编码 -- GIcon
		public String GIcon
		{
			get { return (String)GetValue(GIconProperty); }
			set { SetValue(GIconProperty, value); }
		}
		public static readonly DependencyProperty GIconProperty = DependencyProperty.Register("GIcon", typeof(String), typeof(GButton), new PropertyMetadata("\ue604"));
		#endregion

		#region 按钮文本下划线
		public TextDecorationCollection ContentDecorations
		{
			get { return (TextDecorationCollection)GetValue(ContentDecorationsProperty); }
			set { SetValue(ContentDecorationsProperty, value); }
		}
		public static readonly DependencyProperty ContentDecorationsProperty = DependencyProperty.Register("ContentDecorations", typeof(TextDecorationCollection), typeof(GButton), new PropertyMetadata(null)); 
		#endregion

	}
}
