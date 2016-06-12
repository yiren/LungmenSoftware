namespace LungmenSoftware.MigrationNumac
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnForDisplay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChassisBoards", "ChassisName", c => c.String());
            AddColumn("dbo.EPROMs", "ChassisName", c => c.String());
            AddColumn("dbo.EPROMs", "ChassisBoardName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EPROMs", "ChassisBoardName");
            DropColumn("dbo.EPROMs", "ChassisName");
            DropColumn("dbo.ChassisBoards", "ChassisName");
        }
    }
}
