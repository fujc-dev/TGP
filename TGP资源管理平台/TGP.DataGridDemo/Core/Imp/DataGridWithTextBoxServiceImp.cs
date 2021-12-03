using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGP.DataGridDemo
{
    internal class DataGridWithTextBoxServiceImp : IDataGridDataTemplateService
    {
        public String CreateCellXaml(ColumnItem column, bool bindingParameter, int? gridColum)
        {
            return String.Format("<TextBox Text=\"{0}Binding {1}={2}{3}{4}{5}{6}\" VerticalAlignment=\"Center\" Margin=\"1\" HorizontalAlignment=\"{7}\" TextWrapping=\"{8}\"{9} />",
                    "{",
                     (bindingParameter ? "ConverterParameter" : "Path"),
                     column.BindingName,
                     (!String.IsNullOrWhiteSpace(column.StringFormat) ? String.Format(",StringFormat={0}", column.StringFormat) : ""),
                     (!String.IsNullOrWhiteSpace(column.ConverterResourceKey) ? ",Converter={StaticResource " + column.ConverterResourceKey + "}" : ""),
                     ", Mode=TwoWay,UpdateSourceTrigger=PropertyChanged",
                     "}",
                      column.Alignment.ToString(),
                      column.TextWrapping.ToString(),
                      (gridColum != null ? String.Format(" Grid.Column=\"{0}\"", gridColum.Value.ToString()) : "")
                  );
        }
    }
}
