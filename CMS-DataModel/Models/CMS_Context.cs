namespace CMS_DataModel.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CMS_Context : DbContext
    {
        public CMS_Context()
            : base("name=CMS_Context")
        {
        }

        public virtual DbSet<CMS_BusinessDayLog> CMS_BusinessDayLog { get; set; }
        public virtual DbSet<CMS_BussinessHour> CMS_BussinessHour { get; set; }
        public virtual DbSet<CMS_Categories> CMS_Categories { get; set; }
        public virtual DbSet<CMS_Companies> CMS_Companies { get; set; }
        public virtual DbSet<CMS_Customer> CMS_Customer { get; set; }
        public virtual DbSet<CMS_CustomerOnStore> CMS_CustomerOnStore { get; set; }
        public virtual DbSet<CMS_Discount> CMS_Discount { get; set; }
        public virtual DbSet<CMS_Drawer> CMS_Drawer { get; set; }
        public virtual DbSet<CMS_DrawerLog> CMS_DrawerLog { get; set; }
        public virtual DbSet<CMS_Employee> CMS_Employee { get; set; }
        public virtual DbSet<CMS_EmployeeOnStore> CMS_EmployeeOnStore { get; set; }
        public virtual DbSet<CMS_EmployeeWorking> CMS_EmployeeWorking { get; set; }
        public virtual DbSet<CMS_GeneralSetting> CMS_GeneralSetting { get; set; }
        public virtual DbSet<CMS_ImagesLink> CMS_ImagesLink { get; set; }
        public virtual DbSet<CMS_Module> CMS_Module { get; set; }
        public virtual DbSet<CMS_ModuleOnStore> CMS_ModuleOnStore { get; set; }
        public virtual DbSet<CMS_ModulePermission> CMS_ModulePermission { get; set; }
        public virtual DbSet<CMS_News> CMS_News { get; set; }
        public virtual DbSet<CMS_Order> CMS_Order { get; set; }
        public virtual DbSet<CMS_OrderDetail> CMS_OrderDetail { get; set; }
        public virtual DbSet<CMS_OrderPaid> CMS_OrderPaid { get; set; }
        public virtual DbSet<CMS_Price> CMS_Price { get; set; }
        public virtual DbSet<CMS_ProductDetail> CMS_ProductDetail { get; set; }
        public virtual DbSet<CMS_ProductOnStore> CMS_ProductOnStore { get; set; }
        public virtual DbSet<CMS_Products> CMS_Products { get; set; }
        public virtual DbSet<CMS_Refund> CMS_Refund { get; set; }
        public virtual DbSet<CMS_RefundDetail> CMS_RefundDetail { get; set; }
        public virtual DbSet<CMS_Reservation> CMS_Reservation { get; set; }
        public virtual DbSet<CMS_ReservationDetail> CMS_ReservationDetail { get; set; }
        public virtual DbSet<CMS_Role> CMS_Role { get; set; }
        public virtual DbSet<CMS_Season> CMS_Season { get; set; }
        public virtual DbSet<CMS_SeasonOnRepeat> CMS_SeasonOnRepeat { get; set; }
        public virtual DbSet<CMS_ShiftLog> CMS_ShiftLog { get; set; }
        public virtual DbSet<CMS_Store> CMS_Store { get; set; }
        public virtual DbSet<CMS_StoreSetting> CMS_StoreSetting { get; set; }
        public virtual DbSet<CMS_Table> CMS_Table { get; set; }
        public virtual DbSet<CMS_TableOnAssigned> CMS_TableOnAssigned { get; set; }
        public virtual DbSet<CMS_WorkingTimes> CMS_WorkingTimes { get; set; }
        public virtual DbSet<CMS_Zone> CMS_Zone { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CMS_BusinessDayLog>()
                .HasMany(e => e.CMS_ShiftLog)
                .WithRequired(e => e.CMS_BusinessDayLog)
                .HasForeignKey(e => e.BusinessDaySession)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Categories>()
                .HasMany(e => e.CMS_Categories1)
                .WithOptional(e => e.CMS_Categories2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<CMS_Categories>()
                .HasMany(e => e.CMS_Products)
                .WithOptional(e => e.CMS_Categories)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<CMS_Companies>()
                .Property(e => e.LinkBlog)
                .IsUnicode(false);

            modelBuilder.Entity<CMS_Companies>()
                .Property(e => e.LinkFB)
                .IsUnicode(false);

            modelBuilder.Entity<CMS_Companies>()
                .Property(e => e.LinkTwiter)
                .IsUnicode(false);

            modelBuilder.Entity<CMS_Companies>()
                .Property(e => e.LinkInstagram)
                .IsUnicode(false);

            modelBuilder.Entity<CMS_Companies>()
                .Property(e => e.ImageURL)
                .IsUnicode(false);

            modelBuilder.Entity<CMS_Companies>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CMS_Companies>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CMS_Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<CMS_Customer>()
                .HasMany(e => e.CMS_Reservation)
                .WithOptional(e => e.CMS_Customer)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<CMS_Discount>()
                .HasMany(e => e.CMS_OrderDetail)
                .WithOptional(e => e.CMS_Discount)
                .HasForeignKey(e => e.DiscountID);

            modelBuilder.Entity<CMS_Drawer>()
                .HasMany(e => e.CMS_DrawerLog)
                .WithRequired(e => e.CMS_Drawer)
                .HasForeignKey(e => e.DrawerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Drawer>()
                .HasMany(e => e.CMS_Refund)
                .WithOptional(e => e.CMS_Drawer)
                .HasForeignKey(e => e.DrawerID);

            modelBuilder.Entity<CMS_Employee>()
                .HasMany(e => e.CMS_EmployeeWorking)
                .WithRequired(e => e.CMS_Employee)
                .HasForeignKey(e => e.EmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_GeneralSetting>()
                .HasMany(e => e.CMS_StoreSetting)
                .WithRequired(e => e.CMS_GeneralSetting)
                .HasForeignKey(e => e.SettingID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Module>()
                .HasMany(e => e.CMS_ModuleOnStore)
                .WithRequired(e => e.CMS_Module)
                .HasForeignKey(e => e.ModuleID);

            modelBuilder.Entity<CMS_News>()
                .Property(e => e.ImageURL)
                .IsUnicode(false);

            modelBuilder.Entity<CMS_Order>()
                .HasMany(e => e.CMS_Order1)
                .WithOptional(e => e.CMS_Order2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<CMS_Order>()
                .HasMany(e => e.CMS_OrderDetail)
                .WithRequired(e => e.CMS_Order)
                .HasForeignKey(e => e.OrderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Order>()
                .HasMany(e => e.CMS_OrderPaid)
                .WithOptional(e => e.CMS_Order)
                .HasForeignKey(e => e.OrderID);

            modelBuilder.Entity<CMS_Order>()
                .HasMany(e => e.CMS_Refund)
                .WithRequired(e => e.CMS_Order)
                .HasForeignKey(e => e.OrderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Order>()
                .HasMany(e => e.CMS_Reservation)
                .WithOptional(e => e.CMS_Order)
                .HasForeignKey(e => e.OrderID);

            modelBuilder.Entity<CMS_Order>()
                .HasMany(e => e.CMS_TableOnAssigned)
                .WithOptional(e => e.CMS_Order)
                .HasForeignKey(e => e.OrderID);

            modelBuilder.Entity<CMS_OrderDetail>()
                .Property(e => e.Quantity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CMS_OrderDetail>()
                .HasMany(e => e.CMS_OrderDetail1)
                .WithOptional(e => e.CMS_OrderDetail2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<CMS_OrderDetail>()
                .HasMany(e => e.CMS_RefundDetail)
                .WithRequired(e => e.CMS_OrderDetail)
                .HasForeignKey(e => e.DetailID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Products>()
                .HasMany(e => e.CMS_ImagesLink)
                .WithOptional(e => e.CMS_Products)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<CMS_Products>()
                .HasMany(e => e.CMS_OrderDetail)
                .WithOptional(e => e.CMS_Products)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<CMS_Products>()
                .HasMany(e => e.CMS_ProductDetail)
                .WithRequired(e => e.CMS_Products)
                .HasForeignKey(e => e.ProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Products>()
                .HasMany(e => e.CMS_ProductOnStore)
                .WithRequired(e => e.CMS_Products)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<CMS_Products>()
                .HasMany(e => e.CMS_Products1)
                .WithRequired(e => e.CMS_Products2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<CMS_Refund>()
                .HasMany(e => e.CMS_RefundDetail)
                .WithRequired(e => e.CMS_Refund)
                .HasForeignKey(e => e.RefundID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Reservation>()
                .HasMany(e => e.CMS_ReservationDetail)
                .WithRequired(e => e.CMS_Reservation)
                .HasForeignKey(e => e.ReservationID);

            modelBuilder.Entity<CMS_Role>()
                .HasMany(e => e.CMS_EmployeeOnStore)
                .WithRequired(e => e.CMS_Role)
                .HasForeignKey(e => e.RoleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Role>()
                .HasMany(e => e.CMS_ModulePermission)
                .WithRequired(e => e.CMS_Role)
                .HasForeignKey(e => e.RoleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Season>()
                .HasMany(e => e.CMS_Price)
                .WithOptional(e => e.CMS_Season)
                .HasForeignKey(e => e.SeasonID);

            modelBuilder.Entity<CMS_Season>()
                .HasMany(e => e.CMS_SeasonOnRepeat)
                .WithRequired(e => e.CMS_Season)
                .HasForeignKey(e => e.SeasonID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_ShiftLog>()
                .HasMany(e => e.CMS_DrawerLog)
                .WithRequired(e => e.CMS_ShiftLog)
                .HasForeignKey(e => e.ShiftSession)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Store>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_BussinessHour)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_Categories)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_CustomerOnStore)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_Discount)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_EmployeeOnStore)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_EmployeeWorking)
                .WithOptional(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_ModuleOnStore)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_ModulePermission)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_Order)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_Price)
                .WithOptional(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_ProductOnStore)
                .WithOptional(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_Reservation)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_Role)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_Season)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_StoreSetting)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Store>()
                .HasMany(e => e.CMS_Table)
                .WithRequired(e => e.CMS_Store)
                .HasForeignKey(e => e.StoreID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Table>()
                .HasMany(e => e.CMS_Reservation)
                .WithOptional(e => e.CMS_Table)
                .HasForeignKey(e => e.TableID);

            modelBuilder.Entity<CMS_WorkingTimes>()
                .HasMany(e => e.CMS_EmployeeWorking)
                .WithRequired(e => e.CMS_WorkingTimes)
                .HasForeignKey(e => e.WorkingTimeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CMS_Zone>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<CMS_Zone>()
                .HasMany(e => e.CMS_Table)
                .WithRequired(e => e.CMS_Zone)
                .HasForeignKey(e => e.ZoneID)
                .WillCascadeOnDelete(false);
        }
    }
}
