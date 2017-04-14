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
    /// 滚动条样式
    /// </summary>
    public static class GScrollViewer
    {

        #region 图片路径
        public static Geometry GetGeometry(DependencyObject obj)
        {
            return (Geometry)obj.GetValue(GeometryProperty);
        }

        public static void SetGeometry(DependencyObject obj, Geometry value)
        {
            obj.SetValue(GeometryProperty, value);
        }

        public static readonly DependencyProperty GeometryProperty = DependencyProperty.RegisterAttached("Geometry", typeof(Geometry), typeof(GScrollViewer), new PropertyMetadata(null));

        #endregion




    }
}
