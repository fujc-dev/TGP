using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TGP.CarouselDemo
{
    public class CarouselItem : INotifyPropertyChanged
    {
        public CarouselItem(String imgPath)
        {
            this.ID = Guid.NewGuid();
            if (this.Item == null) this.Item = new Image();
            this.Item.Source = new BitmapImage(new Uri("pack://application:,,,/TGP.CarouselDemo;component/" + imgPath, UriKind.RelativeOrAbsolute));
        }

        public Image Item { get; private set; }

        //============================INotifyPropertyChanged============================
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        private Boolean? mIsChecked = false;

        public Boolean? IsChecked
        {
            get { return mIsChecked; }
            set { mIsChecked = value; OnPropertyChanged("IsChecked"); }
        }

        public Guid ID { get; private set; }
    }
}
