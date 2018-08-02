namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblNew_AddColType_AddColLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_News", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.CMS_News", "Link", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_News", "Link");
            DropColumn("dbo.CMS_News", "Type");
        }
    }
}
