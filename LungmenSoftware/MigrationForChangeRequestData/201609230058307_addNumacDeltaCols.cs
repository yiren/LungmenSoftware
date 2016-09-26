namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNumacDeltaCols : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NumacChangeDeltas", "Panel", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "ChassisName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NumacChangeDeltas", "ChassisName");
            DropColumn("dbo.NumacChangeDeltas", "Panel");
        }
    }
}
