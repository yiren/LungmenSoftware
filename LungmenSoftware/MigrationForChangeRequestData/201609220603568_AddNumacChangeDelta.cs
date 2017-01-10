namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumacChangeDelta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NumacChangeDeltas",
                c => new
                    {
                        NumacChangeDeltaId = c.Guid(nullable: false),
                        ModuleBoardId = c.Guid(nullable: false),
                        OriginalValue = c.String(),
                        NewValue = c.String(),
                        ChangeRequestId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.NumacChangeDeltaId)
                .ForeignKey("dbo.ChangeRequests", t => t.ChangeRequestId, cascadeDelete: true)
                .Index(t => t.ChangeRequestId);
            
            CreateTable(
                "dbo.ModuleInfoes",
                c => new
                    {
                        ModuleInfoId = c.Int(nullable: false, identity: true),
                        ModuleBoardName = c.String(),
                        SocketLocation = c.String(),
                        SerialNumber = c.String(),
                        Program = c.String(),
                        Rev = c.String(),
                        NumacChangeDeltaId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ModuleInfoId)
                .ForeignKey("dbo.NumacChangeDeltas", t => t.NumacChangeDeltaId, cascadeDelete: true)
                .Index(t => t.NumacChangeDeltaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModuleInfoes", "NumacChangeDeltaId", "dbo.NumacChangeDeltas");
            DropForeignKey("dbo.NumacChangeDeltas", "ChangeRequestId", "dbo.ChangeRequests");
            DropIndex("dbo.ModuleInfoes", new[] { "NumacChangeDeltaId" });
            DropIndex("dbo.NumacChangeDeltas", new[] { "ChangeRequestId" });
            DropTable("dbo.ModuleInfoes");
            DropTable("dbo.NumacChangeDeltas");
        }
    }
}
