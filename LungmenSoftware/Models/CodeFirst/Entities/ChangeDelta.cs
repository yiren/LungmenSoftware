using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class ChangeDelta
    {
        public Guid ChangeDeltaId { get; set; }

        public int FoxSoftwareId { get; set; }
        public string originalValue { get; set; }
        public string newValue { get; set; }
        public List<int> NewWorkstationIds { get; set; }

        public Guid ChangeRequestId { get; set; }
        public ChangeRequest ChangeRequest { get; set; }
    }
}