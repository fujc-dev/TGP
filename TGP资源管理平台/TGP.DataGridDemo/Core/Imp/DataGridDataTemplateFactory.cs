using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// <see cref="DataTemplate"/>构建工厂对象
    /// </summary>
    public interface IDataGridDataTemplateFactory
    {
        /// <summary>
        /// 获取模板构建服务
        /// </summary>
        /// <param name="columnType">列显示类型</param>
        /// <returns></returns>
        IDataGridDataTemplateService GetService(ColumnType columnType);
    }

    public class DataGridDataTemplateFactory : IDataGridDataTemplateFactory
    {

        public IDataGridDataTemplateService GetService(ColumnType columnType)
        {
            IDataGridDataTemplateService service = null;
            //
            switch (columnType)
            {
                case ColumnType.TextBlock:
                default:
                    service = new DataGridWithTextBlockServiceImp();
                    break;
                case ColumnType.CheckBox:
                    service = new DataGridWithCheckBoxServiceImp();
                    break;
                case ColumnType.TextBox:
                    service = new DataGridWithTextBoxServiceImp();
                    break;
                case ColumnType.ComboBox:
                    service = new DataGridWithComboBoxServiceImp();
                    break;

            }
            return service;
        }
    }
}
