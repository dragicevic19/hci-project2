namespace TrainTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartureTime = c.DateTime(nullable: false),
                        Route_Id = c.Int(),
                        Train_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrainRoutes", t => t.Route_Id)
                .ForeignKey("dbo.Trains", t => t.Train_Id)
                .Index(t => t.Route_Id)
                .Index(t => t.Train_Id);
            
            CreateTable(
                "dbo.TrainRoutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StationOnRoutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Station_Id = c.Int(),
                        TrainRoute_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.Station_Id)
                .ForeignKey("dbo.TrainRoutes", t => t.TrainRoute_Id)
                .Index(t => t.Station_Id)
                .Index(t => t.TrainRoute_Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location_X = c.Double(nullable: false),
                        Location_Y = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Capacity = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrainSeats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Train_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trains", t => t.Train_Id)
                .Index(t => t.Train_Id);
            
            CreateTable(
                "dbo.OneSeats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Row = c.Int(nullable: false),
                        Col = c.Int(nullable: false),
                        TrainSeats_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrainSeats", t => t.TrainSeats_Id)
                .Index(t => t.TrainSeats_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        IsPurchased = c.Boolean(nullable: false),
                        PurchaseDateTime = c.DateTime(nullable: false),
                        EndStation_Id = c.Int(),
                        StartStation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StationOnRoutes", t => t.EndStation_Id)
                .ForeignKey("dbo.StationOnRoutes", t => t.StartStation_Id)
                .Index(t => t.EndStation_Id)
                .Index(t => t.StartStation_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "StartStation_Id", "dbo.StationOnRoutes");
            DropForeignKey("dbo.Tickets", "EndStation_Id", "dbo.StationOnRoutes");
            DropForeignKey("dbo.Departures", "Train_Id", "dbo.Trains");
            DropForeignKey("dbo.TrainSeats", "Train_Id", "dbo.Trains");
            DropForeignKey("dbo.OneSeats", "TrainSeats_Id", "dbo.TrainSeats");
            DropForeignKey("dbo.Departures", "Route_Id", "dbo.TrainRoutes");
            DropForeignKey("dbo.StationOnRoutes", "TrainRoute_Id", "dbo.TrainRoutes");
            DropForeignKey("dbo.StationOnRoutes", "Station_Id", "dbo.Stations");
            DropIndex("dbo.Tickets", new[] { "StartStation_Id" });
            DropIndex("dbo.Tickets", new[] { "EndStation_Id" });
            DropIndex("dbo.OneSeats", new[] { "TrainSeats_Id" });
            DropIndex("dbo.TrainSeats", new[] { "Train_Id" });
            DropIndex("dbo.StationOnRoutes", new[] { "TrainRoute_Id" });
            DropIndex("dbo.StationOnRoutes", new[] { "Station_Id" });
            DropIndex("dbo.Departures", new[] { "Train_Id" });
            DropIndex("dbo.Departures", new[] { "Route_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Tickets");
            DropTable("dbo.OneSeats");
            DropTable("dbo.TrainSeats");
            DropTable("dbo.Trains");
            DropTable("dbo.Stations");
            DropTable("dbo.StationOnRoutes");
            DropTable("dbo.TrainRoutes");
            DropTable("dbo.Departures");
        }
    }
}
