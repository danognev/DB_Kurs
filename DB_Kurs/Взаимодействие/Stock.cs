using System;
using System.Linq;
using System.Windows.Forms;

namespace DB_Kurs.Формы
{
  public partial class Main : Form
  {
    private uint old_num_id, new_num_id, old_war_id, new_war_id;
    private Mysql.stock stock = new Mysql.stock();
    private void StockCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < stock_datagrid.RowCount)
      {
        row = e.RowIndex;
        stock_datagrid[stock_id.Index, e.RowIndex].Selected = true;
        stock_id_textbox.Text = stock_datagrid[stock_id.Index, e.RowIndex].Value.ToString();
        stock_id_nomenclature_combobox.SelectedIndex = 
        stock_id_nomenclature_combobox.Items.IndexOf(stock_datagrid[stock_id_nomenclature.Index,row].Value);
        stock_id_warehouse_combobox.SelectedIndex = 
        stock_id_warehouse_combobox.Items.IndexOf(stock_datagrid[stock_id_warehouse.Index, row].Value);
        stock_value_textbox.Text = stock_datagrid[stock_value.Index, e.RowIndex].Value.ToString();
        stock_delete_btn.Enabled = true;
      }
      else
      {
        stock_id_textbox.Clear();
        stock_value_textbox.Clear();
        stock_id_nomenclature_combobox.SelectedIndex = -1;
        stock_id_warehouse_combobox.SelectedIndex = -1;
        stock_delete_btn.Enabled = false;
      }
      stock_save_btn.Enabled = false;
    }
    private void StockEnter(object sender, EventArgs e)
    {
      if (sender.Equals(stock_value_textbox))
        old_text = stock_value_textbox.Text;
      if (sender.Equals(stock_id_warehouse_combobox))
        old_war_id = (uint)stock_id_warehouse_combobox.SelectedIndex;
      if (sender.Equals(stock_id_nomenclature_combobox))
        old_num_id = (uint)stock_id_nomenclature_combobox.SelectedIndex;
    }
    private void StockSaveBtnClick(object sender, EventArgs e)
    {
      var row = db.stock.Where(x => x.id.ToString() == stock_id_textbox.Text.ToString()).First();
      row.value = int.Parse(new_text);
      row.id_warehouse = int.Parse(stock_id_warehouse_combobox.Items[(int)new_war_id].ToString());
      row.id_nomenclature = int.Parse(stock_id_nomenclature_combobox.Items[(int)new_num_id].ToString());
      stock_datagrid[stock_value.Index, this.row].Value = new_text;
      stock_datagrid[stock_id_nomenclature.Index, this.row].Value = row.id_nomenclature;
      stock_datagrid[stock_id_warehouse.Index, this.row].Value = row.id_warehouse;
      db.SaveChanges();
      stock_save_btn.Enabled = false;
    }
    private void StockDeleteBtnClick(object sender, EventArgs e)
    {
      var row = db.stock.Where(x => x.id.ToString() == stock_id_textbox.Text.ToString()).First();
      db.stock.Remove(row);
      db.SaveChanges();
      stock_datagrid.Rows.RemoveAt(this.row);
      this.row--;
      if (this.row < 0 && stock_datagrid.RowCount > 0)
        this.row = 0;
      StockCellClick(this, new DataGridViewCellEventArgs(stock_id.Index, this.row));
    }
    private void StockAddBtnClick(object sender, EventArgs e)
    {
      try
      {
        if (stock_id_warehouse_combobox.SelectedItem == null || stock_id_nomenclature_combobox.SelectedItem == null ||
        stock_value_textbox.Text == null)
          throw new Exception();
        stock.id_warehouse = int.Parse(stock_id_warehouse_combobox.SelectedItem.ToString());
        stock.id_nomenclature = int.Parse(stock_id_nomenclature_combobox.SelectedItem.ToString());
        stock.value = int.Parse(stock_value_textbox.Text);
        db.stock.Add(stock);
        db.SaveChanges();
        StockAddRow(stock);
      }
      catch(Exception) {
        MessageBox.Show("Необходимо заполнить все поля!", "Ошибка добавления");
      }
    }
    private void StockAddRow(Mysql.stock stock)
    {
      stock_datagrid.Rows.Add();
      stock_datagrid[stock_id.Index, stock_datagrid.Rows.Count - 1].Value = stock.id;
      stock_datagrid[stock_id_warehouse.Index, stock_datagrid.Rows.Count - 1].Value = stock.id_warehouse;
      stock_datagrid[stock_id_nomenclature.Index, stock_datagrid.Rows.Count - 1].Value = stock.id_nomenclature;
      stock_datagrid[stock_value.Index, stock_datagrid.Rows.Count - 1].Value = stock.value;
      StockCellClick(this, new DataGridViewCellEventArgs(stock_id.Index, stock_datagrid.Rows.Count - 1));
    }
    private void StockParamsChanged(object sender, EventArgs e)
    {
      if (stock_id_textbox.Text.Length != 0)
      {
        if (sender.Equals(stock_value_textbox))
          new_text = stock_value_textbox.Text;
        if (sender.Equals(stock_id_warehouse_combobox))
          new_war_id = (uint)stock_id_warehouse_combobox.SelectedIndex;
        if (sender.Equals(stock_id_nomenclature_combobox))
          new_num_id = (uint)stock_id_nomenclature_combobox.SelectedIndex;
        stock_save_btn.Enabled = ((old_text != new_text || old_num_id != new_num_id || old_war_id != new_war_id) && stock_id_textbox.Text != null) ? true : false;
      }
    }
  }
}
