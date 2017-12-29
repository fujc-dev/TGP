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

namespace MIS.ClientUI.Controls
{
	/// <summary>
	/// 分页控件
	/// </summary>
	[TemplatePart(Name = MISPager.MIS_PART_CONTENT, Type = typeof(StackPanel))]
	[TemplatePart(Name = MISPager.MIS_PART_PREVIOUSPAGE, Type = typeof(MISImageButton))]
	[TemplatePart(Name = MISPager.MIS_PART_NEXTPAGE, Type = typeof(MISImageButton))]
	[TemplatePart(Name = MISPager.MIS_PART_COUNT, Type = typeof(TextBlock))]
	[TemplatePart(Name = MISPager.MIS_PART_PAGEINDEX, Type = typeof(TextBlock))]
	public partial class MISPager : Control
	{
		private const String MIS_PART_CONTENT = "PART_Content";
		private const String MIS_PART_PREVIOUSPAGE = "PART_Previouspage";
		private const String MIS_PART_NEXTPAGE = "PART_Nextpage";
		private const String MIS_PART_COUNT = "PART_Count";
		private const String MIS_PART_PAGEINDEX = "PART_PageIndex";

		private MISImageButton PART_Nextpage;  //下一页事件
		private MISImageButton PART_Previouspage; //上一页事件
		private StackPanel PART_Content;  //子页码
		private TextBlock PART_Count;
		private TextBlock PART_PageIndex;

		private PagerType mPagerType = PagerType.Default;  //当前分页控件类型，复杂、默认
		private List<Int32> mCurrentPagers = new List<Int32>(); //当前分页控件显示的页码索引
		private Boolean mCurrentIsAddEllipsisCtrl = false;  //当前是否已添加省略号控件(当前还是可以直接在集合控件中比对)
		
        public MISPager()
		{

		}

		//初始化控件时调用的系统方法
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this.PART_Content = this.GetTemplateChild(MISPager.MIS_PART_CONTENT) as StackPanel;
			this.PART_Nextpage = this.GetTemplateChild(MISPager.MIS_PART_NEXTPAGE) as MISImageButton;
			this.PART_Previouspage = this.GetTemplateChild(MISPager.MIS_PART_PREVIOUSPAGE) as MISImageButton;
			this.PART_Count = this.GetTemplateChild(MISPager.MIS_PART_COUNT) as TextBlock;
			this.PART_PageIndex = this.GetTemplateChild(MISPager.MIS_PART_PAGEINDEX) as TextBlock;
			//计算页码数
			this.PageCount = (Int32)Math.Ceiling((Double)this.Total / (Double)this.PageSize);
			this.PART_Count.Text = this.Total.ToString();
			//当总页码小于7页，显示1、2、3、4、5、6、7
			if (this.PageCount <= 7)
			{
				this.mPagerType = PagerType.Default;
				for (int i = 0; i < this.PageCount; i++)
				{
					var misImgBtn = new MISLinkButton()
					{
						Content = (i + 1).ToString(),
						Width = 35,
						BorderThickness = new Thickness(1, 0, 0, 0),
						Style = Application.Current.FindResource("DefaultLinkButton2Style") as Style
					};
					this.mCurrentPagers.Add((i + 1));
					misImgBtn.Click += OnMisImgBtn_Click;
					if (this.PART_Content != null)
					{
						this.PART_Content.Children.Add(misImgBtn);
					}
				}
			}
			else
			{
				this.mPagerType = PagerType.Complex;
				for (int i = 0; i < 5; i++)
				{
					var misImgBtn = new MISLinkButton() { Content = (i + 1).ToString(), Width = 35, BorderThickness = new Thickness(1, 0, 0, 0), Style = Application.Current.FindResource("DefaultLinkButton2Style") as Style };
					misImgBtn.Click += OnMisImgBtn_Click;
					if (i.Equals(0)) misImgBtn.Tag = 0;  //设置左控制点
					if (i.Equals(4)) misImgBtn.Tag = 5;  //设置右控制点
					this.mCurrentPagers.Add((i + 1));
					if (this.PART_Content != null)
					{
						this.PART_Content.Children.Add(misImgBtn);
					}
				}
				this.PART_Content.Children.Add(new MISLinkButton() { Content = "...", Width = 35, BorderThickness = new Thickness(1, 0, 0, 0), Style = Application.Current.FindResource("DefaultLinkButton3Style") as Style });
				this.PART_Content.Children.Add(new MISLinkButton() { Content = this.PageCount.ToString(), Width = 35, BorderThickness = new Thickness(1, 0, 0, 0), Style = Application.Current.FindResource("DefaultLinkButton2Style") as Style });
			}
			this.SetLinkButtonFocus(0);
			this._SetNextpageAndPreviouspageState();
			if (this.PART_Previouspage != null)
			{
				this.PART_Previouspage.Click += OnPART_Previouspage_Click;
			}
			if (this.PART_Nextpage != null)
			{
				this.PART_Nextpage.Click += OnPART_Nextpage_Click;
			}
		}

