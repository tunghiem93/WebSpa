using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Common
{
    public static class Commons
    {
        public const string Image100_50 = "http://placehold.it/100x50";
        public const string Image200_100 = "http://placehold.it/200x100";
        public const string Image500_500 = "http://placehold.it/500x500";
        public const string Image600_450 = "http://placehold.it/600x450";
        public const string Image920_535 = "http://placehold.it/920x535";

        public static int WidthProduct = Convert.ToInt16(ConfigurationManager.AppSettings["WidthProduct"]);
        public static int HeightProduct = Convert.ToInt16(ConfigurationManager.AppSettings["HeightProduct"]);
        public static int WidthCate = Convert.ToInt16(ConfigurationManager.AppSettings["WidthCate"]);
        public static int HeightCate = Convert.ToInt16(ConfigurationManager.AppSettings["HeightCate"]);

        public static int WidthEmp = Convert.ToInt16(ConfigurationManager.AppSettings["WidthEmp"]);
        public static int HeightEmp = Convert.ToInt16(ConfigurationManager.AppSettings["HeightEmp"]);

        public static int WidthImageNews = Convert.ToInt16(ConfigurationManager.AppSettings["WidthImageNews"]);
        public static int HeightImageNews = Convert.ToInt16(ConfigurationManager.AppSettings["HeightImageNews"]);
        public static int WidthImageSilder = Convert.ToInt16(ConfigurationManager.AppSettings["WidthImageSilder"]);
        public static int HeightImageSilder = Convert.ToInt16(ConfigurationManager.AppSettings["HeightImageSilder"]);
        public static string Phone1 = ConfigurationManager.AppSettings["Phone1"];
        public static string Phone2 = ConfigurationManager.AppSettings["Phone2"];
        public static string Email1 = ConfigurationManager.AppSettings["Email1"];
        public static string Email2 = ConfigurationManager.AppSettings["Email2"];
        public static string AddressCompany = ConfigurationManager.AppSettings["AddressCompnay"];
        public static string CompanyTitle = ConfigurationManager.AppSettings["CompanyTitle"];
        public static string HostImage = ConfigurationManager.AppSettings["HostImage"];
        public static string _PublicImages = string.IsNullOrEmpty(ConfigurationManager.AppSettings["PublicImages"]) ? "" : ConfigurationManager.AppSettings["PublicImages"];
        public static string _ImageDefault = _PublicImages + "Employees/team1.jpg";
        public static string _ImageProductDefault = _PublicImages + "Products/Product1.jpg";

        public static DateTime MinDate = new DateTime(1900, 01, 01, 00, 00, 00, DateTimeKind.Unspecified);
        public static DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, DateTimeKind.Unspecified);

        public const string PasswordChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        #region Enum
        public enum EBlogType
        {
            Image = 0,
            Iframe = 1,
            Video = 2,
            Audio = 3,
        }

        public enum EStatus
        {
            Actived = 1,
            Deleted = 9,
        }

        public enum EProductType
        {
            Service = 1,
            Product = 2,
            Procudure = 3,
        }

        public enum EValueType
        {
            Percent = 0,
            Currency = 1,
        }
        #endregion
    }
}
