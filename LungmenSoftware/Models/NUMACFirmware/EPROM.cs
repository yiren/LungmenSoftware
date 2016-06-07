using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungmenSoftware.Models.NUMACFirmware
{
    public class EPROM
    {
        [Key]
        public Guid EPROMId { get; set; }

        public string SocketLocation { get; set; }

        public string EPROMAssembly { get; set; }

        public int EPROMAssemblyRev { get; set; }

        public int PartsListRev { get; set; }

        public string EPROMProgram { get; set; }

        public int EPROMProgramRev { get; set; }

        public string EPROMSerialNumber { get; set; }

        public Guid ChassisBoardId { get; set; }

        public ChassisBoard ChassisBoard { get; set; }


    }
}
