using LungmenSoftware.Models.CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class NumacChangeDelta
    {
        
        public Guid NumacChangeDeltaId { get; set; }

        public Guid ModuleBoardId { get; set; }

        public string ChassisName { get; set; }

        public string ModuleBoardName { get; set; }

        public string Panel { get; set; }

        public string OriAssembly { get; set; }

        public string OriSerialNumber { get; set; }

        public string OriProgram { get; set; }

        public string OriRev { get; set; }



        public string Assembly { get; set; }

        public string SerialNumber { get; set; }

        public string Program { get; set; }

        public string Rev { get; set; }

        public string SocketLocation { get; set; }


        public Guid ChangeRequestId { get; set; }

        public ChangeRequest ChangeRequest { get; set; }
    
    }
}