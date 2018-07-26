namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLanguage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CMS_Language",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        Name = c.String(),
                        Symbol = c.String(maxLength: 10),
                        IsActive = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CMS_LanguageDetail",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        LanguageID = c.String(nullable: false, maxLength: 100),
                        LanguageKeyID = c.String(nullable: false, maxLength: 100),
                        Text = c.String(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CMS_LanguageKey",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false),
                        Status = c.Byte(nullable: false),
                        Code = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CMS_LanguageKey");
            DropTable("dbo.CMS_LanguageDetail");
            DropTable("dbo.CMS_Language");
        }
    }
}
