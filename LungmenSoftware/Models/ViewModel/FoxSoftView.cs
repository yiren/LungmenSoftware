using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.ViewModel
{
    public class FoxSoftView
    {
        public string WorkStationName { get; set; }
        public List<FoxSoftwareInfo> APList { get; set; }
        public List<FoxSoftwareInfo> OSList { get; set; }
        public List<FoxSoftware> APs { get; set; }
        public List<FoxSoftware> OSs { get; set; }
    }

    public class SoftwareToUpdate
    {
        public int FoxSoftwareId { get; set; }
        public string Rev { get; set; }
        public List<WKAndFoxJoinTable> JoinTableUpdate { get; set; } 
    }
}