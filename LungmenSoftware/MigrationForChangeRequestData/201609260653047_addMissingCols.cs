namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMissingCols : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NumacChangeDeltas", "ModuleBoardName", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "OriSocketLocation", c => c.String());
            AddColumn("dbo.NumacChangeDeltas", "SocketLocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NumacChangeDeltas", "SocketLocation");
            DropColumn("dbo.NumacChangeDeltas", "OriSocketLocation");
            DropColumn("dbo.NumacChangeDeltas", "ModuleBoardName");
        }
    }
}
