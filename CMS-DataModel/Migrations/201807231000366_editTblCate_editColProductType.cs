namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblCate_editColProductType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Categories", "ProductTypeCode", c => c.Int(nullable: false));
            DropColumn("dbo.CMS_Categories", "ProductType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CMS_Categories", "ProductType", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.CMS_Categories", "ProductTypeCode");
        }
    }
}
