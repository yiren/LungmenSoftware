using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LungmenSoftware.Models.DRS;
using System.Collections;


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
                orderby s.SystemName, p.DRSPanelName
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

        public IEnumerable<FID> GetFidsById(string id)
        {
            Guid panelId = new Guid(id);
            var data = db.FIDs.AsNoTracking().Where(f => f.DrsPanelId.Equals(panelId)).ToList();
            return data;
        }

        public IEnumerable<DrsSystemPanelViewModel> GetDrsSystemPanelList()
        {
            var data = from s in db.DrsSystems.AsNoTracking()
                       join p in db.DrsPanels.AsNoTracking() on s.SystemId equals p.SystemId
                       select new DrsSystemPanelViewModel
                       {
                           DRSPanelName = p.DRSPanelName,
                           SystemName = s.SystemName,
                           DrsPanelId = p.DrsPanelId,
                           SystemId = s.SystemId
                       };
            return data.ToList();
        }

        public FID GetFidById(Guid fidId)
        {
            return db.FIDs.FirstOrDefault(f => f.FidId.Equals(fidId));
        }

        public bool IsUpdated()
        {
            if (db.SaveChanges() != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}