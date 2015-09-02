using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LungmenSoftware.Models
{
    [MetadataType(typeof(FoxSoftwareMetadata))]
    public partial class FoxSoftware
    {
        public class FoxSoftwareMetadata
        {
            [JsonIgnore]
            public virtual FoxSoftwareType FoxSoftwareType { get; set; }
            [JsonIgnore]
            public virtual ICollection<WKAndFoxJoinTable> WKAndFoxJoinTables { get; set; }
        }
    }
}