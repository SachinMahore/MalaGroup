//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MalaGroupERP.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Calls
    {
        public long ID { get; set; }
        public Nullable<int> PageID { get; set; }
        public Nullable<long> AutoGenID { get; set; }
        public string CallSubject { get; set; }
        public string CallComment { get; set; }
        public string CallNameIds { get; set; }
        public string ReleatedToIds { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
