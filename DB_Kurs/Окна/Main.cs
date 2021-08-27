using System;
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
    }
    private void MainFormTabIndexChanged(object sender, EventArgs e)
    {
      switch(main_tab_control.SelectedIndex) {
        case (int)Tabs.stock: {
          break;
        }
      }
    }

    private void MainFormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }
  }
}
