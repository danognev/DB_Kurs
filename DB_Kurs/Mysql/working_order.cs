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
    
    public partial class working_order
    {
        public int id { get; set; }
        public int nomenclature { get; set; }
        public int user_id { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public Nullable<int> value { get; set; }
        public string description { get; set; }
    
        public virtual nomenclature nomenclature1 { get; set; }
        public virtual users users { get; set; }
    }
}
