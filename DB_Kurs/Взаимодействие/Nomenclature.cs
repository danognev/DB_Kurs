using System;
using System.Linq;
using System.Windows.Forms;

namespace DB_Kurs.Формы
{
		public static class TimeExtensions
		{
				public static TimeSpan StripMilliseconds(this TimeSpan time)
				{
						return new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds);
				}
		}
		public partial class Main
		{
				private Mysql.nomenclature nomenclature = new Mysql.nomenclature();
				private void NomenclatureAddRow(Mysql.nomenclature nomenclature)
				{
						nomenclature_datagrid.Rows.Add();
						nomenclature_datagrid[nomenclature_id.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.id;
						nomenclature_datagrid[nomenclature_name.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.name;
						nomenclature_datagrid[nomenclature_unit.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.unit;
						nomenclature_datagrid[nomenclature_renewal_method.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.renewal_method;
						nomenclature_datagrid[nomenclature_marriage_rate.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.marriage_rate;
						nomenclature_datagrid[nomenclature_additional_order_level.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.additional_order_level;
						nomenclature_datagrid[nomenclature_additional_order_value.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.additional_order_value;
						nomenclature_datagrid[nomenclature_production_route.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.production_route;
						nomenclature_datagrid[nomenclature_specification.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.specification;
						nomenclature_datagrid[nomenclature_waiting_period.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.waiting_period;
						nomenclature_datagrid[nomenclature_write_off_mode.Index, nomenclature_datagrid.Rows.Count - 1].Value = nomenclature.material_write_off_mode;
						NomenclatureCellClick(this, new DataGridViewCellEventArgs(nomenclature_id.Index, nomenclature_datagrid.Rows.Count - 1));
				}
				private void NomenclatureSaveClick(object sender, EventArgs e)
				{
					try
					{
            var nomenclature = db.nomenclature.Where(x => x.id.ToString() == nomenclature_id_textbox.Text).First();
						if (nomenclature_name_tb.TextLength == 0)
							throw new Exception("Поле 'Наименование' не должно быть пустым");
						if (nomenclature_unit_tb.TextLength == 0)
							throw new Exception("Поле 'Единицы измерения' не должно быть пустым");
						if (nomenclature_rw_method_cb.SelectedIndex == -1)
							throw new Exception("Необходимо выбрать метод возобновления");
						if (!int.TryParse(nomenclature_marriage_rate_tb.Text, out int m_rate) || m_rate < 0 || m_rate > 100)
							throw new Exception("Некорректное значение в поле 'Процент брака'");
						if (!int.TryParse(nomenclature_ado_level_tb.Text, out int ado_level) || ado_level < 0)
							throw new Exception("Некорректное значение в поле 'Точка дозаказа'");
						if (!int.TryParse(nomenclature_ado_value_tb.Text, out int ado_value) || ado_value < 0)
							throw new Exception("Некорректное значение в поле 'Количество для дозаказа'");
						if (nomenclature_proute_cb.SelectedIndex == -1)
							throw new Exception("Необходимо выбрать маршрут производства");
						if (nomenclature_specification_cb.SelectedIndex == -1)
							throw new Exception("Необходимо выбрать спецификацию");
						if (nomenclature_woff_method_cb.SelectedIndex == -1)
							throw new Exception("Необходимо выбрать метод списания");
						nomenclature.name = nomenclature_name_tb.Text;
						nomenclature.unit = nomenclature_unit_tb.Text;
						nomenclature.renewal_method = nomenclature_rw_method_cb.SelectedItem.ToString();
						nomenclature.waiting_period = nomenclature_waiting_period_dp.Value.TimeOfDay.StripMilliseconds();
						nomenclature.marriage_rate = m_rate;
						nomenclature.additional_order_level = ado_level;
						nomenclature.additional_order_value = ado_value;
						nomenclature.production_route = int.Parse(nomenclature_proute_cb.SelectedItem.ToString());
						nomenclature.specification = int.Parse(nomenclature_specification_cb.SelectedItem.ToString());
						nomenclature.material_write_off_mode = nomenclature_woff_method_cb.SelectedItem.ToString();
						db.SaveChanges();
            nomenclature_datagrid[nomenclature_id.Index, row].Value = nomenclature.id;
            nomenclature_datagrid[nomenclature_name.Index, row].Value = nomenclature.name;
            nomenclature_datagrid[nomenclature_unit.Index, row].Value = nomenclature.unit;
            nomenclature_datagrid[nomenclature_renewal_method.Index, row].Value = nomenclature.renewal_method;
            nomenclature_datagrid[nomenclature_marriage_rate.Index, row].Value = nomenclature.marriage_rate;
            nomenclature_datagrid[nomenclature_additional_order_level.Index, row].Value = nomenclature.additional_order_level;
            nomenclature_datagrid[nomenclature_additional_order_value.Index, row].Value = nomenclature.additional_order_value;
            nomenclature_datagrid[nomenclature_production_route.Index, row].Value = nomenclature.production_route;
            nomenclature_datagrid[nomenclature_specification.Index, row].Value = nomenclature.specification;
            nomenclature_datagrid[nomenclature_waiting_period.Index, row].Value = nomenclature.waiting_period;
            nomenclature_datagrid[nomenclature_write_off_mode.Index, row].Value = nomenclature.material_write_off_mode;
            nomenclature_save_btn.Enabled = false;
          }
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Ошибка сохранения!");
					}
		    }

				private void NomenclatureAddClick(object sender, EventArgs e)
				{
						try {
								if (nomenclature_name_tb.TextLength == 0)
										throw new Exception("Поле 'Наименование' не должно быть пустым");
								if(nomenclature_unit_tb.TextLength == 0)
										throw new Exception("Поле 'Единицы измерения' не должно быть пустым");
								if(nomenclature_rw_method_cb.SelectedIndex == -1)
										throw new Exception("Необходимо выбрать метод возобновления");
								if (!int.TryParse(nomenclature_marriage_rate_tb.Text, out int m_rate) || m_rate < 0 || m_rate > 100)
										throw new Exception("Некорректное значение в поле 'Процент брака'");
								if (!int.TryParse(nomenclature_ado_level_tb.Text, out int ado_level) || ado_level < 0)
										throw new Exception("Некорректное значение в поле 'Точка дозаказа'");
								if (!int.TryParse(nomenclature_ado_value_tb.Text, out int ado_value) || ado_value < 0)
										throw new Exception("Некорректное значение в поле 'Количество для дозаказа'");
								if (nomenclature_proute_cb.SelectedIndex == -1)
										throw new Exception("Необходимо выбрать маршрут производства");
								if (nomenclature_specification_cb.SelectedIndex == -1)
										throw new Exception("Необходимо выбрать спецификацию");
								if (nomenclature_woff_method_cb.SelectedIndex == -1)
										throw new Exception("Необходимо выбрать метод списания");
								nomenclature.name = nomenclature_name_tb.Text;
								nomenclature.unit = nomenclature_unit_tb.Text;
								nomenclature.renewal_method = nomenclature_rw_method_cb.SelectedItem.ToString();
								nomenclature.waiting_period = nomenclature_waiting_period_dp.Value.TimeOfDay.StripMilliseconds();
								nomenclature.marriage_rate = m_rate;
								nomenclature.additional_order_level = ado_level;
								nomenclature.additional_order_value = ado_value;
								nomenclature.production_route = int.Parse(nomenclature_proute_cb.SelectedItem.ToString());
								nomenclature.specification = int.Parse(nomenclature_specification_cb.SelectedItem.ToString());
								nomenclature.material_write_off_mode = nomenclature_woff_method_cb.SelectedItem.ToString();
								db.nomenclature.Add(nomenclature);
								db.SaveChanges();
								NomenclatureAddRow(nomenclature);
						}
						catch(Exception ex) {
								MessageBox.Show(ex.Message, "Ошибка добавления!");
						}
				}

				private void NomenclatureDeleteClick(object sender, EventArgs e)
				{
						var row = db.nomenclature.Where(x => x.id.ToString() == nomenclature_id_textbox.Text.ToString()).First();
						db.nomenclature.Remove(row);
						db.SaveChanges();
						nomenclature_datagrid.Rows.RemoveAt(this.row);
						this.row--;
						if (this.row < 0 && nomenclature_datagrid.RowCount > 0)
								this.row = 0;
            for (int i = 0; i < stock_datagrid.RowCount; i++)
              if (stock_datagrid[stock_id_nomenclature.Index, i].Value.ToString() == nomenclature_id_textbox.Text)
                stock_datagrid.Rows.RemoveAt(i);
            if (worder_id_nomenclature_cb.Items.Contains(nomenclature_id_textbox.Text))
              worder_id_nomenclature_cb.Items.Remove(worder_id_nomenclature_cb.Items.IndexOf(nomenclature_id_textbox.Text));
            if (sof_components_id_nomenclature_cb.Items.Contains(nomenclature_id_textbox.Text))
              sof_components_id_nomenclature_cb.Items.Remove(sof_components_id_nomenclature_cb.Items.IndexOf(nomenclature_id_textbox.Text));
            if (stock_id_nomenclature_combobox.Items.Contains(nomenclature_id_textbox.Text))
              stock_id_nomenclature_combobox.Items.Remove(stock_id_nomenclature_combobox.Items.IndexOf(nomenclature_id_textbox.Text));
            NomenclatureCellClick(this, new DataGridViewCellEventArgs(nomenclature_id.Index, this.row));
				}
				private void NomenclatureCellClick(object sender, DataGridViewCellEventArgs e)
				{
						if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < nomenclature_datagrid.RowCount)
						{
								row = e.RowIndex;
								nomenclature_datagrid[nomenclature_id.Index, e.RowIndex].Selected = true;
								nomenclature_id_textbox.Text = nomenclature_datagrid[nomenclature_id.Index, row].Value.ToString();
								nomenclature_name_tb.Text = nomenclature_datagrid[nomenclature_name.Index, row].Value.ToString();
								nomenclature_unit_tb.Text = nomenclature_datagrid[nomenclature_unit.Index, row].Value.ToString();
								nomenclature_rw_method_cb.SelectedIndex =
								nomenclature_rw_method_cb.Items.IndexOf(nomenclature_datagrid[nomenclature_renewal_method.Index, row].Value.ToString());
								nomenclature_marriage_rate_tb.Text = nomenclature_datagrid[nomenclature_marriage_rate.Index, row].Value.ToString();
								nomenclature_ado_level_tb.Text = nomenclature_datagrid[nomenclature_additional_order_level.Index, row].Value.ToString();
								nomenclature_ado_value_tb.Text = nomenclature_datagrid[nomenclature_additional_order_value.Index, row].Value.ToString();
								nomenclature_waiting_period_dp.Text = nomenclature_datagrid[nomenclature_waiting_period.Index, row].Value.ToString();
								nomenclature_proute_cb.SelectedIndex =
								nomenclature_proute_cb.Items.IndexOf(nomenclature_datagrid[nomenclature_production_route.Index, row].Value);
								nomenclature_specification_cb.SelectedIndex =
								nomenclature_specification_cb.Items.IndexOf(nomenclature_datagrid[nomenclature_specification.Index, row].Value);
								nomenclature_woff_method_cb.SelectedIndex =
								nomenclature_woff_method_cb.Items.IndexOf(nomenclature_datagrid[nomenclature_write_off_mode.Index, row].Value.ToString());
                nomenclature_waiting_period_dp.Value = DateTime.Parse(nomenclature_datagrid[nomenclature_waiting_period.Index, row].Value.ToString());
								nomenclature_delete_btn.Enabled = true;
						}
						else
						{
								nomenclature_id_textbox.Clear();
								nomenclature_name_tb.Clear();
								nomenclature_unit_tb.Clear();
								nomenclature_marriage_rate_tb.Clear();
								nomenclature_ado_level_tb.Clear();
								nomenclature_ado_value_tb.Clear();
								nomenclature_waiting_period_dp.Value = System.DateTime.Today;
								nomenclature_rw_method_cb.SelectedIndex = -1;
								nomenclature_proute_cb.SelectedIndex = -1;
								nomenclature_specification_cb.SelectedIndex = -1;
								nomenclature_woff_method_cb.SelectedIndex = -1;
								nomenclature_delete_btn.Enabled = false;
						}
						nomenclature_save_btn.Enabled = true;
				}
		}
}
