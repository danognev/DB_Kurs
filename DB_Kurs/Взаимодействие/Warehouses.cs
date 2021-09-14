using System;
using System.Linq;
using System.Windows.Forms;

namespace DB_Kurs.Формы
{
  public partial class Main : Form
  {
    private Mysql.warehouse warehouse = new Mysql.warehouse();
    private void WarehouseDiscriptionTextBoxEnter(object sender, EventArgs e)
    {
      old_text = warehouse_discription_textbox.Text;
    }
    private void WarehouseSaveBtnClick(object sender, EventArgs e)
    {
      var row = db.warehouse.Where(x => x.id.ToString() == warehouse_id_textbox.Text.ToString()).First();
      row.discription = new_text;
      warehouse_datagrid[description_warehouse.Index, this.row].Value = new_text;
      db.SaveChanges();
      warehouse_save_btn.Enabled = false;
    }
    private void WarehouseDiscriptionChanged(object sender, EventArgs e)
    {
      if (warehouse_id_textbox.Text.Length != 0)
      {
        new_text = warehouse_discription_textbox.Text;
        warehouse_save_btn.Enabled = (old_text != new_text) ? true : false;
      }
    }
    private void WarehouseAddClick(object sender, EventArgs e)
    {
        warehouse.discription = warehouse_discription_textbox.Text;
        db.warehouse.Add(warehouse);
        db.SaveChanges();
        WarehouseAddRow(warehouse);
    }
    private void WarehouseDeleteClick(object sender, EventArgs e)
    {
      var row = db.warehouse.Where(x => x.id.ToString() == warehouse_id_textbox.Text.ToString()).First();
      db.warehouse.Remove(row);
      db.SaveChanges();
      warehouse_datagrid.Rows.RemoveAt(this.row);
      this.row--;
      if (this.row < 0 && warehouse_datagrid.RowCount > 0)
        this.row = 0;
      WarehouseContentClick(this, new DataGridViewCellEventArgs(id_warehouse.Index, this.row));
    }
    private void WarehouseAddRow(Mysql.warehouse warehouse) {
      warehouse_datagrid.Rows.Add();
      warehouse_datagrid[id_warehouse.Index, warehouse_datagrid.Rows.Count - 1].Value = warehouse.id;
      warehouse_datagrid[description_warehouse.Index, warehouse_datagrid.Rows.Count - 1].Value = warehouse.discription;
      WarehouseContentClick(this, new DataGridViewCellEventArgs(id_warehouse.Index, warehouse_datagrid.Rows.Count - 1));
    }
    private void WarehouseContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < warehouse_datagrid.RowCount)
      {
        row = e.RowIndex;
        warehouse_datagrid[id_warehouse.Index, e.RowIndex].Selected = true;
        warehouse_id_textbox.Text = warehouse_datagrid[id_warehouse.Index, e.RowIndex].Value.ToString();
        warehouse_discription_textbox.Text = warehouse_datagrid[description_warehouse.Index, e.RowIndex].Value.ToString();
        warehouse_delete_btn.Enabled = true;
      }
      else {
        warehouse_id_textbox.Clear();
        warehouse_discription_textbox.Clear();
        warehouse_delete_btn.Enabled = false;
      }
      warehouse_save_btn.Enabled = false;
    }
  }
}
