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
    public Main()
    {
      InitializeComponent();
    }

    private void Main_Load(object sender, EventArgs e)
    {
      // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet.working_order". При необходимости она может быть перемещена или удалена.
      this.working_orderTableAdapter.Fill(this.dataSet.working_order);
      // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet.work_center". При необходимости она может быть перемещена или удалена.
      this.work_centerTableAdapter.Fill(this.dataSet.work_center);
      // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet.warehouse". При необходимости она может быть перемещена или удалена.
      this.warehouseTableAdapter.Fill(this.dataSet.warehouse);
      // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet.technolog_map". При необходимости она может быть перемещена или удалена.
      this.technolog_mapTableAdapter.Fill(this.dataSet.technolog_map);
      // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet.stock". При необходимости она может быть перемещена или удалена.
      this.stockTableAdapter.Fill(this.dataSet.stock);
      // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet.output_log". При необходимости она может быть перемещена или удалена.
      this.output_logTableAdapter.Fill(this.dataSet.output_log);
      // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet.operations". При необходимости она может быть перемещена или удалена.
      this.operationsTableAdapter.Fill(this.dataSet.operations);
      // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet.set_of_components". При необходимости она может быть перемещена или удалена.
      this.set_of_componentsTableAdapter.Fill(this.dataSet.set_of_components);
      // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.specification". При необходимости она может быть перемещена или удалена.
      this.specificationTableAdapter.Fill(this.dataSet1.specification);
      // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet.nomenclature". При необходимости она может быть перемещена или удалена.
      this.nomenclatureTableAdapter.Fill(this.dataSet.nomenclature);

    }

    private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
    {

    }
  }
}
