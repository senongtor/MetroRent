namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptiontoSeekTenantRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeekTenantRequests", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SeekTenantRequests", "Description");
        }
    }
}
