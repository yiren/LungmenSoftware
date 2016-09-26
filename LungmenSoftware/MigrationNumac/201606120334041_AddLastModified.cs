namespace LungmenSoftware.MigrationNumac
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chassis", "LastModifiedBy", c => c.String());
            AddColumn("dbo.Chassis", "LastModifiedDate", c => c.String());
            AddColumn("dbo.ChassisBoards", "LastModifiedBy", c => c.String());
            AddColumn("dbo.ChassisBoards", "LastModifiedDate", c => c.String());
            AddColumn("dbo.EPROMs", "LastModifiedBy", c => c.String());
            AddColumn("dbo.EPROMs", "LastModifiedDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EPROMs", "LastModifiedDate");
            DropColumn("dbo.EPROMs", "LastModifiedBy");
            DropColumn("dbo.ChassisBoards", "LastModifiedDate");
            DropColumn("dbo.ChassisBoards", "LastModifiedBy");
            DropColumn("dbo.Chassis", "LastModifiedDate");
            DropColumn("dbo.Chassis", "LastModifiedBy");
        }
    }
}
