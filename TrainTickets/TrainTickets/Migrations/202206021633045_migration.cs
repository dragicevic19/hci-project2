namespace TrainTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "SeatId", "dbo.OneSeats");
            DropIndex("dbo.Tickets", new[] { "SeatId" });
            DropColumn("dbo.Tickets", "SeatId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "SeatId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "SeatId");
            AddForeignKey("dbo.Tickets", "SeatId", "dbo.OneSeats", "Id", cascadeDelete: true);
        }
    }
}
