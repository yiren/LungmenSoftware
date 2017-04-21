using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LungmenSoftware.Models.CodeFirst;
using LungmenSoftware.Models.CodeFirst.Entities;
using LungmenSoftware.Models.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Validation;
using LungmenSoftware.Models.DRS;

namespace LungmenSoftware.Models.Service
{
    public class ChangeRequestService
    {
        private ChangeProcessDbContext db=new ChangeProcessDbContext();
        private LungmenSoftwareDataEntities ldb=new LungmenSoftwareDataEntities();
        private ApplicationDbContext _identityDb=new ApplicationDbContext();
        private NumacDataService numacDataService = new NumacDataService();
        private DrsDataService drsDataService = new DrsDataService();

        public List<ChangeRequestInfo> GetChangeRequestHistory()
        {
            var query = from cr in db.ChangeRequests.Where(c=>c.IsActive==false)
                        join s in db.ChangeRequestStatuses.Where(s=>s.IsCurrent==true)
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

        public List<DrsChangeDetailViewModel> GetDrsChangeRequestRecordById(string fidId)
        {
            var id = new Guid(fidId);
            var query = from d in db.DrsChangeDeltas.Where(d => d.FidId.Equals(id))
                        join r in db.ChangeRequests on d.ChangeRequestId equals r.ChangeRequestId
                        select new DrsChangeDetailViewModel()
                        {
                            FormSerialNumber = r.SerialNumber,
                            ApprovedBy = r.ApprovedBy,
                            ReviewBy = r.ReviewBy,
                            CreatedBy = r.CreatedBy,
                            CreateDate = r.CreateDate,
                            LastModifiedDate = r.LastModifiedDate,
                            OriChecksum=d.OriChecksum,
                            OriEPROMRev=d.OriEPROMRev,
                            OriModuleType=d.OriModuleType,
                            Checksum=d.Checksum,
                            EPROMRev=d.EPROMRev,
                            ModuleType=d.ModuleType,
                            Description=d.Description,
                            FIDDiagramNo=d.FIDDiagramNo,
                            DesignDoc=r.DesignDoc
                            
                        }
                        ;
            return query.ToList();
        }

        public List<NumacChangeDetailViewModel> GetNumacChangeRequestRecordById(string chassisBoardId)
        {
            var id = new Guid(chassisBoardId);
            var query = from d in db.NumacChangeDeltas.Where(d => d.ModuleBoardId.Equals(id))
                            join  r in db.ChangeRequests on d.ChangeRequestId equals r.ChangeRequestId
                        select new NumacChangeDetailViewModel()
                        {
                            FormSerialNumber=r.SerialNumber,
                            ApprovedBy=r.ApprovedBy,
                            ReviewBy=r.ReviewBy,
                            CreatedBy=r.CreatedBy,
                            CreateDate=r.CreateDate,
                            LastModifiedDate=r.LastModifiedDate,
                            ModuleBoardName=d.ModuleBoardName,
                            OriAssembly=d.OriAssembly,
                            OriProgram=d.OriProgram,
                            OriRev=d.OriRev,
                            OriSerialNumber=d.OriSerialNumber,
                            DesignDoc=r.DesignDoc,
                            Assembly=d.Assembly,
                            Program=d.Program,
                            Rev=d.Rev,
                            SerialNumber=d.SerialNumber
                        }
                        ;
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
            var query = from cr in db.ChangeRequests.Where(r=>r.IsActive==true)
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
                    InitialDate = DateTime.Now,
                    ChangeRequestId = crEntry.ChangeRequestId,
                    ChangeRequest = crEntry,
                    StatusTypeId = 1,
                    ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(1),
                    IsCurrent = true
                }
            };

            db.SaveChanges();
        }


