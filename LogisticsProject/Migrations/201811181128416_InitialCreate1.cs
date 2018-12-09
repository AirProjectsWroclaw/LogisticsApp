namespace LogisticsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        routeId = c.Int(nullable: false, identity: true),
                        distance = c.Double(nullable: false),
                        duration = c.Double(nullable: false),
                        cityFrom_CityId = c.Int(),
                        cityTo_CityId = c.Int(),
                    })
                .PrimaryKey(t => t.routeId)
                .ForeignKey("dbo.Cities", t => t.cityFrom_CityId)
                .ForeignKey("dbo.Cities", t => t.cityTo_CityId)
                .Index(t => t.cityFrom_CityId)
                .Index(t => t.cityTo_CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "cityTo_CityId", "dbo.Cities");
            DropForeignKey("dbo.Routes", "cityFrom_CityId", "dbo.Cities");
            DropIndex("dbo.Routes", new[] { "cityTo_CityId" });
            DropIndex("dbo.Routes", new[] { "cityFrom_CityId" });
            DropTable("dbo.Routes");
        }
    }
}
