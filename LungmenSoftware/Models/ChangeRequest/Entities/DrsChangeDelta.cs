using LungmenSoftware.Models.CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class DrsChangeDelta
    {
        
        public Guid DrsChangeDeltaId { get; set; }

        public Guid FidId { get; set; }

        public string FIDDiagramNo { get; set; }

        public string DrsPanelName { get; set; }

        public int Division { get; set; }

        public string Description { get; set; }
        public string ModuleType { get; set; }

        public string Checksum { get; set; }

        public string EPROMRev { get; set; }

        public string OriModuleType { get; set; }

        public string OriChecksum { get; set; }

        public string OriEPROMRev { get; set; }

        public Guid ChangeRequestId { get; set; }

        public ChangeRequest ChangeRequest { get; set; }
    
    }
}