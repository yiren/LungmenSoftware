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
    
    public partial class WorkStationHardwareType
    {
        public WorkStationHardwareType()
        {
            this.FoxWorkStations = new HashSet<FoxWorkStation>();
        }
    
        public int Id { get; set; }
        public string HardwareTypeName { get; set; }
    
        public virtual ICollection<FoxWorkStation> FoxWorkStations { get; set; }
    }
}