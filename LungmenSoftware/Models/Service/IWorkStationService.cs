using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungmenSoftware.Models.Service
{
    interface IWorkStationsService
    {
         List<FoxWorkStation> GetAllWorkstations();
        List<FoxWorkStation> GetWorkStationsByType(string typeName);
         FoxWorkStation GetWorkStationByName(String name);
         FoxWorkStation GetWorkStationById(int id);
         
    }
}
