namespace LungmenSoftware.MigrationForIdentity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChtAliasColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "ChtAlias", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetRoles", "ChtAlias");
        }
    }
}
