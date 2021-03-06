//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AsyncLab.WebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BINSIGNM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BINSIGNM()
        {
            this.BINSIGNDs = new HashSet<BINSIGND>();
            this.BINSIGNBs = new HashSet<BINSIGNB>();
        }
    
        public string SIGNID { get; set; }
        public string SID { get; set; }
        public string SIGNKIND { get; set; }
        public string KINDNOS { get; set; }
        public string STARTYPE { get; set; }
        public string SIGNUSER { get; set; }
        public Nullable<System.DateTime> BINTIME { get; set; }
        public string CRT_USER { get; set; }
        public Nullable<System.DateTime> CRT_TIME { get; set; }
        public string REMARK { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BINSIGND> BINSIGNDs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BINSIGNB> BINSIGNBs { get; set; }
    }
}
