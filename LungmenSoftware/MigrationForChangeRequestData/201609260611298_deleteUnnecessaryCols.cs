namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteUnnecessaryCols : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.NumacChangeDeltas", "OriPanel");
            DropColumn("dbo.NumacChangeDeltas", "OriChassisName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NumacChangeDeltas", "OriChassisName", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "OriPanel", c => c.String());
        }
    }
}
