using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class ChangeRequestStatus
    {
        //Status Pattern Level 2
        public int ChangeStatusId { get; set; }

        //Status Pattern Level 3

        public int StatusTypeId { get; set; }

        public Guid ChangeRequestId { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime? ChangeDate { get; set; }

        public DateTime? ReviewDate { get; set; }

        public DateTime? CommentDate { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}