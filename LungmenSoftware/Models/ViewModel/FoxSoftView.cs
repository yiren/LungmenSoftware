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
}