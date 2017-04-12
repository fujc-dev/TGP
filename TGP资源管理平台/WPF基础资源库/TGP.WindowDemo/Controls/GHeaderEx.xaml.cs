using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TGP.WindowDemo
{
    /// <summary>
    /// 自定义头部扩展  by dane
    /// </summary>
    public partial class GHeaderEx : Control
    {
        public GHeaderEx()
        {
            this.ClickCommand = new RoutedUICommand();
            this.BindCommand(ClickCommand, OnClickCommand);

        }

        public ICommand ClickCommand { get; protected set; }  //窗体拖动

        private void OnClickCommand(object sender, ExecutedRoutedEventArgs e)
        {
            
            MessageBox.Show(e.Parameter.ToString());
        }
    }
}
