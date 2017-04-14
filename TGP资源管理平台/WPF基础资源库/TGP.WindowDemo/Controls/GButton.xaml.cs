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
    /// 自定义按钮 by dane
    /// 2017-04-11  增加鼠标经过、鼠标按下背景颜色和字体颜色
    ///                      增加GToggleIcon属性，用于WindowBase窗体最大化处理，部分按钮需要两个图片支持
    /// 
    /// </summary>
    public partial class GButton : Button
    {
        public GButton()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(GButton), new FrameworkPropertyMetadata(typeof(GButton)));
        }

        #region 图标
        public ImageSource GIcon
        {
            get { return (ImageSource)GetValue(GIconProperty); }
            set { SetValue(GIconProperty, value); }
        }
        public static readonly DependencyProperty GIconProperty = DependencyProperty.Register("GIcon", typeof(ImageSource), typeof(GButton), new PropertyMetadata(null));
        #endregion

        #region 切换图标(部分按钮需要两个图片支持)
        public ImageSource GToggleIcon
        {
            get { return (ImageSource)GetValue(GToggleIconProperty); }
            set { SetValue(GToggleIconProperty, value); }
        }
        public static readonly DependencyProperty GToggleIconProperty = DependencyProperty.Register("GToggleIcon", typeof(ImageSource), typeof(GButton), new PropertyMetadata(null));

        #endregion


        public Geometry GeometryIcon
        {
            get { return (Geometry)GetValue(GeometryIconProperty); }
            set { SetValue(GeometryIconProperty, value); }
        }


        public static readonly DependencyProperty GeometryIconProperty = DependencyProperty.Register("GeometryIcon", typeof(Geometry), typeof(GButton), new PropertyMetadata(null));


        #region 图标宽度/高度
        public Double IconWidth
        {
            get { return (Double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
        public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register("IconWidth", typeof(Double), typeof(GButton), new PropertyMetadata(18D));


        public Double IconHeight
        {
            get { return (Double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }
        public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register("IconHeight", typeof(Double), typeof(GButton), new PropertyMetadata(18D)); 
        #endregion

        #region 图标边距
        //图标边距
        public Thickness GIconMargin
        {
            get { return (Thickness)GetValue(GIconMarginProperty); }
            set { SetValue(GIconMarginProperty, value); }
        }
        public static readonly DependencyProperty GIconMarginProperty = DependencyProperty.Register("GIconMargin", typeof(Thickness), typeof(GButton), new PropertyMetadata(new Thickness(2)));

        #endregion

        #region 按钮圆角
        //按钮圆角
        public CornerRadius GCornerRadius
        {
            get { return (CornerRadius)GetValue(GCornerRadiusProperty); }
            set { SetValue(GCornerRadiusProperty, value); }
        }

        //默认不为圆角
        public static readonly DependencyProperty GCornerRadiusProperty = DependencyProperty.Register("GCornerRadius", typeof(CornerRadius), typeof(GButton), new PropertyMetadata(new CornerRadius(0)));

        #endregion

        #region 是否启动图标动画(目前默认动画为图标旋转)
        //是否启动图标动画(目前默认动画为图标旋转)

        public Boolean AllowsAnimation
        {
            get { return (Boolean)GetValue(AllowsAnimationProperty); }
            set { SetValue(AllowsAnimationProperty, value); }
        }

        public static readonly DependencyProperty AllowsAnimationProperty = DependencyProperty.Register("AllowsAnimation", typeof(Boolean), typeof(GButton), new PropertyMetadata(true));

        #endregion

        #region 鼠标按下背景颜色
        public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(GButton), new PropertyMetadata(Brushes.DarkBlue));
        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }
        #endregion

        #region 鼠标按钮边框颜色
        public Brush PressedBorderBrush
        {
            get { return (Brush)GetValue(PressedBorderBrushProperty); }
            set { SetValue(PressedBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.Register("PressedBorderBrush", typeof(Brush), typeof(GButton), new PropertyMetadata(null));
        #endregion

        #region 鼠标按下字体颜色
        public static readonly DependencyProperty PressedForegroundProperty = DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(GButton), new PropertyMetadata(Brushes.White));
        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }
        #endregion

        #region 鼠标经过背景颜色
        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(GButton), new PropertyMetadata(Brushes.RoyalBlue));

        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }
        #endregion

        #region 鼠标经过边框颜色
        public Brush MouseOverBorderBrush
        {
            get { return (Brush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.Register("MouseOverBorderBrush", typeof(Brush), typeof(GButton), new PropertyMetadata(null));
        #endregion

        #region 鼠标经过字体颜色
        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(GButton), new PropertyMetadata(Brushes.White));

        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}