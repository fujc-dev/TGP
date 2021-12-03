using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGP.DataGridDemo
{
    internal class DataGridWithComboBoxServiceImp : IDataGridDataTemplateService
    {
        public String CreateCellXaml(ColumnItem column, bool bindingParameter, int? gridColum)
        {
            return String.Format("<ComboBox "
                //设置绑定值
                + " ItemsSource=\"{0}Binding {1}={2}{3}\"  "
                + " SelectedValuePath=\"{4}\" "
                + " DisplayMemberPath=\"{5}\" "
                + " SelectedItem=\"{6}\" "
                + " SelectedValue=\"{7}\""
                + " VerticalAlignment=\"Center\" "
                + " Margin=\"0\" "
                + " HorizontalAlignment=\"{8}\" {9} />",
                    "{",
                     (bindingParameter ? "ConverterParameter" : "Path"),
                     column.ColumnComboBox.ComboBoxBindName,
                      "}",
                     (!String.IsNullOrWhiteSpace(column.ColumnComboBox.SelectedValuePath) ? column.ColumnComboBox.SelectedValuePath : ""),
                      (!String.IsNullOrWhiteSpace(column.ColumnComboBox.DisplayMemberPath) ? column.ColumnComboBox.DisplayMemberPath : ""),
                     (!String.IsNullOrWhiteSpace(column.ColumnComboBox.SelectedItemBindName) ? "{Binding " + column.ColumnComboBox.SelectedItemBindName + "}" : ""),
                      "{Binding " + column.BindingName + (!String.IsNullOrWhiteSpace(column.ConverterResourceKey) ? ",Converter={StaticResource " + column.ConverterResourceKey + "}" : "") + ",Mode=TwoWay,UpdateSourceTrigger=PropertyChanged}",
                      column.Alignment.ToString(),
                      (gridColum != null ? String.Format(" Grid.Column=\"{0}\"", gridColum.Value.ToString()) : "")
                  );
        }
    }
}
