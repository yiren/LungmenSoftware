namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNextStatusIsCurrentAndOwner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeRequests", "Owner", c => c.String());
            AddColumn("dbo.ChangeRequestStatus", "IsCurrent", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChangeRequestStatusTypes", "NextStatusId", c => c.Int());
            CreateIndex("dbo.ChangeRequestStatusTypes", "NextStatusId");
            AddForeignKey("dbo.ChangeRequestStatusTypes", "NextStatusId", "dbo.ChangeRequestStatusTypes", "StatusTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChangeRequestStatusTypes", "NextStatusId", "dbo.ChangeRequestStatusTypes");
            DropIndex("dbo.ChangeRequestStatusTypes", new[] { "NextStatusId" });
            DropColumn("dbo.ChangeRequestStatusTypes", "NextStatusId");
            DropColumn("dbo.ChangeRequestStatus", "IsCurrent");
            DropColumn("dbo.ChangeRequests", "Owner");
        }
    }
}
