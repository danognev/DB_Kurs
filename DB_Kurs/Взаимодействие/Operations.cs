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
    private Mysql.operations operations = new Mysql.operations();
    private void OperationsAddRow(Mysql.operations operations)
    {
      operations_datagrid.Rows.Add();
      operations_datagrid[operations_id.Index, operations_datagrid.Rows.Count - 1].Value = operations.id;
      operations_datagrid[operations_setup_time.Index, operations_datagrid.Rows.Count - 1].Value = operations.setup_time;
      operations_datagrid[operations_pg_time.Index, operations_datagrid.Rows.Count - 1].Value = operations.processing_time;
      operations_datagrid[operations_transit_time.Index, operations_datagrid.Rows.Count - 1].Value = operations.transit_time;
      operations_datagrid[operations_description.Index, operations_datagrid.Rows.Count - 1].Value = operations.description;
      operations_datagrid[operations_next_id.Index, operations_datagrid.Rows.Count - 1].Value = operations.next_operation_id;
      operations_datagrid[operations_id_wc.Index, operations_datagrid.Rows.Count - 1].Value = operations.work_center_id;
      OperationsCellClick(this, new DataGridViewCellEventArgs(operations_id.Index, operations_datagrid.Rows.Count - 1));
    }
    private void OperationsAddClick(object sender, EventArgs e)
    {
      try
      {
        if (operations_next_id_cb.SelectedIndex == -1)
          operations.next_operation_id = null;
        else operations.next_operation_id = int.Parse(operations_next_id_cb.SelectedItem.ToString());
        if (operations_wc_id_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать код рабочего центра!");
        else operations.work_center_id = int.Parse(operations_wc_id_cb.SelectedItem.ToString());
        int value;
        if(!int.TryParse(operations_setup_time_tb.Text,out value) || value < 0)
          throw new Exception("Некорректные данные в поле 'Время наладки'");
        else
          operations.setup_time = value;
        if (!int.TryParse(operations_pg_time_tb.Text, out value) || value < 0)
          throw new Exception("Некорректные данные в поле 'Время обработки'");
        else
          operations.processing_time = value;
        if (!int.TryParse(operations_transit_time_tb.Text, out value) || value < 0)
          throw new Exception("Некорректные данные в поле 'Время перехода'");
        else
          operations.transit_time = value;
        if (operations_description_tb.Text.Length == 0)
          operations.description = null;
        else operations.description = operations_description_tb.Text;
        db.operations.Add(operations);
        db.SaveChanges();
        OperationsAddRow(operations);
      }
      catch(Exception ex) {
        MessageBox.Show(ex.Message, "Ошибка добавления");
      }
    }
    private void OperationsCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < operations_datagrid.RowCount)
      {
        row = e.RowIndex;
        operations_datagrid[operations_id.Index, e.RowIndex].Selected = true;
        operations_id_tb.Text = operations_datagrid[operations_id.Index,row].Value.ToString();
        operations_setup_time_tb.Text = operations_datagrid[operations_setup_time.Index,row].Value.ToString();
        operations_pg_time_tb.Text = operations_datagrid[operations_pg_time.Index,row].Value.ToString();
        operations_transit_time_tb.Text = operations_datagrid[operations_transit_time.Index,row].Value.ToString();
        if (operations_datagrid[operations_description.Index, row].Value == null)
          operations_description_tb.Text = "";
        else operations_description_tb.Text = operations_datagrid[operations_description.Index, row].Value.ToString();
        operations_wc_id_cb.SelectedIndex =
        operations_wc_id_cb.Items.IndexOf(operations_datagrid[operations_id_wc.Index, row].Value.ToString());
        if (operations_datagrid[operations_next_id.Index, row].Value == null)
          operations_next_id_cb.SelectedIndex = -1;
        else operations_next_id_cb.SelectedIndex =
        operations_next_id_cb.Items.IndexOf(operations_datagrid[operations_next_id.Index, row].Value.ToString());
        operations_delete_btn.Enabled = true;
      }
      else
      {
        operations_id_tb.Clear();
        operations_setup_time_tb.Clear();
        operations_description_tb.Clear();
        operations_pg_time_tb.Clear();
        operations_transit_time_tb.Clear();
        operations_wc_id_cb.SelectedIndex = -1;
        operations_next_id_cb.SelectedIndex = -1;
        operations_delete_btn.Enabled = false;
      }
      operations_save_btn.Enabled = false;
    }
  }
}
