namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblCus_addCol_fbID_GoogleID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Customer", "FbID", c => c.String(maxLength: 100));
            AddColumn("dbo.CMS_Customer", "GoogleID", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Customer", "GoogleID");
            DropColumn("dbo.CMS_Customer", "FbID");
        }
    }
}
