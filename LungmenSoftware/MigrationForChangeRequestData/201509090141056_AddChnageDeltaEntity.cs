namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChnageDeltaEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChangeDeltas",
                c => new
                    {
                        ChangeDeltaId = c.Guid(nullable: false),
                        FoxSoftwareId = c.Int(nullable: false),
                        originalValue = c.String(),
                        newValue = c.String(),
                        ChangeRequestId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ChangeDeltaId)
                .ForeignKey("dbo.ChangeRequests", t => t.ChangeRequestId, cascadeDelete: true)
                .Index(t => t.ChangeRequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChangeDeltas", "ChangeRequestId", "dbo.ChangeRequests");
            DropIndex("dbo.ChangeDeltas", new[] { "ChangeRequestId" });
            DropTable("dbo.ChangeDeltas");
        }
    }
}
