using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace LungmenSoftware.Models.ViewModel
{
    public class FoxSoftwareInfo
    {
        public string SoftwareName { get; set; }

        public int SoftwareId { get; set; }
        public string Rev { get; set; }
        public string Procedure { get; set; }
        public string Software_Library_Identification { get; set; }
        public string Media_Identification { get; set; }
        public string Note { get; set; }
        public Nullable<int> SoftwareTypeId { get; set; }
        public Nullable<bool> IsLocked { get; set; }

    }
}