		#region 依赖属性

		#region 当前DataGrid显示的数据总条数，用于计算页码数
		/// <summary>
		/// 当前DataGrid显示的数据总条数，用于计算页码数
		/// </summary>
		public Int32 Total
		{
			get { return (Int32)GetValue(TotalProperty); }
			set { SetValue(TotalProperty, value); }
		}

		public static readonly DependencyProperty TotalProperty =
			DependencyProperty.Register("Total", typeof(Int32), typeof(MISPager), new PropertyMetadata(0));

		#endregion

		#region 当前DataGrid每页显示条数，用于计算页码数
		/// <summary>
		/// 每页显示条数
		/// </summary>
		public Int32 PageSize
		{
			get { return (Int32)GetValue(PageSizeProperty); }
			set { SetValue(PageSizeProperty, value); }
		}

		public static readonly DependencyProperty PageSizeProperty =
			DependencyProperty.Register("PageSize", typeof(Int32), typeof(MISPager), new PropertyMetadata(10));


		#endregion

		#region 当前DataGrid当前页码索引

		/// <summary>
		/// 页码索引
		/// </summary>
		public Int32 PageIndex
		{
			get { return (Int32)GetValue(PageIndexProperty); }
			set { SetValue(PageIndexProperty, value); }
		}

		public static readonly DependencyProperty PageIndexProperty =
			DependencyProperty.Register("PageIndex", typeof(Int32), typeof(MISPager), new FrameworkPropertyMetadata(1));


		#endregion

		#region 当前DataGrid总页数
		/// <summary>
		/// 页码数
		/// </summary>
		public Int32 PageCount
		{
			get { return (Int32)GetValue(PageCountProperty); }
			set { SetValue(PageCountProperty, value); }
		}

		public static readonly DependencyProperty PageCountProperty =
			DependencyProperty.Register("PageCount", typeof(Int32), typeof(MISPager), new PropertyMetadata(0));

		#endregion

		#endregion

		#region 路由事件

		//注册分页路由事件
		public static readonly RoutedEvent PageChangedEvent = EventManager.RegisterRoutedEvent("PageChanged",
			RoutingStrategy.Bubble, typeof(EventHandler<PageChangedEventArgs>), typeof(MISPager));


		public event EventHandler<PageChangedEventArgs> PageChanged
		{
			add
			{
				this.AddHandler(PageChangedEvent, value);
			}
			remove
			{
				this.RemoveHandler(PageChangedEvent, value);
			}
		}


		#endregion

		#region 私有方法

