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
    
    public partial class operations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public operations()
        {
            this.technolog_map = new HashSet<technolog_map>();
        }
    
        public int id { get; set; }
        public int work_center_id { get; set; }
        public Nullable<int> setup_time { get; set; }
        public Nullable<int> processing_time { get; set; }
        public Nullable<int> transit_time { get; set; }
        public string description { get; set; }
        public Nullable<int> next_operation_id { get; set; }
    
        public virtual work_center work_center { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<technolog_map> technolog_map { get; set; }
    }
}