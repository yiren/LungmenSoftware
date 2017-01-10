namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOriginalValueCols : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NumacChangeDeltas", "OriPanel", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "OriChassisName", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "OriAssembly", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "OriSerialNumber", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "OriProgram", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "OriRev", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NumacChangeDeltas", "OriRev");
            DropColumn("dbo.NumacChangeDeltas", "OriProgram");
            DropColumn("dbo.NumacChangeDeltas", "OriSerialNumber");
            DropColumn("dbo.NumacChangeDeltas", "OriAssembly");
            DropColumn("dbo.NumacChangeDeltas", "OriChassisName");
            DropColumn("dbo.NumacChangeDeltas", "OriPanel");
        }
    }
}
