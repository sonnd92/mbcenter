using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Business.Front_End.IRepository;
using Web365Utility;

namespace Web365.Controllers
{
    public class LayoutController : Controller
    {
        private IArticleRepositoryFE _articleRepositoryFe;
        private IOtherRepositoryFE _orderRepositoryFe;
        private IVideoRepositoryFE _videoRepositoryFe;
        private IPictureRepositoryFE _pictureRepositoryFe;

        public LayoutController(IArticleRepositoryFE articleRepositoryFe, IOtherRepositoryFE orderRepositoryFe, IVideoRepositoryFE videoRepositoryFe, IPictureRepositoryFE pictureRepositoryFe)
        {
            _articleRepositoryFe = articleRepositoryFe;
            _orderRepositoryFe = orderRepositoryFe;
            _videoRepositoryFe = videoRepositoryFe;
            _pictureRepositoryFe = pictureRepositoryFe;
        }

        // GET: Layout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuTop()
        {
            ViewData.Model = _orderRepositoryFe.GetServiceItems();
            return View();
        }

        public ActionResult Footer()
        {
            return View();
        }

        public ActionResult BookForm()
        {
            return View();
        }

        public ActionResult VideosAndPictures()
        {
            ViewData.Model = _pictureRepositoryFe.GetListByType((int)StaticEnum.ImageType.Library, string.Empty, 0,
                ConfigWeb.QuantityImageInLibrary);
            ViewBag.Video = _videoRepositoryFe.GetHomePageVideo();
            return View();
        }

        public ActionResult NewestNews()
        {
            ViewData.Model = _articleRepositoryFe.GetTopByType((int)StaticEnum.NewsType.News, 0,
                ConfigWeb.PageSizeNewestNews);
            return View();
        }

        public ActionResult MenuRight(bool? isPromotion)
        {
            ViewData.Model = _articleRepositoryFe.GetTopByType((int)StaticEnum.NewsType.FeedBack, 0, 100);
            //if (articleId.HasValue)
            //{
            //    ViewData["ListOtherNews"] = _articleRepositoryFe.GetOtherArticle((int)StaticEnum.NewsType.News,
            //        articleId.Value, 0,
            //        ConfigWeb.OtherNewsQuantity);
            //}
            //else
            //{
            ViewBag.IsPromotion = isPromotion == true;
            ViewData["ListOtherNews"] = _articleRepositoryFe.GetTopByType(
                isPromotion == true ? (int)StaticEnum.NewsType.Promotion : (int)StaticEnum.NewsType.News,
                0,
                ConfigWeb.OtherNewsQuantity);
            //}
            return View();
        }
    }
}