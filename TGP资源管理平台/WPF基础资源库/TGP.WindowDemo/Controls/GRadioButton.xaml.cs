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
    /// RadioButton 单选按钮样式
    /// </summary>
    public static class GRadioButton
    {
        public static Thickness GetGIconMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(GIconMarginProperty);
        }

        public static void SetGIconMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(GIconMarginProperty, value);
        }

        public static readonly DependencyProperty GIconMarginProperty = DependencyProperty.RegisterAttached("GIconMargin", typeof(Thickness), typeof(GRadioButton), new PropertyMetadata(new Thickness(2)));



    }
}
