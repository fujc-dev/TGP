using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TGP.DataGridDemo
{
    public class Data
    {
        public String 入库单号 { get; set; }
        public String 金额 { get; set; }
        public String 供货商 { get; set; }
        public String 单据号 { get; set; }

        public String 验收人 { get; set; }
        public String 验收日期 { get; set; }

        public String 制单人 { get; set; }
        public String 入库日期 { get; set; }
        public virtual decimal Quarter1 { get; set; }
        public virtual decimal Quarter2 { get; set; }
        public virtual decimal Quarter3 { get; set; }
        public virtual decimal Quarter4 { get; set; }
        public decimal Total
        {
            get
            {
                return this.Quarter1 + this.Quarter2 + this.Quarter3 + this.Quarter4;
            }
        }
        public static ICollection<Data> CreateSaleDatas()
        {
            ObservableCollection<Data> items = new ObservableCollection<Data>();

            items.Add(new Data()
            {
                入库单号 = "TGP100001",
                金额 = "1.2",
                供货商 = "韩货乐天集团成都分公司",
                单据号 = "TGP-56822-212346",
                验收人 = "陈宏子",
                验收日期 = "2017.12.12",
                制单人 = "晏玉屏",
                入库日期 = "2.16.12.12",
                Quarter1 = 100,
                Quarter2 = 200,
                Quarter3 = 300,
                Quarter4 = 400
            });
            items.Add(new Data()
            {
                入库单号 = "TGP100002",
                金额 = "1.5",
                供货商 = "韩货乐天集团成都分公司",
                单据号 = "TGP-56822-212346",
                验收人 = "秦婷婷",
                验收日期 = "2017.12.12",
                制单人 = "彭玉林",
                入库日期 = "2.16.12.12",
                Quarter1 = 100,
                Quarter2 = 200,
                Quarter3 = 300,
                Quarter4 = 400
            });
            items.Add(new Data()
            {
                入库单号 = "TGP100003",
                金额 = "1.8",
                供货商 = "韩货乐天集团成都分公司",
                单据号 = "TGP-56822-212346",
                验收人 = "曹丽娜",
                验收日期 = "2017.12.12",
                制单人 = "曾盼丽",
                入库日期 = "2.16.12.12",
                Quarter1 = 100,
                Quarter2 = 200,
                Quarter3 = 300,
                Quarter4 = 400
            });
            items.Add(new Data()
            {
                入库单号 = "TGP100004",
                金额 = "1.9",
                供货商 = "韩货乐天集团成都分公司",
                单据号 = "TGP-56822-212346",
                验收人 = "李娜",
                验收日期 = "2017.12.12",
                制单人 = "谭艳红 ",
                入库日期 = "2.16.12.12",
                Quarter1 = 100,
                Quarter2 = 200,
                Quarter3 = 300,
                Quarter4 = 400
            });
            return items;
        }
    }

    public class Data2 : Data
    {
        public decimal Month1 { get; set; }

        public decimal Month2 { get; set; }

        public decimal Month3 { get; set; }

        public override decimal Quarter1
        {
            get
            {
                return this.Month1 + this.Month2 + this.Month3;
            }
            set
            {

            }
        }

        public decimal Month4 { get; set; }

        public decimal Month5 { get; set; }

        public decimal Month6 { get; set; }

        public override decimal Quarter2
        {
            get
            {
                return this.Month4 + this.Month5 + this.Month6;
            }
            set
            {

            }
        }

        public decimal Month7 { get; set; }

        public decimal Month8 { get; set; }

        public decimal Month9 { get; set; }

        public override decimal Quarter3
        {
            get
            {
                return this.Month7 + this.Month8 + this.Month9;
            }
            set
            {

            }
        }

        public decimal Month10 { get; set; }

        public decimal Month11 { get; set; }

        public decimal Month12 { get; set; }

        public override decimal Quarter4
        {
            get
            {
                return this.Month10 + this.Month11 + this.Month12;
            }
            set
            {

            }
        }

        public Boolean Test { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public static ICollection<Data2> CreateSaleData2s()
        {
            ObservableCollection<Data2> items = new ObservableCollection<Data2>();

            items.Add(new Data2() { 入库单号 = "TGP-646213168797", 金额 = "0.664", Month1 = 1, Month2 = 2, Month3 = 3, Month4 = 4, Month5 = 5, Month6 = 6, Month7 = 7, Month8 = 8, Month9 = 9, Month10 = 10, Month11 = 11, Month12 = 12 });
            items.Add(new Data2() { 入库单号 = "TGP-646213168797", 金额 = "0.664", Month1 = 1, Month2 = 2, Month3 = 3, Month4 = 4, Month5 = 5, Month6 = 6, Month7 = 7, Month8 = 8, Month9 = 9, Month10 = 10, Month11 = 11, Month12 = 12 });
            items.Add(new Data2() { 入库单号 = "TGP-646213168797", 金额 = "0.664", Month1 = 1, Month2 = 2, Month3 = 3, Month4 = 4, Month5 = 5, Month6 = 6, Month7 = 7, Month8 = 8, Month9 = 9, Month10 = 10, Month11 = 11, Month12 = 12 });
            items.Add(new Data2() { 入库单号 = "TGP-646213168797", 金额 = "0.664", Month1 = 1, Month2 = 2, Month3 = 3, Month4 = 4, Month5 = 5, Month6 = 6, Month7 = 7, Month8 = 9, Month9 = 9, Month10 = 10, Month11 = 11, Month12 = 12 });

            items.Add(new Data2()
            {
                入库单号 = "合计",
                Month1 = items.Sum(a => a.Month1),
                Month2 = items.Sum(a => a.Month2),
                Month3 = items.Sum(a => a.Month3),
                Month4 = items.Sum(a => a.Month4),
                Month5 = items.Sum(a => a.Month5),
                Month6 = items.Sum(a => a.Month6),
                Month7 = items.Sum(a => a.Month7),
                Month8 = items.Sum(a => a.Month8),
                Month9 = items.Sum(a => a.Month9),
                Month10 = items.Sum(a => a.Month10),
                Month11 = items.Sum(a => a.Month11),
                Month12 = items.Sum(a => a.Month12),
            });

            return items;
        }

    }

    public class Data3 : Data
    {
        /// <summary>
        /// 完成状态
        /// </summary>
        public bool CompleteState { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public static ICollection<Data3> CreateSaleData3s()
        {
            ObservableCollection<Data3> items = new ObservableCollection<Data3>();

            items.Add(new Data3() { 入库单号 = "TGP-646213168797", 金额 = "0.664", Quarter1 = 123123, Quarter2 = 456456, Quarter3 = 981347, Quarter4 = 9013000, CompleteState = true });
            items.Add(new Data3() { 入库单号 = "TGP-646213168797", 金额 = "0.664", Quarter1 = 123123, Quarter2 = 45546, Quarter3 = 78612346, Quarter4 = 7892137, CompleteState = false });
            items.Add(new Data3() { 入库单号 = "TGP-646213168797", 金额 = "0.664", Quarter1 = 123123, Quarter2 = 5789678, Quarter3 = 6781266, Quarter4 = 768776, CompleteState = false });
            items.Add(new Data3() { 入库单号 = "TGP-646213168797", 金额 = "0.664", Quarter1 = 456456, Quarter2 = 451236, Quarter3 = 561377, Quarter4 = 4131334, CompleteState = true });

            items.Add(new Data3()
            {
                入库单号 = "合计",
                Quarter1 = items.Sum(a => a.Quarter1),
                Quarter2 = items.Sum(a => a.Quarter2),
                Quarter3 = items.Sum(a => a.Quarter3),
                Quarter4 = items.Sum(a => a.Quarter4),
                CompleteState = items.Count(a => a.CompleteState) == items.Count
            });

            return items;
        }

    }
}
