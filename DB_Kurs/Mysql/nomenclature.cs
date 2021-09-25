//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DB_Kurs.Mysql
{
    using System;
    using System.Collections.Generic;
    
    public partial class nomenclature
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public nomenclature()
        {
            this.stock = new HashSet<stock>();
            this.working_order = new HashSet<working_order>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string unit { get; set; }
        public string renewal_method { get; set; }
        public Nullable<int> marriage_rate { get; set; }
        public Nullable<int> additional_order_level { get; set; }
        public Nullable<int> additional_order_value { get; set; }
        public Nullable<System.TimeSpan> waiting_period { get; set; }
        public Nullable<int> production_route { get; set; }
        public Nullable<int> specification { get; set; }
        public string material_write_off_mode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<stock> stock { get; set; }
        public virtual specification specification1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<working_order> working_order { get; set; }
    }
}
