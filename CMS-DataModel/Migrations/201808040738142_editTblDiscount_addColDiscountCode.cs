namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblDiscount_addColDiscountCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Discount", "DiscountCode", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Discount", "DiscountCode");
        }
    }
}
