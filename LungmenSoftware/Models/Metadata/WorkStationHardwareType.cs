using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LungmenSoftware.Models
{
    [MetadataType(typeof(WorkStationHardwareTypeMetaData))]
    public partial class WorkStationHardwareType
    {
        public class WorkStationHardwareTypeMetaData
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }
            
            [Display(Name="工作站類型")]
            public string HardwareTypeName { get; set; }
        }
    }
}