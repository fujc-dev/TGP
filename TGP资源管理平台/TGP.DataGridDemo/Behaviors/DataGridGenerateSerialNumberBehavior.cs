using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// <see cref="DataGrid"/>生成序列号行为
    /// </summary>
    public class DataGridGenerateSerialNumberBehavior : Behavior<DataGrid>
    {
        public DataGridGenerateSerialNumberBehavior()
        {

        }

        protected override void OnAttached()
        {
            base.OnAttached();
            //为DataGrid创建一个动态序列号
            foreach (var v in this.AssociatedObject.Items)
            {
                DataGridRow dgr = (DataGridRow)this.AssociatedObject.ItemContainerGenerator.ContainerFromItem(v);
                if (dgr != null)
                {
                    dgr.Header = dgr.GetIndex() + 1;
                }
                else
                {
                    break;
                }
            }
            this.AssociatedObject.LoadingRow += (sender, e) => { e.Row.Header = e.Row.GetIndex() + 1; };
        }
    }
}
