using System.Collections.Generic;
using LungmenSoftware.Models.CodeFirst;
using LungmenSoftware.Models.CodeFirst.Entities;

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
            
            #region ChangeRequestStatType Data
            ChangeRequestStatusType PendingForApproval=new ChangeRequestStatusType()
            {
                StatusName = "Waiting For Approval"
            };
            ChangeRequestStatusType HasComment= new ChangeRequestStatusType()
            {
                StatusName = "Review Comment-Clarification Required"
            };

            ChangeRequestStatusType Complete =new ChangeRequestStatusType()
            {
                StatusName = "Completed"
            };
            context.ChangeRequestStatusTypes.AddOrUpdate(PendingForApproval);
            context.ChangeRequestStatusTypes.AddOrUpdate(HasComment);
            context.ChangeRequestStatusTypes.AddOrUpdate(Complete);
            #endregion

            var pendingForApprovalEntry = context.Entry(PendingForApproval).Entity;
            var pendingForApprovalId= pendingForApprovalEntry.StatusTypeId;
            var hasCommentIdEntry = context.Entry(HasComment).Entity;
            var hasCommentId = hasCommentIdEntry.StatusTypeId;
            var completeIdEntry = context.Entry(Complete).Entity;
            var completeId = completeIdEntry.StatusTypeId;
            #region ChangeRequest Data
            ChangeRequest cr1=new ChangeRequest()
            {
                ChangeRequestId = Guid.NewGuid(),
                CreatedBy = "Test1",
                SerialNumber = "20150825E001",
                CreateDate = DateTime.Parse("2015/08/25"),
                
            };

            ChangeRequest cr2 = new ChangeRequest()
            {
                ChangeRequestId = Guid.NewGuid(),
                CreatedBy = "Test2",
                SerialNumber = "20150826P002",
                CreateDate = DateTime.Parse("2015/08/26"),
                
            };

            ChangeRequest cr3 = new ChangeRequest()
            {
                ChangeRequestId = Guid.NewGuid(),
                CreatedBy = "Test3",
                SerialNumber = "20150827E003",
                CreateDate = DateTime.Parse("2015/08/27"),
                
            };

            context.ChangeRequests.AddOrUpdate(cr1);
            context.ChangeRequests.AddOrUpdate(cr2);
            context.ChangeRequests.AddOrUpdate(cr3);
            #endregion

            var cr1Entry = context.Entry(cr1).Entity;
            var cr1Id = cr1Entry.ChangeRequestId;
            var cr2Entry = context.Entry(cr2).Entity;
            var cr2Id = cr2Entry.ChangeRequestId;
            var cr3Entry = context.Entry(cr3).Entity;
            var cr3Id = cr3Entry.ChangeRequestId;


            #region ChangeRequestStatus Data

            ChangeRequestStatus statusForCR1 = new ChangeRequestStatus()
            {
                StatusTypeId = pendingForApprovalId,
                ChangeRequestStatusType = pendingForApprovalEntry,
                ChangeRequestId = cr1Id,
                ChangeRequest = cr1Entry,
                InitialDate = DateTime.Today-TimeSpan.FromDays(8)
            };

            ChangeRequestStatus firstStatusForCR2 = new ChangeRequestStatus()
            {
                StatusTypeId = pendingForApprovalId,
                ChangeRequestStatusType = PendingForApproval,
                ChangeRequestId = cr2Id,
                ChangeRequest = cr2Entry,
                InitialDate = DateTime.Today - TimeSpan.FromDays(8),
                EndDate = DateTime.Parse("2015/08/27")

            };

            ChangeRequestStatus secondStatusForCR2 =new ChangeRequestStatus()
            {
                StatusTypeId = hasCommentId,
                ChangeRequestStatusType = hasCommentIdEntry,
                ChangeRequestId = cr2Id,
                ChangeRequest = cr2Entry,
                InitialDate = DateTime.Today-TimeSpan.FromDays(5),
                
            };

            ChangeRequestStatus statusForCR3 = new ChangeRequestStatus()
            {
                StatusTypeId = completeId,
                ChangeRequestStatusType = completeIdEntry,
                ChangeRequestId = cr3Id,
                ChangeRequest = cr3Entry,
                InitialDate = DateTime.Today - TimeSpan.FromDays(3)
            };

            context.ChangeRequestStatuses.AddOrUpdate(statusForCR1);
            context.ChangeRequestStatuses.AddOrUpdate(firstStatusForCR2);
            context.ChangeRequestStatuses.AddOrUpdate(secondStatusForCR2);
            context.ChangeRequestStatuses.AddOrUpdate(statusForCR3);

            #endregion
        }
    }
}