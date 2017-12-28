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
    public partial class MISPager : Control
    {
        //分页明细布局
        private const String MIS_PART_CONTENT = "PART_Content";
        //上一页
        private const String MIS_PART_PREVIOUSPAGE = "PART_Previouspage";
        //下一页
        private const String MIS_PART_NEXTPAGE = "PART_Nextpage";


        private MISImageButton PART_Nextpage;  //下一页事件
        private MISImageButton PART_Previouspage; //上一页事件
        private StackPanel PART_Content;  //子页码

        public MISPager()
        {
            this.PageIndex = 1;
            this.PageSize = 10;
            this.Total = 1119;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PART_Content = this.GetTemplateChild(MISPager.MIS_PART_CONTENT) as StackPanel;
            this.PART_Nextpage = this.GetTemplateChild(MISPager.MIS_PART_NEXTPAGE) as MISImageButton;
            this.PART_Previouspage = this.GetTemplateChild(MISPager.MIS_PART_PREVIOUSPAGE) as MISImageButton;
            //计算页码数
            this.PageCount = (Int32)Math.Ceiling((Double)this.Total / (Double)this.PageSize);
            //当总页码小于10页，显示1、2、3、4、5、6、7、8、9
            if (this.PageCount < 10)
            {
                this._CreateStackPanelItem();
            }
            else
            {
                _CreateStackPanelItem2();
            }
            if (this.PART_Nextpage == null)
            {

            }
            if (this.PART_Previouspage == null)
            {

            }
            if (this.PART_Content == null)
            {

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
            DependencyProperty.Register("PageSize", typeof(Int32), typeof(MISPager), new PropertyMetadata(0));


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
            DependencyProperty.Register("PageIndex", typeof(Int32), typeof(MISPager), new PropertyMetadata(0));

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

        #region 私有方法

        private void _CreateStackPanelItem()
        {
            for (int i = 1; i < 10; i++)
            {
                var misImgBtn = new MISLinkButton() { Content = i.ToString(), Width = 35, BorderThickness = new Thickness(1, 0, 0, 0), Style = Application.Current.FindResource("DefaultLinkButton2Style") as Style };
                if (this.PART_Content != null)
                {
                    this.PART_Content.Children.Add(misImgBtn);
                }
            }
        }

        private void _CreateStackPanelItem2()
        {
            for (int i = 1; i < 6; i++)
            {
                var misImgBtn = new MISLinkButton() { Content = i.ToString(), Width = 35, Margin = new Thickness(1, 0, 0, 0), Style = Application.Current.FindResource("DefaultLinkButton2Style") as Style };
                if (this.PART_Content != null)
                {
                    this.PART_Content.Children.Add(misImgBtn);
                }
            }
            this.PART_Content.Children.Add(new MISLinkButton() { Content = "...", Width = 35, Margin = new Thickness(1, 0, 0, 0), Style = Application.Current.FindResource("DefaultLinkButton2Style") as Style });
            this.PART_Content.Children.Add(new MISLinkButton() { Content = "10", Width = 35, Margin = new Thickness(1, 0, 0, 0), Style = Application.Current.FindResource("DefaultLinkButton2Style") as Style });
        }
        #endregion
    }
}
