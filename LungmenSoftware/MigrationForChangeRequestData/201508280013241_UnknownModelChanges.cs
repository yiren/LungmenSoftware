namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnknownModelChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChangeRequests", "LastModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ChangeRequests", "LastModifiedDate", c => c.DateTime());
        }
    }
}
