namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblProduct_AddColShortDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Products", "ShortDescription", c => c.String(maxLength: 500));
            AddColumn("dbo.CMS_Products", "ShortDescriptionUS", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Products", "ShortDescriptionUS");
            DropColumn("dbo.CMS_Products", "ShortDescription");
        }
    }
}
