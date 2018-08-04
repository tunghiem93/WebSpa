namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTblProduct_AddColForProcedures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Products", "Preparation", c => c.String(maxLength: 500));
            AddColumn("dbo.CMS_Products", "Process", c => c.String(maxLength: 500));
            AddColumn("dbo.CMS_Products", "SpaTreatment", c => c.String(maxLength: 500));
            AddColumn("dbo.CMS_Products", "Effect", c => c.String(maxLength: 500));
            AddColumn("dbo.CMS_Products", "PreparationUS", c => c.String());
            AddColumn("dbo.CMS_Products", "ProcessUS", c => c.String(maxLength: 500));
            AddColumn("dbo.CMS_Products", "SpaTreatmentUS", c => c.String(maxLength: 500));
            AddColumn("dbo.CMS_Products", "EffectUS", c => c.String(maxLength: 500));
            AddColumn("dbo.CMS_Products", "Duration", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Products", "Duration");
            DropColumn("dbo.CMS_Products", "EffectUS");
            DropColumn("dbo.CMS_Products", "SpaTreatmentUS");
            DropColumn("dbo.CMS_Products", "ProcessUS");
            DropColumn("dbo.CMS_Products", "PreparationUS");
            DropColumn("dbo.CMS_Products", "Effect");
            DropColumn("dbo.CMS_Products", "SpaTreatment");
            DropColumn("dbo.CMS_Products", "Process");
            DropColumn("dbo.CMS_Products", "Preparation");
        }
    }
}