        //For AngularJS Inv
        public ChangeRequest InitNewChangeRequestRecord(string createdBy="test2@taipower.com.tw")
        {
            ChangeRequest cr = new ChangeRequest();
            cr.ChangeRequestId = Guid.NewGuid();
            cr.CreatedBy = createdBy;
            cr.CreateDate = DateTime.Now;
            cr.LastModifiedDate = DateTime.Now;
            cr.SerialNumber = GetSerialNumber();
            cr.ChangeRequestStatuses = new List<ChangeRequestStatus>()
            {
                new ChangeRequestStatus()
                {
                    InitialDate = DateTime.Now,
                    ChangeRequestId = cr.ChangeRequestId,
                    ChangeRequest = cr,
                    StatusTypeId = 1,
                    //ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(1),
                    IsCurrent = true
                }
            };
            return cr;
        }

        

        public ChangeRequest AddDrsChangeRequestEntry(ChangeRequest crEntry)
        {
            foreach(var item in crEntry.DrsChangeDeltas)
            {
                FID oriFid = drsDataService.GetFidById(item.FidId);
                item.OriChecksum = oriFid.Checksum;
                item.OriEPROMRev = oriFid.EPROMRev;
                item.OriModuleType = oriFid.ModuleType;
                item.ChangeRequestId = crEntry.ChangeRequestId;
                item.ChangeRequest = crEntry;
                item.DrsChangeDeltaId = Guid.NewGuid();
            }
            db.ChangeRequestMessages.Add(new ChangeRequestMessage()
            {
                ChangeRequest = crEntry,
                ChangeRequestId = crEntry.ChangeRequestId,
                CreateBy = crEntry.CreatedBy,
                CreateTime = DateTime.Now,
                Message = "軟體變更需求送審"
            });

            db.ChangeRequests.Add(crEntry);
            if (db.SaveChanges() != -1)
            {
                return db.ChangeRequests
                    .Include(c => c.DrsChangeDeltas)
                    .First(c => c.ChangeRequestId.Equals(crEntry.ChangeRequestId));
            }
            else
            {
                return null;
            }
        }

        //For AngularJS Numac
        public ChangeRequest AddNumacChangeRequestEntry(ChangeRequest crEntry)
        {
            

            foreach (var item in crEntry.NumacChangeDeltas)
            {
                var oriModule = numacDataService.GetModuleByModuleId(item.ModuleBoardId);
                item.OriAssembly = oriModule.Assembly;
                item.OriProgram = oriModule.Program;
                item.OriSerialNumber = oriModule.SerialNumber;
                item.OriRev = oriModule.Rev;
                item.SocketLocation = oriModule.SocketLocation;
                item.ChangeRequest = crEntry;
                item.ChangeRequestId = crEntry.ChangeRequestId;
                item.NumacChangeDeltaId = Guid.NewGuid();
            }

            db.ChangeRequestMessages.Add(new ChangeRequestMessage()
            {
                ChangeRequest = crEntry,
                ChangeRequestId = crEntry.ChangeRequestId,
                CreateBy = crEntry.CreatedBy,
                CreateTime = DateTime.Now,
                Message = "軟體變更需求送審"
            });

            db.ChangeRequests.Add(crEntry);
            if (db.SaveChanges() != -1)
            {
                return db.ChangeRequests
                    .Include(c => c.NumacChangeDeltas)
                    .First(c => c.ChangeRequestId.Equals(crEntry.ChangeRequestId));
            }
            else
            {
                return null;
            }
        }

        

