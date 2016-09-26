namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDesignDocCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeRequests", "DesignDoc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChangeRequests", "DesignDoc");
        }
    }
}
