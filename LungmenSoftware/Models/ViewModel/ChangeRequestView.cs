using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models.CodeFirst.Entities;
using System.ComponentModel.DataAnnotations;

namespace LungmenSoftware.Models.ViewModel
{
    public class ChangeRequestView
    {
        public List<ChangeRequestInfo> ChangeRequests { get; set; }
    }

    public class ChangeRequestSearchViewModel
    {
        //ChangeRequest Part
        [DisplayName("表單編號")]
        public string SerialNumber { get; set; }
        [DisplayName("表單建立人")]
        public string CreatedBy { get; set; }
        [DisplayName("表單建立日期")]
        public string CreateStartDate { get; set; }
        public string CreateEndDate { get; set; }
        [DisplayName("變更敘述")]
        public string Description { get; set; }

        //Status
        [DisplayName("軟體名稱")]
        public string SoftwareName { get; set; }
        //public string WorkStationName { get; set; }
        [DisplayName("表單狀態")]
        public int StatusTypeId { get; set; }

        public List<CheckBoxListModel> Status { get; set; }

        public List<ChangeRequestInfo> SearchResult { get; set; }
    }

    public class ChangeRequestViewModelForModification
    {
        public ChangeRequest ChangeRequest { get; set; }
        public List<ChangeDelta> ChangeDeltas { get; set; }

        public ChangeRequestMessage ChangeRequestMessage { get; set; }
        public List<ChangeRequestMessage> ChangeRequestMessages { get; set; }

    }

    public class ChangeRequestViewModelForDetail
    {
        public ChangeRequest ChangeRequest { get; set; }
        public List<ChangeRequestStatus> ChangeRequestStatuses { get; set; }

        public List<ChangeDelta> ChangeDeltas { get; set; }

        public List<NumacChangeDelta> NumacChangeDeltas { get; set; }
        public List<DrsChangeDelta> DrsChangeDeltas { get; set; }
        public ChangeRequestMessage ChangeRequestMessage { get; set; }
        public List<ChangeRequestMessage> ChangeRequestMessages { get; set; }
    }

    public class ChangeRequestInfo
    {
        //change request class
        public Guid ChangeRequestId { get; set; }

        [DisplayName("表單編號")]
        public string SerialNumber { get; set; }

        [DisplayName("軟體變更敘述")]
        public string Description { get; set; }

        [DisplayName("負責人")]
        public string CreatedBy { get; set; }

        [DisplayName("表單位置")]
        public string Owner { get; set; }

        [DisplayName("審查")]
        public string ReviewBy { get; set; }

        [DisplayName("核准")]
        public string ApprovedBy { get; set; }

        [DisplayName("建立日期")]
        public DateTime CreateDate { get; set; }

        [DisplayName("最後修改日期")]
        public DateTime? LastModifiedDate { get; set; }

        //status type class
        public int StatusTypeId { get; set; }
        [DisplayName("狀態名稱")]
        public string StatusName { get; set; }

        //status class
        [DisplayName("建立日期")]
        public DateTime InitialDate { get; set; }

        public DateTime? ChangeDate { get; set; }
        [DisplayName("結案日期")]
        public DateTime? EndDate { get; set; }

        [DisplayName("原建置值")]
        public string OriginalValue { get; set; }

        [DisplayName("變更值")]
        public string NewValue { get; set; }

    }
    public class DrsChangeDetailViewModel
    {
 
        public string OriEPROMRev { get; set; }


        public string OriModuleType { get; set; }


        public string OriChecksum { get; set; }


        public string Description { get; set; }
        [DisplayName("FID編號")]
        public string FIDDiagramNo { get; set; }

        public string DRSPanelName { get; set; }


        public string Checksum { get; set; }

        public string ModuleType { get; set; }


        public string EPROMRev { get; set; }


        public DateTime CreateDate { get; set; }
        [DisplayName("結案日期")]
        public DateTime LastModifiedDate { get; set; }


        [DisplayName("表單編號")]
        public string FormSerialNumber { get; set; }

        [DisplayName("軟體變更文件")]
        public string DesignDoc { get; set; }

        [DisplayName("負責人")]
        public string CreatedBy { get; set; }

        [DisplayName("審查")]
        public string ReviewBy { get; set; }

        [DisplayName("核準")]
        public string ApprovedBy { get; set; }
    }
    public class NumacChangeDetailViewModel{

        
        public string OriAssembly { get; set; }

        
        public string OriSerialNumber { get; set; }

     
        public string OriProgram { get; set; }

        
        public string OriRev { get; set; }

        public string ModuleBoardName { get; set; }

        public string Assembly { get; set; }


        public string SerialNumber { get; set; }
        
        public string Program { get; set; }

       
        public string Rev { get; set; }

    
        public DateTime CreateDate { get; set; }

        public DateTime LastModifiedDate { get; set; }


        [DisplayName("表單編號")]
        public string FormSerialNumber { get; set; }

        [DisplayName("軟體變更文件")]
        public string DesignDoc { get; set; }

        [DisplayName("負責人")]
        public string CreatedBy { get; set; }
        
        [DisplayName("審查")]
        public string ReviewBy { get; set; }
        
        [DisplayName("核準")]
        public string ApprovedBy { get; set; }
    }
}