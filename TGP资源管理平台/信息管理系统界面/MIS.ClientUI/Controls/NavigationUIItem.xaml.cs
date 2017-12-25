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

namespace MIS.ClientUI.Controls
{
    /// <summary>
    /// 自定义管理系统顶部导航选项卡按钮
    /// </summary>
    public partial class NavigationUIItem : Button
    {
        //鼠标按下背景图片
        #region 鼠标按下背景图片
        public ImageSource MousePressdBackGroundImage
        {
            get { return (ImageSource)GetValue(MousePressdBackGroundImageProperty); }
            set { SetValue(MousePressdBackGroundImageProperty, value); }
        }
        public static readonly DependencyProperty MousePressdBackGroundImageProperty =
            DependencyProperty.Register("MousePressdBackGroundImage", typeof(ImageSource), typeof(NavigationUIItem), new PropertyMetadata(new BitmapImage(new Uri("../images/navbg.png", UriKind.Relative))));

        #endregion

        //导航模块图标
        #region 导航模块图标
        public ImageSource NavigationModuleIcon
        {
            get { return (ImageSource)GetValue(NavigationModuleIconProperty); }
            set { SetValue(NavigationModuleIconProperty, value); }
        }
        public static readonly DependencyProperty NavigationModuleIconProperty =
            DependencyProperty.Register("NavigationModuleIcon", typeof(ImageSource), typeof(NavigationUIItem), new PropertyMetadata(null));  //new BitmapImage(new Uri("../images/icon01.png", UriKind.Relative))

        #endregion

        //当前选中状态



    }
}
