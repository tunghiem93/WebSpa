namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblOrder_addColReason : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Order", "Reason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Order", "Reason");
        }
    }
}
