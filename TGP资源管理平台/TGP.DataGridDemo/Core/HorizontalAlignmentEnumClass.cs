using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TGP.DataGridDemo
{
    public class HorizontalAlignmentEnumClass
    {
        static HorizontalAlignmentEnumClass()
        {
            StringDict = new Dictionary<HorizontalAlignment, String>() {
                { HorizontalAlignment.Left, "左"},
                { HorizontalAlignment.Center, "中"},
                { HorizontalAlignment.Right, "右"},
         };
        }
        public static Dictionary<HorizontalAlignment, String> StringDict { get; private set; }
    }
}
