namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDrsChangeDelta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DrsChangeDeltas",
                c => new
                    {
                        DrsChangeDeltaId = c.Guid(nullable: false),
                        FidId = c.Guid(nullable: false),
                        ModuleType = c.String(),
                        Checksum = c.String(),
                        EPROMRev = c.String(),
                        OriModuleType = c.String(),
                        OriChecksum = c.String(),
                        OriEPROMRev = c.String(),
                        ChangeRequestId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.DrsChangeDeltaId)
                .ForeignKey("dbo.ChangeRequests", t => t.ChangeRequestId, cascadeDelete: true)
                .Index(t => t.ChangeRequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DrsChangeDeltas", "ChangeRequestId", "dbo.ChangeRequests");
            DropIndex("dbo.DrsChangeDeltas", new[] { "ChangeRequestId" });
            DropTable("dbo.DrsChangeDeltas");
        }
    }
}
