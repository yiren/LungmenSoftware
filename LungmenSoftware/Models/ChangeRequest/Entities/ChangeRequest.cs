using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("表單編號")]
        public string SerialNumber { get; set; }

        [DisplayName("軟體變更敘述")]
        public string Description { get; set; }

        [DisplayName("軟體變更文件")]
        public string DesignDoc { get; set; }

        [DisplayName("申請人")]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("審查")]
        public string ReviewBy { get; set; }
        [ScaffoldColumn(false)]
        [DisplayName("核可")]
        public string ApprovedBy { get; set; }

        //Change Request Owner Tied To Status 
        [DisplayName("表單位置")]
        public string Owner { get; set; }

        [DisplayName("是否已結案")]
        public bool? IsActive { get; set; }

        [DisplayName("建立日期")]
        public DateTime CreateDate { get; set; }

        [DisplayName("最後修改日期")]
        public DateTime LastModifiedDate { get; set; }

        [DisplayName("備註")]
        public string Note { get; set; }

        public List<ChangeRequestStatus> ChangeRequestStatuses { get; set; }

        public List<ChangeDelta> ChangeDeltas { get; set; }

        public List<ChangeRequestMessage> ChangeRequestMessages { get; set; }

        public List<NumacChangeDelta> NumacChangeDeltas { get; set; }
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