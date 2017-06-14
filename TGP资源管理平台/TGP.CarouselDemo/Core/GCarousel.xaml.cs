using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Transitionals.Controls;

namespace TGP.CarouselDemo
{
    /// <summary>
    /// GCarousel.xaml 的交互逻辑
    /// </summary>
    [TemplatePart(Name = TGP_PART_CONTENT, Type = typeof(TransitionElement))]
    [TemplatePart(Name = TGP_PART_CAROUSELLIST, Type = typeof(ItemsControl))]
    public partial class GCarousel : Control
    {
        private const String TGP_PART_CONTENT = "PART_Content";
        private const String TGP_PART_CAROUSELLIST = "PART_CarouselList";

        public GCarousel()
        {
            this.ItemsSource = new ObservableCollection<CarouselItem>();
            this.ItemsSource.Add(new CarouselItem("images/5660373fe752d4fba3f110a5d5f13501.jpg"));
            this.ItemsSource.Add(new CarouselItem("images/b44deb446c4c4d8e613bc0aa4aeb722f.jpg"));
            this.ItemsSource.Add(new CarouselItem("images/e5c7e544eec3c5d4807e9171a1989dd2.jpg"));
            this.ItemsSource.Add(new CarouselItem("images/f2898bf21765819cc71a8bce386d1307.jpg"));
            this.ItemsSource.Add(new CarouselItem("images/fed986790d39882e7807bb5824316a53.jpg"));
        }

        #region ============================依赖属性============================

        /// <summary>
        /// 自动动画
        /// </summary>
        public Boolean AutomaticAnimation
        {
            get { return (Boolean)GetValue(AutomaticAnimationProperty); }
            set { SetValue(AutomaticAnimationProperty, value); }
        }
        public static readonly DependencyProperty AutomaticAnimationProperty = DependencyProperty.Register("AutomaticAnimation", typeof(Boolean), typeof(GCarousel), new PropertyMetadata(true));


        public ObservableCollection<CarouselItem> ItemsSource
        {
            get { return (ObservableCollection<CarouselItem>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<CarouselItem>), typeof(GCarousel), new PropertyMetadata(new ObservableCollection<CarouselItem>()));



        #endregion


        #region ============================自定义命令============================

        public ICommand ClickSwitchCommand { get; private set; }  //点击RadioButton切换命令

        private void OnBindingCommand()
        {
            this.ClickSwitchCommand = new RoutedCommand();
            var commandBind = new CommandBinding(ClickSwitchCommand);
            commandBind.Executed += new ExecutedRoutedEventHandler(OnClickSwitchCommand);
            this.CommandBindings.Add(commandBind);
        }

        private void OnClickSwitchCommand(object sender, ExecutedRoutedEventArgs e)  //
        {
            //停止动画
            Console.WriteLine(e.Parameter);
            this.currentIndex = this.ItemsSource.GetIndex((Guid)e.Parameter);
        }

        #endregion

        private Int32 currentIndex = 0;
        private TransitionElement PART_Content;
        private ItemsControl PART_CarouselList;


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PART_Content = GetTemplateChild(TGP_PART_CONTENT) as TransitionElement;
            this.PART_CarouselList = GetTemplateChild(TGP_PART_CAROUSELLIST) as ItemsControl;
            if (this.PART_CarouselList != null)
            {
                if (this.ItemsSource.Count < 5)
                {
                    throw new ArgumentException("用户播放的集合大小不能小于5");
                }
                this.PART_CarouselList.ItemsSource = this.ItemsSource;
            }
            if (this.AutomaticAnimation)
            {
                Task.Factory.StartNew(() =>
                {
                    //定时器轮播处理, 间隔时间 2s
                    Int32 interval = 3000;
                    CarouselTimer.Start<Object>(interval, OnTimerCallback, "Object");
                });
            }
            //设置命令
            this.OnBindingCommand();
        }

        #region ============================私有方法============================
        /// <summary>
        /// 获取当前显示图片
        /// </summary>
        /// <returns></returns>
        private CarouselItem GetIndex()
        {
            //获取上次一个Item

            var item = this.ItemsSource[currentIndex];
            item.IsChecked = true;
            //移除其他Rb的选中状态
            this.ItemsSource.ForEach((o) =>
            {
                if (o.ID != item.ID) o.IsChecked = false;
            });
            if (currentIndex >= this.ItemsSource.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            return item;
        }
        /// <summary>
        ///  轮播回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        private void OnTimerCallback<T>(T obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.PART_Content.Transition = TransitionFactory.Instance.GetRandomTransition();
                this.PART_Content.Content = this.GetIndex().Item;
                Console.WriteLine(DateTime.Now);
            });
        } 
        #endregion

    }
}
