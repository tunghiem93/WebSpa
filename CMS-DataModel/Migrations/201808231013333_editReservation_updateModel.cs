namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editReservation_updateModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Reservation", "CustomerName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Reservation", "CustomerName");
        }
    }
}
