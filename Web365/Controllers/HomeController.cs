using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Business.Front_End.IRepository;
using Web365Utility;

namespace Web365.Controllers
{
    public class HomeController : Controller
    {
        private IArticleRepositoryFE _articleRepositoryFe;
        private IOtherRepositoryFE _otherRepository;
        private IAdvertiesRepositoryFE _advertiesRepositoryFe;
        private IPictureRepositoryFE _pictureRepositoryFe;

        public HomeController(IArticleRepositoryFE articleRepositoryFe, IOtherRepositoryFE otherRepository, IAdvertiesRepositoryFE advertiesRepositoryFe, IPictureRepositoryFE pictureRepositoryFe)
        {
            _articleRepositoryFe = articleRepositoryFe;
            _otherRepository = otherRepository;
            _advertiesRepositoryFe = advertiesRepositoryFe;
            _pictureRepositoryFe = pictureRepositoryFe;
        }

        public ActionResult Index()
        {
            ViewBag.IsHome = true;
            return View();
        }

        public ActionResult Introduce()
        {
            ViewData.Model = _articleRepositoryFe
                .GetListByType((int)StaticEnum.NewsType.Introduce, string.Empty, 0, 1)
                .List
                .FirstOrDefault();
            return View();
        }

        public ActionResult About()
        {
            ViewData.Model = _articleRepositoryFe
                .GetListByType((int)StaticEnum.NewsType.Introduce, string.Empty, 0, 1)
                .List
                .FirstOrDefault();
            return View();
        }

        public ActionResult BannerSlider()
        {
            ViewData.Model = _advertiesRepositoryFe
                .GetItemById((int)StaticEnum.Advertise.HomeBannerSlide);

            return View();
        }

        public ActionResult BannerHome()
        {
            ViewData.Model = _advertiesRepositoryFe.GetItemById((int) StaticEnum.Advertise.HomeAdvertise);
            return View();
        }

        public ActionResult Service()
        {
            ViewData.Model = _otherRepository.GetServiceItems().Where(c => c.IsFeartured == true);
            return View();
        }

        public ActionResult CustomerFeedback()
        {
            ViewData.Model = _articleRepositoryFe.GetTopByType((int)StaticEnum.NewsType.FeedBack, 0, 100);
            return View();
        }

        public ActionResult Expert()
        {
            ViewData.Model = _articleRepositoryFe.GetTopByType((int)StaticEnum.NewsType.Expert, 0, ConfigWeb.PageSizeExpert);
            return View();
        }
    }
}