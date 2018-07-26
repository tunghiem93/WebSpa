namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUSFieldFor_Product_Cate_News : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Categories", "NameUS", c => c.String(maxLength: 50));
            AddColumn("dbo.CMS_Categories", "DescriptionUS", c => c.String(maxLength: 500));
            AddColumn("dbo.CMS_Products", "NameUS", c => c.String(maxLength: 100));
            AddColumn("dbo.CMS_Products", "DescriptionUS", c => c.String(maxLength: 256));
            AddColumn("dbo.CMS_News", "TitleUS", c => c.String(maxLength: 150));
            AddColumn("dbo.CMS_News", "Short_DescriptionUS", c => c.String(maxLength: 500));
            AddColumn("dbo.CMS_News", "DescriptionUS", c => c.String(storeType: "ntext"));
            AddColumn("dbo.CMS_News", "CategoryUS", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_News", "CategoryUS");
            DropColumn("dbo.CMS_News", "DescriptionUS");
            DropColumn("dbo.CMS_News", "Short_DescriptionUS");
            DropColumn("dbo.CMS_News", "TitleUS");
            DropColumn("dbo.CMS_Products", "DescriptionUS");
            DropColumn("dbo.CMS_Products", "NameUS");
            DropColumn("dbo.CMS_Categories", "DescriptionUS");
            DropColumn("dbo.CMS_Categories", "NameUS");
        }
    }
}
