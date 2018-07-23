namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColStoreID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CMS_CustomerOnStore", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_ModuleOnStore", "ModuleID", "dbo.CMS_Module");
            DropForeignKey("dbo.CMS_ModuleOnStore", "StoreID", "dbo.CMS_Store");
            DropIndex("dbo.CMS_CustomerOnStore", new[] { "StoreID" });
            DropIndex("dbo.CMS_ModuleOnStore", new[] { "StoreID" });
            DropIndex("dbo.CMS_ModuleOnStore", new[] { "ModuleID" });
            AddColumn("dbo.CMS_Employee", "StoreID", c => c.String(maxLength: 100));
            AddColumn("dbo.CMS_Customer", "StoreID", c => c.String(maxLength: 100));
            AddColumn("dbo.CMS_Customer", "LastTransaction", c => c.DateTime());
            AddColumn("dbo.CMS_Customer", "MemberTierID", c => c.String(maxLength: 100));
            AddColumn("dbo.CMS_Customer", "Point", c => c.Int());
            AddColumn("dbo.CMS_Companies", "StoreID", c => c.String(maxLength: 100));
            AlterColumn("dbo.CMS_Products", "StoreID", c => c.String(maxLength: 100));
            DropTable("dbo.CMS_CustomerOnStore");
            DropTable("dbo.CMS_ModuleOnStore");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CMS_ModuleOnStore",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        ModuleID = c.String(nullable: false, maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CMS_CustomerOnStore",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        CustomerID = c.String(nullable: false, maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        LastTransaction = c.DateTime(),
                        MemberTierID = c.String(maxLength: 100),
                        Point = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.CMS_Products", "StoreID", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.CMS_Companies", "StoreID");
            DropColumn("dbo.CMS_Customer", "Point");
            DropColumn("dbo.CMS_Customer", "MemberTierID");
            DropColumn("dbo.CMS_Customer", "LastTransaction");
            DropColumn("dbo.CMS_Customer", "StoreID");
            DropColumn("dbo.CMS_Employee", "StoreID");
            CreateIndex("dbo.CMS_ModuleOnStore", "ModuleID");
            CreateIndex("dbo.CMS_ModuleOnStore", "StoreID");
            CreateIndex("dbo.CMS_CustomerOnStore", "StoreID");
            AddForeignKey("dbo.CMS_ModuleOnStore", "StoreID", "dbo.CMS_Store", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CMS_ModuleOnStore", "ModuleID", "dbo.CMS_Module", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CMS_CustomerOnStore", "StoreID", "dbo.CMS_Store", "ID");
        }
    }
}
