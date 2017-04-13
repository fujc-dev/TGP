using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
	/// WPF轮播控件
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
		 * 
		 */

		#region 数据源
		public ObservableCollection<Carousel> CarouselSource
		{
			get { return (ObservableCollection<Carousel>)GetValue(CarouselSourceProperty); }
			set { SetValue(CarouselSourceProperty, value); }
		}
		public static readonly DependencyProperty CarouselSourceProperty = DependencyProperty.Register("CarouselSource", typeof(ObservableCollection<Carousel>), typeof(GCarousel), new PropertyMetadata(null));

		#endregion

		public Double GridWidth
		{
			get { return (Double)GetValue(GridWidthProperty); }
			set { SetValue(GridWidthProperty, value); }
		}
		public static readonly DependencyProperty GridWidthProperty = DependencyProperty.Register("GridWidth", typeof(Double), typeof(GCarousel), new PropertyMetadata(270D));


		public Double GridHeight
		{
			get { return (Double)GetValue(GridHeightProperty); }
			set { SetValue(GridHeightProperty, value); }
		}
		public static readonly DependencyProperty GridHeightProperty = DependencyProperty.Register("GridHeight", typeof(Double), typeof(GCarousel), new PropertyMetadata(195D));






		#region 每当应用程序代码或内部进程调用 System.Windows.FrameworkElement.ApplyTemplate，都将调用此方法。

		private const String TGP_PART_Navigation = "PART_Navigation";

		private const String TGP_PART_Left = "PART_Left";
		private const String TGP_PART_Right = "PART_Right";
		private const String TGP_PART_Inner = "PART_Inner";

		private StackPanel PART_Navigation;
		private Button PART_Left, PART_Right;
		private Canvas PART_Inner;
		private Int32 CurrentIndex = 0;
		private Double MainWidth = 0, GridCanvasLeft = 0, GridCanvasRight = 0;
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
			//
			this.OnNormalLoad();
			//生成Grid集合
			this.OnInitializeBindGridList();
			//依次生成按钮
			this.OnInitializeBindButtonList();
			//初始化时显示3张图片，取第一张，第二张以及最后一张，并绑定到控件上
			if (this.CarouselSource.Count >= 3) //图片必须大于3张
			{
				// 获取当前画板的高度与宽度
				var canvasWidth = this.Width;
				var canvasHeight = this.Height;
				//强制必须让这个画板宽度大于两个Grid的宽度
				this.MainWidth = this.GridWidth + 150;
				if (canvasWidth >= GridWidth * 2)
				{
					//计算中间显示图片的位置
					this.GridCanvasLeft = (canvasWidth - this.MainWidth) / 2;
					//计算最右边显示图片的位置
					var gridRightWidth = GridWidth - (canvasWidth - this.GridCanvasLeft - this.MainWidth);   //获取剩余的显示宽度
					this.GridCanvasRight = (canvasWidth - this.MainWidth) / 2 + this.MainWidth - gridRightWidth;

					//设置第一个显示值
					this.GridList[this.GridList.Count - 1].Visibility = Visibility.Visible;
					this.GridList[this.GridList.Count - 1].SetValue(Canvas.LeftProperty, (Double)0);
					this.GridList[this.GridList.Count - 1].SetValue(Canvas.TopProperty, (Double)30);

					//设置中间显示
					this.GridList[0].Visibility = Visibility.Visible;
					this.GridList[0].SetValue(Canvas.LeftProperty, (Double)this.GridCanvasLeft);
					this.GridList[0].SetValue(Canvas.TopProperty, (Double)0);
					this.GridList[0].SetValue(Panel.ZIndexProperty, (Int32)1);
					this.GridList[0].Height = this.Height;
					this.GridList[0].Width = this.MainWidth;
					this.GridList[0].Children[1].Opacity = 0;

					//设置最右边显示值
					this.GridList[1].Visibility = Visibility.Visible;
					this.GridList[1].SetValue(Canvas.LeftProperty, (Double)GridCanvasRight);
					this.GridList[1].SetValue(Canvas.TopProperty, (Double)30);
					this.PART_Left.Click += OnPressedLeftButton;
					this.PART_Right.Click += OnPressedRightButton;
				}
			}
			base.OnApplyTemplate();
		}


		//默认加载
		private void OnNormalLoad()
		{
			//
			if (this.CarouselSource == null)
			{
				this.CarouselSource = new ObservableCollection<Carousel>();
				var imageUri = new Uri("pack://application:,,,/TGP.WindowDemo;component/Carousel/201703211700_1180x500.jpg", UriKind.RelativeOrAbsolute);   //
				var carousel = new Carousel(imageUri, imageUri); this.CarouselSource.Add(carousel);
				imageUri = new Uri("pack://application:,,,/TGP.WindowDemo;component/Carousel/201703231056_1180x500.jpg", UriKind.RelativeOrAbsolute);   //
				carousel = new Carousel(imageUri, imageUri); this.CarouselSource.Add(carousel);
				imageUri = new Uri("pack://application:,,,/TGP.WindowDemo;component/Carousel/201703280934_1180x500.jpg", UriKind.RelativeOrAbsolute);   //
				carousel = new Carousel(imageUri, imageUri); this.CarouselSource.Add(carousel);
				imageUri = new Uri("pack://application:,,,/TGP.WindowDemo;component/Carousel/201704011731_1180x500.jpg", UriKind.RelativeOrAbsolute);   //
				carousel = new Carousel(imageUri, imageUri); this.CarouselSource.Add(carousel);
				imageUri = new Uri("pack://application:,,,/TGP.WindowDemo;component/Carousel/201704051917_1180x500.jpg", UriKind.RelativeOrAbsolute);   //
				carousel = new Carousel(imageUri, imageUri); this.CarouselSource.Add(carousel);
				imageUri = new Uri("pack://application:,,,/TGP.WindowDemo;component/Carousel/201704101655_1180x500.jpg", UriKind.RelativeOrAbsolute);   //
				carousel = new Carousel(imageUri, imageUri); this.CarouselSource.Add(carousel);
			}
		}
		//构建需要的Button按钮对象
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
		//构建需要的Grid对象
		private void OnInitializeBindGridList()
		{
			//<Grid x:Name="PART_LeftGrid" Height="150" Width="175" Canvas.Top="30">
			//    <Image x:Name="PART_LeftImage"  Source="/TGP.WindowDemo;component/Carousel/201703211700_1180x500.jpg" Stretch="Fill" Margin="0"/>
			//    <Rectangle Fill="Black" Height="Auto" Margin="0" Stroke="Black" Opacity="0.8"/>
			//</Grid>
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
				_Grid.Visibility = Visibility.Collapsed;
				_Grid.SetValue(Panel.ZIndexProperty, -999);
				this.PART_Inner.Children.Add(_Grid);
				this.GridList.Add(_Grid);
			}
		}
		private static Object lockObj = new Object();
		private void OnNavigationClick(object sender, RoutedEventArgs e)
		{

			var currentButton = sender as GButton;
			this.CurrentIndex = (Int32)currentButton.Tag;

		}

		private void OnPressedLeftButton(object sender, RoutedEventArgs e)
		{
			lock (lockObj)
			{
				if (CurrentIndex < this.GridList.Count)  //总数为6，当CurrentIndex为5
				{
					//1、将当前左侧的Grid影藏
					//获取当前左侧的Grid
					Grid leftGrid = this.CurrentIndex == 0 ? this.GridList[this.GridList.Count - 1] : this.GridList[this.CurrentIndex - 1];
					leftGrid.Visibility = Visibility.Collapsed;
					leftGrid.SetValue(Panel.ZIndexProperty, -999);
					//
					//2、将当前中间显示Grid高度设置为默认高度、宽度设置为默认宽度并将位置移动到左上角，并设置其Opacity
					Grid manGrid = this.GridList[this.CurrentIndex];
					manGrid.Visibility = System.Windows.Visibility.Visible;
					manGrid.SetValue(Canvas.LeftProperty, (Double)0);
					manGrid.SetValue(Canvas.TopProperty, (Double)30);
					manGrid.SetValue(Panel.ZIndexProperty, -999);
					manGrid.Children[1].Opacity = 0.8;
					//3、将右侧的Grid显示为中间
					Grid rightGrid = this.CurrentIndex == this.GridList.Count - 1 ? this.GridList[0] : this.GridList[this.CurrentIndex + 1];
					rightGrid.Visibility = Visibility.Visible;
					rightGrid.SetValue(Canvas.LeftProperty, (Double)GridCanvasLeft);
					rightGrid.SetValue(Canvas.TopProperty, (Double)0);
					rightGrid.SetValue(Panel.ZIndexProperty, 1);
					rightGrid.Height = this.Height;
					rightGrid.Width = this.MainWidth;
					rightGrid.Children[1].Opacity = 0;
					//4、显示右侧的图片
					Grid rightGridToCenter = null;
					if (this.CurrentIndex == this.GridList.Count - 1) { rightGridToCenter = this.GridList[1]; }
					else if (this.CurrentIndex == this.GridList.Count - 2) { rightGridToCenter = this.GridList[0]; }
					else { rightGridToCenter = this.GridList[this.CurrentIndex + 2]; }
					rightGridToCenter.Visibility = Visibility.Visible;
					rightGridToCenter.SetValue(Canvas.LeftProperty, (Double)GridCanvasRight);
					rightGridToCenter.SetValue(Canvas.TopProperty, (Double)30);
					if (this.CurrentIndex == this.GridList.Count - 1) this.CurrentIndex = 0; else this.CurrentIndex++;
				}
			}
		}
		private void OnPressedRightButton(object sender, RoutedEventArgs e)
		{
			lock (lockObj)
			{
				if (this.CurrentIndex > -1)
				{
					//获取右侧的Grid使其影藏
					Grid rightGridToHide = this.CurrentIndex == this.GridList.Count - 1 ? this.GridList[0] : this.GridList[this.CurrentIndex + 1];
					rightGridToHide.Visibility = Visibility.Collapsed;
					rightGridToHide.SetValue(Panel.ZIndexProperty, -999);
					//获取中间显示的Grid设置为默认高度、宽度设置为默认宽度并将位置移动到右侧，并设置其Opacity
					Grid mainGridToRight = this.GridList[this.CurrentIndex];
					mainGridToRight.Visibility = System.Windows.Visibility.Visible;
					mainGridToRight.SetValue(Canvas.LeftProperty, (Double)GridCanvasRight);
					mainGridToRight.SetValue(Canvas.TopProperty, (Double)30);
					mainGridToRight.SetValue(Panel.ZIndexProperty, -999);
					mainGridToRight.Children[1].Opacity = 0.8;
					//将最左侧的Grid显示在中间
					Grid leftGridToCenter = this.CurrentIndex == 0 ? this.GridList[this.GridList.Count - 1] : this.GridList[this.CurrentIndex - 1];
					leftGridToCenter.Visibility = Visibility.Visible;
					leftGridToCenter.SetValue(Canvas.LeftProperty, (Double)GridCanvasLeft);
					leftGridToCenter.SetValue(Canvas.TopProperty, (Double)0);
					leftGridToCenter.SetValue(Panel.ZIndexProperty, 1);
					leftGridToCenter.Height = this.Height;
					leftGridToCenter.Width = this.MainWidth;
					leftGridToCenter.Children[1].Opacity = 0;
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
					leftGridToLeft.Visibility = Visibility.Visible;
					leftGridToLeft.SetValue(Canvas.LeftProperty, (Double)0);
					leftGridToLeft.SetValue(Canvas.TopProperty, (Double)30);
					if (this.CurrentIndex == 0) this.CurrentIndex = this.GridList.Count - 1; else this.CurrentIndex--;
				}

			}
		}
		/// <summary>
		/// 创建从右到中动画
		/// </summary>
		private void OnCarouselRightToCenterStoryboard()
		{
			Storyboard CarouselCenterToLeftStoryboard = new Storyboard();			//
			var doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
			var keyFrames = doubleAnimationUsingKeyFrames.KeyFrames;
			keyFrames.Add(new EasingDoubleKeyFrame(130, TimeSpan.FromSeconds(1)));

		}
		/// <summary>
		/// 创建从中到左动画
		/// </summary>
		private void OnCarouselCenterToLeftStoryboard()
		{

		}
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
