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
      foreach (var specification in db.specification)
      {
        SpecificationAddRow(specification);
      }
      foreach (var set_of_components in db.set_of_components)
      {
        SetOfComponentsAddRow(set_of_components);
      }
      foreach (var wc in db.work_center)
      {
        WorkCenterAddRow(wc);
      }
      foreach (var operations in db.operations)
      {
        OperationsAddRow(operations);
      }
      foreach (var tmap in db.technolog_map)
      {
        TechnologMapAddRow(tmap);
      }
      foreach (var ol in db.output_log)
      {
        OutputLogAddRow(ol);
      }
      foreach (var worder in db.working_order)
      {
        WorkingOrderAddRow(worder);
      }
      MainFormTabIndexChanged(this, new EventArgs());
    }
    private void MainFormTabIndexChanged(object sender, EventArgs e)
    {
      switch (main_tab_control.SelectedIndex) {
        case (int)Tabs.nomenclature: {
            var t_route = db.technolog_map.ToList();
            foreach(var tr in t_route) {
              if (!nomenclature_proute_cb.Items.Contains(tr.id))
                nomenclature_proute_cb.Items.Add(tr.id);
              else continue;
            }
            var specification = db.specification.ToList();
            foreach (var sp in specification)
            {
              if (!nomenclature_specification_cb.Items.Contains(sp.id))
                nomenclature_specification_cb.Items.Add(sp.id);
              else continue;
            }
            NomenclatureCellClick(this, new DataGridViewCellEventArgs(nomenclature_id.Index, 0));
            break;
          }
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
            StockCellClick(this, new DataGridViewCellEventArgs(stock_id.Index, 0));
            break;
        }
        case (int)Tabs.set_of_components: {
            var nomenclature = db.nomenclature.ToList();
            foreach (var nom in nomenclature)
              if (!sof_components_id_nomenclature_cb.Items.Contains(nom.id))
                sof_components_id_nomenclature_cb.Items.Add(nom.id);
              else continue;
            SetOfComponentsCellClick(this, new DataGridViewCellEventArgs(sof_components_id.Index, 0));
            break;
          }
        case (int)Tabs.specification:
          {
            var sof_components = db.set_of_components.ToList();
            foreach (var sof in sof_components)
              if (!specification_sof_components_cb.Items.Contains(sof.id_set))
                specification_sof_components_cb.Items.Add(sof.id_set);
              else continue;
            SpecificationCellClick(this, new DataGridViewCellEventArgs(specification_id.Index, 0));
            break;
          }
        case (int)Tabs.operations:
          {
            var work_center = db.work_center.ToList();
            foreach (var wc in work_center)
              if (!operations_wc_id_cb.Items.Contains(wc.id))
                operations_wc_id_cb.Items.Add(wc.id);
              else continue;
            var operations = db.operations.ToList();
            foreach (var operation in operations)
              if (!operations_next_id_cb.Items.Contains(operation.id))
                operations_next_id_cb.Items.Add(operation.id);
              else continue;
            OperationsCellClick(this, new DataGridViewCellEventArgs(operations_id.Index, 0));
            break;
          }
        case (int)Tabs.output_log: {
            var work_center = db.work_center.ToList();
            foreach (var wc in work_center)
              if (!outlog_wc_id_cb.Items.Contains(wc.id))
                outlog_wc_id_cb.Items.Add(wc.id);
              else continue;
            var work_order = db.working_order.ToList();
            foreach (var wo in work_order)
              if (!outlog_worder_id_cb.Items.Contains(wo.id))
                outlog_worder_id_cb.Items.Add(wo.id);
              else continue;
            OutputLogCellClick(this, new DataGridViewCellEventArgs(outlog_id_comp.Index, 0));
            break;
        }
        case (int)Tabs.technolog_map:
          {
            var operations = db.operations.ToList();
            foreach (var operation in operations)
              if (!tmap_id_operation_cb.Items.Contains(operation.id))
                tmap_id_operation_cb.Items.Add(operation.id);
              else continue;
            TechnologMapCellClick(this, new DataGridViewCellEventArgs(tmap_id.Index, 0));
            break;
          }
        case (int)Tabs.working_order: {
            var nomenclature = db.nomenclature.ToList();
            foreach (var num in nomenclature)
              if (!worder_id_nomenclature_cb.Items.Contains(num.id))
                worder_id_nomenclature_cb.Items.Add(num.id);
              else continue;
            var users = db.users.ToList();
            foreach (var user in users)
              if (!worder_user_id_cb.Items.Contains(user.id_user))
                worder_user_id_cb.Items.Add(user.id_user);
              else continue;
            WorkingOrderCellClick(this, new DataGridViewCellEventArgs(worder_id.Index, 0));
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
