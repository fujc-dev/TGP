using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGP.WindowDemo
{
    public class LocalDB
    {
        private static Object lockObj = new Object();

        public static List<Simple> GetList()
        {
            lock (lockObj)
            {
                List<Simple> _dt = new List<Simple>();
                Simple _ = new Simple() { ID = 1, Name = "四川省", ParentID = -1 }; _dt.Add(_);
                _ = new Simple() { ID = 2, Name = "重庆市", ParentID = -1 }; _dt.Add(_);
                _ = new Simple() { ID = 3, Name = "贵州省", ParentID = -1 }; _dt.Add(_);
                _ = new Simple() { ID = 4, Name = "云南省", ParentID = -1 }; _dt.Add(_);
                //区级
                _ = new Simple() { ID = 5, Name = "北京市", ParentID = 1 }; _dt.Add(_);
                _ = new Simple() { ID = 6, Name = "石家庄市", ParentID = 1 }; _dt.Add(_);
                _ = new Simple() { ID = 7, Name = "唐山市", ParentID = 2 }; _dt.Add(_);
                _ = new Simple() { ID = 8, Name = "秦皇岛市", ParentID = 2 }; _dt.Add(_);
                _ = new Simple() { ID = 9, Name = "邯郸市", ParentID = 2 }; _dt.Add(_);
                _ = new Simple() { ID = 10, Name = "邢台市", ParentID = 3 }; _dt.Add(_);
                _ = new Simple() { ID = 11, Name = "保定市", ParentID = 3 }; _dt.Add(_);
                _ = new Simple() { ID = 12, Name = "张家口市", ParentID = 3 }; _dt.Add(_);
                _ = new Simple() { ID = 13, Name = "承德市", ParentID = 4 }; _dt.Add(_);
                _ = new Simple() { ID = 14, Name = "沧州市", ParentID = 4 }; _dt.Add(_);
                _ = new Simple() { ID = 15, Name = "廊坊市", ParentID = 4 }; _dt.Add(_);

                //县级
                _ = new Simple() { ID = 0, Name = "小河区", ParentID = 5 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "开阳县", ParentID = 5 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "市辖区", ParentID = 5 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "南明区", ParentID = 5 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "云岩区", ParentID = 5 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "花溪区", ParentID = 6 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "乌当区", ParentID = 6 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "白云区", ParentID = 6 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "小河区", ParentID = 6 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "开阳县", ParentID = 7 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "息烽县", ParentID = 7 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "修文县", ParentID = 7 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "清镇市", ParentID = 7 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "钟山区", ParentID = 8 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "水城县", ParentID = 8 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "盘　县", ParentID = 8 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "市辖区", ParentID = 8 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "汇川区", ParentID = 9 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "遵义县", ParentID = 9 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "桐梓县", ParentID = 9 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "绥阳县", ParentID = 10 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "正安县", ParentID = 10 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "凤冈县", ParentID = 10 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "湄潭县", ParentID = 10 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "余庆县", ParentID = 11 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "习水县", ParentID = 11 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "赤水市", ParentID = 11 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "仁怀市", ParentID = 11 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "市辖区", ParentID = 12 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "西秀区", ParentID = 12 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "平坝县", ParentID = 12 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "普定县", ParentID = 12 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "赫章县", ParentID = 12 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "凯里市", ParentID = 12 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "黄平县", ParentID = 12 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "施秉县", ParentID = 12 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "三穗县", ParentID = 12 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "镇远县", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "岑巩县", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "天柱县", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "锦屏县", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "剑河县", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "台江县", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "黎平县", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "榕江县", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "从江县", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "雷山县", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "麻江县", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "丹寨县", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "都匀市", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "福泉市", ParentID = 13 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "荔波县", ParentID = 14 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "贵定县", ParentID = 14 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "瓮安县", ParentID = 15 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "独山县", ParentID = 15 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "平塘县", ParentID = 15 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "罗甸县", ParentID = 15 }; _dt.Add(_);
                _ = new Simple() { ID = 0, Name = "长顺县", ParentID = 15 }; _dt.Add(_);
                return _dt;
            }

        }

    }
}
