namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfileImagePathtoSeekRoomRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeekRoomRequests", "ProfileImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SeekRoomRequests", "ProfileImagePath");
        }
    }
}
