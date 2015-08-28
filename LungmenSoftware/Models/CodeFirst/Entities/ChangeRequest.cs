using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class ChangeRequest
    {
        //Business Columns

        public Guid ChangeRequestId { get; set; }
        [ScaffoldColumn(false)]
        public string SerialNumber { get; set; }

        public string Description { get; set; }
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public string ReviewBy { get; set; }
        [ScaffoldColumn(false)]
        public string ApprovedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime LastModifiedDate { get; set; }

        public string Note { get; set; }
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