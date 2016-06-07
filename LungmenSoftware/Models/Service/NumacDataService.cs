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


        public List<FirmwareViewModel> GetFirmwareList()
        {
            var data = from c in db.Chassis.AsNoTracking()
                join b in db.ChassisBoards.AsNoTracking() on c.ChassisId equals b.ChassisId
                join e in db.EPROMs.AsNoTracking() on b.ChassisBoardId equals e.ChassisBoardId
                orderby b.ChassBoardName, e.SocketLocation
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
    }
}
