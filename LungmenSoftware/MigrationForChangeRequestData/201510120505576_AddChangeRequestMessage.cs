namespace LungmenSoftware.MigrationForChangeRequestData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChangeRequestMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChangeRequestMessages",
                c => new
                    {
                        ChangeRequestMessageId = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        CreateBy = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        ChangeRequestId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ChangeRequestMessageId)
                .ForeignKey("dbo.ChangeRequests", t => t.ChangeRequestId, cascadeDelete: true)
                .Index(t => t.ChangeRequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChangeRequestMessages", "ChangeRequestId", "dbo.ChangeRequests");
            DropIndex("dbo.ChangeRequestMessages", new[] { "ChangeRequestId" });
            DropTable("dbo.ChangeRequestMessages");
        }
    }
}
