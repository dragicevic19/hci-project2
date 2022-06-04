namespace TrainTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StationOnRoutes", "TrainRoute_Id", "dbo.TrainRoutes");
            DropIndex("dbo.StationOnRoutes", new[] { "TrainRoute_Id" });
            RenameColumn(table: "dbo.StationOnRoutes", name: "TrainRoute_Id", newName: "TrainRouteId");
            CreateTable(
                "dbo.DepartureTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.Time(nullable: false, precision: 7),
                        RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrainRoutes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.RouteId);
            
            AlterColumn("dbo.StationOnRoutes", "TrainRouteId", c => c.Int(nullable: false));
            CreateIndex("dbo.StationOnRoutes", "TrainRouteId");
            AddForeignKey("dbo.StationOnRoutes", "TrainRouteId", "dbo.TrainRoutes", "Id", cascadeDelete: true);
            DropColumn("dbo.StationOnRoutes", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StationOnRoutes", "DateTime", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.StationOnRoutes", "TrainRouteId", "dbo.TrainRoutes");
            DropForeignKey("dbo.DepartureTimes", "RouteId", "dbo.TrainRoutes");
            DropIndex("dbo.StationOnRoutes", new[] { "TrainRouteId" });
            DropIndex("dbo.DepartureTimes", new[] { "RouteId" });
            AlterColumn("dbo.StationOnRoutes", "TrainRouteId", c => c.Int());
            DropTable("dbo.DepartureTimes");
            RenameColumn(table: "dbo.StationOnRoutes", name: "TrainRouteId", newName: "TrainRoute_Id");
            CreateIndex("dbo.StationOnRoutes", "TrainRoute_Id");
            AddForeignKey("dbo.StationOnRoutes", "TrainRoute_Id", "dbo.TrainRoutes", "Id");
        }
    }
}
