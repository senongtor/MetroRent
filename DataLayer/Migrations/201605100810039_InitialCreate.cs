namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeekRoomRequestId = c.Int(nullable: false),
                        Region = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SeekRoomRequests", t => t.SeekRoomRequestId, cascadeDelete: true)
                .Index(t => t.SeekRoomRequestId);
            
            CreateTable(
                "dbo.ProfileImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        filePath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeekTenantRequestId = c.Int(nullable: false),
                        filePath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SeekTenantRequests", t => t.SeekTenantRequestId, cascadeDelete: true)
                .Index(t => t.SeekTenantRequestId);
            
            CreateTable(
                "dbo.SeekRoomRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        BudgetLowerBound = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BudgetUpperBound = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Gender = c.Int(nullable: false),
                        RentalStartDate = c.DateTime(nullable: false),
                        RequestCreateTime = c.DateTime(nullable: false),
                        ContactPersonName = c.String(),
                        ContactPersonPhone = c.String(),
                        ContactPersonEmail = c.String(),
                        IsRequestActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SeekTenantRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        RoomRegion = c.Int(nullable: false),
                        Address = c.String(),
                        MonthlyRentalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RoomType = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        RentalStartDate = c.DateTime(nullable: false),
                        RequestCreateTime = c.DateTime(nullable: false),
                        ContactPersonName = c.String(),
                        ContactPersonPhone = c.String(),
                        ContactPersonEmail = c.String(),
                        IsRequestActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomImages", "SeekTenantRequestId", "dbo.SeekTenantRequests");
            DropForeignKey("dbo.Locations", "SeekRoomRequestId", "dbo.SeekRoomRequests");
            DropIndex("dbo.RoomImages", new[] { "SeekTenantRequestId" });
            DropIndex("dbo.Locations", new[] { "SeekRoomRequestId" });
            DropTable("dbo.SeekTenantRequests");
            DropTable("dbo.SeekRoomRequests");
            DropTable("dbo.RoomImages");
            DropTable("dbo.ProfileImages");
            DropTable("dbo.Locations");
        }
    }
}
