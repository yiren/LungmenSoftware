using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.DRS
{
    public class DrsModel
    {
    }

    public class DrsSystem
    {
        public Guid SystemId { get; set; }
        public string SystemName { get; set; }

        [JsonIgnore]
        public ICollection<DrsPanel> DrsPanel { get; set; }
    }

    public class DrsPanel
    {
        public Guid PanelId { get; set; }
        public string DrsPanelName { get; set; }

        public Guid SystemId { get; set; }
        public DrsSystem DrsSystem { get; set; }
        [JsonIgnore]
        public ICollection<FID> FIDs { get; set; }

    }

    public class FID
    {
        public Guid FidId { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string FidDiagramNo { get; set; }

        [StringLength(50)]
        public string ModuleType { get; set; }

        public int Division { get; set; }

        [StringLength(20)]
        public string EPROMSpecNo { get; set; }
        [StringLength(25)]
        public string Checksum { get; set; }

        [StringLength(15)]
        public string Rev { get; set; }
        public Guid DrsPanelId { get; set; }
        [JsonIgnore]
        public Panel DrsPanel { get; set; }
    }
}