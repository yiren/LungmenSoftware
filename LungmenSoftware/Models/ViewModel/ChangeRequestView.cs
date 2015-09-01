using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LungmenSoftware.Models.CodeFirst.Entities;

namespace LungmenSoftware.Models.ViewModel
{
    public class ChangeRequestView
    {
        public List<ChangeRequestInfo> ChangeRequests { get; set; }
    }

    public class ChangeRequestInfo
    {
        //change request class
        public Guid ChangeRequestId { get; set; }

        public string SerialNumber { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public string Owner { get; set; }

        public string ReviewBy { get; set; }

        public string ApprovedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        //status type class
        public int StatusTypeId { get; set; }
        public string StatusName { get; set; }

        //status class
        public DateTime InitialDate { get; set; }

        public DateTime? ChangeDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}