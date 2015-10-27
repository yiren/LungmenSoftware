﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class ChangeDelta
    {
        public Guid ChangeDeltaId { get; set; }

        public int FoxSoftwareId { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public List<RevInfo> RevInfos { get; set; }

        public Guid ChangeRequestId { get; set; }
        public ChangeRequest ChangeRequest { get; set; }
    }
}