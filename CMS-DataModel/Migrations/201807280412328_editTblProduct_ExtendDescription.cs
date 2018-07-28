namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblProduct_ExtendDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CMS_Products", "Description", c => c.String(maxLength: 4000));
            AlterColumn("dbo.CMS_Products", "DescriptionUS", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CMS_Products", "DescriptionUS", c => c.String(maxLength: 256));
            AlterColumn("dbo.CMS_Products", "Description", c => c.String(maxLength: 256));
        }
    }
}