		/// <summary>
		/// 计算当前选中的分页按钮的索引
		/// </summary>
		private Int32 CalculationCurrentSelectPagerButtonWithIndex()
		{
			//当前控件显示的页码集合
			return this.mCurrentPagers.FindIndex((o) => { return o == this.PageIndex; });
		}
		/// <summary>
		/// 维护当前分页控件显示的页码数据
		/// </summary>
		/// <param name="addSubtract"></param>
		private void _MaintainCurrentPagers(AddSubtract addSubtract)
		{
			if (addSubtract == AddSubtract.Add)
			{
				for (int i = 0; i < this.mCurrentPagers.Count; i++)
				{
					this.mCurrentPagers[i] = this.mCurrentPagers[i] + 1;
				}
			}
			if (addSubtract == AddSubtract.subtract)
			{
				for (int i = 0; i < this.mCurrentPagers.Count; i++)
				{
					this.mCurrentPagers[i] = this.mCurrentPagers[i] - 1;
				}
			}

		}
		/// <summary>
		/// 下一页
		/// </summary>
		private void OnPART_Nextpage_Click(object sender, RoutedEventArgs e)
		{
			var _index = this.CalculationCurrentSelectPagerButtonWithIndex() + 1;
			this.PageIndex++;
			this._SetNextpageAndPreviouspageState();
			if (this.mPagerType == PagerType.Complex) //复杂分页有效
			{
				// _index == 4 时为右侧控制点
				if (_index == 4)
				{
					if (this.PageIndex == this.PageCount - 1)
					{
						this.PART_Nextpage.IsEnabled = false; //设置下一页不可用
					}
					//检测当前是否已添加省略号控件
					if (!this.mCurrentIsAddEllipsisCtrl)
					{
						this.mCurrentIsAddEllipsisCtrl = true;
						//在翻页控件第一个位置添加一个省略号控件
						this.PART_Content.Children.Insert(0, new MISLinkButton() { Content = "...", Width = 35, BorderThickness = new Thickness(1, 0, 0, 0), Style = Application.Current.FindResource("DefaultLinkButton3Style") as Style });
					}
					//刷新UI(所有的分页控件加1)
					this._RefreshPager(AddSubtract.Add);
					this._MaintainCurrentPagers(AddSubtract.Add);
				}
				else
				{
					this.SetLinkButtonFocus(_index);
				}
			}
			else
			{
				//if (this.PageIndex == this.PageCount ) return;
				this.SetLinkButtonFocus(_index);
			}

		}
		/// <summary>
		/// 上一页
		/// </summary>
		private void OnPART_Previouspage_Click(object sender, RoutedEventArgs e)
		{
			//当前PageIndex在界面上显示的索引,用于判断控制点   
			var _index = this.CalculationCurrentSelectPagerButtonWithIndex() - 1;
			this.PageIndex--;
			this._SetNextpageAndPreviouspageState();
			if (this.mPagerType == PagerType.Complex)  //复杂分页有效
			{
				if (this.PageIndex == 1)
				{
					if (this.mCurrentIsAddEllipsisCtrl)
					{
						this.mCurrentIsAddEllipsisCtrl = false;
						this.PART_Content.Children.RemoveAt(0);
						this.SetLinkButtonFocus(0);
					}
					return;
				}
				if (_index == 0) //当前位置在左控制点时
				{
					//刷新UI(所有的分页控件减1)
					this._RefreshPager(AddSubtract.subtract);
					this._MaintainCurrentPagers(AddSubtract.subtract);
				}
				else
				{
					this.SetLinkButtonFocus(_index);
				}
			}
			else
			{
				//if (this.PageIndex == 1) return;
				this.SetLinkButtonFocus(_index);
			}
		}

		private void SetLinkButtonFocus(Int32 index)
		{
			if (this.mCurrentIsAddEllipsisCtrl) //包含省略号控件
			{
				this.PART_Content.Children[index + 1].Focus();
			}
			else
			{
				this.PART_Content.Children[index].Focus();
			}
		}

		protected virtual void OnPageChanged()
		{
			var eventArgs = new PageChangedEventArgs(this.PageIndex) { RoutedEvent = PageChangedEvent, Source = this };
			this.RaiseEvent(eventArgs);
		}

		private void _RefreshPager(AddSubtract addSubtract)
		{
			/*
			 * 1、默认分页的按钮为7个
			 * 2、当分页总数小于等于7时，直接显示1-7个分页按钮
			 * 3、当分页总数大于7时，显示当时为1、2、3、4、5、...、999(999为总页数)
			 * 4、
			 * **/
			if (this.PART_Content.Children.Count > 0)
			{
				int _index = 0;  //
				int _contentCount = this.PART_Content.Children.Count;
				if (this.mCurrentIsAddEllipsisCtrl) //当前包含前缀省略号控件
				{
					_index = 1;
					_contentCount = _contentCount - 1;
				}
				for (int i = 0; i < _contentCount - 2; i++)
				{
					var misLinkBtn = this.PART_Content.Children[_index] as MISLinkButton;
					if (misLinkBtn != null)
					{
						misLinkBtn.Content = addSubtract == AddSubtract.Add ? (Convert.ToInt32(misLinkBtn.Content) + 1).ToString() : (Convert.ToInt32(misLinkBtn.Content) - 1).ToString();
					}
					_index++;
				}
				if (addSubtract == AddSubtract.Add)
				{
					//设置倒数第一个按钮会选中状态
					this.PART_Content.Children[_index - 2].Focus();
				}
				else
				{   //设置第二个按钮会选中状态
					if (this.mCurrentIsAddEllipsisCtrl)
					{
						this.PART_Content.Children[2].Focus();
					}
					else
					{
						this.PART_Content.Children[1].Focus();
					}
				}
			}







		}

