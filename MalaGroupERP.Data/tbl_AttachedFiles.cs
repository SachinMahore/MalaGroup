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
    
    public partial class tbl_AttachedFiles
    {
        public long ID { get; set; }
        public Nullable<int> PageID { get; set; }
        public Nullable<long> AGID { get; set; }
        public string SystemFileName { get; set; }
        public string OriginalFileName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedByID { get; set; }
    }
}