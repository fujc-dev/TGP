using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TGP.WindowDemo
{
    /// <summary>
    /// WPF轮播控件，初步完成图片动态切换,未实现切换动画，如果要实现动画就需要大改
    ///                       增加自动布局
    ///                       实现点击左右导航动画
    /// 待解决问题：图像位置需要优化，
    /// </summary>
    [TemplatePart(Name = GCarousel.TGP_PART_Navigation, Type = typeof(StackPanel))]
    [TemplatePart(Name = GCarousel.TGP_PART_Left, Type = typeof(Button))]
    [TemplatePart(Name = GCarousel.TGP_PART_Right, Type = typeof(Button))]
    [TemplatePart(Name = GCarousel.TGP_PART_Inner, Type = typeof(Canvas))]
    public partial class GCarousel : ContentControl
    {
        /**
		 * 分析：左右需要一个上一页下一页的导航 
		 * 中间显示图片
		 * 加载方式：从右到左
		 * 位置区域A:B:C
         * 
         * 
         * 根据依赖属性的优先级原则（ https://msdn.microsoft.com/zh-cn/library/ms743230(v=vs.110).aspx ）动画的优先级非常高。
         * 因此，当一个TimeLine（动画）‘拥有’一个依赖属性，手动设置依赖属性是不起作用的。一个简单的解决办法就是断开动画和依赖属性的关联：
         *
         * private void test2_Click(object sender, RoutedEventArgs e)
         * {
         *      box.BeginAnimation(Canvas.LeftProperty, null);
         *      Canvas.SetLeft(box, 50);
         * }
		 */

        #region 数据源
        public ObservableCollection<Carousel> CarouselSource
        {
            get { return (ObservableCollection<Carousel>)GetValue(CarouselSourceProperty); }
            set { SetValue(CarouselSourceProperty, value); }
        }
        public static readonly DependencyProperty CarouselSourceProperty = DependencyProperty.Register("CarouselSource", typeof(ObservableCollection<Carousel>), typeof(GCarousel), new PropertyMetadata(null));

        #endregion

        #region 每当应用程序代码或内部进程调用 System.Windows.FrameworkElement.ApplyTemplate，都将调用此方法。

        #region 资源声明
        private const String TGP_PART_Navigation = "PART_Navigation";
        private const String TGP_PART_Left = "PART_Left";
        private const String TGP_PART_Right = "PART_Right";
        private const String TGP_PART_Inner = "PART_Inner";
        #endregion

        /// <summary>
        /// 底部标签集合
        /// </summary>
        private StackPanel PART_Navigation;
        /// <summary>
        /// 左右切换按钮
        /// </summary>
        private Button PART_Left, PART_Right;
        /// <summary>
        /// 主画布
        /// </summary>
        private Canvas PART_Inner;
        /// <summary>
        /// 当前主显示图像索引
        /// </summary>
        private Int32 CurrentIndex = 0;
        /// <summary>
        /// B区域图像的起点位置
        /// </summary>
        private Double GridCanvasLeft = 0;
        /// <summary>
        /// C区域图像起点位置
        /// </summary>
        private Double GridCanvasRight = 0;
        /// <summary>
        /// 默认的图像宽度
        /// </summary>
        private Double GridWidth = 400D;
        /// <summary>
        /// 默认的图像高度
        /// </summary>
        private Double GridHeight = 195D;
        /// <summary>
        /// 画布宽度
        /// </summary>
        private Double CanvasWidth = 0D;
        /// <summary>
        /// 所有的图像控件集合
        /// </summary>
        private ObservableCollection<Grid> GridList;
        public override void OnApplyTemplate()
        {
            //初始化界面控件
            this.PART_Navigation = GetTemplateChild(TGP_PART_Navigation) as StackPanel;
            //
            this.PART_Left = GetTemplateChild(TGP_PART_Left) as Button;
            //
            this.PART_Right = GetTemplateChild(TGP_PART_Right) as Button;
            //
            this.PART_Inner = GetTemplateChild(TGP_PART_Inner) as Canvas;
            //获取图像数据源
            this.OnNormalLoad();
            //初始化时显示3张图片，取第一张，第二张以及最后一张，并绑定到控件上
            if (this.CarouselSource.Count >= 3) //图片必须大于3张
            {
                // 获取当前画板的高度与宽度
                this.CanvasWidth = this.Width = double.IsNaN(this.Width) ? GridWidth * 2 : this.Width;
                var canvasHeight = this.Height;
                //设置默认的图片长度
                this.GridWidth = this.CanvasWidth / 2;
                //强制必须让这个画板宽度大于两个Grid的宽度
                if (this.CanvasWidth >= GridWidth * 2)
                {
                    //生成Grid集合
                    this.OnInitializeBindGridList();
                    //依次生成按钮
                    this.OnInitializeBindButtonList();
                    //计算中间显示图片的位置
                    this.GridCanvasLeft = (this.CanvasWidth - this.GridWidth) / 2;
                    //计算最右边显示图片的位置
                    var gridRightWidth = this.GridWidth - (this.CanvasWidth - this.GridCanvasLeft - this.GridWidth);   //获取剩余的显示宽度
                    this.GridCanvasRight = this.GridCanvasLeft + this.GridWidth - gridRightWidth;

                    //设置第一个显示值
                    this.GridList[this.GridList.Count - 1].SetValue(Canvas.LeftProperty, (Double)0);
                    this.GridList[this.GridList.Count - 1].SetValue(Canvas.TopProperty, (Double)30);
                    this.GridList[this.GridList.Count - 1].Tag = "1";
                    //设置中间显示
                    this.GridList[0].SetValue(Canvas.LeftProperty, (Double)this.GridCanvasLeft);
                    this.GridList[0].SetValue(Canvas.TopProperty, (Double)0);
                    this.GridList[0].SetValue(Panel.ZIndexProperty, (Int32)3);
                    this.GridList[0].Height = this.Height;
                    this.GridList[0].Width = this.GridWidth;
                    this.GridList[0].Children[1].Opacity = 0;
                    this.GridList[0].Tag = "1";
                    //设置标签集合的第一项为选中项
                    this.PART_Navigation.Children[0].Focus();
                    //设置最右边显示值
                    this.GridList[1].SetValue(Canvas.LeftProperty, (Double)GridCanvasRight);
                    this.GridList[1].SetValue(Canvas.TopProperty, (Double)30);
                    this.GridList[1].Tag = "1";
                    this.PART_Left.Click += OnPressedLeftButton;
                    this.PART_Right.Click += OnPressedRightButton;
                }
            }
            base.OnApplyTemplate();
        }
        /// <summary>
        /// 默认加载，默认加载轮播数据
        /// </summary>
        private void OnNormalLoad()
        {
            if (this.CarouselSource == null)
            {
                this.CarouselSource = new ObservableCollection<Carousel>();
                var imageUri = new Uri("pack://application:,,,/TGP.WindowDemo;component/Carousel/201704101655_1180x500.jpg", UriKind.RelativeOrAbsolute);   //
                var carousel = new Carousel(imageUri, imageUri); this.CarouselSource.Add(carousel);
                imageUri = new Uri("pack://application:,,,/TGP.WindowDemo;component/Carousel/201703231056_1180x500.jpg", UriKind.RelativeOrAbsolute);   //
                carousel = new Carousel(imageUri, imageUri); this.CarouselSource.Add(carousel);
                imageUri = new Uri("pack://application:,,,/TGP.WindowDemo;component/Carousel/201703211700_1180x500.jpg", UriKind.RelativeOrAbsolute);   //
                carousel = new Carousel(imageUri, imageUri); this.CarouselSource.Add(carousel);
                imageUri = new Uri("pack://application:,,,/TGP.WindowDemo;component/Carousel/201703280934_1180x500.jpg", UriKind.RelativeOrAbsolute);   //
                carousel = new Carousel(imageUri, imageUri); this.CarouselSource.Add(carousel);
                imageUri = new Uri("pack://application:,,,/TGP.WindowDemo;component/Carousel/201704011731_1180x500.jpg", UriKind.RelativeOrAbsolute);   //
                carousel = new Carousel(imageUri, imageUri); this.CarouselSource.Add(carousel);
                imageUri = new Uri("pack://application:,,,/TGP.WindowDemo;component/Carousel/201704051917_1180x500.jpg", UriKind.RelativeOrAbsolute);   //
                carousel = new Carousel(imageUri, imageUri); this.CarouselSource.Add(carousel);
            }
        }
        /// <summary>
        /// 构建需要的Button按钮对象
        /// </summary>
        private void OnInitializeBindButtonList()
        {
            if (this.PART_Navigation != null)
            {
                for (int i = 0; i < this.CarouselSource.Count; i++)
                {
                    GButton tgp = new GButton();
                    tgp.Click += OnNavigationClick;
                    tgp.Style = Application.Current.FindResource("DefaultImageButtonStyle") as Style;
                    tgp.GIcon = this.CarouselSource[i].NavigationIcon;
                    tgp.BorderThickness = new Thickness(3);
                    tgp.Margin = new Thickness(0);
                    tgp.Width = 60D;
                    tgp.Tag = i;
                    tgp.HorizontalAlignment = HorizontalAlignment.Stretch;
                    tgp.VerticalAlignment = VerticalAlignment.Stretch;
                    this.PART_Navigation.Children.Add(tgp);
                }
            }
        }
        /// <summary>
        /// 构建需要的Grid对象
        /// </summary>
        private void OnInitializeBindGridList()
        {
            this.GridList = new ObservableCollection<Grid>();
            for (int i = 0; i < this.CarouselSource.Count; i++)
            {
                String contentGrid = "<Grid  xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' Height=\"" + GridHeight + "\" Width=\"" + GridWidth + "\"/>";
                String contentImage = "<Image xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' Source=\"" + this.CarouselSource[i].DisplayPanelImage.ToString() + "\" Stretch=\"Fill\" Margin=\"0\"/>";
                String contenetRectangle = "<Rectangle xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' Fill=\"Black\" Height=\"Auto\" Margin=\"0\" Stroke=\"Black\" Opacity=\"0.8\"/>";
                Grid _Grid = XamlReader.Parse(contentGrid) as Grid;
                Image _Image = XamlReader.Parse(contentImage) as Image;
                Rectangle _Rectangle = XamlReader.Parse(contenetRectangle) as Rectangle;
                _Grid.Children.Add(_Image);
                _Grid.Children.Add(_Rectangle);
                _Grid.SetValue(Canvas.LeftProperty, -this.GridWidth);
                _Grid.SetValue(Canvas.TopProperty, 30D);
                _Grid.Tag = "0";
                _Grid.SetValue(Panel.ZIndexProperty, -999);
                this.PART_Inner.Children.Add(_Grid);
                this.GridList.Add(_Grid);
            }
        }
        //锁
        private static Object lockObj = new Object();
        /// <summary>
        /// 底部滑动事件（调用左右两侧导航的事件即可）
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件数据</param>
        private void OnNavigationClick(object sender, RoutedEventArgs e)
        {
            //var currentButton = sender as GButton;
            //this.CurrentIndex = (Int32)currentButton.Tag;
        }
        /// <summary>
        /// 左侧滑动事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件数据</param>
        private void OnPressedLeftButton(object sender, RoutedEventArgs e)
        {
            lock (lockObj)
            {
                //当前索引不能超过总数
                if (CurrentIndex < this.GridList.Count)
                {
                    foreach (var o in this.GridList)
                    {
                        if (o.Tag.ToString() == "0")
                        {
                            //o.SetValue(Panel.ZIndexProperty, -999);
                            o.BeginAnimation(Canvas.LeftProperty, null);
                            o.BeginAnimation(Canvas.TopProperty, null);
                            o.SetValue(Canvas.LeftProperty, this.CanvasWidth);  //Canvas宽度
                            o.SetValue(Canvas.TopProperty, 30D);
                        }
                    }
                    //将当前中间显示Grid高度设置为默认高度、宽度设置为默认宽度并将位置移动到左上角，并设置其Opacity
                    Grid manGrid = this.GridList[this.CurrentIndex];
                    var sb1 = this.CreateCarouselCenterToLeftStoryboard(manGrid);
                    sb1.Begin();
                    //将右侧的Grid显示为中间
                    Grid rightGrid = null;
                    if (this.CurrentIndex == this.GridList.Count - 1)
                    {
                        rightGrid = this.GridList[0];
                        this.PART_Navigation.Children[0].Focus();
                    }
                    else
                    {
                        this.PART_Navigation.Children[this.CurrentIndex + 1].Focus();
                        rightGrid = this.GridList[this.CurrentIndex + 1];
                    }
                    var sb2 = this.CreateCarouselRightToCenterStoryboard(rightGrid);
                    sb2.Begin();
                    //显示右侧的图片
                    Grid zeroToTightGrid = null;
                    if (this.CurrentIndex == this.GridList.Count - 1) { zeroToTightGrid = this.GridList[1]; }
                    else if (this.CurrentIndex == this.GridList.Count - 2) { zeroToTightGrid = this.GridList[0]; }
                    else { zeroToTightGrid = this.GridList[this.CurrentIndex + 2]; }
                    var sb3 = this.CreateZeroToRightStoryboard(zeroToTightGrid);
                    sb3.Begin();
                    //将当前左侧的Grid影藏
                    Grid leftGrid = this.CurrentIndex == 0 ? this.GridList[this.GridList.Count - 1] : this.GridList[this.CurrentIndex - 1];
                    leftGrid.SetValue(Panel.ZIndexProperty, -999);
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(1000);
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            leftGrid.Tag = "0";
                            leftGrid.BeginAnimation(Canvas.LeftProperty, null);
                            leftGrid.BeginAnimation(Canvas.TopProperty, null);
                            leftGrid.SetValue(Canvas.LeftProperty, this.CanvasWidth);  //Canvas宽度
                            leftGrid.SetValue(Canvas.TopProperty, 30D);
                        }));
                    });
                    //移动到下一个选中项
                    if (this.CurrentIndex == this.GridList.Count - 1) this.CurrentIndex = 0; else this.CurrentIndex++;
                }
            }
        }
        /// <summary>
        /// 右侧滑动事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件数据</param>
        private void OnPressedRightButton(object sender, RoutedEventArgs e)
        {
            lock (lockObj)
            {
                if (this.CurrentIndex > -1)
                {
                    foreach (var o in this.GridList)
                    {
                        if (o.Tag.ToString() == "0")
                        {
                            //o.SetValue(Panel.ZIndexProperty, -999);
                            o.BeginAnimation(Canvas.LeftProperty, null);
                            o.BeginAnimation(Canvas.TopProperty, null);
                            o.SetValue(Canvas.LeftProperty, -this.GridWidth);  //Canvas宽度
                            o.SetValue(Canvas.TopProperty, 30D);
                        }
                    }
                    //获取中间显示的Grid设置为默认高度、宽度设置为默认宽度并将位置移动到右侧，并设置其Opacity
                    Grid mainGridToRight = this.GridList[this.CurrentIndex];  //获取当前中间显示的图片
                    this.CreateCarouselCenterToRightStoryboard(mainGridToRight).Begin();
                    //将最左侧的Grid显示在中间
                    Grid leftGridToCenter = null;
                    if (this.CurrentIndex == 0)
                    {
                        leftGridToCenter = this.GridList[this.GridList.Count - 1];
                        this.PART_Navigation.Children[this.GridList.Count - 1].Focus();
                    }
                    else
                    {
                        leftGridToCenter = this.GridList[this.CurrentIndex - 1];
                        this.PART_Navigation.Children[this.CurrentIndex - 1].Focus();
                    }
                    this.CreateCarouselLeftToCenterStoryboard(leftGridToCenter).Begin();
                    //获取下一个Grid显示在左侧
                    Grid leftGridToLeft = null;
                    if (this.CurrentIndex == 0)
                    {
                        leftGridToLeft = this.GridList[this.GridList.Count - 2];
                    }
                    else if (this.CurrentIndex == 1)
                    {
                        leftGridToLeft = this.GridList[this.GridList.Count - 1];
                    }
                    else
                    {
                        leftGridToLeft = this.GridList[this.CurrentIndex - 2];
                    }
                    this.CreateZeroToLeftStoryboard(leftGridToLeft).Begin();
                    //获取右侧的Grid使其影藏
                    Grid rightGridToHide = this.CurrentIndex == this.GridList.Count - 1 ? this.GridList[0] : this.GridList[this.CurrentIndex + 1];
                    rightGridToHide.SetValue(Panel.ZIndexProperty, -999);
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(1000);
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            rightGridToHide.Tag = "0";
                            rightGridToHide.BeginAnimation(Canvas.LeftProperty, null);
                            rightGridToHide.BeginAnimation(Canvas.TopProperty, null);
                            rightGridToHide.SetValue(Canvas.LeftProperty, -this.GridWidth);  //Canvas宽度
                            rightGridToHide.SetValue(Canvas.TopProperty, 30D);
                        }));
                    });
                    if (this.CurrentIndex == 0) this.CurrentIndex = this.GridList.Count - 1; else this.CurrentIndex--;
                }
            }
        }

        #region 点击左侧按钮时3个区域需要处理的动画
        /// <summary>
        /// 创建从右到中动画
        /// </summary>
        /// <param name="targetSource">指定的控件</param>
        private Storyboard CreateCarouselRightToCenterStoryboard(Grid targetSource)
        {
            //设置当前控件为显示状态
            /**
             * 1、设置Canvas.LeftProperty值为 (Double)GridCanvasLeft；
             * 2、设置Canvas.TopProperty值为 (Double)0；
             * 3、设置Panel.ZIndexProperty值为1，将设置顶层显示
             * 4、设置其高度为正常显示高度，默认为控件显示高度
             * 5、设置其宽度为中间显示宽度，为正常宽度+150(默认后面可能会调整)
             * 6、设置Grid控件中的Rectangle的透明度为透明
             */
            targetSource.Tag = "1";
            targetSource.Width = this.GridWidth;
            targetSource.SetValue(Panel.ZIndexProperty, 3);
            Storyboard carouselRightToCenterStoryboard = new Storyboard();            //创建画板，目标一个从右边到中间运动的
            //-------------------------将Grid控件从右边向中间移动
            //添加一个Canvas.Left移动的关键帧
            var gridMoveToLeftKeyFrames = new DoubleAnimationUsingKeyFrames();
            gridMoveToLeftKeyFrames.KeyFrames.Add(new EasingDoubleKeyFrame(this.GridCanvasLeft, TimeSpan.FromSeconds(1)));
            //设置关键帧的目标源
            Storyboard.SetTarget(gridMoveToLeftKeyFrames, targetSource);
            //设置关键帧的目标属性
            Storyboard.SetTargetProperty(gridMoveToLeftKeyFrames, new PropertyPath("(Canvas.Left)"));
            //将动画添加到故事画板
            carouselRightToCenterStoryboard.Children.Add(gridMoveToLeftKeyFrames);
            //-------------------------Grid子控件Rectangle透明度的动画
            var rectangleOpacityKeyFrames = new DoubleAnimationUsingKeyFrames();
            rectangleOpacityKeyFrames.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(rectangleOpacityKeyFrames, targetSource.Children[1]);
            Storyboard.SetTargetProperty(rectangleOpacityKeyFrames, new PropertyPath("(UIElement.Opacity)"));
            carouselRightToCenterStoryboard.Children.Add(rectangleOpacityKeyFrames);
            //-------------------------Grid高度变化关键帧
            var gridHeightKeyFrames = new DoubleAnimationUsingKeyFrames();
            gridHeightKeyFrames.KeyFrames.Add(new EasingDoubleKeyFrame(this.Height, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(gridHeightKeyFrames, targetSource);
            Storyboard.SetTargetProperty(gridHeightKeyFrames, new PropertyPath("(FrameworkElement.Height)"));
            carouselRightToCenterStoryboard.Children.Add(gridHeightKeyFrames);
            //-------------------------Grid移动Canvas.Top关键帧
            var gridMoveToTopKeyFrames = new DoubleAnimationUsingKeyFrames();
            gridMoveToTopKeyFrames.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(gridMoveToTopKeyFrames, targetSource);
            Storyboard.SetTargetProperty(gridMoveToTopKeyFrames, new PropertyPath("(Canvas.Top)"));
            carouselRightToCenterStoryboard.Children.Add(gridMoveToTopKeyFrames);
            return carouselRightToCenterStoryboard; //返回当前创建的动画
        }
        /// <summary>
        /// 创建从中到左动画
        /// </summary>
        private Storyboard CreateCarouselCenterToLeftStoryboard(Grid targetSource)
        {
            targetSource.Tag = "1";
            targetSource.Width = this.GridWidth;
            targetSource.SetValue(Panel.ZIndexProperty, 2);
            Storyboard carouselCenterToLeftStoryboard = new Storyboard();
            //设置当前Grid在Canvas中距离顶部的位置
            var A = new DoubleAnimationUsingKeyFrames();
            A.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
            A.KeyFrames.Add(new EasingDoubleKeyFrame(30, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(A, targetSource);
            Storyboard.SetTargetProperty(A, new PropertyPath("(Canvas.Top)"));
            carouselCenterToLeftStoryboard.Children.Add(A);
            //设置当前Grid在Canvas中距离左侧的位置
            var B = new DoubleAnimationUsingKeyFrames();
            B.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(B, targetSource);
            Storyboard.SetTargetProperty(B, new PropertyPath("(Canvas.Left)"));
            carouselCenterToLeftStoryboard.Children.Add(B);
            //这只控件的透明度为半透明0.8
            var C = new DoubleAnimationUsingKeyFrames();
            C.KeyFrames.Add(new EasingDoubleKeyFrame(0.8, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(C, targetSource.Children[1]);
            Storyboard.SetTargetProperty(C, new PropertyPath("(UIElement.Opacity)"));
            carouselCenterToLeftStoryboard.Children.Add(C);
            //设置控件的高度为默认高度
            var D = new DoubleAnimationUsingKeyFrames();
            D.KeyFrames.Add(new EasingDoubleKeyFrame(this.GridHeight, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(D, targetSource);
            Storyboard.SetTargetProperty(D, new PropertyPath("(FrameworkElement.Height)"));
            carouselCenterToLeftStoryboard.Children.Add(D);
            return carouselCenterToLeftStoryboard;
        }
        /// <summary>
        /// 将右侧的下一个控件显示到C位置
        /// </summary>
        /// <param name="targetSource">指定的控件</param>
        /// <returns></returns>
        private Storyboard CreateZeroToRightStoryboard(Grid targetSource)
        {
            targetSource.Tag = "1";
            targetSource.SetValue(Panel.ZIndexProperty, 2);
            Storyboard carouselZeroToRightStoryboard = new Storyboard();
            //
            var A = new DoubleAnimationUsingKeyFrames();
            A.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
            A.KeyFrames.Add(new EasingDoubleKeyFrame(30, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(A, targetSource);
            Storyboard.SetTargetProperty(A, new PropertyPath("(Canvas.Top)"));
            carouselZeroToRightStoryboard.Children.Add(A);
            //
            var B = new DoubleAnimationUsingKeyFrames();
            B.KeyFrames.Add(new EasingDoubleKeyFrame(this.GridCanvasRight, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(B, targetSource);
            Storyboard.SetTargetProperty(B, new PropertyPath("(Canvas.Left)"));
            carouselZeroToRightStoryboard.Children.Add(B);
            return carouselZeroToRightStoryboard;
        }
        private Storyboard CreateLeftToZeroStoryboard(Grid targetSource)
        {
            targetSource.SetValue(Panel.ZIndexProperty, -999);
            Storyboard carouselLeftToZeroStoryboard = new Storyboard();
            var A = new DoubleAnimationUsingKeyFrames();
            A.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
            A.KeyFrames.Add(new EasingDoubleKeyFrame(2, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(A, targetSource);
            Storyboard.SetTargetProperty(A, new PropertyPath("(UIElement.Visibility)"));
            carouselLeftToZeroStoryboard.Children.Add(A);
            return carouselLeftToZeroStoryboard;
        }

        #endregion

        #region 点击右侧按钮时3个区域需要处理的动画
        /// <summary>
        /// 创建从左到中动画
        /// </summary>
        /// <param name="targetSource">指定的控件</param>
        private Storyboard CreateCarouselLeftToCenterStoryboard(Grid targetSource)
        {
            /*
             * A区域图像向B区域移动动画
             * 1、设置Canvas.Left为B区域的GridCanvasLeft
             * 2、设置Canvas.Top为B区域的0
             * 3、设置图像的透明度为0
             * 4、设置控件的宽度为this.MainWidth
             * 5、设置控件的高度为当前控件的高度this.Height
             * 6、设置当前图片为顶层图像
             * */
            targetSource.Tag = "1";
            targetSource.Width = this.GridWidth;
            targetSource.SetValue(Panel.ZIndexProperty, 3);
            Storyboard carouselAToBStoryboard = new Storyboard();            //创建画板，A区域向B区域移动动画
            //添加一个Canvas.Left移动的关键帧
            var gridMoveToLeftKeyFrames = new DoubleAnimationUsingKeyFrames();
            gridMoveToLeftKeyFrames.KeyFrames.Add(new EasingDoubleKeyFrame(this.GridCanvasLeft, TimeSpan.FromSeconds(1)));
            //设置关键帧的目标源
            Storyboard.SetTarget(gridMoveToLeftKeyFrames, targetSource);
            //设置关键帧的目标属性
            Storyboard.SetTargetProperty(gridMoveToLeftKeyFrames, new PropertyPath("(Canvas.Left)"));
            //将动画添加到故事画板
            carouselAToBStoryboard.Children.Add(gridMoveToLeftKeyFrames);
            //-------------------------Grid子控件Rectangle透明度的动画
            var rectangleOpacityKeyFrames = new DoubleAnimationUsingKeyFrames();
            rectangleOpacityKeyFrames.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(rectangleOpacityKeyFrames, targetSource.Children[1]);
            Storyboard.SetTargetProperty(rectangleOpacityKeyFrames, new PropertyPath("(UIElement.Opacity)"));
            carouselAToBStoryboard.Children.Add(rectangleOpacityKeyFrames);
            //-------------------------Grid高度变化关键帧
            var gridHeightKeyFrames = new DoubleAnimationUsingKeyFrames();
            gridHeightKeyFrames.KeyFrames.Add(new EasingDoubleKeyFrame(this.Height, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(gridHeightKeyFrames, targetSource);
            Storyboard.SetTargetProperty(gridHeightKeyFrames, new PropertyPath("(FrameworkElement.Height)"));
            carouselAToBStoryboard.Children.Add(gridHeightKeyFrames);
            //-------------------------Grid移动Canvas.Top关键帧
            var gridMoveToTopKeyFrames = new DoubleAnimationUsingKeyFrames();
            gridMoveToTopKeyFrames.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(gridMoveToTopKeyFrames, targetSource);
            Storyboard.SetTargetProperty(gridMoveToTopKeyFrames, new PropertyPath("(Canvas.Top)"));
            carouselAToBStoryboard.Children.Add(gridMoveToTopKeyFrames);
            return carouselAToBStoryboard; //返回当前创建的动画

        }

        private Storyboard CreateCarouselCenterToRightStoryboard(Grid targetSource)
        {
            targetSource.Tag = "1";
            targetSource.Width = this.GridWidth;
            targetSource.SetValue(Panel.ZIndexProperty, 2);
            Storyboard carouselBToCStoryboard = new Storyboard();
            //设置当前Grid在Canvas中距离顶部的位置
            var A = new DoubleAnimationUsingKeyFrames();
            A.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
            A.KeyFrames.Add(new EasingDoubleKeyFrame(30, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(A, targetSource);
            Storyboard.SetTargetProperty(A, new PropertyPath("(Canvas.Top)"));
            carouselBToCStoryboard.Children.Add(A);
            //设置当前Grid在Canvas中距离左侧的位置
            var B = new DoubleAnimationUsingKeyFrames();
            B.KeyFrames.Add(new EasingDoubleKeyFrame(this.GridCanvasRight, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(B, targetSource);
            Storyboard.SetTargetProperty(B, new PropertyPath("(Canvas.Left)"));
            carouselBToCStoryboard.Children.Add(B);
            //这只控件的透明度为半透明0.8
            var C = new DoubleAnimationUsingKeyFrames();
            C.KeyFrames.Add(new EasingDoubleKeyFrame(0.8, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(C, targetSource.Children[1]);
            Storyboard.SetTargetProperty(C, new PropertyPath("(UIElement.Opacity)"));
            carouselBToCStoryboard.Children.Add(C);
            //设置控件的高度为默认高度
            var D = new DoubleAnimationUsingKeyFrames();
            D.KeyFrames.Add(new EasingDoubleKeyFrame(this.GridHeight, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(D, targetSource);
            Storyboard.SetTargetProperty(D, new PropertyPath("(FrameworkElement.Height)"));
            carouselBToCStoryboard.Children.Add(D);
            return carouselBToCStoryboard;
        }

        private Storyboard CreateZeroToLeftStoryboard(Grid targetSource)
        {
            targetSource.Tag = "1";
            targetSource.SetValue(Panel.ZIndexProperty, 2);
            Storyboard carouselZeroToRightStoryboard = new Storyboard();
            //
            var A = new DoubleAnimationUsingKeyFrames();
            A.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
            A.KeyFrames.Add(new EasingDoubleKeyFrame(30, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(A, targetSource);
            Storyboard.SetTargetProperty(A, new PropertyPath("(Canvas.Top)"));
            carouselZeroToRightStoryboard.Children.Add(A);
            //
            var B = new DoubleAnimationUsingKeyFrames();
            B.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(1)));
            Storyboard.SetTarget(B, targetSource);
            Storyboard.SetTargetProperty(B, new PropertyPath("(Canvas.Left)"));
            carouselZeroToRightStoryboard.Children.Add(B);
            return carouselZeroToRightStoryboard;
        }
        #endregion

        #endregion
    }

    public class Carousel
    {
        public Carousel(Uri navigationIconUri, Uri displayPanelIamgeUri)
        {
            this.NavigationIcon = new BitmapImage(navigationIconUri);
            this.DisplayPanelImage = new BitmapImage(displayPanelIamgeUri);
        }
        public ImageSource NavigationIcon { get; private set; }  //导航图标
        public ImageSource DisplayPanelImage { get; private set; } //显示面板图片
    }
}
