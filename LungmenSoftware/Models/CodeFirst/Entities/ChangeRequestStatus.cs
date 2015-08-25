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

        public string StatusName { get; set; }

        public ICollection<ChangeRequest> ChangeRequests { get; set; }


    }
}