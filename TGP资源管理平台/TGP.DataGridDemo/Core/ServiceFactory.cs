using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGP.DataGridDemo
{
    public class ServiceFactory
    {
        private static IDictionary<Type, Object> container = new Dictionary<Type, Object>();
        //
        static ServiceFactory()
        {
            container.Add(typeof(IDataGridMultiLineService), new DataGridMultiLineServiceImp());
            container.Add(typeof(IDataGridDataTemplateFactory), new DataGridDataTemplateFactory());
        }

        public static T GetService<T>()
        {
            Object obj = null;
            container.TryGetValue(typeof(T), out obj);
            return (T)obj;
        }
    }
}
