namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reAddModuleInfoCols : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModuleInfoes", "ChassisName", c => c.String());
            AddColumn("dbo.ModuleInfoes", "SocketLocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ModuleInfoes", "SocketLocation");
            DropColumn("dbo.ModuleInfoes", "ChassisName");
        }
    }
}
