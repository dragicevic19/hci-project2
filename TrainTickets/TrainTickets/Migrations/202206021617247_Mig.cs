namespace TrainTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "EndStationId", "dbo.StationOnRoutes");
            DropIndex("dbo.Tickets", new[] { "EndStationId" });
            AddColumn("dbo.Tickets", "StartStationId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "StartStationId");
            CreateIndex("dbo.Tickets", "EndStationId");
            AddForeignKey("dbo.Tickets", "EndStationId", "dbo.StationOnRoutes", "Id", cascadeDelete: true);
        }
    }
}
