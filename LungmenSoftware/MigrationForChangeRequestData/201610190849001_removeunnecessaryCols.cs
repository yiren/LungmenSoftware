namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeunnecessaryCols : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ModuleInfoes", "NumacChangeDeltaId", "dbo.NumacChangeDeltas");
            DropIndex("dbo.ModuleInfoes", new[] { "NumacChangeDeltaId" });
            DropColumn("dbo.NumacChangeDeltas", "OriSocketLocation");
            DropTable("dbo.ModuleInfoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ModuleInfoes",
                c => new
                    {
                        ModuleInfoId = c.Int(nullable: false, identity: true),
                        ChassisName = c.String(),
                        SocketLocation = c.String(),
                        Assembly = c.String(),
                        SerialNumber = c.String(),
                        Program = c.String(),
                        Rev = c.String(),
                        NumacChangeDeltaId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ModuleInfoId);
            
            AddColumn("dbo.NumacChangeDeltas", "OriSocketLocation", c => c.String());
            CreateIndex("dbo.ModuleInfoes", "NumacChangeDeltaId");
            AddForeignKey("dbo.ModuleInfoes", "NumacChangeDeltaId", "dbo.NumacChangeDeltas", "NumacChangeDeltaId", cascadeDelete: true);
        }
    }
}
