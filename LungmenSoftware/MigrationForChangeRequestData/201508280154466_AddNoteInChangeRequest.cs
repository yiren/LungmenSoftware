namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNoteInChangeRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeRequests", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChangeRequests", "Note");
        }
    }
}
