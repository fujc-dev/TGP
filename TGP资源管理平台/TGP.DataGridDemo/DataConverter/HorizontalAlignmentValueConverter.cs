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
    /// 位置转换器
    /// </summary>
    public class HorizontalAlignmentValueConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "左";
            }
            HorizontalAlignment horizontalAlignment = (HorizontalAlignment)value;
            String horizontalAlignmentStr = "左";
            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    horizontalAlignmentStr = "左";
                    break;
                case HorizontalAlignment.Center:
                    horizontalAlignmentStr = "中";
                    break;
                case HorizontalAlignment.Right:
                    horizontalAlignmentStr = "右";
                    break;
                case HorizontalAlignment.Stretch:
                    horizontalAlignmentStr = "自适应";
                    break;
            }
            return horizontalAlignmentStr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center;
            String horizontalAlignmentStr = (String)value;
            switch (horizontalAlignmentStr)
            {
                case "左":
                    horizontalAlignment = HorizontalAlignment.Left; break;
                case "中":
                    horizontalAlignment = HorizontalAlignment.Center; break;
                case "右":
                    horizontalAlignment = HorizontalAlignment.Right; break;
                default:
                    horizontalAlignment = HorizontalAlignment.Stretch; break;
            }
            return horizontalAlignment;
        }
    }
}
