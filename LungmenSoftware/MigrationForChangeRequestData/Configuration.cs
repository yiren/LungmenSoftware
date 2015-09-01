using System.Collections.Generic;
using LungmenSoftware.Models.CodeFirst;
using LungmenSoftware.Models.CodeFirst.Entities;
using Microsoft.Ajax.Utilities;

namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LungmenSoftware.Models.CodeFirst.ChangeProcessDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            
            MigrationsDirectory = @"MigrationForChangeRequestData";
        }

        protected override void Seed(LungmenSoftware.Models.CodeFirst.ChangeProcessDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            List<ChangeRequestStatusType> statusTypes=new List<ChangeRequestStatusType>()
            {
                
            };
            if (!context.ChangeRequestStatuses.Any())
            {
                #region ChangeRequestStatType Data
                ChangeRequestStatusType PendingForApproval = new ChangeRequestStatusType()
                {
                    StatusName = "Waiting For Approval"
                };
                ChangeRequestStatusType HasComment = new ChangeRequestStatusType()
                {
                    StatusName = "Review Comment-Clarification Required"
                };

                ChangeRequestStatusType Complete = new ChangeRequestStatusType()
                {
                    StatusName = "Completed"
                };
                ChangeRequestStatusType Cancel = new ChangeRequestStatusType()
                {
                    StatusName = "Cancelled"
                };

                context.ChangeRequestStatusTypes.AddOrUpdate(PendingForApproval);
                context.ChangeRequestStatusTypes.AddOrUpdate(HasComment);
                context.ChangeRequestStatusTypes.AddOrUpdate(Complete);
                context.ChangeRequestStatusTypes.AddOrUpdate(Cancel);
                #endregion

                var pendingForApprovalEntry = context.Entry(PendingForApproval).Entity;
                var pendingForApprovalId = pendingForApprovalEntry.StatusTypeId;
                var hasCommentIdEntry = context.Entry(HasComment).Entity;
                var hasCommentId = hasCommentIdEntry.StatusTypeId;
                var completeIdEntry = context.Entry(Complete).Entity;
                var completeId = completeIdEntry.StatusTypeId;
                var cancelEntry = context.Entry(Cancel).Entity;
                var cancelId = cancelEntry.StatusTypeId;


                #region ChangeRequest Data
                ChangeRequest cr1 = new ChangeRequest()
                {
                    ChangeRequestId = Guid.NewGuid(),
                    CreatedBy = "test1@taipower.com.tw",
                    SerialNumber = "20150825E001",
                    CreateDate = DateTime.Parse("2015/08/25"),
                    LastModifiedDate = DateTime.Now - TimeSpan.FromDays(3),
                    Description = "MVD Driver Version Update",
                    Owner = "test2@taipower.com.tw"
                };

                ChangeRequest cr2 = new ChangeRequest()
                {
                    ChangeRequestId = Guid.NewGuid(),
                    CreatedBy = "test2@taipower.com.tw",
                    SerialNumber = "20150826P002",
                    CreateDate = DateTime.Parse("2015/08/26"),
                    LastModifiedDate = DateTime.Now - TimeSpan.FromDays(3),
                    Description = "FBM 232 CP Image Version Update",
                    Owner = "test2@taipower.com.tw"
                };

                ChangeRequest cr3 = new ChangeRequest()
                {
                    ChangeRequestId = Guid.NewGuid(),
                    CreatedBy = "test1@taipower.com.tw",
                    SerialNumber = "20150827E003",
                    CreateDate = DateTime.Parse("2015/08/27"),
                    LastModifiedDate = DateTime.Now - TimeSpan.FromDays(3),
                    Description = "FDSI Version Update",
                    Owner = "test2@taipower.com.tw"
                };

                ChangeRequest cr4 = new ChangeRequest()
                {
                    ChangeRequestId = Guid.NewGuid(),
                    CreatedBy = "test4@taipower.com.tw",
                    SerialNumber = "20150828P004",
                    CreateDate = DateTime.Parse("2015/08/28"),
                    LastModifiedDate = DateTime.Now - TimeSpan.FromDays(2),
                    Description = "FoxView Upgrade",
                    Owner = "test2@taipower.com.tw"

                };

                context.ChangeRequests.AddOrUpdate(cr1);
                context.ChangeRequests.AddOrUpdate(cr2);
                context.ChangeRequests.AddOrUpdate(cr3);
                context.ChangeRequests.AddOrUpdate(cr4);
                #endregion

                var cr1Entry = context.Entry(cr1).Entity;
                var cr1Id = cr1Entry.ChangeRequestId;
                var cr2Entry = context.Entry(cr2).Entity;
                var cr2Id = cr2Entry.ChangeRequestId;
                var cr3Entry = context.Entry(cr3).Entity;
                var cr3Id = cr3Entry.ChangeRequestId;
                var cr4Entry = context.Entry(cr4).Entity;
                var cr4Id = cr4Entry.ChangeRequestId;


                #region ChangeRequestStatus Data

                ChangeRequestStatus statusForCR1 = new ChangeRequestStatus()
                {
                    StatusTypeId = pendingForApprovalId,
                    ChangeRequestStatusType = pendingForApprovalEntry,
                    ChangeRequestId = cr1Id,
                    ChangeRequest = cr1Entry,
                    InitialDate = DateTime.Today - TimeSpan.FromDays(8),
                    IsCurrent = true
                };

                ChangeRequestStatus firstStatusForCR2 = new ChangeRequestStatus()
                {
                    StatusTypeId = pendingForApprovalId,
                    ChangeRequestStatusType = PendingForApproval,
                    ChangeRequestId = cr2Id,
                    ChangeRequest = cr2Entry,
                    InitialDate = DateTime.Today - TimeSpan.FromDays(8),
                    ChangeDate = DateTime.Now - TimeSpan.FromDays(5),
                    EndDate = DateTime.Now - TimeSpan.FromDays(5),
                    IsCurrent = false
                };

                ChangeRequestStatus secondStatusForCR2 = new ChangeRequestStatus()
                {
                    StatusTypeId = hasCommentId,
                    ChangeRequestStatusType = hasCommentIdEntry,
                    ChangeRequestId = cr2Id,
                    ChangeRequest = cr2Entry,
                    InitialDate = DateTime.Today - TimeSpan.FromDays(5),
                    IsCurrent = true

                };

                ChangeRequestStatus firstForCR3 = new ChangeRequestStatus()
                {
                    StatusTypeId = pendingForApprovalId,
                    ChangeRequestStatusType = pendingForApprovalEntry,
                    ChangeRequestId = cr3Id,
                    ChangeRequest = cr3Entry,
                    InitialDate = DateTime.Today - TimeSpan.FromDays(3),
                    EndDate = DateTime.Today - TimeSpan.FromDays(4),
                    IsCurrent = false

                };

                ChangeRequestStatus secondForCR3 = new ChangeRequestStatus()
                {
                    StatusTypeId = completeId,
                    ChangeRequestStatusType = completeIdEntry,
                    ChangeRequestId = cr3Id,
                    ChangeRequest = cr3Entry,
                    InitialDate = DateTime.Today - TimeSpan.FromDays(3),
                    IsCurrent = true
                };

                ChangeRequestStatus fistStatusForCR4 = new ChangeRequestStatus()
                {
                    StatusTypeId = pendingForApprovalId,
                    ChangeRequestStatusType = pendingForApprovalEntry,
                    ChangeRequestId = cr4Id,
                    ChangeRequest = cr4Entry,
                    InitialDate = DateTime.Today - TimeSpan.FromDays(5),
                    EndDate = DateTime.Today - TimeSpan.FromDays(4),
                    IsCurrent = false
                };

                ChangeRequestStatus secondStatusForCR4 = new ChangeRequestStatus()
                {
                    StatusTypeId = hasCommentId,
                    ChangeRequestStatusType = hasCommentIdEntry,
                    ChangeRequestId = cr4Id,
                    ChangeRequest = cr4Entry,
                    InitialDate = DateTime.Today - TimeSpan.FromDays(4),
                    EndDate = DateTime.Today - TimeSpan.FromDays(3),
                    IsCurrent = false
                };

                ChangeRequestStatus thirdStatusForCR4 = new ChangeRequestStatus()
                {
                    StatusTypeId = cancelId,
                    ChangeRequestStatusType = cancelEntry,
                    ChangeRequestId = cr4Id,
                    ChangeRequest = cr4Entry,
                    InitialDate = DateTime.Today - TimeSpan.FromDays(2),
                    IsCurrent = true
                };

                context.ChangeRequestStatuses.AddOrUpdate(statusForCR1);
                context.ChangeRequestStatuses.AddOrUpdate(firstStatusForCR2);
                context.ChangeRequestStatuses.AddOrUpdate(secondStatusForCR2);
                context.ChangeRequestStatuses.AddOrUpdate(firstForCR3);
                context.ChangeRequestStatuses.AddOrUpdate(secondForCR3);
                context.ChangeRequestStatuses.AddOrUpdate(fistStatusForCR4);
                context.ChangeRequestStatuses.AddOrUpdate(secondStatusForCR4);
                context.ChangeRequestStatuses.AddOrUpdate(thirdStatusForCR4);

                #endregion
            }

        }
    }
}
