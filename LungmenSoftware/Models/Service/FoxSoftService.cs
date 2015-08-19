using System;
using System.Collections.Generic;
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
    }
}