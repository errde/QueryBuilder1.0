//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QueryBuilder1._0
{
    using System;
    using System.Collections.Generic;
    
    public partial class OPERAATTORI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPERAATTORI()
        {
            this.SQL_OPERAATTORI = new HashSet<SQL_OPERAATTORI>();
        }
    
        public double ID { get; set; }
        public string OPERAATTORI1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SQL_OPERAATTORI> SQL_OPERAATTORI { get; set; }
    }
}