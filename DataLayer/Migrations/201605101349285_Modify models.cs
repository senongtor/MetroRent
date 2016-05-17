namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifymodels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SeekRoomRequests", "Title", c => c.String());
            AlterColumn("dbo.SeekTenantRequests", "Title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SeekTenantRequests", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.SeekRoomRequests", "Title", c => c.String(nullable: false));
        }
    }
}
