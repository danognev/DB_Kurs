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
    private void SpecificationAddRow(Mysql.specification specification) {
      specification_datagrid.Rows.Add();
      specification_datagrid[specification_id.Index, specification_datagrid.Rows.Count - 1].Value = specification.id;
      specification_datagrid[specification_status.Index, specification_datagrid.Rows.Count - 1].Value = specification.status;
      specification_datagrid[specification_set_of_components.Index, specification_datagrid.Rows.Count - 1].Value = specification.set_of_components;
      specification_datagrid[specification_description.Index, specification_datagrid.Rows.Count - 1].Value = specification.discription;
      SpecificationCellClick(this, new DataGridViewCellEventArgs(specification_id.Index, specification_datagrid.Rows.Count - 1));
    }
    private void SpecificationCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < specification_datagrid.RowCount)
      {
        row = e.RowIndex;
        specification_datagrid[specification_id.Index, e.RowIndex].Selected = true;
        specification_id_tb.Text = specification_datagrid[specification_id.Index, e.RowIndex].Value.ToString();
        specification_status_cb.SelectedIndex =
        specification_status_cb.Items.IndexOf(specification_datagrid[specification_status.Index, row].Value);
        specification_sof_components_cb.SelectedIndex =
        specification_sof_components_cb.Items.IndexOf(specification_datagrid[specification_set_of_components.Index, row].Value);
        specification_description_tb.Text = specification_datagrid[specification_description.Index, e.RowIndex].Value.ToString();
        specification_delete_btn.Enabled = true;
      }
      else
      {
        specification_id_tb.Clear();
        specification_description_tb.Clear();
        specification_status_cb.SelectedIndex = -1;
        specification_sof_components_cb.SelectedIndex = -1;
        specification_delete_btn.Enabled = false;
      }
      specification_save_btn.Enabled = false;
    }
  }
}
