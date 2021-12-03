using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGP.DataGridDemo
{
    internal class DataGridWithCheckBoxServiceImp : IDataGridDataTemplateService
    {
        public String CreateCellXaml(ColumnItem column, bool bindingParameter, int? gridColum)
        {
            return String.Format("<CheckBox IsChecked=\"{0}Binding {1}={2}{3}{4}{5}\" VerticalAlignment=\"Center\" Margin=\"0\" HorizontalAlignment=\"{6}\" {7} />",
                    "{",
                     (bindingParameter ? "ConverterParameter" : "Path"),
                     column.BindingName,
                     (!String.IsNullOrWhiteSpace(column.ConverterResourceKey) ? ",Converter={StaticResource " + column.ConverterResourceKey + "}" : ""),
                     ", Mode=TwoWay,UpdateSourceTrigger=PropertyChanged",
                      "}",
                      column.Alignment.ToString(),
                      (gridColum != null ? String.Format(" Grid.Column=\"{0}\"", gridColum.Value.ToString()) : "")
                  );
        }
    }
}
