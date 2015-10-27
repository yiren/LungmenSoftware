using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace LungmenSoftware.Models
{
    [MetadataType(typeof(FoxWorkStationMetaData))]
    public partial class FoxWorkStation
    {
        public class FoxWorkStationMetaData{
            
            [Required]
            [Display(Name = "工作站名稱")]
            public string WorkStationName { get; set; }
            [JsonIgnore]
            public virtual ICollection<WKAndFoxJoinTable> WKAndFoxJoinTables { get; set; }
            [JsonIgnore]
            [DisplayName("工作站類型")]
            public virtual WorkStationHardwareType WorkStationHardwareType { get; set; }

        }
    }
}