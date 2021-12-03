using System;
using System.Collections.Generic;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// 字典通用
    /// </summary>
    public class DictionaryCommonValueConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            { 
                return null;
            }
            IDictionary<string, object> dic = value as IDictionary<string, object>;
            if (dic == null)
            {
                return null;
            }
            object dicValue;
            if (dic.TryGetValue(parameter.ToString(), out dicValue))
            {
                return dicValue;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("不支持的转换。");
        }
    }
}
