using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.ViewModel
{
    public class WorkStationsSearchViewModel
    {
        public FoxWorkStation workstation { get; set; }
        public IEnumerable<FoxWorkStation> AvailableWorkstations {get; set;}
        public IEnumerable<FoxWorkStation> SelectedWorkstations {get; set;}
        public FoxWorkStation PostedWorkstations { get; set; }
    }
}