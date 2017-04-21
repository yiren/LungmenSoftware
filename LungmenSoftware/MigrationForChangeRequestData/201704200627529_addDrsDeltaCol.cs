namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDrsDeltaCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DrsChangeDeltas", "FIDDiagramNo", c => c.String());
            AddColumn("dbo.DrsChangeDeltas", "Division", c => c.Int(nullable: false));
            AddColumn("dbo.DrsChangeDeltas", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DrsChangeDeltas", "Description");
            DropColumn("dbo.DrsChangeDeltas", "Division");
            DropColumn("dbo.DrsChangeDeltas", "FIDDiagramNo");
        }
    }
}
