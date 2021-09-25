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
    private Mysql.specification specification = new Mysql.specification();
    private void SpecificationAddRow(Mysql.specification specification) {
      specification_datagrid.Rows.Add();
      specification_datagrid[specification_id.Index, specification_datagrid.Rows.Count - 1].Value = specification.id;
      specification_datagrid[specification_status.Index, specification_datagrid.Rows.Count - 1].Value = specification.status;
      specification_datagrid[specification_set_of_components.Index, specification_datagrid.Rows.Count - 1].Value = specification.set_of_components;
      if (specification.discription == null)
        specification_datagrid[specification_description.Index, specification_datagrid.Rows.Count - 1].Value = null;
      else
        specification_datagrid[specification_description.Index, specification_datagrid.Rows.Count - 1].Value = specification.discription;
      SpecificationCellClick(this, new DataGridViewCellEventArgs(specification_id.Index, specification_datagrid.Rows.Count - 1));
    }
    private void SpecificationAddClick(object sender, EventArgs e)
    {
      try {
        if (specification_status_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать состояние");
        if (specification_sof_components_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать набор компонентов");
        if (specification_description_tb.TextLength == 0)
          specification.discription = null;
        else specification.discription = specification_description_tb.Text;
        specification.status = specification_status_cb.SelectedItem.ToString();
        specification.set_of_components = int.Parse(specification_sof_components_cb.SelectedItem.ToString());
        db.specification.Add(specification);
        db.SaveChanges();
        SpecificationAddRow(specification);
      }
      catch(Exception ex) {
        MessageBox.Show(ex.Message, "Ошибка добавления!");
      }
    }

    private void SpecificationSaveClick(object sender, EventArgs e)
    {
      var row = db.specification.Where(x => x.id.ToString() == specification_id_tb.Text.ToString()).First();
      try
      {
        if (specification_status_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать состояние");
        if (specification_sof_components_cb.SelectedIndex == -1)
          throw new Exception("Необходимо выбрать набор компонентов");
        if (specification_description_tb.TextLength == 0)
          row.discription = null;
        else row.discription = specification_description_tb.Text;
        row.status = specification_status_cb.SelectedItem.ToString();
        row.set_of_components = int.Parse(specification_sof_components_cb.SelectedItem.ToString());
        specification_datagrid[specification_description.Index, this.row].Value = row.discription;
        specification_datagrid[specification_set_of_components.Index, this.row].Value = row.set_of_components;
        specification_datagrid[specification_status.Index, this.row].Value = row.status;
        db.SaveChanges();
        specification_save_btn.Enabled = false;
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Ошибка сохранения!");
      }
    }

    private void SpecificationDeleteClick(object sender, EventArgs e)
    {
      var row = db.specification.Where(x => x.id.ToString() == specification_id_tb.Text.ToString()).First();
      db.specification.Remove(row);
      db.SaveChanges();
      specification_datagrid.Rows.RemoveAt(this.row);
      this.row--;
      if (this.row < 0 && specification_datagrid.RowCount > 0)
        this.row = 0;
      SpecificationCellClick(this, new DataGridViewCellEventArgs(specification_id.Index, this.row));
    }
    private void SpecificationCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < specification_datagrid.RowCount)
      {
        row = e.RowIndex;
        specification_datagrid[specification_id.Index, e.RowIndex].Selected = true;
        specification_id_tb.Text = specification_datagrid[specification_id.Index, e.RowIndex].Value.ToString();
        specification_status_cb.SelectedIndex =
        specification_status_cb.Items.IndexOf(specification_datagrid[specification_status.Index, row].Value);
        if(specification_datagrid[specification_set_of_components.Index, row].Value != null)
         specification_sof_components_cb.SelectedIndex =
         specification_sof_components_cb.Items.IndexOf(specification_datagrid[specification_set_of_components.Index, row].Value);
        if (specification_datagrid[specification_description.Index, e.RowIndex].Value == null)
          specification_description_tb.Text = "";
        else specification_description_tb.Text = specification_datagrid[specification_description.Index, e.RowIndex].Value.ToString();
        specification_delete_btn.Enabled = true;
      }
      else
      {
        specification_id_tb.Clear();
        specification_description_tb.Clear();
        specification_status_cb.SelectedIndex = -1;
        specification_sof_components_cb.SelectedIndex = -1;
        specification_delete_btn.Enabled = false;
      }
      specification_save_btn.Enabled = true;
    }
  }
}
