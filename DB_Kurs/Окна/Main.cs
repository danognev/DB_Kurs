using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Kurs.Формы
{
  public partial class Main : Form
  {
    private Mysql.ConnectionString db;
    public Main()
    {
      InitializeComponent();
      db = new Mysql.ConnectionString();
    }

    private void Main_Load(object sender, EventArgs e)
    {
      warehouse_datagrid.Rows.Add();
    }
  }
}
