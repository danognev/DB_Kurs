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
    private Mysql.output_log o_log = new Mysql.output_log();
    private void OutputLogAddRow(Mysql.output_log o_log)
    {
      outlog_datagrid.Rows.Add();
      outlog_datagrid[outlog_id_comp.Index, outlog_datagrid.Rows.Count - 1].Value = o_log.id;
      outlog_datagrid[outlog_wc_id.Index, outlog_datagrid.Rows.Count - 1].Value = o_log.work_center_id;
      outlog_datagrid[outlog_worder_id.Index, outlog_datagrid.Rows.Count - 1].Value = o_log.working_order_id;
      outlog_datagrid[outlog_time.Index, outlog_datagrid.Rows.Count - 1].Value = o_log.elapsed_time;
      OutputLogCellClick(this, new DataGridViewCellEventArgs(outlog_id_comp.Index, outlog_datagrid.Rows.Count - 1));
    }
    private void OutputLogCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < outlog_datagrid.RowCount)
      {
        row = e.RowIndex;
        outlog_datagrid[outlog_id_comp.Index, e.RowIndex].Selected = true;
        outlog_id_tb.Text = outlog_datagrid[outlog_id_comp.Index, e.RowIndex].Value.ToString();
        outlog_wc_id_cb.SelectedIndex =
        outlog_wc_id_cb.Items.IndexOf(outlog_datagrid[outlog_wc_id.Index, row].Value);
        outlog_worder_id_cb.SelectedIndex =
        outlog_worder_id_cb.Items.IndexOf(outlog_datagrid[outlog_worder_id.Index, row].Value);
        outlog_time_tb.Text = outlog_datagrid[outlog_time.Index, e.RowIndex].Value.ToString();
        outlog_delete_btn.Enabled = true;
      }
      else
      {
        outlog_id_tb.Clear();
        outlog_time_tb.Clear();
        outlog_wc_id_cb.SelectedIndex = -1;
        outlog_worder_id_cb.SelectedIndex = -1;
        outlog_delete_btn.Enabled = false;
      }
      outlog_save_btn.Enabled = true;
    }

    private void OutputLogSaveClick(object sender, EventArgs e)
    {
      try
      {
        var o_log = db.output_log.Where(x => x.id.ToString() == outlog_id_tb.Text).First();
        if (outlog_wc_id_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать рабочий центр");
        if (outlog_worder_id_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать производственный заказ");
        if (!int.TryParse(outlog_time_tb.Text, out int out_time))
          throw new Exception("Некорректные данные в поле 'Затраченное время'");
        o_log.working_order_id = int.Parse(outlog_worder_id_cb.SelectedItem.ToString());
        o_log.work_center_id = int.Parse(outlog_wc_id_cb.SelectedItem.ToString());
        o_log.elapsed_time = out_time;
        db.SaveChanges();
        outlog_datagrid[outlog_wc_id.Index, row].Value = o_log.work_center_id;
        outlog_datagrid[outlog_worder_id.Index, row].Value = o_log.working_order_id;
        outlog_datagrid[outlog_time.Index, row].Value = o_log.elapsed_time;
        outlog_save_btn.Enabled = false;
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Ошибка сохранения!");
      }
    }

    private void OutputLogAddClick(object sender, EventArgs e)
    {
      try {
        if (outlog_wc_id_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать рабочий центр");
        if (outlog_worder_id_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать производственный заказ");
        if (!int.TryParse(outlog_time_tb.Text, out int out_time))
          throw new Exception("Некорректные данные в поле 'Затраченное время'");
        o_log.working_order_id = int.Parse(outlog_worder_id_cb.SelectedItem.ToString());
        o_log.work_center_id = int.Parse(outlog_wc_id_cb.SelectedItem.ToString());
        o_log.elapsed_time = out_time;
        db.output_log.Add(o_log);
        db.SaveChanges();
        OutputLogAddRow(o_log);
      }
      catch(Exception ex) {
        MessageBox.Show(ex.Message, "Ошибка добавления!");
      }
    }

    private void OutputLogDeleteClick(object sender, EventArgs e)
    {
      var row = db.output_log.Where(x => x.id.ToString() == outlog_id_tb.Text.ToString()).First();
      db.output_log.Remove(row);
      db.SaveChanges();
      outlog_datagrid.Rows.RemoveAt(this.row);
      this.row--;
      if (this.row < 0 && outlog_datagrid.RowCount > 0)
        this.row = 0;
      OutputLogCellClick(this, new DataGridViewCellEventArgs(outlog_id_comp.Index, this.row));
    }
  }
}
