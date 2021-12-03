using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// 自定义的列项目集合
    /// </summary>
    [Serializable]
    public sealed class ColumnItemCollection : IList<ColumnItem>, ICollection<ColumnItem>, ICollection, IEnumerable<ColumnItem>, IEnumerable
    {
        /// <summary>
        /// 获取父级
        /// </summary>
        private readonly ColumnItem Parent;

        private readonly List<ColumnItem> items;

        /// <summary>
        /// 实例化 ColumnItemCollection 类新实例
        /// </summary>
        /// <param name="parent">父级</param>
        internal ColumnItemCollection(ColumnItem parent)
        {
            this.Parent = parent;
            this.items = new List<ColumnItem>();
        }

        /// <summary>
        /// 根据索引获取或设置列项目
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public ColumnItem this[int index]
        {
            get
            {
                return this.items[index];
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                ColumnItem item = this.items[index];
                if (!item.Equals(value))
                {
                    if (value.Parent != null)
                    {
                        if (!value.Parent.Equals(this.Parent))
                        {
                            throw new ArgumentException(string.Format("{0} 已属于 {1} 列的子级。", value.Name, value.Parent.Name));
                        }
                        else
                        {
                            int i = this.items.IndexOf(value);
                            this.items[index] = value; //交换
                            this.items[i] = item;
                        }
                    }
                    else
                    {
                        this.SetItem(value, this.Parent);
                        this.ResetItem(item);
                        this.items[index] = value;
                    }
                }
            }
        }

        private void SetItem(ColumnItem item, ColumnItem parent)
        {
            item.Parent = parent;
            item.Level = parent.Level + 1;
            if (item.Columns.Count > 0)
            {
                foreach (var col in item.Columns)
                {
                    SetItem(col, item);
                }
            }
        }

        private void ResetItem(ColumnItem item)
        {
            item.Parent = null;
            item.Level = 0;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item">项目</param>
        public void Add(ColumnItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (item.Parent != null)
            {
                throw new ArgumentException(string.Format("{0} 已属于 {1} 列的子级。", item.Name, item.Parent.Name));
            }
            this.SetItem(item, this.Parent);
            this.items.Add(item);
        }

        /// <summary>
        /// 插入项目
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="item">项目</param>
        public void Insert(int index, ColumnItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (item.Parent != null)
            {
                throw new ArgumentException(string.Format("{0} 已属于 {1} 列的子级。", item.Name, item.Parent.Name));
            }
            this.items.Insert(index, item);
            this.SetItem(item, this.Parent);
        }

        /// <summary>
        /// 搜索指定项目的索引
        /// </summary>
        /// <param name="item">项目</param>
        /// <returns></returns>
        public int IndexOf(ColumnItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return this.items.IndexOf(item);
        }

        /// <summary>
        /// 全部清除
        /// </summary>
        public void Clear()
        {
            foreach (var item in this.items)
            {
                this.ResetItem(item);
            }
            this.items.Clear();
        }

        /// <summary>
        /// 是否包含列项目
        /// </summary>
        /// <param name="item">项目</param>
        /// <returns></returns>
        public bool Contains(ColumnItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return this.items.Contains(item);
        }

        /// <summary>
        /// 复制到数组
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="arrayIndex">开始索引</param>
        public void CopyTo(ColumnItem[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            this.items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 获取列总数
        /// </summary>
        public int Count
        {
            get
            {
                //
                //var count = 0;
                //this.items.ForEach((o) =>
                //{
                //    if (o.Visibility == System.Windows.Visibility.Visible)
                //    {
                //        count++;
                //    }
                //});
                //return count;
                return this.items.Count;
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// 移除项目
        /// </summary>
        /// <param name="item">项目</param>
        /// <returns></returns>
        public bool Remove(ColumnItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            bool remove = this.items.Remove(item);
            if (remove)
            {
                this.ResetItem(item);
            }
            return remove;
        }

        /// <summary>
        /// 移除指定索引处的列项目
        /// </summary>
        /// <param name="index">索引</param>
        public void RemoveAt(int index)
        {
            var item = this.items[index];
            this.ResetItem(item);
            this.items.RemoveAt(index);
        }

        /// <summary>
        /// 枚举
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ColumnItem> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// 制复到数组
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            ICollection coll = this.items;
            coll.CopyTo(array, index);
        }

        /// <summary>
        /// 是否同步
        /// </summary>
        bool ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection)this.items).IsSynchronized;
            }
        }

        /// <summary>
        /// 同步对象
        /// </summary>
        object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection)this.items).SyncRoot;
            }
        }

        /// <summary>
        ///  排序
        /// </summary>
        public void ToSort()
        {
            this.items.Sort();
        }
    }
}
