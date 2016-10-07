using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungmenSoftware.Models.NUMACFirmware;
using LungmenSoftware.Models.ViewModel;
//using LungmenSoftware.Models;
using LungmenSoftware.Models.V2;

namespace LungmenSoftware.Models.Service
{
    public class NumacDataService
    {
        NumacFirewareDbContext db = new NumacFirewareDbContext();
        NumacDbContext db2 = new NumacDbContext();



        public List<NUMACFirmware.Chassis> GetChassis()
        {
            return db.Chassis.ToList();
        }

        public List<ChassisBoard> GetChassisBoards()
        {
            return db.ChassisBoards.ToList();
        }

        public List<FirmwareViewModelV2> GetFirmwareListV2()
        {
            var data = from c in db2.NumacSystems.AsNoTracking()
                       join b in db2.Chassis.AsNoTracking() on c.SystemId equals b.SystemId
                       join e in db2.ModuleBoards.AsNoTracking() on b.ChassisId equals e.ChassisId
                       orderby b.ChassisName, e.ModuleBoardName
                       select new FirmwareViewModelV2()
                       {
                           ChassisName = c.Name,
                           SerialNumber = e.SerialNumber,
                           SubSystem = b.ChassisName,
                           Assembly = e.Assembly,
                           ChassisBoardName = e.ModuleBoardName,
                           Program = e.Program,
                           Rev = e.Rev,
                           SocketLocation = e.SocketLocation,
                           Panel = c.Panel,
                           ChassisId = e.ChassisId,
                           ChassisBoardId = e.ModuleBoardId
                       };
            return data.ToList();
        }



        public List<FirmwareViewModel> GetFirmwareList()
        {
            var data = from c in db.Chassis.AsNoTracking()
                       join b in db.ChassisBoards.AsNoTracking() on c.ChassisId equals b.ChassisId
                       join e in db.EPROMs.AsNoTracking() on b.ChassisBoardId equals e.ChassisBoardId
                       orderby c.ChassisName, b.ChassBoardName, e.SocketLocation
                       select new FirmwareViewModel()
                       {
                           ChassisName = c.ChassisName,
                           SerialNumber = e.EPROMSerialNumber,
                           Assembly = e.EPROMAssembly,
                           ChassisBoardName = b.ChassBoardName,
                           Program = e.EPROMProgram,
                           Rev = e.EPROMProgramRev,
                           SocketLocation = e.SocketLocation,
                           Panel = c.Panel,
                           ChassisId = c.ChassisId,
                           ChassisBoardId = b.ChassisBoardId
                       };


            return data.ToList();
        }

        public NUMACFirmware.Chassis GetChassisById(Guid chassisId)
        {
            return db.Chassis.Find(chassisId);
        }

        public ChassisBoard GetChassisBoardById(Guid chassisBoardId)
        {
            return db.ChassisBoards.Find(chassisBoardId);
        }



        public List<NumacSystem> GetSystemPanelList()
        {
            return db2.NumacSystems.OrderBy(o => o.Panel).ToList();
        }

        public List<V2.Chassis> GetSubSystemById(Guid systemId)
        {

            return db2.Chassis.AsNoTracking().Where(c => c.SystemId.Equals(systemId)).OrderBy(c => c.ChassisName).ToList();
        }

        public List<ModuleBoard> GetModulesById(Guid chassisId)
        {
            return db2.ModuleBoards.AsNoTracking().Where(b => b.ChassisId.Equals(chassisId)).OrderBy(b => b.ModuleBoardName).ThenBy(b => b.SocketLocation).ToList();
        }

        public ModuleBoard GetModuleByModuleId(Guid moduleId)
        {
            return db2.ModuleBoards.FirstOrDefault(m => m.ModuleBoardId.Equals(moduleId));
        }

        public bool IsUpdated()
        {
            if (db2.SaveChanges() != -1)
            {
                return true;
            }else
            {
                return false;
            }
        }
    }
}
