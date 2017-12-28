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
    /// LinkButton(链接按钮)
    /// </summary>
    public partial class MISLinkButton : Button
    {
        //链接按钮字体颜色
        #region 链接按钮字体颜色
        public Brush LBForeground
        {
            get { return (Brush)GetValue(LBForegroundProperty); }
            set { SetValue(LBForegroundProperty, value); }
        }

        public static readonly DependencyProperty LBForegroundProperty =
            DependencyProperty.Register("LBForeground", typeof(Brush), typeof(MISLinkButton), new PropertyMetadata(null));  //#9595BC 


        #endregion

        //鼠标经过字体颜色
        #region 鼠标经过字体颜色
        public Brush LBMouseOverForeground
        {
            get { return (Brush)GetValue(LBMouseOverForegroundProperty); }
            set { SetValue(LBMouseOverForegroundProperty, value); }
        }

        public static readonly DependencyProperty LBMouseOverForegroundProperty =
            DependencyProperty.Register("LBMouseOverForeground", typeof(Brush), typeof(MISLinkButton), new PropertyMetadata(null));

        #endregion

        //鼠标按下字体颜色
        #region 鼠标按下字体颜色
        public Brush LBPressdForeground
        {
            get { return (Brush)GetValue(LBPressdForegroundProperty); }
            set { SetValue(LBPressdForegroundProperty, value); }
        }

        public static readonly DependencyProperty LBPressdForegroundProperty =
            DependencyProperty.Register("LBPressdForeground", typeof(Brush), typeof(MISLinkButton), new PropertyMetadata(null));

        #endregion


        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(MISLinkButton), new PropertyMetadata(null));

    }
}
