using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Kurs.Формы
{
  public partial class Main : Form
  {
    private Mysql.stock stock = new Mysql.stock();
    private void StockCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < stock_datagrid.RowCount)
      {
        row = e.RowIndex;
        stock_datagrid[stock_id.Index, e.RowIndex].Selected = true;
        stock_id_textbox.Text = stock_datagrid[stock_id.Index, e.RowIndex].Value.ToString();
        //stock_discription_textbox.Text = warehouse_datagrid[description_warehouse.Index, e.RowIndex].Value.ToString();
        stock_delete_btn.Enabled = true;
      }
      else
      {
        stock_id_textbox.Clear();
        stock_value_textbox.Clear();
        stock_delete_btn.Enabled = false;
      }
      stock_save_btn.Enabled = false;
    }
  }
}
