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
    private Mysql.technolog_map tmap = new Mysql.technolog_map();
    private void TechnologMapAddRow(Mysql.technolog_map tmap)
    {
      tmap_datagrid.Rows.Add();
      tmap_datagrid[tmap_id.Index, tmap_datagrid.Rows.Count - 1].Value = tmap.id;
      tmap_datagrid[tmap_operation_id.Index, tmap_datagrid.Rows.Count - 1].Value = tmap.id_initial_operation;
      tmap_datagrid[tmap_status.Index, tmap_datagrid.Rows.Count - 1].Value = tmap.status;
      tmap_datagrid[tmap_description.Index, tmap_datagrid.Rows.Count - 1].Value = tmap.description;
      TechnologMapCellClick(this, new DataGridViewCellEventArgs(tmap_id.Index, tmap_datagrid.Rows.Count - 1));
    }
    private void TechnologMapAddClick(object sender, EventArgs e)
    {
      try {
        if (tmap_id_operation_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать код начальной операции!");
        else tmap.id_initial_operation = int.Parse(tmap_id_operation_cb.SelectedItem.ToString());
        if (tmap_status_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать состояние!");
        else tmap.status = tmap_status_cb.SelectedItem.ToString();
        if (tmap_description_tb.Text.Length == 0)
          throw new Exception("Поле Описание не может быть пустым!");
        else tmap.description = tmap_description_tb.Text;
        db.technolog_map.Add(tmap);
        db.SaveChanges();
        TechnologMapAddRow(tmap);
      }
      catch(Exception ex) {
        MessageBox.Show(ex.Message, "Ошибка добавления");
      }
    }
    private void TechnologMapDeleteClick(object sender, EventArgs e)
    {
      var row = db.technolog_map.Where(x => x.id.ToString() == tmap_id_tb.Text.ToString()).First();
      db.technolog_map.Remove(row);
      db.SaveChanges();
      tmap_datagrid.Rows.RemoveAt(this.row);
      this.row--;
      if (this.row < 0 && tmap_datagrid.RowCount > 0)
        this.row = 0;
      TechnologMapCellClick(this, new DataGridViewCellEventArgs(tmap_id.Index, this.row));
    }
    private void TechnologMapCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < tmap_datagrid.RowCount)
      {
        row = e.RowIndex;
        tmap_datagrid[tmap_id.Index, e.RowIndex].Selected = true;
        tmap_id_tb.Text = tmap_datagrid[tmap_id.Index, e.RowIndex].Value.ToString();
        tmap_description_tb.Text = tmap_datagrid[tmap_description.Index, e.RowIndex].Value.ToString();
        tmap_id_operation_cb.SelectedIndex =
        tmap_id_operation_cb.Items.IndexOf(tmap_datagrid[tmap_operation_id.Index, row].Value);
        tmap_status_cb.SelectedIndex =
        tmap_status_cb.Items.IndexOf(tmap_datagrid[tmap_status.Index, row].Value);
        tmap_delete_btn.Enabled = true;
      }
      else
      {
        tmap_id_tb.Clear();
        tmap_description_tb.Clear();
        tmap_id_operation_cb.SelectedIndex = -1;
        tmap_status_cb.SelectedIndex = -1;
        tmap_delete_btn.Enabled = false;
      }
      tmap_save_btn.Enabled = false;
    }
  }
}
