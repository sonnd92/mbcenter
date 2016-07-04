using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web365Utility
{
    public static class StaticEnum
    {
        /// <summary>
        /// Enum about type file when upload of download
        /// </summary>
        public enum FileType
        {
            //type file jpg, jpeg, png, gif...
            Image = 0,
            //thumb type file jpg, jpeg, png, gif...
            ImageThumb = 1,
            //type file doc, docx, xls, xlsx, pdf...
            Document = 2,
            //type file mp3, mp4, avi, mkv...
            Multimedia = 3,
            //type file rar, zip, dll, exe...
            File = 4
        }

        public enum TypeCache : int
        {
            Caching = 1,
            Redis = 2,
            Memcached = 3
        }

        public enum BookingType : int
        {
            ForMom = 1,
            ForBaby = 2
        }

        public enum NewsType : int
        {
            News = 1,
            FeedBack = 2,
            Introduce = 3,
            CareProcedure = 4,
            Expert = 5,
            Promotion = 6
        }

        public enum Advertise
        {
            HomeBannerSlide = 1,
            HomeAdvertise = 2,
            HomeLibrary = 3,
        }

        public enum ImageType
        {
            Library = 5,
            Service = 4,
            Customer = 3,
            BannerSlide = 2,
            News = 1
        }
    }
}
