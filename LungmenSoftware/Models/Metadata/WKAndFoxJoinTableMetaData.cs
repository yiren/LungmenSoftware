using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LungmenSoftware.Models
{
    [MetadataType(typeof(WKAndFoxJoinTableMetaData))]
    public partial class WKAndFoxJoinTable
    {
        public class WKAndFoxJoinTableMetaData
        {
            [JsonIgnore]
            public virtual FoxSoftware FoxSoftware { get; set; }
            [JsonIgnore]
            public virtual FoxWorkStation FoxWorkStation { get; set; }
        }
    }
    
}