//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LungmenSoftware.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChangeStatu
    {
        public ChangeStatu()
        {
            this.ChangeRequests = new HashSet<ChangeRequest>();
        }
    
        public int ChangeStatusId { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<ChangeRequest> ChangeRequests { get; set; }
    }
}
