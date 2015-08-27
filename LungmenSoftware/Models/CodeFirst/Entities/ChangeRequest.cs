using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class ChangeRequest
    {
        //Business Columns

        public Guid ChangeRequestId { get; set; }
        
        public string SerialNumber { get; set; }

        public string CreatedBy { get; set; }

        public string ReviewBy { get; set; }

        public string ApprovedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public List<ChangeRequestStatus> ChangeRequestStatuses { get; set; }

        //Status Pattern Level 1
        //public DateTime CreateDate { get; set; }

        //public DateTime ReviewDate { get; set; }

        //public DateTime? CommentDate { get; set; }

        //public DateTime? ApprovedDate { get; set; }

        //public DateTime? CompletedDate { get; set; }

        //Status Pattern Level 2
        //public int ChangeStatus { get; set; }

        //public DateTime StatusDate { get; set; }


    }
}