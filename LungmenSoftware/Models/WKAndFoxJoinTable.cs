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
    
    public partial class WKAndFoxJoinTable
    {
        public long Id { get; set; }
        public string Rev { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<int> FoxWorkStationId { get; set; }
        public Nullable<int> FoxSoftwareId { get; set; }
        public string LastModifier { get; set; }
        public string Note { get; set; }
        public Nullable<bool> IsLocked { get; set; }
    
        public virtual FoxSoftware FoxSoftware { get; set; }
        public virtual FoxWorkStation FoxWorkStation { get; set; }
    }
}
