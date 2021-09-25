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
    private Mysql.set_of_components sof_components = new Mysql.set_of_components();
    private void SetOfComponentsAddRow(Mysql.set_of_components set_of_components)
    {
      sof_components_datagrid.Rows.Add();
      sof_components_datagrid[sof_components_id.Index, sof_components_datagrid.Rows.Count - 1].Value = set_of_components.id_set;
      sof_components_datagrid[sof_components_id_nomenclature.Index, sof_components_datagrid.Rows.Count - 1].Value = set_of_components.nomenclature;
      sof_components_datagrid[sof_components_value.Index, sof_components_datagrid.Rows.Count - 1].Value = set_of_components.value;
      SetOfComponentsCellClick(this, new DataGridViewCellEventArgs(sof_components_id.Index, sof_components_datagrid.Rows.Count - 1));
    }
    private void SetOfComponentsAddClick(object sender, EventArgs e)
    {
      if (sof_components_id_nomenclature_cb.SelectedIndex == -1)
        sof_components.nomenclature = null;
      else sof_components.nomenclature = int.Parse(sof_components_id_nomenclature_cb.SelectedItem.ToString());
      try {
        if (int.Parse(sof_components_value_tb.Text) < 0)
          throw new Exception();
        else sof_components.value = int.Parse(sof_components_value_tb.Text);
        db.set_of_components.Add(sof_components);
        db.SaveChanges();
        SetOfComponentsAddRow(sof_components);
      }
      catch(Exception) {
        MessageBox.Show("Некорректные данные в поле 'Количество'", "Ошибка добавления!");
      }
    }
    private void SetOfComponentsDeleteClick(object sender, EventArgs e)
    {
      var row = db.set_of_components.Where(x => x.id_set.ToString() == sof_components_id_tb.Text.ToString()).First();
      db.set_of_components.Remove(row);
      db.SaveChanges();
      sof_components_datagrid.Rows.RemoveAt(this.row);
      this.row--;
      if (this.row < 0 && sof_components_datagrid.RowCount > 0)
        this.row = 0;
      SetOfComponentsCellClick(this, new DataGridViewCellEventArgs(sof_components_id.Index, this.row));
    }
    private void SetOfComponentsSaveClick(object sender, EventArgs e)
    {
      var row = db.set_of_components.Where(x => x.id_set.ToString() == sof_components_id_tb.Text.ToString()).First();
      try
      {
        if (!int.TryParse(sof_components_value_tb.Text, out int value) || value < 0)
          throw new Exception("Некорректное значение в поле 'Количество'");
        row.value = value;
        if (sof_components_id_nomenclature_cb.SelectedIndex != -1)
        {
          row.nomenclature = int.Parse(sof_components_id_nomenclature_cb.SelectedItem.ToString());
          sof_components_datagrid[sof_components_id_nomenclature.Index, this.row].Value = row.nomenclature;
        }
        else sof_components_datagrid[sof_components_id_nomenclature.Index, this.row].Value = null;
        sof_components_datagrid[sof_components_value.Index, this.row].Value = value;
        db.SaveChanges();
        sof_components_save_btn.Enabled = false;
      }
      catch(Exception ex) {
        MessageBox.Show(ex.Message, "Ошибка сохранения!");
      }
    }
    private void SetOfComponentsCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < sof_components_datagrid.RowCount)
      {
        row = e.RowIndex;
        sof_components_datagrid[sof_components_id.Index, e.RowIndex].Selected = true;
        sof_components_id_tb.Text = sof_components_datagrid[sof_components_id.Index, e.RowIndex].Value.ToString();
        if(sof_components_datagrid[sof_components_id_nomenclature.Index, row].Value != null)
          sof_components_id_nomenclature_cb.SelectedIndex =
        sof_components_id_nomenclature_cb.Items.IndexOf(sof_components_datagrid[sof_components_id_nomenclature.Index, row].Value);
        sof_components_value_tb.Text = sof_components_datagrid[sof_components_value.Index, e.RowIndex].Value.ToString();
        sof_components_delete_btn.Enabled = true;
      }
      else
      {
        sof_components_id_tb.Clear();
        sof_components_value_tb.Clear();
        sof_components_id_nomenclature_cb.SelectedIndex = -1;
        sof_components_delete_btn.Enabled = false;
      }
      sof_components_save_btn.Enabled = true;
    }
  }
}
