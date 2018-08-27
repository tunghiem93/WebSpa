using CMS_DataModel.Models;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace CMS_DataModel.Models
{
    public class ContextHandler : CreateDatabaseIfNotExists<CMS_Context>
    {

        protected override void Seed(CMS_Context context)
        {
            base.Seed(context);
            CreateDefaultData(context);
        }

        public override void InitializeDatabase(CMS_Context context)
        {
            try
            {
                if (context.Database.Exists()) return;
                base.InitializeDatabase(context);
            }
            catch (SqlException ex) { throw new ArgumentException(ex.Message, ex); }
        }

        private void CreateDefaultData(CMS_Context context)
        {
            try
            {
                //var isInsertDefault = ConfigurationManager.AppSettings["InsertDefaultDataWhenGenerateNewDB"];
                //if (!string.IsNullOrEmpty(isInsertDefault) && isInsertDefault.ToString().ToLower() == "true")
                //{
                //    var hier = HierarchyData.HierarchyConfigs;

                //    /*-- Insert Modules --*/
                //    #region -- Modules --
                //    if (hier.Modules != null && hier.Modules.Count > 0)
                //    {
                //        foreach (ModuleConfigElement parM in hier.Modules)
                //        {
                //            var parEnt = new Module()
                //            {
                //                ID = Guid.NewGuid().ToString(),
                //                Name = parM.Display,
                //                Code = parM.Code,
                //                CreatedUser = "Administrator",
                //                ModifiedUser = "Administrator",
                //                ParentID = String.Empty,
                //                CreatedDate = DateTime.UtcNow,
                //                LastModified = DateTime.UtcNow,
                //                Status = parM.Status
                //            };
                //            context.Modules.Add(parEnt);
                //            if (parM.Modules != null && parM.Modules.Count > 0)
                //            {
                //                foreach (ModuleConfigElement chlM in parM.Modules)
                //                {
                //                    var chlEnt = new Module()
                //                    {
                //                        ID = Guid.NewGuid().ToString(),
                //                        Name = chlM.Display,
                //                        Code = chlM.Code,
                //                        CreatedUser = "Administrator",
                //                        ModifiedUser = "Administrator",
                //                        ParentID = parEnt.ID,
                //                        CreatedDate = DateTime.UtcNow,
                //                        LastModified = DateTime.UtcNow,
                //                        Status = parM.Status
                //                    };
                //                    context.Modules.Add(chlEnt);
                //                }
                //            }
                //        }
                //    }
                //    #endregion

                //    /*-- Insert GeneralSettings --*/
                //    #region -- GeneralSettings -- 
                //    if (hier.Settings != null && hier.Settings.Count > 0)
                //    {
                //        foreach (SettingConfigElement set in hier.Settings)
                //        {
                //            var parEnt = new GeneralSetting()
                //            {
                //                ID = Guid.NewGuid().ToString(),
                //                Name = set.Name,
                //                DisplayName = set.DisplayName,
                //                Code = set.Code,
                //                CreatedDate = DateTime.UtcNow,
                //                LastModified = DateTime.UtcNow,
                //                CreatedUser = "Administrator",
                //                ModifiedUser = "Administrator",
                //                ObjectType = set.ObjectType,
                //                Value = set.Value,
                //                Status = set.Status
                //            };
                //            context.GeneralSettings.Add(parEnt);
                //        }
                //    }
                //    #endregion

                //    /*-- Insert ProductType --*/
                //    #region -- ProductType --
                //    if (hier.ProductTypes != null && hier.ProductTypes.Count > 0)
                //    {
                //        foreach (ProductTypeConfigElement pro in hier.ProductTypes)
                //        {
                //            var parEnt = new ProductType()
                //            {
                //                ID = Guid.NewGuid().ToString(),
                //                Name = pro.DisplayName,
                //                Code = pro.Code,
                //                CreatedDate = DateTime.UtcNow,
                //                LastModified = DateTime.UtcNow,
                //                CreatedUser = "Administrator",
                //                ModifiedUser = "Administrator",
                //                Status = pro.Status
                //            };
                //            context.ProductTypes.Add(parEnt);
                //        }
                //    }
                //    #endregion

                //    /*-- Insert PrinterType --*/
                //    #region -- PrinterType --
                //    if (hier.PrinterTypes != null && hier.PrinterTypes.Count > 0)
                //    {
                //        foreach (PrinterTypeConfigElement priT in hier.PrinterTypes)
                //        {
                //            var parEnt = new PrinterType()
                //            {
                //                ID = Guid.NewGuid().ToString(),
                //                Name = priT.Display,
                //                Code = priT.Code,
                //                CreatedDate = DateTime.UtcNow,
                //                LastModified = DateTime.UtcNow,
                //                CreatedUser = "Administrator",
                //                ModifiedUser = "Administrator",
                //                Status = priT.Status
                //            };
                //            context.PrinterTypes.Add(parEnt);
                //        }
                //    }
                //    #endregion

                //    /*-- Insert PaymentMethod --*/
                //    #region -- PaymentMethod --
                //    if (hier.PaymentMethods != null && hier.PaymentMethods.Count > 0)
                //    {
                //        foreach (PaymentMethodConfigElement payM in hier.PaymentMethods)
                //        {
                //            var parEnt = new PaymentMethod()
                //            {
                //                ID = Guid.NewGuid().ToString(),
                //                Name = payM.DisplayName,
                //                Code = payM.Code,
                //                CreatedDate = DateTime.UtcNow,
                //                LastModified = DateTime.UtcNow,
                //                CreatedUser = "Administrator",
                //                ModifiedUser = "Administrator",
                //                Status = payM.Status,
                //                SerialNo = payM.SerialNo,
                //                FixedPayAmount = payM.FixedPayAmount,
                //                IsAllowedKickDrawer = payM.IsAllowedKickDrawer,
                //                IsGiveBackChange = payM.IsGiveBackChange,
                //                IsIncludedOnSale = payM.IsIncludedOnSale,
                //                IsHasConfirmCode = payM.IsHasConfirmCode,
                //                IsStandardMethod = true,
                //            };
                //            context.PaymentMethods.Add(parEnt);
                //            if (payM.PaymentMethods != null && payM.PaymentMethods.Count > 0)
                //            {
                //                foreach (PaymentMethodConfigElement child in payM.PaymentMethods)
                //                {
                //                    var childEnt = new PaymentMethod()
                //                    {
                //                        ID = Guid.NewGuid().ToString(),
                //                        Name = child.DisplayName,
                //                        Code = child.Code,
                //                        CreatedDate = DateTime.UtcNow,
                //                        LastModified = DateTime.UtcNow,
                //                        CreatedUser = "Administrator",
                //                        ModifiedUser = "Administrator",
                //                        Status = child.Status,
                //                        SerialNo = child.SerialNo,
                //                        FixedPayAmount = child.FixedPayAmount,
                //                        IsAllowedKickDrawer = child.IsAllowedKickDrawer,
                //                        IsGiveBackChange = child.IsGiveBackChange,
                //                        IsIncludedOnSale = child.IsIncludedOnSale,
                //                        IsHasConfirmCode = child.IsHasConfirmCode,
                //                        ParentID = parEnt.ID
                //                    };
                //                    context.PaymentMethods.Add(childEnt);
                //                }
                //            }
                //        }
                //    }
                //    #endregion

                //    context.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private static bool FindError(SqlErrorCollection errors, string error)
        {
            return errors.Cast<SqlError>().ToList().Exists(x => x.ToString().ToLower().Contains(error));
        }
    }
}
