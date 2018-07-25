namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblImage_addColOffset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_ImagesLink", "Offset", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_ImagesLink", "Offset");
        }
    }
}
