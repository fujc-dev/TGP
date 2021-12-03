using System;
using System.Collections.Generic;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// 字典布尔中文
    /// </summary>
    public class DictionaryChineseBooleanValueConverter : System.Windows.Data.IValueConverter
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
            if (dic.TryGetValue(parameter.ToString(), out dicValue) && dicValue is Boolean)
            {
                return System.Convert.ToBoolean(dicValue) ? "是" : "否";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("不支持的转换。");
        }

    }
}
