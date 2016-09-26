namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialConfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChangeRequests",
                c => new
                    {
                        ChangeRequestId = c.Guid(nullable: false),
                        SerialNumber = c.String(nullable: false, maxLength: 50),
                        CreatedBy = c.String(nullable: false, maxLength: 100),
                        ReviewBy = c.String(maxLength: 100),
                        ApprovedBy = c.String(maxLength: 100),
                        LastModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChangeRequestId);
            
            CreateTable(
                "dbo.ChangeRequestStatus",
                c => new
                    {
                        ChangeStatusId = c.Int(nullable: false, identity: true),
                        StatusTypeId = c.Int(nullable: false),
                        ChangeRequestId = c.Guid(nullable: false),
                        InitialDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ChangeStatusId)
                .ForeignKey("dbo.ChangeRequests", t => t.ChangeRequestId, cascadeDelete: true)
                .ForeignKey("dbo.ChangeRequestStatusTypes", t => t.StatusTypeId, cascadeDelete: true)
                .Index(t => t.StatusTypeId)
                .Index(t => t.ChangeRequestId);
            
            CreateTable(
                "dbo.ChangeRequestStatusTypes",
                c => new
                    {
                        StatusTypeId = c.Int(nullable: false, identity: true),
                        StatusName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.StatusTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChangeRequestStatus", "StatusTypeId", "dbo.ChangeRequestStatusTypes");
            DropForeignKey("dbo.ChangeRequestStatus", "ChangeRequestId", "dbo.ChangeRequests");
            DropIndex("dbo.ChangeRequestStatus", new[] { "ChangeRequestId" });
            DropIndex("dbo.ChangeRequestStatus", new[] { "StatusTypeId" });
            DropTable("dbo.ChangeRequestStatusTypes");
            DropTable("dbo.ChangeRequestStatus");
            DropTable("dbo.ChangeRequests");
        }
    }
}
