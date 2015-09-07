using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
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
                    Software_Library_Identification=wkandsoft.Media_Identification,
                    Media_Identification=wkandsoft.Media_Identification,
                    IsLocked = wkandsoft.IsLocked,
                    SoftwareId=soft.FoxSoftwareId,
                    Note=wkandsoft.Note,
                    SoftwareTypeId=soft.FoxSoftwareTypeId
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


        public bool UpdateSoftwareRev(FoxSoftware softFromDb, string rev)
        {
            var query = ldb.WKAndFoxJoinTables
                .Where(j => j.FoxSoftwareId == softFromDb.FoxSoftwareId).ToList();
            foreach (var soft in query)
            {
                soft.Rev = rev;
            }

            var isUpdated=ldb.SaveChanges();

            return isUpdated > 0;
        }
    }
}