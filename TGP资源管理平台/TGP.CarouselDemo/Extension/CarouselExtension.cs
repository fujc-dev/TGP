using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGP.CarouselDemo
{
    public static class CarouselExtension
    {

        public static void ForEach(this ObservableCollection<CarouselItem> collection, Action<CarouselItem> action)
        {
            new List<CarouselItem>(collection).ForEach(action);
        }

        public static Int32 GetIndex(this ObservableCollection<CarouselItem> collection, Guid id)
        {
            Int32 index = 0;  //默认返回第一个
            collection.ForEach((o) =>
            {
                if (o.ID == id)
                {
                    index = collection.IndexOf(o);
                    return;
                }
            });
            return index;
        }


    }
}
