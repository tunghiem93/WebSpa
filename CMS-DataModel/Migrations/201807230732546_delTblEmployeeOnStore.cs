namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delTblEmployeeOnStore : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CMS_EmployeeOnStore", "RoleID", "dbo.CMS_Role");
            DropForeignKey("dbo.CMS_EmployeeOnStore", "StoreID", "dbo.CMS_Store");
            DropIndex("dbo.CMS_EmployeeOnStore", new[] { "StoreID" });
            DropIndex("dbo.CMS_EmployeeOnStore", new[] { "RoleID" });
            AddColumn("dbo.CMS_Products", "Cost", c => c.Double(nullable: false));
            AddColumn("dbo.CMS_Products", "Unit", c => c.Int());
            AddColumn("dbo.CMS_Products", "Measure", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.CMS_Products", "Quantity", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CMS_Products", "Limit", c => c.Int(nullable: false));
            AddColumn("dbo.CMS_Products", "ExtraPrice", c => c.Double(nullable: false));
            AddColumn("dbo.CMS_Products", "IsAllowedDiscount", c => c.Boolean(nullable: false));
            AddColumn("dbo.CMS_Products", "IsCheckedStock", c => c.Boolean(nullable: false));
            AddColumn("dbo.CMS_Products", "IsAllowedOpenPrice", c => c.Boolean(nullable: false));
            AddColumn("dbo.CMS_Products", "IsPrintedOnCheck", c => c.Boolean(nullable: false));
            AddColumn("dbo.CMS_Products", "ExpiredDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CMS_Products", "IsAutoAddToOrder", c => c.Boolean(nullable: false));
            AddColumn("dbo.CMS_Products", "IsComingSoon", c => c.Boolean(nullable: false));
            AddColumn("dbo.CMS_Products", "IsShowInReservation", c => c.Boolean(nullable: false));
            AddColumn("dbo.CMS_Products", "IsRecommend", c => c.Boolean(nullable: false));
            AddColumn("dbo.CMS_News", "Author", c => c.String(maxLength: 250));
            AddColumn("dbo.CMS_News", "Source", c => c.String(maxLength: 250));
            AddColumn("dbo.CMS_News", "Category", c => c.String(maxLength: 250));
            DropTable("dbo.CMS_EmployeeOnStore");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CMS_EmployeeOnStore",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        RoleID = c.String(nullable: false, maxLength: 100),
                        EmployeeID = c.String(nullable: false, maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.CMS_News", "Category");
            DropColumn("dbo.CMS_News", "Source");
            DropColumn("dbo.CMS_News", "Author");
            DropColumn("dbo.CMS_Products", "IsRecommend");
            DropColumn("dbo.CMS_Products", "IsShowInReservation");
            DropColumn("dbo.CMS_Products", "IsComingSoon");
            DropColumn("dbo.CMS_Products", "IsAutoAddToOrder");
            DropColumn("dbo.CMS_Products", "ExpiredDate");
            DropColumn("dbo.CMS_Products", "IsPrintedOnCheck");
            DropColumn("dbo.CMS_Products", "IsAllowedOpenPrice");
            DropColumn("dbo.CMS_Products", "IsCheckedStock");
            DropColumn("dbo.CMS_Products", "IsAllowedDiscount");
            DropColumn("dbo.CMS_Products", "ExtraPrice");
            DropColumn("dbo.CMS_Products", "Limit");
            DropColumn("dbo.CMS_Products", "Quantity");
            DropColumn("dbo.CMS_Products", "Measure");
            DropColumn("dbo.CMS_Products", "Unit");
            DropColumn("dbo.CMS_Products", "Cost");
            CreateIndex("dbo.CMS_EmployeeOnStore", "RoleID");
            CreateIndex("dbo.CMS_EmployeeOnStore", "StoreID");
            AddForeignKey("dbo.CMS_EmployeeOnStore", "StoreID", "dbo.CMS_Store", "ID");
            AddForeignKey("dbo.CMS_EmployeeOnStore", "RoleID", "dbo.CMS_Role", "ID");
        }
    }
}
