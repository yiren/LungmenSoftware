using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LungmenSoftware.Models
{
    [MetadataType(typeof(FoxAppSoftwareMetaData))]
    public partial class FoxAppSoftware
    {
        public class FoxAppSoftwareMetaData
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }
            [Required]
            [Display(Name = "軟體名稱")]
            public string Name { get; set; }
            [Required]
            [Display(Name = "版本")]
            public Nullable<double> Rev { get; set; }
            public string Procedure { get; set; }
            public string Note { get; set; }
        }
    }
}