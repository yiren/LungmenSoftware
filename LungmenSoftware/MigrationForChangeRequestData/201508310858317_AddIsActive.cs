namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeRequests", "IsActive", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChangeRequests", "IsActive");
        }
    }
}
