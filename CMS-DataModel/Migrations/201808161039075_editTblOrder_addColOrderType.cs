namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblOrder_addColOrderType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Order", "OrderType", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Order", "OrderType");
        }
    }
}
