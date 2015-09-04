using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.ViewModel
{
    public class FoxWorkStationInfo
    {
        public string WorkStationName { get; set; }

        public string SoftwareName { get; set; }

        public string Rev { get; set; }

        public string Procedure { get; set; }

        public string Note { get; set; }

        public int WorkstationId { get; set; }
    }
}