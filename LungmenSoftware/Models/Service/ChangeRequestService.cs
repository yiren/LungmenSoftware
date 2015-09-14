﻿using System;
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

        public ChangeRequest InitNewChangeRequestRecord(ChangeRequest crEntry)
        {
            crEntry.ChangeRequestStatuses = new List<ChangeRequestStatus>()
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
            return crEntry;
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
                    status.EndDate=DateTime.Now;
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

        public void StatusUpdateForApproval(ChangeRequest cr)
        {
            UpdatePreviousStatus(cr);

            cr.IsActive = false;

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

        public bool AddChangeRequestRecord(ChangeRequest crEntry)
        {
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
    }

   
}