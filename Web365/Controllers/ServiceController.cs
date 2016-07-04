using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Business.Front_End.IRepository;
using Web365Utility;

namespace Web365.Controllers
{
    public class ServiceController : Controller
    {
        private IOtherRepositoryFE _otherRepositoryFe;
        private IArticleRepositoryFE _articleRepositoryFe;

        public ServiceController(IOtherRepositoryFE otherRepositoryFe, IArticleRepositoryFE articleRepositoryFe)
        {
            _otherRepositoryFe = otherRepositoryFe;
            _articleRepositoryFe = articleRepositoryFe;
        }

        // GET: Service
        public ActionResult Index()
        {
            ViewData.Model = _otherRepositoryFe.GetServiceItems();
            return View();
        }

        public ActionResult CareProcedures()
        {
            ViewData.Model = _articleRepositoryFe.GetListByType((int) StaticEnum.NewsType.CareProcedure, string.Empty, 0, 20);
            return View();
        }

        public ActionResult ServiceDetail(string nameAscii)
        {
            ViewData.Model = _otherRepositoryFe.GetServiceItemByNameascii(nameAscii);
            return View();
        }

        public ActionResult StepsOfService(int id, string title)
        {
            ViewBag.Title = title;
            ViewData.Model = _otherRepositoryFe.GetStepOfServiceItems(id);
            return View();
        }

        public ActionResult PriceTable(int id, string title)
        {
            ViewBag.Title = title;
            ViewData.Model = _otherRepositoryFe.GetDetailServiceItems(id);
            return View();
        }
    }
}