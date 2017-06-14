using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using Transitionals;

namespace TGP.CarouselDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<CarouselItem> Carousel { get; set; }
        public MainWindow()
        {
            InitializeComponent();

     

            this.Carousel = new ObservableCollection<CarouselItem>();
            this.Carousel.Add(new CarouselItem("images/5660373fe752d4fba3f110a5d5f13501.jpg"));
            this.Carousel.Add(new CarouselItem("images/b44deb446c4c4d8e613bc0aa4aeb722f.jpg"));
            this.Carousel.Add(new CarouselItem("images/e5c7e544eec3c5d4807e9171a1989dd2.jpg"));
            this.Carousel.Add(new CarouselItem("images/f2898bf21765819cc71a8bce386d1307.jpg"));
            this.Carousel.Add(new CarouselItem("images/fed986790d39882e7807bb5824316a53.jpg"));
            this.CarouselCtrl.ItemsSource = this.Carousel;

        }  




        

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CarouselTimer.Stop();
        }
    }

}
