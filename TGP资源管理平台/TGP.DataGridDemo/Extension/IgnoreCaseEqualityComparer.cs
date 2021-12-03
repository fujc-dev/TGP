using System.Collections.Generic;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// 不区分大小写的字符串转换(用于字典转换)
    /// </summary>
    public class IgnoreCaseEqualityComparer : IEqualityComparer<string>
    {
        /// <summary>
        /// 比较两个对象
        /// </summary>
        /// <param name="x">第一个值</param>
        /// <param name="y">第二个值</param>
        /// <returns></returns>
        public bool Equals(string x, string y)
        {
            return string.Compare(x, y, System.StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        /// <summary>
        /// 返回该字符串大写的哈希代码
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public int GetHashCode(string obj)
        {
            if (obj == null)
            {
                string.Empty.GetHashCode();
            }
            return obj.ToUpperInvariant().GetHashCode();
        }
    }
}
