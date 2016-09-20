using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class ChangeDelta
    {
        public Guid ChangeDeltaId { get; set; }

        public int FoxSoftwareId { get; set; }
        [DisplayName("原建置值")]
        public string OriginalValue { get; set; }
        [DisplayName("欲修改值")]
        public string NewValue { get; set; }
        public List<RevInfo> RevInfos { get; set; }

        public Guid ChangeRequestId { get; set; }
        public ChangeRequest ChangeRequest { get; set; }
    }
    public class NumacChangeDelta
    {
        public Guid NumacChangeDeltaId { get; set; }

        public Guid ModuleBoardId { get; set; }

        public string OriginalValue { get; set; }
        [DisplayName("欲修改值")]
        public string NewValue { get; set; }
        public List<RevInfo> RevInfos { get; set; }

        public Guid ChangeRequestId { get; set; }
        public ChangeRequest ChangeRequest { get; set; }
    }
}