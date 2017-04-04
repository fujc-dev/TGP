using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TGP.UI.Core.Controls
{
	/// <summary>
	/// RollImg.xaml 的交互逻辑
	/// </summary>
	public partial class RollImg : UserControl
	{

		public RollImg()
		{
			InitializeComponent();
		}


		/// <summary>  
		/// 是否开始滚动  
		/// </summary>  
		public bool isBegin = false;

		/// <summary>  
		/// 本轮剩余滚动数  
		/// </summary>  
		public int rollNum = 0;

		private List<BitmapImage> _ls_images;
		/// <summary>  
		/// 滚动图片组  
		/// </summary>  
		public List<BitmapImage> ls_images
		{
			set
			{
				if (rollNum > 0)
				{
					// 本轮滚动未结束  
				}
				else
				{
					// 开始新的一轮滚动  
					_ls_images = value;
					rollNum = _ls_images.Count();
				}
			}
			get { return _ls_images; }
		}

		/// <summary>  
		/// 滚动宽度  
		/// </summary>  
		public double width
		{
			get { return this.canvas_board.Width; }
		}

		/// <summary>  
		/// 滚动高度  
		/// </summary>  
		public double height
		{
			get { return this.canvas_board.Height; }
		}

		private int n_index = 0;    // 滚动索引  

		private Storyboard _storyboard_R2L;
		/// <summary>  
		/// 滚动动画板  
		/// </summary>  
		public Storyboard storyboard_R2L
		{
			get
			{
				if (_storyboard_R2L == null)
				{
					_storyboard_R2L = new Storyboard();
					DoubleAnimationUsingKeyFrames daukf_img1 = new DoubleAnimationUsingKeyFrames();
					LinearDoubleKeyFrame k1_img1 = new LinearDoubleKeyFrame(0.0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(5)));
					LinearDoubleKeyFrame k2_img1 = new LinearDoubleKeyFrame(-this.width, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(8)));
					daukf_img1.KeyFrames.Add(k1_img1);
					daukf_img1.KeyFrames.Add(k2_img1);
					_storyboard_R2L.Children.Add(daukf_img1);
					Storyboard.SetTarget(daukf_img1, this.image1);
					Storyboard.SetTargetProperty(daukf_img1, new PropertyPath("(Canvas.Left)"));

					DoubleAnimationUsingKeyFrames daukf_img2 = new DoubleAnimationUsingKeyFrames();
					LinearDoubleKeyFrame k1_img2 = new LinearDoubleKeyFrame(this.width, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(5)));
					LinearDoubleKeyFrame k2_img2 = new LinearDoubleKeyFrame(0.0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(8)));
					daukf_img2.KeyFrames.Add(k1_img2);
					daukf_img2.KeyFrames.Add(k2_img2);
					_storyboard_R2L.Children.Add(daukf_img2);
					Storyboard.SetTarget(daukf_img2, this.image2);
					Storyboard.SetTargetProperty(daukf_img2, new PropertyPath("(Canvas.Left)"));

					_storyboard_R2L.FillBehavior = FillBehavior.Stop;
					_storyboard_R2L.Completed += new EventHandler(storyboard_Completed);
				}
				return _storyboard_R2L;
			}
		}

		void storyboard_Completed(object sender, EventArgs e)
		{
			rollNum--;

			// 显示图片  
			this.ResetStory();

			// 继续下轮动画  
			storyboard_R2L.Begin();
		}

		/// <summary>  
		/// 开始滚动动画  
		/// </summary>  
		public void Begin()
		{
			if (!isBegin)
			{
				isBegin = true;

				// 显示图片  
				this.ResetStory();

				// 开始动画  
				storyboard_R2L.Begin();
			}
		}

		/// <summary>  
		/// 初始化动画版，显示动画中的图片  
		/// </summary>  
		void ResetStory()
		{
			// 复位  
			this.image1.SetValue(Canvas.LeftProperty, 0.0);
			this.image2.SetValue(Canvas.LeftProperty, this.width);
			// 显示图片  
			if (this.ls_images.Count > 0)
			{
				try
				{
					this.image1.Source = this.ls_images[this.n_index++ % this.ls_images.Count];
					this.image2.Source = this.ls_images[this.n_index % this.ls_images.Count];
				}
				catch (Exception ex)
				{
					this.image1.Source = new BitmapImage();
					this.image2.Source = new BitmapImage();
				}
			}
			else
			{
				this.image1.Source = new BitmapImage();
				this.image2.Source = new BitmapImage();
			}
		}
	}
}

