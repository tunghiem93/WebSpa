namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delTblProductOnStore : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CMS_ProductOnStore", "ProductID", "dbo.CMS_Products");
            DropForeignKey("dbo.CMS_ProductOnStore", "StoreID", "dbo.CMS_Store");
            DropIndex("dbo.CMS_ProductOnStore", new[] { "StoreID" });
            DropIndex("dbo.CMS_ProductOnStore", new[] { "ProductID" });
            AddColumn("dbo.CMS_Products", "StoreID", c => c.String(nullable: false, maxLength: 100));
            DropTable("dbo.CMS_ProductOnStore");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CMS_ProductOnStore",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(maxLength: 100),
                        ProductID = c.String(nullable: false, maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        Sequence = c.Int(),
                        Description = c.String(maxLength: 256),
                        Cost = c.Double(nullable: false),
                        Unit = c.Int(),
                        Measure = c.String(nullable: false, maxLength: 50),
                        Quantity = c.Decimal(precision: 18, scale: 2),
                        Limit = c.Int(nullable: false),
                        ExtraPrice = c.Double(nullable: false),
                        IsAllowedDiscount = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsCheckedStock = c.Boolean(nullable: false),
                        IsAllowedOpenPrice = c.Boolean(nullable: false),
                        IsPrintedOnCheck = c.Boolean(nullable: false),
                        ExpiredDate = c.DateTime(nullable: false),
                        IsAutoAddToOrder = c.Boolean(nullable: false),
                        IsComingSoon = c.Boolean(nullable: false),
                        IsShowInReservation = c.Boolean(nullable: false),
                        IsRecommend = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.CMS_Products", "StoreID");
            CreateIndex("dbo.CMS_ProductOnStore", "ProductID");
            CreateIndex("dbo.CMS_ProductOnStore", "StoreID");
            AddForeignKey("dbo.CMS_ProductOnStore", "StoreID", "dbo.CMS_Store", "ID");
            AddForeignKey("dbo.CMS_ProductOnStore", "ProductID", "dbo.CMS_Products", "ID", cascadeDelete: true);
        }
    }
}
