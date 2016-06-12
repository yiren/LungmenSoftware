using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungmenSoftware.Models.NUMACFirmware;
using LungmenSoftware.Models.ViewModel;

namespace LungmenSoftware.Models.Service
{
    public class NumacDataService
    {
        NumacFirewareDbContext db = new NumacFirewareDbContext();

        public List<Chassis> GetChassis()
        {
            return db.Chassis.ToList();
        }

        public List<ChassisBoard> GetChassisBoards()
        {
            return db.ChassisBoards.ToList();
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

        public Chassis GetChassisById(Guid chassisId)
        {
            return db.Chassis.Find(chassisId);
        }

        public ChassisBoard GetChassisBoardById(Guid chassisBoardId)
        {
            return db.ChassisBoards.Find(chassisBoardId);
        }
    }
}
