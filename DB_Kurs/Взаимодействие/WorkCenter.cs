using System;
using System.Linq;
using System.Windows.Forms;

namespace DB_Kurs.Формы
{
  public partial class Main
  {
    private Mysql.work_center workcenter = new Mysql.work_center();
    private void WorkCenterAddRow(Mysql.work_center wcenter)
    {
      wcenter_datagrid.Rows.Add();
      wcenter_datagrid[wcenter_id.Index, wcenter_datagrid.Rows.Count - 1].Value = wcenter.id;
      wcenter_datagrid[wcenter_power.Index, wcenter_datagrid.Rows.Count - 1].Value = wcenter.power;
      wcenter_datagrid[wcenter_description.Index, wcenter_datagrid.Rows.Count - 1].Value = wcenter.description;
      WorkCenterCellClick(this, new DataGridViewCellEventArgs(wcenter_id.Index, wcenter_datagrid.Rows.Count - 1));
    }
    private void WorkCenterAddClick(object sender, EventArgs e)
    {
      try
      {
        workcenter.description = wcenter_description_tb.Text;
        workcenter.power = int.Parse(wcenter_power_tb.Text);
        db.work_center.Add(workcenter);
        db.SaveChanges();
        WorkCenterAddRow(workcenter);
      }
      catch(Exception) {
        MessageBox.Show("Некорректные данные", "Ошибка добавления!");
      }
    }
    private void WorkCenterSaveClick(object sender, EventArgs e)
    {
      var row = db.work_center.Where(x => x.id.ToString() == wcenter_id_tb.Text).First();
      row.description = wcenter_description_tb.Text;
      if (wcenter_power_tb.Text.Length != 0)
        row.power = int.Parse(wcenter_power_tb.Text);
      else row.power = null;
      wcenter_datagrid[wcenter_description.Index, this.row].Value = row.description;
      wcenter_datagrid[wcenter_power.Index, this.row].Value = row.power;
      db.SaveChanges();
      wcenter_save_btn.Enabled = false;
    }
    private void WorkCenterDeleteClick(object sender, EventArgs e)
    {
      var row = db.work_center.Where(x => x.id.ToString() == wcenter_id_tb.Text.ToString()).First();
      db.work_center.Remove(row);
      db.SaveChanges();
      for (int i = 0; i < operations_datagrid.RowCount; i++)
       if (operations_datagrid[operations_id_wc.Index, i].Value.ToString() == wcenter_id_tb.Text)
        operations_datagrid.Rows.RemoveAt(i);
       else continue;
      if (operations_wc_id_cb.Items.Contains(wcenter_id_tb.Text))
       operations_wc_id_cb.Items.Remove(operations_wc_id_cb.Items.IndexOf(wcenter_id_tb.Text));
      wcenter_datagrid.Rows.RemoveAt(this.row);
      this.row--;
      if (this.row < 0 && wcenter_datagrid.RowCount > 0)
        this.row = 0;
      WorkCenterCellClick(this, new DataGridViewCellEventArgs(wcenter_id.Index, this.row));
    }
    private void WorkCenterCellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < wcenter_datagrid.RowCount)
      {
        row = e.RowIndex;
        wcenter_datagrid[wcenter_id.Index, e.RowIndex].Selected = true;
        wcenter_id_tb.Text = wcenter_datagrid[wcenter_id.Index, e.RowIndex].Value.ToString();
        if (wcenter_datagrid[wcenter_power.Index, e.RowIndex].Value != null)
          wcenter_power_tb.Text = wcenter_datagrid[wcenter_power.Index, e.RowIndex].Value.ToString();
        else wcenter_power_tb.Text = "";
        wcenter_description_tb.Text = wcenter_datagrid[wcenter_description.Index, e.RowIndex].Value.ToString();
        wcenter_delete_btn.Enabled = true;
      }
      else
      {
        wcenter_id_tb.Clear();
        wcenter_power_tb.Clear();
        wcenter_description_tb.Clear();
        wcenter_delete_btn.Enabled = false;
      }
      wcenter_save_btn.Enabled = true;
    }
  }
}