        //For AngularJS Inv
        public ChangeRequest AddChangeRequestRecord(ChangeRequest crEntry)
        {
            foreach (var item in crEntry.ChangeDeltas)
            {
                item.ChangeRequest = crEntry;
                item.ChangeRequestId = crEntry.ChangeRequestId;
                item.ChangeDeltaId = Guid.NewGuid();
                foreach (var revInfo in item.RevInfos)
                {
                    revInfo.ChangeDeltaId = item.ChangeDeltaId;
                    revInfo.FoxSoftwareId = item.FoxSoftwareId;
                }
            }
            

            db.ChangeRequestMessages.Add(new ChangeRequestMessage()
            {
                ChangeRequest = crEntry,
                ChangeRequestId = crEntry.ChangeRequestId,
                CreateBy = crEntry.CreatedBy,
                CreateTime = DateTime.Now,
                Message = "軟體變更需求送審"
            });


            db.ChangeRequests.Add(crEntry);
            try
            {
                if (db.SaveChanges() != -1)
                {
                    return db.ChangeRequests
                        .Include(c => c.ChangeDeltas.Select(d => d.RevInfos))
                        .First(c => c.ChangeRequestId.Equals(crEntry.ChangeRequestId));
                }
                else
                {
                    return null;
                }
            }catch(DbEntityValidationException ex)
            {
                var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                var getFullMessage = string.Join("; ", entityError);
                var exceptionMessage = string.Concat(ex.Message, "errors are: ", getFullMessage);
            }

            return null;
        }

