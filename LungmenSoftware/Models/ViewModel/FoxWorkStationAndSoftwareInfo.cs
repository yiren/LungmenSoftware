using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models
{
    public class FoxWorkStationAndSoftwareInfo
    {
        public int WorkstationId { get; set; }

        public int FoxSoftwareId { get; set; }

        public string WorkStationName { get; set; }

        public string SoftwareName { get; set; }

        public string Rev { get; set; }

        public string Procedure { get; set; }

        public string Software_Library_Identification { get; set; }

        public string Media_Identification { get; set; }

        public string Note { get; set; }

        
    }
}