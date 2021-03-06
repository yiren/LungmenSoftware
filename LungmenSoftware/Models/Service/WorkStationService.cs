﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LungmenSoftware.Models.ViewModel;


namespace LungmenSoftware.Models.Service
{
    public class WorkStationService : IWorkStationsService
    {
        private LungmenSoftwareDataEntities ldb;
     
        public WorkStationService()
        {
            ldb = new LungmenSoftwareDataEntities();
        }

        public void UpdateWorkStationInfo(FoxWorkStation wk)
        {
            wk.LastModifiedDate=DateTime.Now;
            ldb.SaveChanges();
        }

        public List<FoxWorkStation> GetAllWorkstations()
        {
            return ldb.FoxWorkStations.ToList(); ;
        }

        public List<FoxWorkStation> GetWorkStationsByType(string typeName)
        {
            return ldb.FoxWorkStations.Where(w => w.WorkStationName.Contains(typeName)).ToList();
        }

        public FoxWorkStation GetWorkStationByName(String name)
        {
            var wkquery = ldb.FoxWorkStations.Where(k => k.WorkStationName.Equals(name));
            int queryCount = wkquery.Count();
            if (queryCount == 0)
            {
                Console.WriteLine("no workstation is found");
                return null;
            }
            return null;
        }

        public FoxWorkStation GetWorkStationById(int id)
        {
            return ldb.FoxWorkStations.Find(id);
        }

        public List<WorkStationHardwareType> GetWKHardareTypes()
        {
            return ldb.WorkStationHardwareTypes.ToList();
        }

        public List<FoxWorkStationInfo> GetWorkStationsBySoftwareId(int softId)
        {
            var softwareName = ldb.FoxSoftwares.Find(softId).SoftwareName;
            var query = from soft in ldb.FoxSoftwares.Where(s => s.FoxSoftwareId == softId)
                join wkJoinTable in ldb.WKAndFoxJoinTables 
                    on soft.FoxSoftwareId equals wkJoinTable.FoxSoftwareId
                join wk in ldb.FoxWorkStations
                    on wkJoinTable.FoxWorkStationId equals wk.WorkStationId
                select new FoxWorkStationInfo()
                {
                    WorkStationName = wk.WorkStationName,
                    SoftwareName=soft.SoftwareName,
                    Rev=wkJoinTable.Rev,
                    Procedure=soft.Procedure,
                    Note=wkJoinTable.Note
                };
            
        

            return query.ToList();
        } 
    }
}