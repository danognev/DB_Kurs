using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Kurs.Формы
{
  public partial class Main
  {
    private void SetOfComponentsAddRow(Mysql.set_of_components set_of_components)
    {
      sof_components_datagrid.Rows.Add();
      sof_components_datagrid[sof_components_id.Index, sof_components_datagrid.Rows.Count - 1].Value = set_of_components.id_set;
      sof_components_datagrid[sof_components_id_nomenclature.Index, sof_components_datagrid.Rows.Count - 1].Value = set_of_components.nomenclature;
      sof_components_datagrid[sof_components_value.Index, sof_components_datagrid.Rows.Count - 1].Value = set_of_components.value;
      SetOfComponentsCellClick(this, new DataGridViewCellEventArgs(sof_components_id.Index, sof_components_datagrid.Rows.Count - 1));
    }
    private void SetOfComponentsCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < sof_components_datagrid.RowCount)
      {
        row = e.RowIndex;
        sof_components_datagrid[sof_components_id.Index, e.RowIndex].Selected = true;
        sof_components_id_tb.Text = sof_components_datagrid[sof_components_id.Index, e.RowIndex].Value.ToString();
        sof_components_id_nomenclature_cb.SelectedIndex =
        sof_components_id_nomenclature_cb.Items.IndexOf(sof_components_datagrid[sof_components_id_nomenclature.Index, row].Value);
        sof_components_value_tb.Text = sof_components_datagrid[sof_components_value.Index, e.RowIndex].Value.ToString();
        sof_components_delete_btn.Enabled = true;
      }
      else
      {
        sof_components_id_tb.Clear();
        sof_components_value_tb.Clear();
        sof_components_id_nomenclature_cb.SelectedIndex = -1;
        sof_components_delete_btn.Enabled = false;
      }
      sof_components_save_btn.Enabled = false;
    }
  }
}
