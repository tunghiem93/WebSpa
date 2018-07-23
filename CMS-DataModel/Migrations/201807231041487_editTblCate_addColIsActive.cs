namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblCate_addColIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Categories", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Categories", "IsActive");
        }
    }
}