		/// <summary>
		/// 设置上一页下一页按钮显示状态
		/// </summary>
		private void _SetNextpageAndPreviouspageState()
		{
			if (this.PageIndex == 1)
			{
				this.PART_Previouspage.IsEnabled = false;
			}
			if (this.PageIndex > 1)
			{
				this.PART_Previouspage.IsEnabled = true;
				this.PART_Nextpage.IsEnabled = true;
			}
			if (this.mPagerType == PagerType.Complex)
			{
				if (this.PageIndex == this.PageCount - 1)
				{
					this.PART_Previouspage.IsEnabled = true;
					this.PART_Nextpage.IsEnabled = false;
				}
			}
			else
			{
				if (this.PageIndex == this.PageCount)
				{
					this.PART_Previouspage.IsEnabled = true;
					this.PART_Nextpage.IsEnabled = false;
				}
			}
			this.PART_PageIndex.Text = this.PageIndex.ToString();
		}
		/// <summary>
		/// 页码索引点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnMisImgBtn_Click(object sender, RoutedEventArgs e)
		{
			//获取当前点击的PageIndex
			var misImgBtn = sender as MISLinkButton;
			this.PageIndex = Convert.ToInt32(misImgBtn.Content);
			this._SetNextpageAndPreviouspageState();
			//当为复杂控件时处理
			if (this.mPagerType == PagerType.Complex)
			{
				this._RefreshPager(misImgBtn);
			}
			//执行路由回调
			OnPageChanged();
		}

		private void _RefreshPager(MISLinkButton misImgBtn)
		{
			//对比点击的控件
			if (misImgBtn.Tag != null)
			{
				if (misImgBtn.Tag.Equals(0))
				{
					if (this.PageIndex > 1)
					{
						if (this.PageIndex == 2 && this.mCurrentIsAddEllipsisCtrl) //当前点击第二页时，显示第一个并移除左侧的省略号控件
						{
							this.mCurrentIsAddEllipsisCtrl = false;
							this.PART_Content.Children.RemoveAt(0);
						}
						//刷新UI(所有的分页控件减1)
						this._RefreshPager(AddSubtract.subtract);
						this._MaintainCurrentPagers(AddSubtract.subtract);
					}
				}
				if (misImgBtn.Tag.Equals(5))
				{
					//检测当前是否已添加省略号控件
					if (!this.mCurrentIsAddEllipsisCtrl)
					{
						this.mCurrentIsAddEllipsisCtrl = true;
						//在翻页控件第一个位置添加一个省略号控件
						this.PART_Content.Children.Insert(0, new MISLinkButton() { Content = "...", Width = 35, BorderThickness = new Thickness(1, 0, 0, 0), Style = Application.Current.FindResource("DefaultLinkButton3Style") as Style });
					}
					//刷新UI(所有的分页控件加1)
					this._RefreshPager(AddSubtract.Add);
					this._MaintainCurrentPagers(AddSubtract.Add);
				}
			}
		#endregion
		}
	}

	/// <summary>
	/// 分页事件参数
	/// </summary>
	public class PageChangedEventArgs : RoutedEventArgs
	{

		public int PageIndex
		{
			get;
			set;
		}

		public PageChangedEventArgs(int pageIndex)
			: base()
		{
			PageIndex = pageIndex;
		}
	}

	/// <summary>
	/// 分页控件类型
	/// </summary>
	public enum PagerType
	{
		/// <summary>
		/// 默认
		/// </summary>
		Default,
		/// <summary>
		/// 复杂
		/// </summary>
		Complex
	}

	public enum AddSubtract
	{
		Add, subtract
	}
}
