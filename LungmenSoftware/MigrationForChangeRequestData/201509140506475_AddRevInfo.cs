namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRevInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RevInfoes",
                c => new
                    {
                        RevInfoId = c.Int(nullable: false, identity: true),
                        JoinTableId = c.Long(),
                        FoxWorkStationId = c.Int(),
                        FoxSoftwareId = c.Int(),
                        WorkStationName = c.String(),
                        SoftwareName = c.String(),
                        Rev = c.String(),
                        Procedure = c.String(),
                        Software_Library_Identification = c.String(),
                        Media_Identification = c.String(),
                        Note = c.String(),
                        ChangeDeltaId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.RevInfoId)
                .ForeignKey("dbo.ChangeDeltas", t => t.ChangeDeltaId, cascadeDelete: true)
                .Index(t => t.ChangeDeltaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RevInfoes", "ChangeDeltaId", "dbo.ChangeDeltas");
            DropIndex("dbo.RevInfoes", new[] { "ChangeDeltaId" });
            DropTable("dbo.RevInfoes");
        }
    }
}
