using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LungmenSoftware.Models.CodeFirst;
using LungmenSoftware.Models.CodeFirst.Entities;
using LungmenSoftware.Models.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LungmenSoftware.Models.Service
{
    public class ChangeRequestService
    {
        private ChangeProcessDbContext db=new ChangeProcessDbContext();
        private LungmenSoftwareDataEntities ldb=new LungmenSoftwareDataEntities();
        private ApplicationDbContext _identityDb=new ApplicationDbContext();

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
                            StatusTypeId = t.StatusTypeId,
                            CreateDate = cr.CreateDate,
                            Description = cr.Description
                        };
            return query.ToList();
        }

        public List<ChangeRequestInfo> GetChangeRequestHistoryByJoinTable(WKAndFoxJoinTable record)
        {
            var query = from r in db.RevInfos.Where(r => r.JoinTableId == record.Id)
                join d in db.ChangeDeltas
                    on r.ChangeDeltaId equals d.ChangeDeltaId
                join cr in db.ChangeRequests.Where(c => c.IsActive == false)
                    on d.ChangeRequestId equals cr.ChangeRequestId
                join s in db.ChangeRequestStatuses.Where(s => s.StatusTypeId == 3)
                    on cr.ChangeRequestId equals s.ChangeRequestId
                select new ChangeRequestInfo()
                {
                    ChangeRequestId = cr.ChangeRequestId,
                    ApprovedBy = cr.ApprovedBy,
                    CreatedBy = cr.CreatedBy,
                    ReviewBy = cr.ReviewBy,
                    SerialNumber = cr.SerialNumber,
                    EndDate = s.EndDate,
                    InitialDate = s.InitialDate,
                    CreateDate = cr.CreateDate,
                    Description = cr.Description,
                    OriginalValue = d.OriginalValue,
                    NewValue = d.NewValue
                };
            return query.ToList();
        }

        //Failed.....
        public List<ChangeRequestInfo> GetChangeRequestHistoryBySoftId(int foxSoftwareId)
        {
            var query=from d in db.ChangeDeltas.Where(d=>d.FoxSoftwareId==foxSoftwareId)
                join cr in db.ChangeRequests.Where(c=>c.IsActive==false) 
                    on d.ChangeRequestId equals cr.ChangeRequestId
                join s in db.ChangeRequestStatuses.Where(s=>s.StatusTypeId==3) 
                    on cr.ChangeRequestId equals s.ChangeRequestId
                      select new ChangeRequestInfo
                      {
                          ChangeRequestId = cr.ChangeRequestId,
                          ApprovedBy = cr.ApprovedBy,
                          CreatedBy = cr.CreatedBy,
                          ReviewBy = cr.ReviewBy,
                          SerialNumber = cr.SerialNumber,
                          EndDate = s.EndDate,
                          InitialDate = s.InitialDate,
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
                    Description = cr.Description,
                    Owner = cr.Owner,
                    StatusTypeId = t.StatusTypeId
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
                    ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(1),
                    IsCurrent = true
                }
            };

            db.SaveChanges();
        }


        //For AngularJS
        public ChangeRequest InitNewChangeRequestRecord(string createdBy)
        {
            ChangeRequest cr = new ChangeRequest();
            cr.ChangeRequestId = Guid.NewGuid();
            cr.CreatedBy = createdBy;
            cr.CreateDate = DateTime.Today;
            cr.LastModifiedDate = DateTime.Today;
            cr.SerialNumber = GetSerialNumber();
            cr.ChangeRequestStatuses = new List<ChangeRequestStatus>()
            {
                new ChangeRequestStatus()
                {
                    InitialDate = DateTime.Today,
                    ChangeRequestId = cr.ChangeRequestId,
                    ChangeRequest = cr,
                    StatusTypeId = 1,
                    //ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(1),
                    IsCurrent = true
                }
            };
            return cr;
        }

        //For AngularJS
        public bool AddChangeRequestRecord(ChangeRequest crEntry)
        {
            foreach (var item in crEntry.ChangeDeltas)
            {
                item.ChangeRequest = crEntry;
                item.ChangeRequestId = crEntry.ChangeRequestId;
                item.ChangeDeltaId = Guid.NewGuid();
            }
            db.ChangeRequests.Add(crEntry);
            if (db.SaveChanges() != -1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public ChangeRequest FindByChangeRequestId(Guid changeRequestId)
        {
           
            return db.ChangeRequests.Find(changeRequestId);
        }

        public void SaveChangeRequestTemp(ChangeRequest cr)
        {
            db.ChangeRequests.Add(cr);
            db.SaveChanges();
        }


        private void UpdatePreviousStatus(ChangeRequest cr)
        {
            var query = from s in db.ChangeRequestStatuses.Where(s => s.ChangeRequestId.Equals(cr.ChangeRequestId))
                join t in db.ChangeRequestStatusTypes on s.StatusTypeId equals t.StatusTypeId
                orderby t.StatusTypeId descending 
                select s;
            foreach (var status in query)
            {
                if (status.EndDate == null)
                {
                    status.EndDate=DateTime.Today;
                    status.IsCurrent = false;
                }
            }

        }

        public void EntityToModifiedState(ChangeRequest cr)
        {
            db.Entry(cr).State=EntityState.Added;
        }

        public string GetReviewer()
        {
            //var reviewer = _identityDb.Roles.Single(r => r.Name.Equals("Reviewer"));

            //var query = from userRoles in reviewer.Users
            //            join users in _identityDb.Users on userRoles.UserId equals users.Id
            //            select users;

            //return query.Last().UserName;

            return "test2@taipower.com.tw";
        }

        public void StatusUpdateForComment(ChangeRequest cr)
        {
            UpdatePreviousStatus(cr);

            db.ChangeRequestStatuses.Add(new ChangeRequestStatus()
            {
                ChangeRequestId = cr.ChangeRequestId,
                ChangeRequest = cr,
                ChangeDate = DateTime.Today,
                StatusTypeId = 2,
                ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(2),
                InitialDate = DateTime.Today,
                IsCurrent = true
            });

            db.SaveChanges();
        }

        public void StatusUpdateForClarification(ChangeRequest cr)
        {
            UpdatePreviousStatus(cr);

            db.ChangeRequestStatuses.Add(new ChangeRequestStatus()
            {
                ChangeRequestId = cr.ChangeRequestId,
                ChangeRequest = cr,
                ChangeDate = DateTime.Today,
                StatusTypeId = 1,
                ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(1),
                InitialDate = DateTime.Today,
                IsCurrent = true
            });

            db.SaveChanges();
        }

        public void StatusUpdateForApproval(ChangeRequest cr, string approver)
        {
            UpdatePreviousStatus(cr);

            cr.IsActive = false;
            cr.ApprovedBy = approver;
            db.ChangeRequestStatuses.Add(new ChangeRequestStatus()
            {
                ChangeRequestId = cr.ChangeRequestId,
                ChangeRequest = cr,
                ChangeDate = DateTime.Today,
                StatusTypeId = 3,
                ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(3),
                EndDate = DateTime.Today,
                InitialDate = DateTime.Today,
                IsCurrent = true
            });

            db.SaveChanges();

        }
        public void StatusUpdateForCancellation(ChangeRequest cr)
        {
            UpdatePreviousStatus(cr);

            cr.IsActive = false;

            db.ChangeRequestStatuses.Add(new ChangeRequestStatus()
            {
                ChangeRequestId = cr.ChangeRequestId,
                ChangeRequest = cr,
                ChangeDate = DateTime.Today,
                StatusTypeId = 4,
                ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(4),
                EndDate = DateTime.Today,
                InitialDate = DateTime.Today,

            });

            db.SaveChanges();
        }


        public string GetSerialNumber()
        {
            return String.Format("{0:yyyyMMdd}", DateTime.Today) + "E" + new Random().Next(1000, 9999);
        }


        public bool UpdateRevPerChangeRequest(ChangeRequest crEntry)
        {
            if (crEntry.IsActive==false)
            {
                return false;
            }
            var deltas =
                db.ChangeDeltas.Include(d => d.RevInfos)
                .Where(d => d.ChangeRequestId.Equals(crEntry.ChangeRequestId));



            foreach (var delta in deltas)
            {
                var currentSoftData=from s in ldb.FoxSoftwares.Where(s=>s.FoxSoftwareId==delta.FoxSoftwareId)
                        join j in ldb.WKAndFoxJoinTables on s.FoxSoftwareId equals j.FoxSoftwareId
                                    where j.Rev.Equals(delta.OriginalValue)
                                    select j;

                if(delta.RevInfos.Count == currentSoftData.Count()) { 
                    foreach (var item in delta.RevInfos)
                    {
                        var jointable = ldb.WKAndFoxJoinTables.Find(item.JoinTableId);
                        jointable.Rev = delta.NewValue;
                    }
                }

            }
            if (ldb.SaveChanges() != -1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        
    }

   
}