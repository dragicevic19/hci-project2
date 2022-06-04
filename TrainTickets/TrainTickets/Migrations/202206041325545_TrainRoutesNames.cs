namespace TrainTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrainRoutesNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrainRoutes", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrainRoutes", "Name");
        }
    }
}
