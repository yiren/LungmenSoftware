using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LungmenSoftware.Models.CodeFirst;
using LungmenSoftware.Models.CodeFirst.Entities;
using LungmenSoftware.Models.ViewModel;

namespace LungmenSoftware.Models.Service
{
    public class ChangeRequestService
    {
        private ChangeProcessDbContext db=new ChangeProcessDbContext();
        
        public List<ChangeRequestInfo> GetChangeRequestHistory()
        {
            var query = from cr in db.ChangeRequests
                        join s in db.ChangeRequestStatuses
                            on cr.ChangeRequestId equals s.ChangeRequestId
                        join t in db.ChangeRequestStatusTypes
                            on s.StatusTypeId equals t.StatusTypeId
                        orderby cr.LastModifiedDate descending
                        select new ChangeRequestInfo
                        {
                            ChangeRequestId = cr.ChangeRequestId,
                            ApprovedBy = cr.ApprovedBy,
                            CreatedBy = cr.CreatedBy,
                            LastModifiedDate = cr.LastModifiedDate,
                            ReviewBy = cr.ReviewBy,
                            SerialNumber = cr.SerialNumber,
                            ChangeDate = s.ChangeDate,
                            EndDate = s.EndDate,
                            InitialDate = s.InitialDate,
                            StatusName = t.StatusName,
                            CreateDate = cr.CreateDate,
                            Description = cr.Description
                        };
            return query.ToList();
        }

        public List<ChangeRequestInfo> GetChangeRequestList()
        {
            var query = from cr in db.ChangeRequests
                join s in db.ChangeRequestStatuses.Where(s=>s.EndDate ==null)
                    on cr.ChangeRequestId equals s.ChangeRequestId
                join t in db.ChangeRequestStatusTypes
                    on s.StatusTypeId equals t.StatusTypeId
                orderby cr.LastModifiedDate descending 
                select new ChangeRequestInfo
                {
                    ChangeRequestId = cr.ChangeRequestId,
                    ApprovedBy = cr.ApprovedBy,
                    CreatedBy = cr.CreatedBy,
                    LastModifiedDate = cr.LastModifiedDate,
                    ReviewBy = cr.ReviewBy,
                    SerialNumber = cr.SerialNumber,
                    ChangeDate = s.ChangeDate,
                    EndDate = s.EndDate,
                    InitialDate = s.InitialDate,
                    StatusName = t.StatusName,
                    CreateDate = cr.CreateDate,
                    Description = cr.Description
                };
            return query.ToList();
        } 

        public List<ChangeRequestStatusType> GetChangeRequestStatusTypess()
        {
            return db.ChangeRequestStatusTypes.ToList();
        }

        public void AddChangeRequestEntry(ChangeRequest crEntry)
        {
            
            crEntry.ChangeRequestStatuses=new List<ChangeRequestStatus>()
            {
                new ChangeRequestStatus()
                {
                    InitialDate = DateTime.Today,

                    ChangeRequestId = crEntry.ChangeRequestId,
                    ChangeRequest = crEntry,
                    StatusTypeId = 1,
                    ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(1)
                }
            };

            db.SaveChanges();
        }

        public ChangeRequest FindChangeRequestById(Guid changeRequestId)
        {
            return db.ChangeRequests.Find(changeRequestId);
        }

        public void SaveChangeRequestTemp(ChangeRequest cr)
        {
            db.ChangeRequests.Add(cr);
            db.SaveChanges();
        }

        public void StatusUpdateForCancellation(ChangeRequest crEntry)
        {
            
            var totalStatusRecord = db.ChangeRequestStatuses.Where(s => 
                s.ChangeRequestId.Equals(crEntry.ChangeRequestId)
                &&
                s.EndDate == null);
            foreach (var record in totalStatusRecord)
            {
                record.ChangeDate=DateTime.Today;
                record.EndDate = DateTime.Today;
            }

            db.ChangeRequestStatuses.Add(new ChangeRequestStatus()
            {
                ChangeRequestId = crEntry.ChangeRequestId,
                ChangeRequest = crEntry,
                ChangeDate = DateTime.Today,
                StatusTypeId = 4,
                ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(4),
                EndDate = DateTime.Today,
                InitialDate = DateTime.Today,

            });

            db.SaveChanges();

        }


        
    }

   
}