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
    
    public partial class tbl_AgentOrder
    {
        public long OrderID { get; set; }
        public Nullable<long> LeadID { get; set; }
        public Nullable<int> AgentID { get; set; }
        public Nullable<int> StepCompleted { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string PinNo { get; set; }
        public Nullable<int> IsCompleted { get; set; }
        public Nullable<long> AccountID { get; set; }
    }
}