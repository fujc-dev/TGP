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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TGP.WindowDemo
{
    /// <summary>
    /// 自定义一个窗体基类
    /// </summary>
    public partial class GWindowBase : Window
    {
        public GWindowBase()
        {
            //设置窗体基础样式
            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = true;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //this.Style = this.FindResource("DefaultWindowStyle") as Style;
            //初始化并绑定命令
            this.DragCommand = new RoutedUICommand();
            this.MinCommand = new RoutedUICommand();
            this.MaxCommand = new RoutedUICommand();
            this.CloseCommand = new RoutedUICommand();
            this.BindCommand(DragCommand, OnDragCommand);
            this.BindCommand(MinCommand, OnMinCommand);
            this.BindCommand(MaxCommand, OnMaxCommand);
            this.BindCommand(CloseCommand, OnCloseCommand);
			this.MaxHeight = SystemParameters.WorkArea.Height + 12 + 2;
        }



        //依赖属性

        #region 是否允许拖动，默认值true
        public Boolean CanDrag
        {
            get { return (Boolean)GetValue(CanDragProperty); }
            set { SetValue(CanDragProperty, value); }
        }

        public static readonly DependencyProperty CanDragProperty = DependencyProperty.Register("CanDrag", typeof(Boolean), typeof(GWindowBase), new PropertyMetadata(true));

        #endregion

        #region 图标
        public ImageSource GIcon
        {
            get { return (ImageSource)GetValue(GIconProperty); }
            set { SetValue(GIconProperty, value); }
        }
        public static readonly DependencyProperty GIconProperty = DependencyProperty.Register("GIcon", typeof(ImageSource), typeof(GWindowBase), new PropertyMetadata(new BitmapImage(new Uri("pack://application:,,,/TGP.WindowDemo;component/icon/102.ico", UriKind.RelativeOrAbsolute))));
        #endregion

        #region 标题
        public String GTitle
        {
            get { return (String)GetValue(GTitleProperty); }
            set { SetValue(GTitleProperty, value); }
        }

        public static readonly DependencyProperty GTitleProperty = DependencyProperty.Register("GTitle", typeof(String), typeof(GWindowBase), new PropertyMetadata("TGP.UI控件库"));

        #endregion

        #region 标题栏头部扩展

        public ControlTemplate HeaderEx
        {
            get { return (ControlTemplate)GetValue(HeaderExProperty); }
            set { SetValue(HeaderExProperty, value); }
        }

        public static readonly DependencyProperty HeaderExProperty = DependencyProperty.Register("HeaderEx", typeof(ControlTemplate), typeof(GWindowBase), new PropertyMetadata(null));






        #endregion

        #region 最小化按钮显示状态
        public Visibility MinButtonVisibility
        {
            get { return (Visibility)GetValue(MinButtonVisibilityProperty); }
            set { SetValue(MinButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty MinButtonVisibilityProperty = DependencyProperty.Register("MinButtonVisibility", typeof(Visibility), typeof(GWindowBase), new PropertyMetadata(Visibility.Visible));

        #endregion

        #region 最大化按钮显示状态
        public Visibility MaxButtonVisibility
        {
            get { return (Visibility)GetValue(MaxButtonVisibilityProperty); }
            set { SetValue(MaxButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty MaxButtonVisibilityProperty = DependencyProperty.Register("MaxButtonVisibility", typeof(Visibility), typeof(GWindowBase), new PropertyMetadata(Visibility.Visible));

        #endregion

        #region 关闭按钮显示状态
        public Visibility CloseButtonVisibility
        {
            get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
            set { SetValue(CloseButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonVisibilityProperty = DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(GWindowBase), new PropertyMetadata(Visibility.Visible));

        #endregion

        #region 默认命令
        public ICommand DragCommand { get; protected set; }  //窗体拖动
        public ICommand MinCommand { get; protected set; } //最小化
        public ICommand MaxCommand { get; protected set; } //最大化
        public ICommand CloseCommand { get; protected set; } //关闭

        private void OnDragCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (CanDrag)
            {
                base.DragMove();                
            }
			e.Handled = true;
        }
        private void OnMinCommand(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            e.Handled = true;
        }
        private void OnMaxCommand(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            e.Handled = true;
        }
        private void OnCloseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
