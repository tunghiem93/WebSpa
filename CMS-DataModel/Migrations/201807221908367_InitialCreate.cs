namespace CMS_DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CMS_BusinessDayLog",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        StartedOn = c.DateTime(nullable: false),
                        ClosedOn = c.DateTime(),
                        ElapsedTime = c.Double(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CMS_ShiftLog",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        BusinessDaySession = c.String(nullable: false, maxLength: 100),
                        StartedOn = c.DateTime(),
                        ClosedOn = c.DateTime(),
                        StartedStaff = c.String(nullable: false, maxLength: 100),
                        ClosedStaff = c.String(maxLength: 100),
                        CurrentState = c.Boolean(),
                        ElapsedTime = c.Double(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_BusinessDayLog", t => t.BusinessDaySession)
                .Index(t => t.BusinessDaySession);
            
            CreateTable(
                "dbo.CMS_DrawerLog",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        DrawerID = c.String(nullable: false, maxLength: 100),
                        ShiftSession = c.String(nullable: false, maxLength: 100),
                        CurrentState = c.Boolean(),
                        StartedOn = c.DateTime(),
                        ClosedOn = c.DateTime(),
                        StartedStaff = c.String(nullable: false, maxLength: 100),
                        ClosedStaff = c.String(maxLength: 100),
                        ActualCash = c.Double(),
                        StartedCash = c.Double(),
                        PaidIn = c.Double(),
                        PaidOut = c.Double(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Drawer", t => t.DrawerID)
                .ForeignKey("dbo.CMS_ShiftLog", t => t.ShiftSession)
                .Index(t => t.DrawerID)
                .Index(t => t.ShiftSession);
            
            CreateTable(
                "dbo.CMS_Drawer",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 50),
                        IPAddress = c.String(maxLength: 30),
                        Port = c.String(maxLength: 20),
                        KickCode = c.String(maxLength: 10),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CMS_Refund",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        OrderID = c.String(nullable: false, maxLength: 100),
                        TotalRefund = c.Double(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        Description = c.String(maxLength: 200),
                        DrawerID = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Order", t => t.OrderID)
                .ForeignKey("dbo.CMS_Drawer", t => t.DrawerID)
                .Index(t => t.OrderID)
                .Index(t => t.DrawerID);
            
            CreateTable(
                "dbo.CMS_Order",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        ParentID = c.String(maxLength: 100),
                        ReceiptNo = c.String(maxLength: 100),
                        OrderNo = c.String(maxLength: 100),
                        TagNumber = c.String(maxLength: 50),
                        SplitNo = c.Byte(nullable: false),
                        Cover = c.Byte(),
                        TotalBill = c.Double(),
                        TotalDiscount = c.Double(),
                        SubTotal = c.Double(),
                        Tip = c.Double(),
                        Tax = c.Double(),
                        Remaining = c.Double(),
                        ServiceCharged = c.Double(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        CustomerID = c.String(maxLength: 100),
                        DrawerID = c.String(maxLength: 100),
                        RoundingAmount = c.Double(),
                        Cashier = c.String(maxLength: 100),
                        Remark = c.String(),
                        TotalPromotion = c.Double(),
                        ReceiptCreatedDate = c.DateTime(),
                        IsTemp = c.Boolean(nullable: false),
                        DrawerCreated = c.String(maxLength: 100),
                        DeliveryOrderID = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Order", t => t.ParentID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.StoreID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.CMS_OrderDetail",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        OrderID = c.String(nullable: false, maxLength: 100),
                        ParentID = c.String(maxLength: 100),
                        ProductID = c.String(maxLength: 100),
                        DiscountID = c.String(maxLength: 100),
                        Quantity = c.Decimal(precision: 10, scale: 2),
                        Price = c.Double(),
                        Tax = c.Double(),
                        ServiceCharged = c.Double(),
                        QueueNumber = c.Int(),
                        State = c.Byte(),
                        DiscountAmount = c.Double(nullable: false),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        DiscountValue = c.Double(nullable: false),
                        DiscountType = c.Byte(nullable: false),
                        Description = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Discount", t => t.DiscountID)
                .ForeignKey("dbo.CMS_Products", t => t.ProductID)
                .ForeignKey("dbo.CMS_OrderDetail", t => t.ParentID)
                .ForeignKey("dbo.CMS_Order", t => t.OrderID)
                .Index(t => t.OrderID)
                .Index(t => t.ParentID)
                .Index(t => t.ProductID)
                .Index(t => t.DiscountID);
            
            CreateTable(
                "dbo.CMS_Discount",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 500),
                        Value = c.Double(),
                        Type = c.Byte(),
                        IsAllowOpenValue = c.Boolean(),
                        IsActive = c.Boolean(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                        IsApplyTotalBill = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.CMS_Store",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        CompanyID = c.String(maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 256),
                        IndustryID = c.String(maxLength: 100),
                        Description = c.String(storeType: "ntext"),
                        Address = c.String(maxLength: 150, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        Street = c.String(maxLength: 256),
                        City = c.String(maxLength: 256),
                        Country = c.String(maxLength: 50),
                        Zipcode = c.String(maxLength: 50),
                        GSTRegNo = c.String(maxLength: 50),
                        TimeZone = c.String(maxLength: 50),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                        Email = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 50),
                        StoreCode = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CMS_BussinessHour",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        Day = c.Byte(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.CMS_Categories",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 50),
                        TotalProducts = c.Int(),
                        Description = c.String(maxLength: 500),
                        ImageURL = c.String(maxLength: 500),
                        ParentID = c.String(maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        ProductType = c.String(nullable: false, maxLength: 100),
                        IsShowInReservation = c.Boolean(nullable: false),
                        Sequence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Categories", t => t.ParentID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.StoreID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.CMS_Products",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        ParentID = c.String(nullable: false, maxLength: 100),
                        TypeCode = c.String(nullable: false, maxLength: 100),
                        CategoryID = c.String(maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 100),
                        ProductCode = c.String(nullable: false, maxLength: 20),
                        BarCode = c.String(maxLength: 20),
                        Description = c.String(maxLength: 256),
                        PrintOutText = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                        ImageURL = c.String(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Products", t => t.ParentID)
                .ForeignKey("dbo.CMS_Categories", t => t.CategoryID)
                .Index(t => t.ParentID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.CMS_ImagesLink",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100),
                        ImageURL = c.String(maxLength: 100),
                        ProductId = c.String(maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CMS_Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.CMS_ProductDetail",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        ProductID = c.String(nullable: false, maxLength: 100),
                        ParameterName = c.String(maxLength: 100),
                        ParameterValue = c.String(maxLength: 200),
                        Description = c.String(maxLength: 500),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Products", t => t.ProductID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.CMS_ProductOnStore",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(maxLength: 100),
                        ProductID = c.String(nullable: false, maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        Sequence = c.Int(),
                        Description = c.String(maxLength: 256),
                        Cost = c.Double(nullable: false),
                        Unit = c.Int(),
                        Measure = c.String(nullable: false, maxLength: 50),
                        Quantity = c.Decimal(precision: 18, scale: 2),
                        Limit = c.Int(nullable: false),
                        ExtraPrice = c.Double(nullable: false),
                        IsAllowedDiscount = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsCheckedStock = c.Boolean(nullable: false),
                        IsAllowedOpenPrice = c.Boolean(nullable: false),
                        IsPrintedOnCheck = c.Boolean(nullable: false),
                        ExpiredDate = c.DateTime(nullable: false),
                        IsAutoAddToOrder = c.Boolean(nullable: false),
                        IsComingSoon = c.Boolean(nullable: false),
                        IsShowInReservation = c.Boolean(nullable: false),
                        IsRecommend = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.StoreID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.CMS_CustomerOnStore",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        CustomerID = c.String(nullable: false, maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        LastTransaction = c.DateTime(),
                        MemberTierID = c.String(maxLength: 100),
                        Point = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.CMS_EmployeeOnStore",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        RoleID = c.String(nullable: false, maxLength: 100),
                        EmployeeID = c.String(nullable: false, maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Role", t => t.RoleID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.StoreID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.CMS_Role",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.CMS_ModulePermission",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        RoleID = c.String(nullable: false, maxLength: 100),
                        ModuleID = c.String(nullable: false, maxLength: 100),
                        IsView = c.Boolean(),
                        IsAction = c.Boolean(),
                        IsActive = c.Boolean(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Role", t => t.RoleID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.StoreID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.CMS_EmployeeWorking",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        EmployeeID = c.String(nullable: false, maxLength: 100),
                        WorkingTimeID = c.String(nullable: false, maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        StoreID = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Employee", t => t.EmployeeID)
                .ForeignKey("dbo.CMS_WorkingTimes", t => t.WorkingTimeID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.EmployeeID)
                .Index(t => t.WorkingTimeID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.CMS_Employee",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        Name = c.String(maxLength: 50),
                        Email = c.String(maxLength: 100),
                        Password = c.String(),
                        IsActive = c.Boolean(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        Phone = c.String(maxLength: 50),
                        PinCode = c.String(maxLength: 50),
                        Gender = c.Boolean(nullable: false),
                        Marital = c.Boolean(nullable: false),
                        HiredDate = c.DateTime(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Street = c.String(maxLength: 200),
                        City = c.String(maxLength: 200),
                        ZipCode = c.String(maxLength: 20),
                        Country = c.String(maxLength: 200),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CMS_WorkingTimes",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        Store_ID = c.String(maxLength: 100),
                        DayOfWeek = c.Byte(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CMS_ModuleOnStore",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        ModuleID = c.String(nullable: false, maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Module", t => t.ModuleID, cascadeDelete: true)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.StoreID)
                .Index(t => t.ModuleID);
            
            CreateTable(
                "dbo.CMS_Module",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentID = c.String(maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CMS_Price",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        ProductID = c.String(nullable: false, maxLength: 100),
                        SeasonID = c.String(maxLength: 100),
                        DefaultPrice = c.Double(nullable: false),
                        SeasonPrice = c.Double(nullable: false),
                        IsActivated = c.Boolean(nullable: false),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        StoreID = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Season", t => t.SeasonID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.SeasonID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.CMS_Season",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsRepeated = c.Boolean(nullable: false),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.CMS_SeasonOnRepeat",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        SeasonID = c.String(nullable: false, maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        Day = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Season", t => t.SeasonID)
                .Index(t => t.SeasonID);
            
            CreateTable(
                "dbo.CMS_Reservation",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        ReservationNo = c.String(maxLength: 100),
                        CustomerID = c.String(maxLength: 100),
                        TableID = c.String(maxLength: 100),
                        OrderID = c.String(maxLength: 100),
                        BookingMethod = c.String(),
                        ReservationStatus = c.Byte(nullable: false),
                        Cover = c.Byte(nullable: false),
                        Remark = c.String(),
                        Date = c.DateTime(nullable: false),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        Mobile = c.String(maxLength: 50),
                        ChangedReason = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Customer", t => t.CustomerID)
                .ForeignKey("dbo.CMS_Table", t => t.TableID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID, cascadeDelete: true)
                .ForeignKey("dbo.CMS_Order", t => t.OrderID)
                .Index(t => t.StoreID)
                .Index(t => t.CustomerID)
                .Index(t => t.TableID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.CMS_Customer",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        IC = c.String(maxLength: 20),
                        Name = c.String(maxLength: 50),
                        IsActive = c.Boolean(),
                        Phone = c.String(maxLength: 20, unicode: false),
                        Email = c.String(maxLength: 100),
                        Gender = c.Boolean(),
                        Marital = c.Boolean(),
                        JoinedDate = c.DateTime(),
                        BirthDate = c.DateTime(),
                        HomeStreet = c.String(maxLength: 200),
                        HomeCity = c.String(maxLength: 200),
                        HomeZipCode = c.String(maxLength: 50),
                        HomeCountry = c.String(maxLength: 100),
                        OfficeStreet = c.String(maxLength: 200),
                        OfficeCity = c.String(maxLength: 200),
                        OfficeZipCode = c.String(maxLength: 50),
                        OfficeCountry = c.String(maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                        Anniversary = c.DateTime(nullable: false),
                        ValidTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CMS_ReservationDetail",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        ReservationID = c.String(nullable: false, maxLength: 100),
                        ProductID = c.String(nullable: false, maxLength: 100),
                        ProductName = c.String(maxLength: 250),
                        Quantity = c.Int(nullable: false),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        ProductType = c.Byte(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Reservation", t => t.ReservationID, cascadeDelete: true)
                .Index(t => t.ReservationID);
            
            CreateTable(
                "dbo.CMS_Table",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        ZoneID = c.String(nullable: false, maxLength: 100),
                        Name = c.String(maxLength: 50),
                        Cover = c.Int(),
                        ViewMode = c.Byte(),
                        XPoint = c.Double(),
                        YPoint = c.Double(),
                        ZPoint = c.Double(),
                        IsActive = c.Boolean(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        IsAllowReservation = c.Boolean(nullable: false),
                        IsTemp = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Zone", t => t.ZoneID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.StoreID)
                .Index(t => t.ZoneID);
            
            CreateTable(
                "dbo.CMS_Zone",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        Name = c.String(maxLength: 50),
                        Description = c.String(unicode: false, storeType: "text"),
                        Width = c.Int(),
                        Height = c.Int(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CMS_StoreSetting",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        StoreID = c.String(nullable: false, maxLength: 100),
                        SettingID = c.String(nullable: false, maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_GeneralSetting", t => t.SettingID)
                .ForeignKey("dbo.CMS_Store", t => t.StoreID)
                .Index(t => t.StoreID)
                .Index(t => t.SettingID);
            
            CreateTable(
                "dbo.CMS_GeneralSetting",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        Name = c.String(maxLength: 50),
                        DisplayName = c.String(maxLength: 70),
                        Value = c.String(),
                        ObjectType = c.String(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        Code = c.Byte(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CMS_RefundDetail",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        RefundID = c.String(nullable: false, maxLength: 100),
                        DetailID = c.String(nullable: false, maxLength: 100),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_OrderDetail", t => t.DetailID)
                .ForeignKey("dbo.CMS_Refund", t => t.RefundID)
                .Index(t => t.RefundID)
                .Index(t => t.DetailID);
            
            CreateTable(
                "dbo.CMS_OrderPaid",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        OrderID = c.String(maxLength: 100),
                        PaymentMethodID = c.String(maxLength: 100),
                        Amount = c.Double(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Order", t => t.OrderID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.CMS_TableOnAssigned",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 100),
                        TableID = c.String(nullable: false, maxLength: 100),
                        OrderID = c.String(maxLength: 100),
                        OrderState = c.Byte(),
                        TableState = c.Byte(),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                        IsCall = c.Boolean(nullable: false),
                        IsGuestCheck = c.Boolean(nullable: false),
                        DateCall = c.DateTime(nullable: false),
                        IsCallForBill = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CMS_Order", t => t.OrderID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.CMS_Companies",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(nullable: false, maxLength: 250),
                        Address = c.String(nullable: false, maxLength: 250),
                        LinkBlog = c.String(maxLength: 250, unicode: false),
                        LinkFB = c.String(maxLength: 250, unicode: false),
                        LinkTwiter = c.String(maxLength: 250, unicode: false),
                        LinkInstagram = c.String(maxLength: 250, unicode: false),
                        ImageURL = c.String(maxLength: 60, unicode: false),
                        BusinessHour = c.String(maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 60, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 60, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CMS_News",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100),
                        Title = c.String(nullable: false, maxLength: 150),
                        Short_Description = c.String(nullable: false, maxLength: 500),
                        Description = c.String(storeType: "ntext"),
                        ImageURL = c.String(maxLength: 60, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        Status = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedUser = c.String(nullable: false, maxLength: 100),
                        ModifiedUser = c.String(nullable: false, maxLength: 100),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CMS_ShiftLog", "BusinessDaySession", "dbo.CMS_BusinessDayLog");
            DropForeignKey("dbo.CMS_DrawerLog", "ShiftSession", "dbo.CMS_ShiftLog");
            DropForeignKey("dbo.CMS_Refund", "DrawerID", "dbo.CMS_Drawer");
            DropForeignKey("dbo.CMS_RefundDetail", "RefundID", "dbo.CMS_Refund");
            DropForeignKey("dbo.CMS_TableOnAssigned", "OrderID", "dbo.CMS_Order");
            DropForeignKey("dbo.CMS_Reservation", "OrderID", "dbo.CMS_Order");
            DropForeignKey("dbo.CMS_Refund", "OrderID", "dbo.CMS_Order");
            DropForeignKey("dbo.CMS_OrderPaid", "OrderID", "dbo.CMS_Order");
            DropForeignKey("dbo.CMS_OrderDetail", "OrderID", "dbo.CMS_Order");
            DropForeignKey("dbo.CMS_RefundDetail", "DetailID", "dbo.CMS_OrderDetail");
            DropForeignKey("dbo.CMS_OrderDetail", "ParentID", "dbo.CMS_OrderDetail");
            DropForeignKey("dbo.CMS_Table", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_StoreSetting", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_StoreSetting", "SettingID", "dbo.CMS_GeneralSetting");
            DropForeignKey("dbo.CMS_Season", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_Role", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_Reservation", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_Table", "ZoneID", "dbo.CMS_Zone");
            DropForeignKey("dbo.CMS_Reservation", "TableID", "dbo.CMS_Table");
            DropForeignKey("dbo.CMS_ReservationDetail", "ReservationID", "dbo.CMS_Reservation");
            DropForeignKey("dbo.CMS_Reservation", "CustomerID", "dbo.CMS_Customer");
            DropForeignKey("dbo.CMS_ProductOnStore", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_Price", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_SeasonOnRepeat", "SeasonID", "dbo.CMS_Season");
            DropForeignKey("dbo.CMS_Price", "SeasonID", "dbo.CMS_Season");
            DropForeignKey("dbo.CMS_Order", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_ModulePermission", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_ModuleOnStore", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_ModuleOnStore", "ModuleID", "dbo.CMS_Module");
            DropForeignKey("dbo.CMS_EmployeeWorking", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_EmployeeWorking", "WorkingTimeID", "dbo.CMS_WorkingTimes");
            DropForeignKey("dbo.CMS_EmployeeWorking", "EmployeeID", "dbo.CMS_Employee");
            DropForeignKey("dbo.CMS_EmployeeOnStore", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_ModulePermission", "RoleID", "dbo.CMS_Role");
            DropForeignKey("dbo.CMS_EmployeeOnStore", "RoleID", "dbo.CMS_Role");
            DropForeignKey("dbo.CMS_Discount", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_CustomerOnStore", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_Categories", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_Products", "CategoryID", "dbo.CMS_Categories");
            DropForeignKey("dbo.CMS_Products", "ParentID", "dbo.CMS_Products");
            DropForeignKey("dbo.CMS_ProductOnStore", "ProductID", "dbo.CMS_Products");
            DropForeignKey("dbo.CMS_ProductDetail", "ProductID", "dbo.CMS_Products");
            DropForeignKey("dbo.CMS_OrderDetail", "ProductID", "dbo.CMS_Products");
            DropForeignKey("dbo.CMS_ImagesLink", "ProductId", "dbo.CMS_Products");
            DropForeignKey("dbo.CMS_Categories", "ParentID", "dbo.CMS_Categories");
            DropForeignKey("dbo.CMS_BussinessHour", "StoreID", "dbo.CMS_Store");
            DropForeignKey("dbo.CMS_OrderDetail", "DiscountID", "dbo.CMS_Discount");
            DropForeignKey("dbo.CMS_Order", "ParentID", "dbo.CMS_Order");
            DropForeignKey("dbo.CMS_DrawerLog", "DrawerID", "dbo.CMS_Drawer");
            DropIndex("dbo.CMS_TableOnAssigned", new[] { "OrderID" });
            DropIndex("dbo.CMS_OrderPaid", new[] { "OrderID" });
            DropIndex("dbo.CMS_RefundDetail", new[] { "DetailID" });
            DropIndex("dbo.CMS_RefundDetail", new[] { "RefundID" });
            DropIndex("dbo.CMS_StoreSetting", new[] { "SettingID" });
            DropIndex("dbo.CMS_StoreSetting", new[] { "StoreID" });
            DropIndex("dbo.CMS_Table", new[] { "ZoneID" });
            DropIndex("dbo.CMS_Table", new[] { "StoreID" });
            DropIndex("dbo.CMS_ReservationDetail", new[] { "ReservationID" });
            DropIndex("dbo.CMS_Reservation", new[] { "OrderID" });
            DropIndex("dbo.CMS_Reservation", new[] { "TableID" });
            DropIndex("dbo.CMS_Reservation", new[] { "CustomerID" });
            DropIndex("dbo.CMS_Reservation", new[] { "StoreID" });
            DropIndex("dbo.CMS_SeasonOnRepeat", new[] { "SeasonID" });
            DropIndex("dbo.CMS_Season", new[] { "StoreID" });
            DropIndex("dbo.CMS_Price", new[] { "StoreID" });
            DropIndex("dbo.CMS_Price", new[] { "SeasonID" });
            DropIndex("dbo.CMS_ModuleOnStore", new[] { "ModuleID" });
            DropIndex("dbo.CMS_ModuleOnStore", new[] { "StoreID" });
            DropIndex("dbo.CMS_EmployeeWorking", new[] { "StoreID" });
            DropIndex("dbo.CMS_EmployeeWorking", new[] { "WorkingTimeID" });
            DropIndex("dbo.CMS_EmployeeWorking", new[] { "EmployeeID" });
            DropIndex("dbo.CMS_ModulePermission", new[] { "RoleID" });
            DropIndex("dbo.CMS_ModulePermission", new[] { "StoreID" });
            DropIndex("dbo.CMS_Role", new[] { "StoreID" });
            DropIndex("dbo.CMS_EmployeeOnStore", new[] { "RoleID" });
            DropIndex("dbo.CMS_EmployeeOnStore", new[] { "StoreID" });
            DropIndex("dbo.CMS_CustomerOnStore", new[] { "StoreID" });
            DropIndex("dbo.CMS_ProductOnStore", new[] { "ProductID" });
            DropIndex("dbo.CMS_ProductOnStore", new[] { "StoreID" });
            DropIndex("dbo.CMS_ProductDetail", new[] { "ProductID" });
            DropIndex("dbo.CMS_ImagesLink", new[] { "ProductId" });
            DropIndex("dbo.CMS_Products", new[] { "CategoryID" });
            DropIndex("dbo.CMS_Products", new[] { "ParentID" });
            DropIndex("dbo.CMS_Categories", new[] { "ParentID" });
            DropIndex("dbo.CMS_Categories", new[] { "StoreID" });
            DropIndex("dbo.CMS_BussinessHour", new[] { "StoreID" });
            DropIndex("dbo.CMS_Discount", new[] { "StoreID" });
            DropIndex("dbo.CMS_OrderDetail", new[] { "DiscountID" });
            DropIndex("dbo.CMS_OrderDetail", new[] { "ProductID" });
            DropIndex("dbo.CMS_OrderDetail", new[] { "ParentID" });
            DropIndex("dbo.CMS_OrderDetail", new[] { "OrderID" });
            DropIndex("dbo.CMS_Order", new[] { "ParentID" });
            DropIndex("dbo.CMS_Order", new[] { "StoreID" });
            DropIndex("dbo.CMS_Refund", new[] { "DrawerID" });
            DropIndex("dbo.CMS_Refund", new[] { "OrderID" });
            DropIndex("dbo.CMS_DrawerLog", new[] { "ShiftSession" });
            DropIndex("dbo.CMS_DrawerLog", new[] { "DrawerID" });
            DropIndex("dbo.CMS_ShiftLog", new[] { "BusinessDaySession" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.CMS_News");
            DropTable("dbo.CMS_Companies");
            DropTable("dbo.CMS_TableOnAssigned");
            DropTable("dbo.CMS_OrderPaid");
            DropTable("dbo.CMS_RefundDetail");
            DropTable("dbo.CMS_GeneralSetting");
            DropTable("dbo.CMS_StoreSetting");
            DropTable("dbo.CMS_Zone");
            DropTable("dbo.CMS_Table");
            DropTable("dbo.CMS_ReservationDetail");
            DropTable("dbo.CMS_Customer");
            DropTable("dbo.CMS_Reservation");
            DropTable("dbo.CMS_SeasonOnRepeat");
            DropTable("dbo.CMS_Season");
            DropTable("dbo.CMS_Price");
            DropTable("dbo.CMS_Module");
            DropTable("dbo.CMS_ModuleOnStore");
            DropTable("dbo.CMS_WorkingTimes");
            DropTable("dbo.CMS_Employee");
            DropTable("dbo.CMS_EmployeeWorking");
            DropTable("dbo.CMS_ModulePermission");
            DropTable("dbo.CMS_Role");
            DropTable("dbo.CMS_EmployeeOnStore");
            DropTable("dbo.CMS_CustomerOnStore");
            DropTable("dbo.CMS_ProductOnStore");
            DropTable("dbo.CMS_ProductDetail");
            DropTable("dbo.CMS_ImagesLink");
            DropTable("dbo.CMS_Products");
            DropTable("dbo.CMS_Categories");
            DropTable("dbo.CMS_BussinessHour");
            DropTable("dbo.CMS_Store");
            DropTable("dbo.CMS_Discount");
            DropTable("dbo.CMS_OrderDetail");
            DropTable("dbo.CMS_Order");
            DropTable("dbo.CMS_Refund");
            DropTable("dbo.CMS_Drawer");
            DropTable("dbo.CMS_DrawerLog");
            DropTable("dbo.CMS_ShiftLog");
            DropTable("dbo.CMS_BusinessDayLog");
        }
    }
}
