using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Kurs.Формы
{
  partial class Main
  {
    private Mysql.working_order worder = new Mysql.working_order();
    private void WorkingOrderAddRow(Mysql.working_order worder)
    {
      worder_datagrid.Rows.Add();
      worder_datagrid[worder_id.Index, worder_datagrid.Rows.Count - 1].Value = worder.id;
      worder_datagrid[worder_id_nomenclature.Index, worder_datagrid.Rows.Count - 1].Value = worder.nomenclature;
      worder_datagrid[worder_user_id.Index, worder_datagrid.Rows.Count - 1].Value = worder.user_id;
      worder_datagrid[worder_status.Index, worder_datagrid.Rows.Count - 1].Value = worder.status;
      worder_datagrid[worder_start_time.Index, worder_datagrid.Rows.Count - 1].Value = worder.start_date;
      worder_datagrid[worder_end_time.Index, worder_datagrid.Rows.Count - 1].Value = worder.end_date;
      worder_datagrid[worder_value.Index, worder_datagrid.Rows.Count - 1].Value = worder.value;
      worder_datagrid[worder_description.Index, worder_datagrid.Rows.Count - 1].Value = worder.description;
      WorkingOrderCellClick(this, new DataGridViewCellEventArgs(worder_id.Index, worder_datagrid.Rows.Count - 1));
    }
    private void WorkingOrderCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < worder_datagrid.RowCount)
      {
        row = e.RowIndex;
        worder_datagrid[worder_id.Index, e.RowIndex].Selected = true;
        worder_id_tb.Text = worder_datagrid[worder_id.Index, e.RowIndex].Value.ToString();
        worder_id_nomenclature_cb.SelectedIndex =
        worder_id_nomenclature_cb.Items.IndexOf(worder_datagrid[worder_id_nomenclature.Index, row].Value);
        worder_user_id_cb.SelectedIndex =
        worder_user_id_cb.Items.IndexOf(worder_datagrid[worder_user_id.Index, row].Value);
        worder_status_cb.SelectedIndex =
        worder_status_cb.Items.IndexOf(worder_datagrid[worder_status.Index, row].Value);
        worder_start_time_dp.Value = DateTime.Parse(worder_datagrid[worder_start_time.Index, row].Value.ToString());
        worder_end_time_dp.Value = DateTime.Parse(worder_datagrid[worder_end_time.Index, row].Value.ToString());
        worder_description_tb.Text = worder_datagrid[worder_description.Index, e.RowIndex].Value.ToString();
        worder_value_tb.Text = worder_datagrid[worder_value.Index, e.RowIndex].Value.ToString();
        if (worder_status_cb.SelectedItem.ToString() == "Запущенный")
          worder_cancel_btn.Enabled = true;
        else worder_cancel_btn.Enabled = false;
        if (worder_status_cb.SelectedItem.ToString() == "Завершенный")
        {
          worder_delete_btn.Enabled = false;
          worder_save_btn.Enabled = false;
        }
        else {
          worder_delete_btn.Enabled = true;
          wcenter_save_btn.Enabled = true;
        }
      }
      else
      {
        worder_id_tb.Clear();
        worder_description_tb.Clear();
        worder_value_tb.Clear();
        worder_id_nomenclature_cb.SelectedIndex = -1;
        worder_status_cb.SelectedIndex = -1;
        worder_user_id_cb.SelectedIndex = -1;
        worder_start_time_dp.Value = DateTime.Now;
        worder_end_time_dp.Value = DateTime.Now;
        worder_delete_btn.Enabled = false;
        worder_cancel_btn.Enabled = false;
      }
    }

    private void WorkingOrderSaveClick(object sender, EventArgs e)
    {
      try
      {
        var worder = db.working_order.Where(x => x.id.ToString() == worder_id_tb.Text).First();
        if (worder_id_nomenclature_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать номенклатуру");
        if (worder_user_id_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать сотрудника");
        if (worder_status_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать состояние");
        if (!int.TryParse(worder_value_tb.Text, out int value) || value < 0)
          throw new Exception("Некорректное количество");
        if (worder_description_tb.TextLength == 0)
          throw new Exception("Описание не может быть пустым");
        var stock = db.stock.Where(x => x.id_nomenclature.ToString() ==
        worder_id_nomenclature_cb.SelectedItem.ToString() && x.value >= value).First();
        stock.value -= value;
        for (int i = 0; i < stock_datagrid.RowCount; i++)
          if (stock_datagrid[stock_id.Index, i].Value.ToString() == stock.id.ToString())
          {
            stock_datagrid[stock_id.Index, i].Value = stock.value;
            break;
          }
          else continue;
        worder.nomenclature = int.Parse(worder_id_nomenclature_cb.SelectedItem.ToString());
        worder.user_id = int.Parse(worder_user_id_cb.SelectedItem.ToString());
        worder.status = worder_status_cb.SelectedItem.ToString();
        worder.value = value;
        worder.description = worder_description_tb.Text;
        worder.start_date = worder_start_time_dp.Value.TimeOfDay.StripMilliseconds();
        worder.end_date = worder_end_time_dp.Value.TimeOfDay.StripMilliseconds();
        db.SaveChanges();
        worder_datagrid[worder_id_nomenclature.Index, row].Value = worder.nomenclature;
        worder_datagrid[worder_user_id.Index, row].Value = worder.user_id;
        worder_datagrid[worder_status.Index, row].Value = worder.status;
        worder_datagrid[worder_start_time.Index, row].Value = worder.start_date;
        worder_datagrid[worder_end_time.Index, row].Value = worder.end_date;
        worder_datagrid[worder_value.Index, row].Value = worder.value;
        worder_datagrid[worder_description.Index, row].Value = worder.description;
        worder_save_btn.Enabled = false;
        if (worder_status_cb.SelectedItem.ToString() == "Запущенный")
          worder_cancel_btn.Enabled = true;
        else worder_cancel_btn.Enabled = false;
      }
      catch (InvalidOperationException)
      {
        MessageBox.Show("На складе недостаточно запасов!", "Ошибка заказа");
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Ошибка сохранения!");
      }
    }

    private void WorkingOrderCancelClick(object sender, EventArgs e)
    {
      var worder = db.working_order.Where(x => x.id.ToString() == worder_id_tb.Text).First();
      int value = int.Parse(worder_value_tb.Text);
      var stock = db.stock.Where(x => x.id_nomenclature.ToString() ==
        worder_id_nomenclature_cb.SelectedItem.ToString() && x.value >= value).First();
      stock.value += value;
      for (int i = 0; i < stock_datagrid.RowCount; i++)
        if (stock_datagrid[stock_id.Index, i].Value.ToString() == stock.id.ToString())
        {
          stock_datagrid[stock_value.Index, i].Value = stock.value;
          break;
        }
        else continue;
      worder_status_cb.SelectedIndex =
      worder_status_cb.Items.IndexOf("Завершенный");
      worder.status = worder_status_cb.SelectedItem.ToString();
      worder_datagrid[worder_status.Index, row].Value = worder.status;
      db.SaveChanges();
    }

    private void WorkingOrderAddClick(object sender, EventArgs e)
    {
      try
      {
        if (worder_id_nomenclature_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать номенклатуру");
        if (worder_user_id_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать сотрудника");
        if (worder_status_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать состояние");
        if (!int.TryParse(worder_value_tb.Text, out int value) || value < 0)
          throw new Exception("Некорректное количество");
        if (worder_description_tb.TextLength == 0)
          throw new Exception("Описание не может быть пустым");
        var stock = db.stock.Where(x => x.id_nomenclature.ToString() == 
        worder_id_nomenclature_cb.SelectedItem.ToString() && x.value >= value).First();
        stock.value -= value;
        for (int i = 0; i < stock_datagrid.RowCount; i++)
          if (stock_datagrid[stock_id.Index, i].Value.ToString() == stock.id.ToString())
          {
            stock_datagrid[stock_value.Index, i].Value = stock.value;
            break;
          }
          else continue;
          worder.nomenclature = int.Parse(worder_id_nomenclature_cb.SelectedItem.ToString());
          worder.user_id = int.Parse(worder_user_id_cb.SelectedItem.ToString());
          worder.status = worder_status_cb.SelectedItem.ToString();
          worder.value = value;
          worder.description = worder_description_tb.Text;
          worder.start_date = worder_start_time_dp.Value.TimeOfDay.StripMilliseconds();
          worder.end_date = worder_end_time_dp.Value.TimeOfDay.StripMilliseconds();
          db.working_order.Add(worder);
          db.SaveChanges();
          WorkingOrderAddRow(worder);
      }
      catch(InvalidOperationException) {
        MessageBox.Show("На складе недостаточно запасов!","Ошибка заказа");
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Ошибка добавления!");
      }
    }

    private void WorkingOrderDeleteClick(object sender, EventArgs e)
    {
      var row = db.working_order.Where(x => x.id.ToString() == worder_id_tb.Text.ToString()).First();
      db.working_order.Remove(row);
      db.SaveChanges();
      for (int i = 0; i < outlog_datagrid.RowCount; i++)
        if (outlog_datagrid[outlog_worder_id.Index, i].Value.ToString() == worder_id_tb.Text)
          outlog_datagrid.Rows.RemoveAt(i);
        else continue;
      if (outlog_worder_id_cb.Items.Contains(worder_id_tb.Text))
        outlog_worder_id_cb.Items.Remove(outlog_worder_id_cb.Items.IndexOf(worder_id_tb.Text));
      worder_datagrid.Rows.RemoveAt(this.row);
      this.row--;
      if (this.row < 0 && worder_datagrid.RowCount > 0)
        this.row = 0;
      WorkingOrderCellClick(this, new DataGridViewCellEventArgs(worder_id.Index, this.row));
    }
  }
}
