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
    
    public partial class syResource
    {
        public syResource()
        {
            this.syNavigationNodes = new HashSet<syNavigationNode>();
            this.syRoleResources = new HashSet<syRoleResource>();
        }
    
        public int ResourceId { get; set; }
        public string Resource { get; set; }
        public Nullable<byte> ResourceTypeId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Nullable<int> UsedIn { get; set; }
        public Nullable<int> IsRight { get; set; }
        public string TabText { get; set; }
        public string PageTitle { get; set; }
        public string Icon { get; set; }
        public string ClickEent { get; set; }
    
        public virtual ICollection<syNavigationNode> syNavigationNodes { get; set; }
        public virtual ICollection<syRoleResource> syRoleResources { get; set; }
    }
}
