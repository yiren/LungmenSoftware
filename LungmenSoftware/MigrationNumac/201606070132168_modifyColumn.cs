namespace LungmenSoftware.MigrationNumac
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyColumn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chassis",
                c => new
                    {
                        ChassisId = c.Guid(nullable: false),
                        ChassisName = c.String(),
                        ChasisSerialNumber = c.String(),
                        Equipment = c.String(),
                        Panel = c.String(),
                    })
                .PrimaryKey(t => t.ChassisId);
            
            CreateTable(
                "dbo.ChassisBoards",
                c => new
                    {
                        ChassisBoardId = c.Guid(nullable: false),
                        ChassBoardName = c.String(),
                        ChassisId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ChassisBoardId)
                .ForeignKey("dbo.Chassis", t => t.ChassisId, cascadeDelete: true)
                .Index(t => t.ChassisId);
            
            CreateTable(
                "dbo.EPROMs",
                c => new
                    {
                        EPROMId = c.Guid(nullable: false),
                        SocketLocation = c.String(),
                        EPROMAssembly = c.String(),
                        EPROMAssemblyRev = c.Int(nullable: false),
                        PartsListRev = c.Int(nullable: false),
                        EPROMProgram = c.String(),
                        EPROMProgramRev = c.Int(nullable: false),
                        EPROMSerialNumber = c.String(),
                        ChassisBoardId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.EPROMId)
                .ForeignKey("dbo.ChassisBoards", t => t.ChassisBoardId, cascadeDelete: true)
                .Index(t => t.ChassisBoardId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EPROMs", "ChassisBoardId", "dbo.ChassisBoards");
            DropForeignKey("dbo.ChassisBoards", "ChassisId", "dbo.Chassis");
            DropIndex("dbo.EPROMs", new[] { "ChassisBoardId" });
            DropIndex("dbo.ChassisBoards", new[] { "ChassisId" });
            DropTable("dbo.EPROMs");
            DropTable("dbo.ChassisBoards");
            DropTable("dbo.Chassis");
        }
    }
}
