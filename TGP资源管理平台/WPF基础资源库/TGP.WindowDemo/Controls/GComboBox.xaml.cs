using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TGP.WindowDemo
{
    /// <summary>
    /// 自定义GComboBox下拉控件 by dane
    /// </summary>
    [TemplatePart(Name = GComboBox.TGP_COMBOBOX_LOADING, Type = typeof(Path))]
    public partial class GComboBox : ComboBox
    {

        /// <summary>
        /// 
        /// </summary>
        public GComboBox()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(GComboBox), new FrameworkPropertyMetadata(typeof(GComboBox)));
        }

        #region 依赖属性

        #region 字典名称
        public String DictName
        {
            get { return (String)GetValue(DictNameProperty); }
            set { SetValue(DictNameProperty, value); }
        }
        public static readonly DependencyProperty DictNameProperty = DependencyProperty.Register("DictName", typeof(String), typeof(GComboBox), new PropertyMetadata(""));

        #endregion



        #region 图标显示
        public ImageSource GIcon
        {
            get { return (ImageSource)GetValue(GIconProperty); }
            set { SetValue(GIconProperty, value); }
        }
        public static readonly DependencyProperty GIconProperty = DependencyProperty.Register("GIcon", typeof(ImageSource), typeof(GComboBox), new PropertyMetadata(null));

        #endregion

        #region 可扩展的图标显示区域
        public ControlTemplate GIconTemplate
        {
            get { return (ControlTemplate)GetValue(GIconTemplateProperty); }
            set { SetValue(GIconTemplateProperty, value); }
        }
        public static readonly DependencyProperty GIconTemplateProperty = DependencyProperty.Register("GIconTemplate", typeof(ControlTemplate), typeof(GComboBox), new PropertyMetadata(null));
        #endregion

        #region GComboBox控件切换按钮自定义样式(可扩展)
        public Style ComboToggleButtonStyle
        {
            get { return (Style)GetValue(ComboToggleButtonStyleProperty); }
            set { SetValue(ComboToggleButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty ComboToggleButtonStyleProperty = DependencyProperty.Register("ComboToggleButtonStyle", typeof(Style), typeof(GComboBox), new PropertyMetadata(null));
        #endregion

        #region 非启用状态的边框颜色
        public Brush EnabledBorderBrush
        {
            get { return (Brush)GetValue(EnabledBorderBrushProperty); }
            set { SetValue(EnabledBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty EnabledBorderBrushProperty = DependencyProperty.Register("EnabledBorderBrush", typeof(Brush), typeof(GComboBox), new PropertyMetadata(null));
        #endregion

        #region ToggleButton填充颜色(由于切换按钮样式是可扩展的，这两个属性就比较鸡肋)
        public Brush ToggleButtonFillBrush
        {
            get { return (Brush)GetValue(ToggleButtonFillBrushProperty); }
            set { SetValue(ToggleButtonFillBrushProperty, value); }
        }

        public static readonly DependencyProperty ToggleButtonFillBrushProperty = DependencyProperty.Register("ToggleButtonFillBrush", typeof(Brush), typeof(GComboBox), new PropertyMetadata(null));


        public Brush ToggleButtonStrokeBrush
        {
            get { return (Brush)GetValue(ToggleButtonStrokeBrushProperty); }
            set { SetValue(ToggleButtonStrokeBrushProperty, value); }
        }
        public static readonly DependencyProperty ToggleButtonStrokeBrushProperty = DependencyProperty.Register("ToggleButtonStrokeBrush", typeof(Brush), typeof(GComboBox), new PropertyMetadata(null));


        #endregion

        #region GComboBox控件边框颜色
        public Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }
        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(GComboBox), new PropertyMetadata(Brushes.Orange));

        #endregion

        #region 水印字体颜色
        public Brush WatermarkForeground
        {
            get { return (Brush)GetValue(WatermarkForegroundProperty); }
            set { SetValue(WatermarkForegroundProperty, value); }
        }
        public static readonly DependencyProperty WatermarkForegroundProperty = DependencyProperty.Register("WatermarkForeground", typeof(Brush), typeof(GComboBox), new PropertyMetadata(Brushes.Beige));

        #endregion

        #region 水印
        public String Watermark
        {
            get { return (String)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(String), typeof(GComboBox), new PropertyMetadata("这是水印(TGP.UI)"));

        #endregion

        #region  GComboBox控件圆角
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(GComboBox), new PropertyMetadata(new CornerRadius(0)));

        #endregion

        #region GComboBox下拉弹出框默认背景颜色
        public Brush PopupBackground
        {
            get { return (Brush)GetValue(PopupBackgroundProperty); }
            set { SetValue(PopupBackgroundProperty, value); }
        }
        public static readonly DependencyProperty PopupBackgroundProperty = DependencyProperty.Register("PopupBackground", typeof(Brush), typeof(GComboBox), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#066EB3"))));

        #endregion

        #region GComboBox获取焦点背景颜色
        public Brush FocusBackground
        {
            get { return (Brush)GetValue(FocusBackgroundProperty); }
            set { SetValue(FocusBackgroundProperty, value); }
        }
        public static readonly DependencyProperty FocusBackgroundProperty = DependencyProperty.Register("FocusBackground", typeof(Brush), typeof(GComboBox), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#365080"))));

        #endregion

        #region GComboBox获取焦点边框颜色
        public Brush FocusBorderBrush
        {
            get { return (Brush)GetValue(FocusBorderBrushProperty); }
            set { SetValue(FocusBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty FocusBorderBrushProperty =
            DependencyProperty.Register("FocusBorderBrush", typeof(Brush), typeof(GComboBox), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E7AE6"))));

        #endregion

        #region GComboBox鼠标经过边框颜色
        public Brush MouseOverBorderBrush
        {
            get { return (Brush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty MouseOverBorderBrushProperty = DependencyProperty.Register("MouseOverBorderBrush", typeof(Brush), typeof(GComboBox), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #region 图标与周边间隔
        public Thickness GIconMargin
        {
            get { return (Thickness)GetValue(GIconMarginProperty); }
            set { SetValue(GIconMarginProperty, value); }
        }
        public static readonly DependencyProperty GIconMarginProperty = DependencyProperty.Register("GIconMargin", typeof(Thickness), typeof(GComboBox), new PropertyMetadata(new Thickness(5)));
        #endregion

        #region 是否显示异步记载动画
        public Boolean ShowLoadingAnimation
        {
            get { return (Boolean)GetValue(ShowLoadingAnimationProperty); }
            set { SetValue(ShowLoadingAnimationProperty, value); }
        }

        public static readonly DependencyProperty ShowLoadingAnimationProperty = DependencyProperty.Register("ShowLoadingAnimation", typeof(Boolean), typeof(GComboBox), new PropertyMetadata(true));

        #endregion

        #region 自动绑定数据，默认绑定字典是自动绑定数据
        public Boolean AutomaticBinding
        {
            get { return (Boolean)GetValue(AutomaticBindingProperty); }
            set { SetValue(AutomaticBindingProperty, value); }
        }

        public static readonly DependencyProperty AutomaticBindingProperty = DependencyProperty.Register("AutomaticBinding", typeof(Boolean), typeof(GComboBox), new PropertyMetadata(true));

        #endregion


        #endregion



        //定义下拉选择命令
        //设置ComboBox选项不可选
        //设置dictName动态绑定ComboBox

        private const String TGP_COMBOBOX_LOADING = "PART_Loading_Image";
        private Path PART_Loading_Image = null;
        private Storyboard mStoryboard = null;

        /// <summary>
        /// 调用 <see cref="FrameworkElement.ApplyTemplate"/> 时进行调用。
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PART_Loading_Image = (Path)GetTemplateChild(TGP_COMBOBOX_LOADING);
            if (AutomaticBinding) this.OnNormalBind();
        }


        protected virtual void OnNormalBind()
        {
            this.SetAndBeginStoryboard();
            Task.Factory.StartNew(new Action(() =>
            {
                Thread.Sleep(1000);
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    this.ItemsSource = LocalDB.GetList();
                    this.SelectedValuePath = "ID";
                    this.DisplayMemberPath = "Name";
                    this.SelectedIndex = 1;
                    this.StopStoryboard();
                }));
            }));
        }
        /// <summary>
        /// 启动异步加载动画
        /// </summary>
        public void SetAndBeginStoryboard()
        {
            if (ShowLoadingAnimation)
            {
                if (this.PART_Loading_Image != null)
                {
                    this.PART_Loading_Image.Visibility = Visibility.Visible;
                    mStoryboard = new Storyboard();
                    DoubleAnimation da = new DoubleAnimation(0, 360, TimeSpan.FromSeconds(2));
                    mStoryboard.Children.Add(da);
                    Storyboard.SetTarget(da, this.PART_Loading_Image);
                    Storyboard.SetTargetProperty(da, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)"));
                    mStoryboard.RepeatBehavior = RepeatBehavior.Forever;
                    mStoryboard.Begin();
                }
            }
        }

        /// <summary>
        /// 停止异步加载动画
        /// </summary>
        public void StopStoryboard()
        {
            if (mStoryboard != null) mStoryboard.Stop();
            if (this.PART_Loading_Image != null) this.PART_Loading_Image.Visibility = Visibility.Collapsed;
        }
    }
}
