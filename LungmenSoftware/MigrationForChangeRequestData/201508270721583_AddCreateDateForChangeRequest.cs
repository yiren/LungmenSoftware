namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateDateForChangeRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeRequests", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChangeRequests", "CreateDate");
        }
    }
}
