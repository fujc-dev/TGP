using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGP.WindowDemo
{
    public class Simple
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public Int32 ID { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
        public Int32 ParentID { get; set; }
    }
}
