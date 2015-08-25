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

        //Status Pattern
        public DateTime CreateDate { get; set; }

        public DateTime ReviewDate { get; set; }

        public DateTime? CommentDate { get; set; }

        public DateTime? ApprovedDate { get; set; }
        
        public DateTime? CompletedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

    }
}