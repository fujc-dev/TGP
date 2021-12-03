using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// 描述 <see cref="ColumnItem"/> 动态列项目，用于在后台构建多列标题绑定项
    /// </summary>
    [Serializable]
    public sealed class ColumnItem : NotifyPropertyChangedBase, ICloneable
    {
        /// <summary>
        /// 实例化 <see cref="ColumnItem"/> 类新实例，一般用于顶级标题栏
        /// </summary>
        /// <param name="name">默认列名</param>
        public ColumnItem(String name)
            : this(name, "")
        {
        }

        /// <summary>
        /// 实例化 <see cref="ColumnItem"/> 类新实例
        /// </summary>
        /// <param name="name">默认列名</param>
        /// <param name="bindingName">绑定名称</param>
        public ColumnItem(String name, String bindingName)
            : this(name, bindingName, "", "", HorizontalAlignment.Left, 80)
        {
        }

        /// <summary>
        /// 实例化 <see cref="ColumnItem"/> 类新实例
        /// </summary>
        /// <param name="name">默认列名</param>
        /// <param name="bindingName">绑定名称</param>
        /// <param name="converterResourceKey">绑定的资源键(传递此资源时，要在资源中声明)</param>
        /// <param name="stringFormat">字符串格式</param>
        /// <param name="alignment">对齐方式</param>
        /// <param name="width">列宽</param>
        /// <param name="visibility">是否显示</param>
        public ColumnItem(String name, String bindingName, String converterResourceKey, String stringFormat, HorizontalAlignment alignment, int width, Visibility visibility)
            : this(name, "", bindingName, converterResourceKey, stringFormat, alignment, width, visibility, ColumnType.TextBlock)
        {

        }

        /// <summary>
        /// 实例化 <see cref="ColumnItem"/> 类新实例
        /// </summary>
        /// <param name="name">默认列名</param>
        /// <param name="bindingName">绑定名称</param>
        /// <param name="converterResourceKey">绑定的资源键(传递此资源时，要在资源中声明)</param>
        /// <param name="stringFormat">字符串格式</param>
        /// <param name="alignment">对齐方式</param>
        /// <param name="width">列宽</param>
        public ColumnItem(String name, String bindingName, String converterResourceKey, String stringFormat, HorizontalAlignment alignment, int width)
            : this(name, "", bindingName, converterResourceKey, stringFormat, alignment, width, Visibility.Visible, ColumnType.TextBlock)
        {

        }

        /// <summary>
        /// 实例化 <see cref="ColumnItem"/> 类新实例
        /// </summary>
        /// <param name="name">默认列名</param>
        /// <param name="extendName">扩展列名</param>
        /// <param name="bindingName">绑定名称</param>
        /// <param name="converterResourceKey">绑定的资源键(传递此资源时，要在资源中声明)</param>
        /// <param name="stringFormat">字符串格式</param>
        /// <param name="alignment">对齐方式</param>
        /// <param name="width">列宽</param>
        public ColumnItem(String name, String extendName, String bindingName, String converterResourceKey, String stringFormat, HorizontalAlignment alignment, int width)
       : this(name, extendName, bindingName, converterResourceKey, stringFormat, alignment, width, Visibility.Visible, ColumnType.TextBlock)
        {

        }

        /// <summary>
        /// 实例化 <see cref="ColumnItem"/> 类新实例
        /// </summary>
        /// <param name="name">默认列名</param>
        /// <param name="bindingName">绑定名称</param>
        /// <param name="converterResourceKey">绑定的资源键(传递此资源时，要在资源中声明)</param>
        /// <param name="stringFormat">字符串格式</param>
        /// <param name="alignment">对齐方式</param>
        /// <param name="width">列宽</param>
        /// <param name="columnType">数据模板内容类型</param>
        public ColumnItem(String name, String bindingName, String converterResourceKey, String stringFormat, HorizontalAlignment alignment, int width, ColumnType columnType)
            : this(name, "", bindingName, converterResourceKey, stringFormat, alignment, width, Visibility.Visible, columnType)
        {

        }
        /// <summary>
        /// 实例化 <see cref="ColumnItem"/> 类新实例
        /// </summary>
        /// <param name="name">默认列名</param>
        /// <param name="extendName">扩展列名</param>
        /// <param name="bindingName">绑定名称</param>
        /// <param name="converterResourceKey">绑定的资源键(传递此资源时，要在资源中声明)</param>
        /// <param name="stringFormat">字符串格式</param>
        /// <param name="alignment">对齐方式</param>
        /// <param name="width">列宽</param>
        /// <param name="columnType">数据模板内容类型</param>
        public ColumnItem(String name, String extendName, String bindingName, String converterResourceKey, String stringFormat, HorizontalAlignment alignment, int width, Visibility visibility)
            : this(name, extendName, bindingName, converterResourceKey, stringFormat, alignment, width, visibility, ColumnType.TextBlock)
        {

        }

        /// <summary>
        /// 实例化 <see cref="ColumnItem"/> 类新实例
        /// </summary>
        /// <param name="name">默认列名</param>
        /// <param name="extendName">扩展列名</param>
        /// <param name="bindingName">绑定名称</param>
        /// <param name="converterResourceKey">绑定的资源键(传递此资源时，要在资源中声明)</param>
        /// <param name="stringFormat">字符串格式</param>
        /// <param name="alignment">对齐方式</param>
        /// <param name="width">列宽</param>
        /// <param name="columnType">数据模板内容类型</param>
        public ColumnItem(String name, String extendName, String bindingName, String converterResourceKey, String stringFormat, HorizontalAlignment alignment, int width, ColumnType columnType)
            : this(name, extendName, bindingName, converterResourceKey, stringFormat, alignment, width, Visibility.Visible, columnType)
        {

        }

        /// <summary>
        /// 实例化 <see cref="ColumnItem"/> 类新实例
        /// </summary>
        /// <param name="name">默认列名</param>
        /// <param name="extendName">扩展列名</param>
        /// <param name="bindingName">绑定名称</param>
        /// <param name="converterResourceKey">绑定的资源键(传递此资源时，要在资源中声明)</param>
        /// <param name="stringFormat">字符串格式</param>
        /// <param name="alignment">对齐方式</param>
        /// <param name="width">列宽</param>
        /// <param name="visibility">是否显示</param>
        /// <param name="columnType">数据模板内容类型</param>
        public ColumnItem(String name, String extendName, String bindingName, String converterResourceKey, String stringFormat, HorizontalAlignment alignment, int width, Visibility visibility, ColumnType columnType)
        {
            this.ID = Guid.NewGuid();
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("titleName", "标题不能为 null 或 空白字符串。");
            }
            this.Name = name;
            this.ExtendName = extendName;
            this.BindingName = (String.IsNullOrWhiteSpace(bindingName)) ? "simple" : bindingName;
            this.ConverterResourceKey = converterResourceKey;
            this.StringFormat = stringFormat;
            this.Alignment = alignment;
            this.Width = width;
            this.Visibility = visibility;
            this.Type = columnType;
            this.Columns = new ColumnItemCollection(this);  //初始化
            this.Level = 0; //默认
            this.TextWrapping = System.Windows.TextWrapping.NoWrap;
        }



        private Int32 mWidth;
        private Visibility mVisibility;
        private HorizontalAlignment mAlignment;

        public Guid ID { get; private set; }


        /// <summary>
        /// 列宽
        /// </summary>
        public Int32 Width
        {
            get { return mWidth; }
            set { mWidth = value; OnPropertyChanged("Width"); }
        }


        /// <summary>
        /// 列显示
        /// </summary>
        public Visibility Visibility
        {
            get { return mVisibility; }
            set { mVisibility = value; OnPropertyChanged("Visibility"); }
        }

        /// <summary>
        /// 获取或设置水平对齐方式
        /// </summary>
        public HorizontalAlignment Alignment
        {
            get { return mAlignment; }
            set { mAlignment = value; OnPropertyChanged("Alignment"); }
        }

        /// <summary>
        /// 默认列名
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// 扩展列名
        /// </summary>
        public String ExtendName { get; set; }

        /// <summary>
        /// 绑定名称
        /// </summary>
        public String BindingName { get; set; }

        /// <summary>
        /// 获取或设置转换资源键
        /// </summary>
        public String ConverterResourceKey { get; set; }

        /// <summary>
        /// 获取或设置字符串格式
        /// </summary>
        public String StringFormat { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public ColumnType Type { get; set; }

        /// <summary>
        /// 获取或设置是否自动换行(默认为 NoWrap)
        /// </summary>
        public TextWrapping TextWrapping { get; set; }

        public ColumnComboBox ColumnComboBox { get; set; }


        /// <summary>
        /// 获取列集合
        /// </summary>
        public ColumnItemCollection Columns { get; private set; }

        /// <summary>
        /// 获取级别
        /// </summary>
        public int Level { get; internal set; }

        /// <summary>
        /// 获取父级
        /// </summary>
        public ColumnItem Parent { get; internal set; }

        /// <summary>
        /// 获取子级深度
        /// </summary>
        /// <returns></returns>
        public int ChildLevelDepth()
        {
            if (this.Columns.Count > 0)
            {
                return this.CalcLevelDepth(true) - this.Level;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 计算获取级别深度
        /// </summary>
        /// <param name="child">计算子级</param>
        /// <returns></returns>
        private int CalcLevelDepth(bool child)
        {
            if (this.Columns.Count > 0)
            {
                int level = this.Columns.Max(c => c.CalcLevelDepth(child));
                if (child)
                {
                    return level;
                }
                else
                {
                    return level - this.Level;
                }
            }
            else
            {
                return this.Level;
            }
        }

        /// <summary>
        /// 获取当前列的所有最后子集的总数
        /// </summary>
        /// <returns></returns>
        public int LastLevelColumnCount()
        {
            int value = 0;
            if (this.Columns.Count > 0)
            {
                value += this.Columns.Sum(c => c.LastLevelColumnCount());
            }
            else
            {
                value = 1;
            }
            return value;
        }

        /// <summary>
        /// 合计总宽度
        /// </summary>
        /// <returns></returns>
        public int TotalGroupWidth()
        {
            int value;
            if (this.Columns.Count > 0)
            {
                value = this.Columns.Sum(c => c.TotalGroupWidth());
            }
            else
            {
                value = this.Width;
            }
            return value;
        }

        /// <summary>
        /// 验证(必须设置绑定值)
        /// </summary>
        internal void CreateVerify()
        {
            if (this.Columns.Count == 0)
            {
                if (String.IsNullOrWhiteSpace(this.BindingName))
                {
                    throw new ArgumentNullException(String.Format("{0} 为末级时，绑定路径 BindingPath 为 null 或空白字符串。", this.Name));
                }
            }
        }

        public void SetColumnComboBox(ColumnComboBox columnComboBox)
        {
            this.ColumnComboBox = columnComboBox;
        }

        #region ICloneable ColumnItem 对象深度复制  
        public object Clone()
        {
            ColumnItem item = (ColumnItem)this.MemberwiseClone();
            item.Parent = null;
            item.Name = this.Name;
            item.ExtendName = this.ExtendName;
            item.Columns = new ColumnItemCollection(item);
            foreach (var o in this.Columns)
            {
                var _o = (ColumnItem)o.Clone();
                _o.Parent = null;
                item.Columns.Add(_o);
            }
            return item;
        }
        private void OnClone(ColumnItemCollection collection, ColumnItemCollection collection2)
        {
            foreach (var item in collection2)
            {
                var _o = (ColumnItem)item.Clone();
                if (item.Columns.Count > 0)
                {
                    OnClone(item.Columns, item.Columns);
                }
                collection.Add(_o);
            }
        }


        #endregion
    }
    [Serializable]
    public class ColumnComboBox : NotifyPropertyChangedBase
    {
        public ColumnComboBox(String comboBoxBindName)
            : this(comboBoxBindName, "", "", "")
        {

        }
        public ColumnComboBox(String comboBoxBindName, String selectedItemBindName)
            : this(comboBoxBindName, selectedItemBindName, "", "")
        {

        }
        public ColumnComboBox(String comboBoxBindName, String selectedItemBindName, String selectedValuePath, String displayMemberPath)
        {
            this.mComboBoxBindName = comboBoxBindName;
            this.mSelectedItemBindName = selectedItemBindName;
            this.mSelectedValuePath = selectedValuePath;
            this.mDisplayMemberPath = displayMemberPath;
        }


        private String mSelectedItemBindName;
        private String mComboBoxBindName;
        private String mSelectedValuePath = "";
        private String mDisplayMemberPath = "";
        private ObservableCollection<string> itemSource;  //

        /// <summary>
        /// 绑定资源
        /// </summary>
        public String ComboBoxBindName
        {
            get
            {
                return mComboBoxBindName;
            }

            set
            {
                mComboBoxBindName = value;
            }
        }

        /// <summary>
        /// 选中值
        /// </summary>
        public string SelectedValuePath
        {
            get
            {
                return mSelectedValuePath;
            }

            set
            {
                mSelectedValuePath = value;
            }
        }

        /// <summary>
        /// 显示值
        /// </summary>
        public string DisplayMemberPath
        {
            get
            {
                return mDisplayMemberPath;
            }

            set
            {
                mDisplayMemberPath = value;
            }
        }

        public string SelectedItemBindName
        {
            get
            {
                return mSelectedItemBindName;
            }

            set
            {
                mSelectedItemBindName = value;
            }
        }
    }
}
