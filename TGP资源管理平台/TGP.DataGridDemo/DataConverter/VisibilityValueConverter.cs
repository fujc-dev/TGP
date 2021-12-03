using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// 显示转换器
    /// </summary>
    public class VisibilityValueConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            Visibility visibility = (Visibility)value;
            Boolean visibilityBool = false;
            switch (visibility)
            {
                case Visibility.Visible:
                    visibilityBool = true;
                    break;
                case Visibility.Hidden:
                case Visibility.Collapsed:
                    visibilityBool = false;
                    break;
            }
            return visibilityBool;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean visibilityBool = (Boolean)value;
            if (visibilityBool) return Visibility.Visible;
            return Visibility.Collapsed;
            //throw new NotSupportedException("不支持的转换。");
        }
    }
}
