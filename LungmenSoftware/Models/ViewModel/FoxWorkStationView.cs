using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models.CodeFirst.Entities;

namespace LungmenSoftware.Models.ViewModel
{
    public class FoxWorkStationView
    {
        public string KeywordForSearch { get; set; }

        public List<FoxSoftwareInfo> WorkStationsBySoftwareId { get; set; }
       
        public List<FoxWorkStation> WPList { get; set; }

        public List<FoxWorkStation> AWList { get; set; }

        public List<FoxWorkStation> OtherList { get; set; }
    }

    public class WorkStationViewModelForEdit
    {
        public FoxWorkStation FoxWorkStation { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; } 
    }
}