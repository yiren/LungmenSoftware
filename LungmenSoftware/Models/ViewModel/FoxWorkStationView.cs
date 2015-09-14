using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LungmenSoftware.Models.CodeFirst.Entities;

namespace LungmenSoftware.Models.ViewModel
{
    public class FoxWorkStationView
    {
        public string KeywordForSearch { get; set; }

        public List<RevInfo> WorkStationsBySoftwareId { get; set; }
       
        public List<FoxWorkStation> WPList { get; set; }

        public List<FoxWorkStation> AWList { get; set; }

        public List<FoxWorkStation> OtherList { get; set; }
    }
}