        public ChangeRequest FindByChangeRequestId(Guid changeRequestId)
        {
           
            return db.ChangeRequests.Include(c=>c.ChangeRequestStatuses).Include(c=>c.ChangeRequestMessages).Include(c=>c.ChangeDeltas).Include(c=>c.NumacChangeDeltas).FirstOrDefault(c=>c.ChangeRequestId.Equals(changeRequestId));
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
            //var reviewers = (from r in _identityDb.Roles.Where(r => r.Name.Equals("Reviewer"))
            //    join jt in _identityDb.ApplicationUserRoles
            //        on r.Id equals jt.RoleId
            //    join u in _identityDb.Users
            //        on jt.UserId equals u.Id
            //    select u).ToList();

            //Random seed=new Random();

            //var reviewer=reviewers[seed.Next(0, reviewers.Count)].UserName;
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
                ChangeDate = DateTime.Now,
                StatusTypeId = 2,
                ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(2),
                InitialDate = DateTime.Now,
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
                ChangeDate = DateTime.Now,
                StatusTypeId = 1,
                ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(1),
                InitialDate = DateTime.Now,
                IsCurrent = true
            });

            db.SaveChanges();
        }

        public void StatusUpdateForApproval(ChangeRequest cr, string approver)
        {
            UpdatePreviousStatus(cr);
            cr.Owner = "已核可";
            cr.IsActive = false;
            cr.ApprovedBy = approver;
            db.ChangeRequestStatuses.Add(new ChangeRequestStatus()
            {
                ChangeRequestId = cr.ChangeRequestId,
                ChangeRequest = cr,
                ChangeDate = DateTime.Now,
                StatusTypeId = 3,
                ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(3),
                EndDate = DateTime.Now,
                InitialDate = DateTime.Now,
                IsCurrent = true
            });

            db.SaveChanges();

        }
        public void StatusUpdateForCancellation(ChangeRequest cr)
        {
            UpdatePreviousStatus(cr);

            cr.IsActive = false;
            cr.Owner = "已取消";
            db.ChangeRequestMessages.Add(new ChangeRequestMessage() {
                ChangeRequest = cr,
                ChangeRequestId = cr.ChangeRequestId,
                CreateBy = cr.CreatedBy,
                CreateTime = DateTime.Now,
                Message = cr.CreatedBy+"取消軟體變更需求"
            });
            db.ChangeRequestStatuses.Add(new ChangeRequestStatus()
            {
                ChangeRequestId = cr.ChangeRequestId,
                ChangeRequest = cr,
                ChangeDate = DateTime.Now,
                StatusTypeId = 4,
                ChangeRequestStatusType = db.ChangeRequestStatusTypes.Find(4),
                EndDate = DateTime.Now,
                InitialDate = DateTime.Now,
                IsCurrent=true
            });

            db.SaveChanges();
        }


        public string GetSerialNumber()
        {
            return String.Format("{0:yyyyMMdd}", DateTime.Today) + "E0" + new Random().Next(000, 999);
        }


        public bool UpdateRevPerChangeRequest(ChangeRequest crEntry)
        {
            if (crEntry.IsActive==false)
            {
                return false;
            }
            if(crEntry.ChangeDeltas.Count>0)
            {
                var deltas =
                db.ChangeDeltas.Include(d => d.RevInfos)
                .Where(d => d.ChangeRequestId.Equals(crEntry.ChangeRequestId)).ToList();

                foreach (var delta in deltas)
                {

                    var currentSoftData = from s in ldb.FoxSoftwares.Where(s => s.FoxSoftwareId == delta.FoxSoftwareId)
                                          join j in ldb.WKAndFoxJoinTables on s.FoxSoftwareId equals j.FoxSoftwareId
                                          where j.Rev.Equals(delta.OriginalValue)
                                          select j;


                    foreach (var item in delta.RevInfos)
                    {
                        var jointable = ldb.WKAndFoxJoinTables.Find(item.JoinTableId);
                        jointable.Rev = delta.NewValue;
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

            if (crEntry.NumacChangeDeltas.Count>0)
            {
                var deltas = db.NumacChangeDeltas.Where(n => n.ChangeRequestId.Equals(crEntry.ChangeRequestId)).ToList();
                foreach (var delta in deltas)
                {
                    var currentModule = numacDataService.GetModuleByModuleId(delta.ModuleBoardId);
                    currentModule.Program = delta.Program;
                    currentModule.Assembly = delta.Assembly;
                    currentModule.SerialNumber = delta.SerialNumber;
                    currentModule.Rev = delta.Rev;
                    
                }
                //
                
                return numacDataService.IsUpdated();

            }
            if (crEntry.DrsChangeDeltas.Count > 0)
            {
                var deltas = db.DrsChangeDeltas.Where(n => n.ChangeRequestId.Equals(crEntry.ChangeRequestId)).ToList();
                foreach (var delta in deltas)
                {
                    var currentFid = drsDataService.GetFidById(delta.FidId);
                    currentFid.ModuleType = delta.ModuleType;
                    currentFid.EPROMRev = delta.EPROMRev;
                    currentFid.Checksum = delta.Checksum;
                    

                }
                //

                return drsDataService.IsUpdated();

            }

            return false;
            

        }


        public ChangeRequest FindDetailCRByChangeRequestId(Guid id)
        {
            //var query = from cr in db.ChangeRequests
            //    join s in db.ChangeRequestStatuses
            //        on cr.ChangeRequestId equals s.ChangeRequestId
            //    join t in db.ChangeRequestStatusTypes.Where(t=>t.StatusTypeId==1)
            //        on s.StatusTypeId equals t.StatusTypeId
            //    select new ChangeRequestInfo()
            //    {

            //    };

            var query = db.ChangeRequests.Where(c=>c.ChangeRequestId.Equals(id))
                .Include(c=>c.ChangeDeltas.Select(d=>d.RevInfos))
                .Include(c=>c.NumacChangeDeltas)
                .Include(c=>c.DrsChangeDeltas)
                .Include(c=>c.ChangeRequestMessages)
                .Include(c=>c.ChangeRequestStatuses.Select(s=>s.ChangeRequestStatusType))
                .FirstOrDefault();
            
            return query;
        }

        public void UpdateChangeDeltas(Guid changeRequestId, List<ChangeDelta> changeDeltas)
        {
            var deltas = db.ChangeDeltas.Where(d => d.ChangeRequestId.Equals(changeRequestId))
                .Include(d=>d.RevInfos).ToList();
                //join d in db.ChangeDeltas
                //    on c.ChangeRequestId equals d.ChangeRequestId
                //join r in db.RevInfos
                //    on d.ChangeDeltaId equals r.ChangeDeltaId
                //select d;

            if (deltas.Count() != changeDeltas.Count())
            {
                throw new Exception("The Count of ChangeDeltas Has Errors");
            }

            for (int i=0;i<deltas.Count();i++)
            {
                deltas[i].NewValue = changeDeltas[i].NewValue;
            }

        }

        public void UpdateDrsChangeDeltas(Guid changeRequestId, List<DrsChangeDelta> drsChangeDeltas)
        {
            var deltas = db.DrsChangeDeltas.Where(n => n.ChangeRequestId.Equals(changeRequestId)).ToList();
            if (deltas.Count() != drsChangeDeltas.Count())
            {
                throw new Exception("The Count of ChangeDeltas Has Errors");
            }

            for (int i = 0; i < deltas.Count(); i++)
            {
                deltas[i].ModuleType = drsChangeDeltas[i].ModuleType;
                deltas[i].EPROMRev = drsChangeDeltas[i].EPROMRev;
                deltas[i].Checksum = drsChangeDeltas[i].Checksum;
            }
        }

        public void UpdateNumacChangeDeltas(Guid changeRequestId, List<NumacChangeDelta> numacChangeDeltas)
        {
            var deltas = db.NumacChangeDeltas.Where(n => n.ChangeRequestId.Equals(changeRequestId)).ToList();
            //join d in db.ChangeDeltas
            //    on c.ChangeRequestId equals d.ChangeRequestId
            //join r in db.RevInfos
            //    on d.ChangeDeltaId equals r.ChangeDeltaId
            //select d;

            if (deltas.Count() != numacChangeDeltas.Count())
            {
                throw new Exception("The Count of ChangeDeltas Has Errors");
            }

            for (int i = 0; i < deltas.Count(); i++)
            {
                deltas[i].Assembly = numacChangeDeltas[i].Assembly;
                deltas[i].Program = numacChangeDeltas[i].Program;
                deltas[i].SerialNumber = numacChangeDeltas[i].SerialNumber;
                deltas[i].Rev = numacChangeDeltas[i].Rev;
            }
        }
        public void AddChangeRequestMessage(ChangeRequestMessage crm)
        {
            db.ChangeRequestMessages.Add(crm);
            db.SaveChanges();
        }

        public List<ChangeRequestInfo> SearchChangeRequestsByForm(ChangeRequestSearchViewModel vm)
        {
           

            var preChangeRequests =db.ChangeRequests.Where(c => 
                    (String.IsNullOrEmpty(vm.SerialNumber) || c.SerialNumber.Contains(vm.SerialNumber)));
            preChangeRequests = preChangeRequests.Where(c=> 
                    (String.IsNullOrEmpty(vm.CreatedBy) || c.CreatedBy.Contains(vm.CreatedBy)));

            preChangeRequests = preChangeRequests.Where(c =>
                    (String.IsNullOrEmpty(vm.Description) || c.Description.Contains(vm.Description)));
            //preChangeRequests = vm.IncludingCompleted == true
            //    ? preChangeRequests
            //    : preChangeRequests.Where(c => c.IsActive == true);

            if (!String.IsNullOrEmpty(vm.CreateStartDate))
            {
                DateTime sd = Convert.ToDateTime(vm.CreateStartDate);
                preChangeRequests = preChangeRequests.Where(c=>c.CreateDate >= sd);
            }

            if (!String.IsNullOrEmpty(vm.CreateEndDate))
            {
                DateTime ed = Convert.ToDateTime(vm.CreateEndDate);
                preChangeRequests = preChangeRequests.Where(c =>c.CreateDate <= ed);
            }

            //||
            //(String.IsNullOrEmpty(vm.CreateStartDate) || c.CreateDate >= Convert.ToDateTime(vm.CreateStartDate)) &&
            //(String.IsNullOrEmpty(vm.CreateEndDate)|| c.CreateDate<=Convert.ToDateTime(vm.CreateEndDate))
            //);
            IQueryable<ChangeRequestStatus> preChangeRequestStatuses=db.ChangeRequestStatuses.Where(s => s.IsCurrent==true);
            IQueryable<ChangeRequestStatusType> preChangeRequestStatusTypes = db.ChangeRequestStatusTypes;
            IQueryable<ChangeRequestStatus> tempQuery = null;
            if (vm.Status.Any(s => s.IsChecked==true))
            {
                 foreach (var item in vm.Status)
                {
                    if (item.IsChecked)
                    {
                        if (tempQuery == null)
                        {
                            tempQuery = preChangeRequestStatuses.Where(s => s.StatusTypeId == item.Value);
                        }
                        else { 
                        tempQuery = tempQuery
                            .Concat(preChangeRequestStatuses.Where(s => s.StatusTypeId == item.Value));
                        }
                    }
                }
                preChangeRequestStatuses = tempQuery;
            }

            


            var query= from cr in preChangeRequests
                join s in preChangeRequestStatuses on cr.ChangeRequestId equals s.ChangeRequestId
                join t in preChangeRequestStatusTypes on s.StatusTypeId equals t.StatusTypeId
                orderby cr.CreateDate descending
                       select new ChangeRequestInfo
                       {
                           ChangeRequestId = cr.ChangeRequestId,
                           ApprovedBy = cr.ApprovedBy,
                           CreatedBy = cr.CreatedBy,
                           ReviewBy = cr.ReviewBy,
                           SerialNumber = cr.SerialNumber,
                           StatusName = t.StatusName,
                           CreateDate = cr.CreateDate,
                           Description = cr.Description,
                           Owner = cr.Owner,
                           StatusTypeId = s.StatusTypeId,
                           EndDate = s.EndDate
                       };


            //var query = from cr in db.ChangeRequests
            //            join s in db.ChangeRequestStatuses
            //                on cr.ChangeRequestId equals s.ChangeRequestId
            //            join t in db.ChangeRequestStatusTypes
            //                on s.StatusTypeId equals t.StatusTypeId
            //            join d in db.ChangeDeltas
            //                on cr.ChangeRequestId equals d.ChangeRequestId
            //            join i in db.RevInfos
            //                on d.ChangeDeltaId equals i.ChangeDeltaId
            //            where (String.IsNullOrEmpty(vm.SerialNumber) || cr.SerialNumber.Contains(vm.SerialNumber)) &&
            //                  (String.IsNullOrEmpty(vm.CreatedBy) || cr.CreatedBy.Contains(vm.CreatedBy)) &&
            //                  s.StatusTypeId == vm.StatusTypeId &&
            //                  cr.IsActive.Equals(vm.IsActive) &&
            //                  (String.IsNullOrEmpty(vm.SoftwareName) || i.SoftwareName.Equals(vm.SoftwareName)) 
            //&&
            //      ((String.IsNullOrEmpty(vm.CreateStartDate)|| cr.CreateDate>= Convert.ToDateTime(vm.CreateStartDate)) &&
            //      (String.IsNullOrEmpty(vm.CreateEndDate)|| cr.CreateDate<=Convert.ToDateTime(vm.CreateEndDate)))
            //orderby cr.LastModifiedDate descending
            //select new ChangeRequestInfo
            //{
            //    ChangeRequestId = cr.ChangeRequestId,
            //    ApprovedBy = cr.ApprovedBy,
            //    CreatedBy = cr.CreatedBy,
            //    LastModifiedDate = cr.LastModifiedDate,
            //    ReviewBy = cr.ReviewBy,
            //    SerialNumber = cr.SerialNumber,
            //    InitialDate = s.InitialDate,
            //    StatusName = t.StatusName,
            //    CreateDate = cr.CreateDate,
            //    Description = cr.Description,
            //    Owner = cr.Owner,
            //    StatusTypeId = t.StatusTypeId
            //};
            return query.ToList();
        }

        public List<CheckBoxListModel> GetChangeRequestStatusTypessForCheckBox()
        {
            var checkboxList= db.ChangeRequestStatusTypes.Select(t => new CheckBoxListModel()
            {
                DisplayName = t.StatusName,
                Value = t.StatusTypeId,
                IsChecked = false
            }).ToList();

            return checkboxList;
        }

        public void DeleteChangeRequest(ChangeRequest r)
        {
            db.ChangeRequests.Remove(r);
            db.SaveChanges();
        }

       

    }

   
}