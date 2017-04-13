using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TGP.WindowDemo
{
    /// <summary>
    /// 处理联动的GComboBox，针对省级联动，数据源获取需要修改 by dane
    /// </summary>
    public partial class LinkedComboBox : GComboBox
    {

        public LinkedComboBox()
        {
            //进行一个不耗时的初始化操作
        }


        #region 依赖属性

        #region 元素名称
        public String ChildrenName
        {
            get { return (String)GetValue(ChildrenNameProperty); }
            set { SetValue(ChildrenNameProperty, value); }
        }
        public static readonly DependencyProperty ChildrenNameProperty = DependencyProperty.Register("ChildrenName", typeof(String), typeof(LinkedComboBox), new PropertyMetadata(null));
        #endregion

        #region 联动等级
        public LinkageGrade LinkageGrade
        {
            get { return (LinkageGrade)GetValue(LinkageGradeProperty); }
            set { SetValue(LinkageGradeProperty, value); }
        }

        public static readonly DependencyProperty LinkageGradeProperty = DependencyProperty.Register("LinkageGrade", typeof(LinkageGrade), typeof(LinkedComboBox), new PropertyMetadata(LinkageGrade.Normal));
        #endregion

        #endregion


        #region 重写方法
        protected override void OnNormalBind()
        {
            //需要强制加载时，默认取省的数据
            if (this.LinkageGrade == LinkageGrade.Constraint)
            {
                this.SetAndBeginStoryboard();
                Task.Factory.StartNew(new Action(() =>
                {
                    Thread.Sleep(3000);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        this.ItemsSource = LocalDB.GetList().FindAll((o) => { return o.ParentID == -1; });
                        this.SelectedValuePath = "ID";
                        this.DisplayMemberPath = "Name";
                        this.SelectedIndex = 0;
                        this.StopStoryboard();
                    }));
                }));

            }
        }

        /// <summary>
        /// 联动选中处理
        /// </summary>
        /// <param name="e">事件源</param>
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            var targetComboBox = e.Source as LinkedComboBox;
            if (targetComboBox != null)
            {
                var selectItem = targetComboBox.SelectedItem as Simple;
                if (selectItem != null)
                {
                    //根据当前的选中值，查询子级控件需要绑定数据集合
                    if (AutomaticBinding)
                    {
                        //查找控件
                        if (!String.IsNullOrWhiteSpace(this.ChildrenName))
                        {
                            var comboBox = this.FindName(this.ChildrenName) as GComboBox;
                            if (comboBox != null)
                            {
                                comboBox.ItemsSource = null;
                                comboBox.SetAndBeginStoryboard();
                                Task.Factory.StartNew(new Action(() =>
                                {
                                    Thread.Sleep(3000);
                                    Application.Current.Dispatcher.Invoke(new Action(() =>
                                    {
                                        //数据检索绑定数据
                                        comboBox.ItemsSource = LocalDB.GetList().FindAll((o) => { return o.ParentID == selectItem.ID; });
                                        comboBox.SelectedValuePath = "ID";
                                        comboBox.DisplayMemberPath = "Name";
                                        comboBox.SelectedIndex = 0;
                                        comboBox.StopStoryboard();
                                    }));
                                }));
                            }

                        }

                    }
                }
            }
            base.OnSelectionChanged(e);
        } 
        #endregion
    }

    /// <summary>
    /// 联动等级
    /// </summary>
    public enum LinkageGrade
    {
        /// <summary>
        /// 需要首次强制加载数据的ComboBox控件
        /// </summary>
        Constraint,
        /// <summary>
        /// 不做任何操作
        /// </summary>
        Normal

    }
}
