using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LungmenSoftware.Models.DRS;

namespace LungmenSoftware.Models.Service
{
    public class DrsDataService
    {
        DrsDbContext db=new DrsDbContext();

        public IEnumerable<DrsListViewModel> GetAllDrsDataList()
        {
            var data = from s in db.DrsSystems.AsNoTracking()
                join p in db.DrsPanels.AsNoTracking() on s.SystemId equals p.SystemId
                join f in db.FIDs.AsNoTracking() on p.DrsPanelId equals f.DrsPanelId
                select new DrsListViewModel()
                {
                    Checksum = f.Checksum,
                    Description = f.Description,
                    Division = f.Division,
                    DRSPanelName = p.DRSPanelName,
                    EPROMRev = f.EPROMRev,
                    EPROMSpecNo = f.EPROMSpecNo,
                    FIDDiagramNo = f.FIDDiagramNo,
                    FIDRev = f.FIDRev,
                    ModuleType = f.ModuleType,
                    SystemName = s.SystemName,
                    DrsPanelId = p.DrsPanelId,
                    FidId = f.FidId,
                    SystemId = s.SystemId
                };

            return data.ToList();


        }
    }
}