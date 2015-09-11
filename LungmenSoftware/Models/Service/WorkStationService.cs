using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<FoxWorkStationAndSoftwareInfo> GetWorkStationsBySoftwareId(int softId)
        {
            var query = from soft in ldb.FoxSoftwares.Where(s => s.FoxSoftwareId == softId)
                join wkJoinTable in ldb.WKAndFoxJoinTables 
                    on soft.FoxSoftwareId equals wkJoinTable.FoxSoftwareId
                join wk in ldb.FoxWorkStations
                    on wkJoinTable.FoxWorkStationId equals wk.WorkStationId
                        //where soft.FoxSoftwareId==1 && soft.FoxSoftwareTypeId==1
                select new FoxWorkStationAndSoftwareInfo()
                {
                    FoxWorkStationId= wk.WorkStationId,
                    WorkStationName = wk.WorkStationName,
                    SoftwareName=soft.SoftwareName,
                    Rev=wkJoinTable.Rev,
                    Procedure=soft.Procedure,
                    Note=wkJoinTable.Note
                };
            
        

            return query.ToList();
        }

        public void AddNewWorkStation(FoxWorkStation wk)
        {
            
            ldb.FoxWorkStations.Add(wk);
            ldb.SaveChanges();
        }


        //For AngularJS
        public List<IGrouping<string,WKAndFoxJoinTable>> AjaxRequestForWorkstationsBySoftId(int id)
        {
            var query = from soft in ldb.FoxSoftwares.Where(s => s.FoxSoftwareId == id)
                        join wkJoinTable in ldb.WKAndFoxJoinTables
                            on soft.FoxSoftwareId equals wkJoinTable.FoxSoftwareId
                        join wk in ldb.FoxWorkStations 
                            on wkJoinTable.FoxWorkStationId equals wk.WorkStationId
                        //where soft.FoxSoftwareId==1 && soft.FoxSoftwareTypeId==1
                        select new SoftAndWks()
                        {
                            Rev = wkJoinTable.Rev,
                            FoxSoftwareId = soft.FoxSoftwareId,
                            FoxWorkStationId = wkJoinTable.FoxWorkStationId,
                            WorkStationName = wk.WorkStationName
                        };

            IEnumerable<IGrouping<string, WKAndFoxJoinTable>> query2 = 
                from s in ldb.FoxSoftwares.Where(s => s.FoxSoftwareId == id)
                         join j in ldb.WKAndFoxJoinTables on s.FoxSoftwareId equals j.FoxSoftwareId
                        group j by j.Rev into grouping
                        orderby grouping.Key descending
                        select grouping;

            return query2.ToList();
        }
    }

    public class SoftAndWks
    {
        public int? FoxWorkStationId { get; set; }
        public int FoxSoftwareId { get; set; }
        public string WorkStationName { get; set; }
        public string Rev { get; set; }
    }
}