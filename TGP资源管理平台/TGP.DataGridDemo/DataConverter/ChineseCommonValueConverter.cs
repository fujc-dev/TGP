using System;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// 中文公共转换器
    /// </summary>
    public class ChineseCommonValueConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.ToString().Trim() == "")
            {
                return "";                
            }
            if (value is DateTime)
            {
                DateTime date = (DateTime)value;
                if (date == date.Date)
                {
                    return System.Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                else
                {
                    return System.Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            else if (value is Boolean)
            {
                return System.Convert.ToBoolean(value) ? "是" : "否";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.ToString().Trim() == "")
            {
                return null;                
            }
            if (targetType == typeof(DateTime) || targetType == typeof(DateTime?))
            {
                DateTime date;
                if (DateTime.TryParse(value.ToString(), out date))
                {
                    return date.Date;
                }
                else
                {
                    return value;
                }
            }
            if (!(value is Boolean) && (targetType == typeof(Boolean) || targetType == typeof(Boolean?)))
            {
                string str = value.ToString().ToLower();
                if (str == "是" || str == "yes" || str == "true" || str == "y" || str == "t")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return value;
            }

        }

    }
}
