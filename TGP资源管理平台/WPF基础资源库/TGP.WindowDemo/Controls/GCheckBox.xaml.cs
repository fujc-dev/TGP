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
    /// 多选按钮 by dane
    /// </summary>
    public static class GCheckBox
    {
        public static Thickness GetGIconMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(GIconMarginProperty);
        }

        public static void SetGIconMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(GIconMarginProperty, value);
        }

        public static readonly DependencyProperty GIconMarginProperty = DependencyProperty.RegisterAttached("GIconMargin", typeof(Thickness), typeof(GCheckBox), new PropertyMetadata(new Thickness(2)));

    }
}
