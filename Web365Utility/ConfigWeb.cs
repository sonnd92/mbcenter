using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Web365Utility
{
    public static class ConfigWeb
    {
        public static readonly string TempPath = ConfigurationManager.AppSettings["TempUpload"];

        public static readonly string ImageThumpPath = ConfigurationManager.AppSettings["UploadImageThumb"];

        public static readonly string ImagePath = ConfigurationManager.AppSettings["UploadImage"];

        public static readonly string FilePath = ConfigurationManager.AppSettings["UploadFile"];

        public static readonly bool UseCache = Convert.ToBoolean(ConfigurationManager.AppSettings["UseCache"]);

        public static readonly bool UseOutputCache = Convert.ToBoolean(ConfigurationManager.AppSettings["UseOutputCache"]);

        public static readonly int MinOnline = Convert.ToInt32(ConfigurationManager.AppSettings["MinOnline"]);

        public static readonly int PageSizeNews = Convert.ToInt32(ConfigurationManager.AppSettings["PageSizeNews"]);
        public static readonly int PageSizePromotion = Convert.ToInt32(ConfigurationManager.AppSettings["PageSizePromotion"]);
        public static readonly int PageSizeProducts = Convert.ToInt32(ConfigurationManager.AppSettings["PageSizeProducts"]);
        public static readonly int PageSizeOtherProducts = Convert.ToInt32(ConfigurationManager.AppSettings["PageSizeOtherProducts"]);
        public static readonly int PageSizeNewestNews = Convert.ToInt32(ConfigurationManager.AppSettings["PageSizeNewestNews"]);
        public static readonly int PageSizeExpert = Convert.ToInt32(ConfigurationManager.AppSettings["PageSizeExpert"]);
        public static readonly int QuantityImageInLibrary = Convert.ToInt32(ConfigurationManager.AppSettings["QuantityImageInLibrary"]);

        public static readonly int OtherNewsQuantity = Convert.ToInt32(ConfigurationManager.AppSettings["OtherNewsQuantity"]);

        public static readonly string SpecialArticle = ConfigurationManager.AppSettings["SpecialArticle"];

        public static readonly string OtherArticle = ConfigurationManager.AppSettings["OtherArticle"];

        public static readonly string Domain = ConfigurationManager.AppSettings["Domain"];
        public static readonly string BackendDomain = ConfigurationManager.AppSettings["BackendDomain"];

        public static readonly bool EnableOptimizations = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableOptimizations"]);
    }
}
