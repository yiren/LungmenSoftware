﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class ChangeRequestStatusType
    {
        //Status Pattern Level 3
        public int StatusTypeId { get; set; }

        public string StatusName { get; set; }

        public ICollection<ChangeRequestStatus> ChangeRequestStatuses { get; set; }
    }
}