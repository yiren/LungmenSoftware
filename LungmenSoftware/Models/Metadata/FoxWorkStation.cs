using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LungmenSoftware.Models
{
    [MetadataType(typeof(FoxWorkStationMetaData))]
    public partial class FoxWorkStation
    {
        public class FoxWorkStationMetaData{
            
            [Required]
            [Display(Name="工作站名稱")]
            public string WorkStationName { get; set; }
        }
    }
}