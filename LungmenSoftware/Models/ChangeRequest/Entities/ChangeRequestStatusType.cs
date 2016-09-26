using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class ChangeRequestStatusType
    {
        //Status Pattern Level 3
        public int StatusTypeId { get; set; }

        [DisplayName("狀態名稱")]
        public string StatusName { get; set; }

        public int? NextStatusId { get; set; }

        public ChangeRequestStatusType NextStatus { get; set; }

    }
}