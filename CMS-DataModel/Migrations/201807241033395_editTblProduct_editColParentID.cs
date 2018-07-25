namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblProduct_editColParentID : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CMS_Products", new[] { "ParentID" });
            AlterColumn("dbo.CMS_Products", "ParentID", c => c.String(maxLength: 100));
            CreateIndex("dbo.CMS_Products", "ParentID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CMS_Products", new[] { "ParentID" });
            AlterColumn("dbo.CMS_Products", "ParentID", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.CMS_Products", "ParentID");
        }
    }
}
