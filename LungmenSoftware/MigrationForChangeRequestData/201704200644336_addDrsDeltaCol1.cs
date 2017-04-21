namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDrsDeltaCol1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DrsChangeDeltas", "DrsPanelName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DrsChangeDeltas", "DrsPanelName");
        }
    }
}
