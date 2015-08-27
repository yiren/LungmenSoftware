namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLastModifiedDateToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChangeRequests", "LastModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ChangeRequests", "LastModifiedDate", c => c.DateTime(nullable: false));
        }
    }
}
