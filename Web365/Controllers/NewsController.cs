using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Business.Front_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365.Controllers
{
    public class NewsController : Controller
    {
        private IArticleRepositoryFE _articleRepositoryFe;

        public NewsController(IArticleRepositoryFE article)
        {
            _articleRepositoryFe = article;
        }
        // GET: News
        public ActionResult Index(int? page)
        {
            ViewBag.Page = page ?? 1;
            return View();
        }

        public ActionResult ListNewestNews(int page)
        {
            ViewData.Model = _articleRepositoryFe.GetNewestNews((int)StaticEnum.NewsType.News, (page - 1) * ConfigWeb.PageSizeNews, ConfigWeb.PageSizeNews);
            return View();
        }

        public ActionResult NewsDetail(string nameascii)
        {
            ViewData.Model = _articleRepositoryFe.GetItemByNameAscii(nameascii);
            return View();
        }

        public ActionResult PromotionDetail(string nameascii)
        {
            ViewData.Model = _articleRepositoryFe.GetItemByNameAscii(nameascii);
            return View();
        }

        public ActionResult Promotion(int? page)
        {
            ViewBag.Page = page ?? 1;
            return View();
        }

        public ActionResult ListPromotion(int page)
        {
            ViewData.Model = _articleRepositoryFe
                .GetListByType((int)StaticEnum.NewsType.Promotion, string.Empty, (page - 1) * ConfigWeb.PageSizePromotion, ConfigWeb.PageSizePromotion);
            return View();
        }
    }
}