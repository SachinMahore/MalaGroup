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
    
    public partial class tbl_EmailTemplates
    {
        public long TemplateID { get; set; }
        public string TemplateName { get; set; }
        public string TemplateSubject { get; set; }
        public string TemplateHTML { get; set; }
        public string TemplatePlainText { get; set; }
        public string AttachmentFileName { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<int> LastModifiedById { get; set; }
        public Nullable<int> TemplateFor { get; set; }
    }
}
