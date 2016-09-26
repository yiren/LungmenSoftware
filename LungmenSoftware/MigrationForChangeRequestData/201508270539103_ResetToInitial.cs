namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResetToInitial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChangeRequestStatus", "ChangeRequestId", "dbo.ChangeRequests");
            DropPrimaryKey("dbo.ChangeRequests");
            AlterColumn("dbo.ChangeRequests", "ChangeRequestId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.ChangeRequests", "ChangeRequestId");
            AddForeignKey("dbo.ChangeRequestStatus", "ChangeRequestId", "dbo.ChangeRequests", "ChangeRequestId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChangeRequestStatus", "ChangeRequestId", "dbo.ChangeRequests");
            DropPrimaryKey("dbo.ChangeRequests");
            AlterColumn("dbo.ChangeRequests", "ChangeRequestId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.ChangeRequests", "ChangeRequestId");
            AddForeignKey("dbo.ChangeRequestStatus", "ChangeRequestId", "dbo.ChangeRequests", "ChangeRequestId", cascadeDelete: true);
        }
    }
}
