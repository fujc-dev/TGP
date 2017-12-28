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
    /// 图片按钮
    /// </summary>
    public partial class MISImageButton : Button
    {

        #region 图片地址
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(MISImageButton), new PropertyMetadata(null));

        #endregion

        #region 显示图片大小
        public Double IconWidth
        {
            get { return (Double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(Double), typeof(MISImageButton), new PropertyMetadata(24D));




        public Double IconHeight
        {
            get { return (Double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(Double), typeof(MISImageButton), new PropertyMetadata(24D));

        #endregion

        public CornerRadius MISCornerRadius
        {
            get { return (CornerRadius)GetValue(MISCornerRadiusProperty); }
            set { SetValue(MISCornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty MISCornerRadiusProperty =
            DependencyProperty.Register("MISCornerRadius", typeof(CornerRadius), typeof(MISImageButton), new PropertyMetadata(new CornerRadius(1)));




        public Geometry GeometryIcon
        {
            get { return (Geometry)GetValue(GeometryIconProperty); }
            set { SetValue(GeometryIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GeometryIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GeometryIconProperty =
            DependencyProperty.Register("GeometryIcon", typeof(Geometry), typeof(MISImageButton), new PropertyMetadata(null));

        
    }
}
