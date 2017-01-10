namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNumacChangeDelta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NumacChangeDeltas", "Assembly", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "SerialNumber", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "Program", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "Rev", c => c.String());
            DropColumn("dbo.NumacChangeDeltas", "OriginalValue");
            DropColumn("dbo.NumacChangeDeltas", "NewValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NumacChangeDeltas", "NewValue", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "OriginalValue", c => c.String());
            DropColumn("dbo.NumacChangeDeltas", "Rev");
            DropColumn("dbo.NumacChangeDeltas", "Program");
            DropColumn("dbo.NumacChangeDeltas", "SerialNumber");
            DropColumn("dbo.NumacChangeDeltas", "Assembly");
        }
    }
}
