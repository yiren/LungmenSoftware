namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModuleInfoColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModuleInfoes", "Assembly", c => c.String());
            DropColumn("dbo.ModuleInfoes", "ModuleBoardName");
            DropColumn("dbo.ModuleInfoes", "SocketLocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ModuleInfoes", "SocketLocation", c => c.String());
            AddColumn("dbo.ModuleInfoes", "ModuleBoardName", c => c.String());
            DropColumn("dbo.ModuleInfoes", "Assembly");
        }
    }
}
