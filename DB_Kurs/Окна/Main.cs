using System;
using System.Linq;
using System.Windows.Forms;

namespace DB_Kurs.Формы
{
  public partial class Main : Form
  {
    private Mysql.ConnectionString db;
    private string old_text, new_text;
    private int row;
    enum Tabs {
      nomenclature,
      specification,
      set_of_components,
      operations,
      output_log,
      stock,
      technolog_map,
      warehouse,
      work_center,
      working_order
    }
    public Main()
    {
      InitializeComponent();
      db = new Mysql.ConnectionString();
    }

    private void Main_Load(object sender, EventArgs e)
    {
      foreach (var warehouse in db.warehouse)
      {
        WarehouseAddRow(warehouse);
      }
      foreach (var stock in db.stock)
      {
        StockAddRow(stock);
      }
      foreach (var nomenclature in db.nomenclature)
      {
        NomenclatureAddRow(nomenclature);
      }
    }
    private void MainFormTabIndexChanged(object sender, EventArgs e)
    {
      switch(main_tab_control.SelectedIndex) {
        case (int)Tabs.stock: {
          var warehouses = db.warehouse.ToList();
          foreach(var warehouse in warehouses) {
              if (!stock_id_warehouse_combobox.Items.Contains(warehouse.id))
                stock_id_warehouse_combobox.Items.Add(warehouse.id);
              else continue;
            }
            var nomenclature = db.nomenclature.ToList();
            foreach (var num in nomenclature)
            {
              if (!stock_id_nomenclature_combobox.Items.Contains(num.id))
                stock_id_nomenclature_combobox.Items.Add(num.id);
              else continue;
            }
            break;
        }
      }
    }
    private void MainFormClosed(object sender, FormClosedEventArgs e)
    {
      db.Dispose();
      Application.Exit();
    }
  }
}
