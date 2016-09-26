using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class ModuleInfo
    {
        public int ModuleInfoId { get; set; }

        public string ChassisName { get; set; }

        public string SocketLocation { get; set; }

        public string Assembly { get; set; }

        public string SerialNumber { get; set; }

        public string Program { get; set; }

        public string Rev { get; set; }

        public NumacChangeDelta NumacChangeDelta { get; set; }

        public Guid NumacChangeDeltaId { get; set; }
    }
}