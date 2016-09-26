namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionInChangeRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeRequests", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChangeRequests", "Description");
        }
    }
}
