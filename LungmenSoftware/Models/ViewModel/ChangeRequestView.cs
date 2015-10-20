using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models.CodeFirst.Entities;

namespace LungmenSoftware.Models.ViewModel
{
    public class ChangeRequestView
    {
        public List<ChangeRequestInfo> ChangeRequests { get; set; }
    }

    public class ChangeRequestSearchViewModel
    {
        //ChangeRequest Part
        [DisplayName("軟體變更表單編號")]
        public string SerialNumber { get; set; }
        [DisplayName("表單建立人")]
        public string CreatedBy { get; set; }
        [DisplayName("表單建立日期")]
        
        public string CreateStartDate { get; set; }
        public string CreateEndDate { get; set; }

        //public string Owner { get; set; }

        //Status
        [DisplayName("修改軟體名稱")]
        public string SoftwareName { get; set; }
        //public string WorkStationName { get; set; }
        [DisplayName("表單狀態")]
        public int StatusTypeId { get; set; }

        [DisplayName("是否結案")]
        public bool IsActive { get; set; }

        public IEnumerable<SelectListItem> Status { get; set; }

    }

    public class ChangeRequestViewModelForModification
    {
        public ChangeRequest ChangeRequest { get; set; }
        public List<ChangeDelta> ChangeDeltas { get; set; }

        public ChangeRequestMessage ChangeRequestMessage { get; set; }
        public List<ChangeRequestMessage> ChangeRequestMessages { get; set; }

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

        public string OriginalValue { get; set; }

        public string NewValue { get; set; } 

    }
}