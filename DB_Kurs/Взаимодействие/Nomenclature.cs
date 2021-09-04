using System.Windows.Forms;

namespace DB_Kurs.Формы
{
  public partial class Main
  {
    private void NomenclatureAddRow(Mysql.nomenclature nomenclature)
    {
      nomenclature_datagrid.Rows.Add();
      nomenclature_datagrid[nomenclature_id.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.id;
      nomenclature_datagrid[nomenclature_name.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.name;
      nomenclature_datagrid[nomenclature_unit.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.unit;
      nomenclature_datagrid[nomenclature_renewal_method.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.renewal_method;
      nomenclature_datagrid[nomenclature_marriage_rate.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.marriage_rate;
      nomenclature_datagrid[nomenclature_additional_order_level.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.additional_order_level;
      nomenclature_datagrid[nomenclature_additional_order_value.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.additional_order_value;
      nomenclature_datagrid[nomenclature_production_route.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.production_route;
      nomenclature_datagrid[nomenclature_specification.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.specification;
      nomenclature_datagrid[nomenclature_waiting_period.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.waiting_period;
      nomenclature_datagrid[nomenclature_write_off_mode.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.material_write_off_mode;
      NomenclatureCellClick(this, new DataGridViewCellEventArgs(nomenclature_id.Index, nomenclature_datagrid.Rows.Count - 1));
    }
    private void NomenclatureCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < nomenclature_datagrid.RowCount)
      {
        row = e.RowIndex;
        nomenclature_datagrid[nomenclature_id.Index, e.RowIndex].Selected = true;
        nomenclature_id_textbox.Text = nomenclature_datagrid[nomenclature_id.Index, row].Value.ToString();
        nomenclature_name_tb.Text = nomenclature_datagrid[nomenclature_name.Index, row].Value.ToString();
        nomenclature_unit_tb.Text = nomenclature_datagrid[nomenclature_unit.Index, row].Value.ToString();
        nomenclature_rw_method_cb.SelectedIndex =
        nomenclature_rw_method_cb.Items.IndexOf(nomenclature_datagrid[nomenclature_renewal_method.Index, row].Value.ToString());
        nomenclature_marriage_rate_tb.Text = nomenclature_datagrid[nomenclature_marriage_rate.Index, row].Value.ToString();
        nomenclature_ado_level_tb.Text = nomenclature_datagrid[nomenclature_additional_order_level.Index, row].Value.ToString();
        nomenclature_ado_value_tb.Text = nomenclature_datagrid[nomenclature_additional_order_value.Index, row].Value.ToString();
        nomenclature_waiting_period_tb.Text = nomenclature_datagrid[nomenclature_waiting_period.Index, row].Value.ToString();
        nomenclature_proute_cb.SelectedIndex =
        nomenclature_proute_cb.Items.IndexOf(nomenclature_datagrid[nomenclature_production_route.Index, row].Value.ToString());
        nomenclature_specification_cb.SelectedIndex =
        nomenclature_specification_cb.Items.IndexOf(nomenclature_datagrid[nomenclature_specification.Index, row].Value.ToString());
        nomenclature_woff_method_cb.SelectedIndex =
        nomenclature_woff_method_cb.Items.IndexOf(nomenclature_datagrid[nomenclature_write_off_mode.Index, row].Value.ToString());
        nomenclature_delete_btn.Enabled = true;
      }
      else
      {
        nomenclature_id_textbox.Clear();
        nomenclature_name_tb.Clear();
        nomenclature_unit_tb.Clear();
        nomenclature_marriage_rate_tb.Clear();
        nomenclature_ado_level_tb.Clear();
        nomenclature_ado_value_tb.Clear();
        nomenclature_waiting_period_tb.Clear();
        nomenclature_rw_method_cb.SelectedIndex = -1;
        nomenclature_proute_cb.SelectedIndex = -1;
        nomenclature_specification_cb.SelectedIndex = -1;
        nomenclature_woff_method_cb.SelectedIndex = -1;
        nomenclature_delete_btn.Enabled = false;
      }
      nomenclature_save_btn.Enabled = false;
    }
  }
}
