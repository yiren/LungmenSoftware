using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using LungmenSoftware.Models.CodeFirst.Entities;
using LungmenSoftware.Models.ViewModel;

namespace LungmenSoftware.Models.Service
{
    public class FoxSoftService
    {
        private LungmenSoftwareDataEntities ldb;

        public FoxSoftService()
        {
            this.ldb = new LungmenSoftwareDataEntities();
        }

        public List<FoxSoftware> GetFoxSoftwaresByTypeId(int softTypeId)
        {
            return ldb.FoxSoftwares.Where(t => t.FoxSoftwareTypeId == softTypeId).ToList();
        }

        public List<FoxSoftwareInfo> GetFoxSoftwareListByWorkStationId(int wkId)
        {
            //var joinTable = ldb.WKAndFoxJoinTables.Where(j => j.FoxWorkStationId == wkId).Select(s=>s.FoxSoftwareId);
            //var foxAppList = ldb.FoxSoftwares.Where(app => app.FoxSoftwareTypeId == 1);
            var query = from wk in ldb.FoxWorkStations.Where(wk => wk.WorkStationId == wkId)
                join wkandsoft in ldb.WKAndFoxJoinTables
                    on wk.WorkStationId equals wkandsoft.FoxWorkStationId
                //FoxSoftwareTypeId==1表示ApplicationSoftware, 2代表SystemSoftware
                join soft in ldb.FoxSoftwares
                    on wkandsoft.FoxSoftwareId equals soft.FoxSoftwareId
                select new FoxSoftwareInfo()
                {
                    SoftwareName=soft.SoftwareName, Rev=wkandsoft.Rev, Procedure=soft.Procedure,
                    Software_Library_Identification=soft.Media_Identification,
                    Media_Identification=soft.Media_Identification,
                    IsLocked = wkandsoft.IsLocked,
                    FoxSoftwareId=soft.FoxSoftwareId,
                    Note=wkandsoft.Note,
                    SoftwareTypeId=soft.FoxSoftwareTypeId,
                    JoinTableId = wkandsoft.Id
                } ;
            
            return query.ToList();
        }

        public List<FoxSoftware> GetSystemSoftwareList()
        {
            var listOfSysSoft = ldb.FoxSoftwares.Where(s => s.FoxSoftwareTypeId == 2).ToList();
            return listOfSysSoft;
            //return ldb.FoxSoftwareTypes.ToList();
        }

        public FoxSoftware GetFoxSoftwareById(int id)
        {
            return ldb.FoxSoftwares.Find(id);
        }

        public void UpdateInstalledSoftwareFromWorkstation(int softwareId, WKAndFoxJoinTable newJoinTable)
        {
            var softs = ldb.FoxSoftwares.ToList();

            var softToUpdate = softs.Single(s => s.FoxSoftwareId.Equals(softwareId));

            var query = from s in softs
                join j in ldb.WKAndFoxJoinTables on s.FoxSoftwareId equals j.FoxSoftwareId
                select j;
           
        }

        public bool UpdateSoftwareRev(List<ChangeDelta> data)
        {
            throw new NotImplementedException();
        }

        public WKAndFoxJoinTable GetJoinTableDataById(long joinTableId)
        {
            return ldb.WKAndFoxJoinTables.Find(joinTableId);
        }
    }
}