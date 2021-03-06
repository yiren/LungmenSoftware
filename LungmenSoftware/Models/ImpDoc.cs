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
    
    public partial class ImpDoc
    {
        public ImpDoc()
        {
            this.ChangeDeltas = new HashSet<ChangeDelta>();
            this.FoxSoftwares = new HashSet<FoxSoftware>();
            this.FoxWorkStations = new HashSet<FoxWorkStation>();
        }
    
        public long ImpDocId { get; set; }
        public string No { get; set; }
        public int Rev { get; set; }
        public string Description { get; set; }
        public short Unit { get; set; }
        public string LastModifier { get; set; }
        public Nullable<System.Guid> ChangeRequestId { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public int ImpDocTypeId { get; set; }
        public string Note { get; set; }
        public string Package { get; set; }
        public Nullable<int> DocTypeId { get; set; }
    
        public virtual ICollection<ChangeDelta> ChangeDeltas { get; set; }
        public virtual ChangeRequest ChangeRequest { get; set; }
        public virtual ImpDocType ImpDocType { get; set; }
        public virtual ICollection<FoxSoftware> FoxSoftwares { get; set; }
        public virtual ICollection<FoxWorkStation> FoxWorkStations { get; set; }
    }
}
