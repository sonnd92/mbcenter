using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Business.Front_End.IRepository;
using Web365Utility;

namespace Web365.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepositoryFE _productRepositoryFe;

        public ProductController(IProductRepositoryFE productRepositoryFe)
        {
            _productRepositoryFe = productRepositoryFe;
        }

        // GET: Product
        public ActionResult Index(int? page, int? orderBy)
        {
            var list = _productRepositoryFe.GetListProducts(page ?? 1, ConfigWeb.PageSizeProducts, orderBy ?? 0);
            ViewBag.Page = page ?? 1;
            ViewBag.OrderBy = orderBy ?? 0;
            ViewBag.ItemShowed =
                list.Total <= ConfigWeb.PageSizeProducts
                    ? list.Total.ToString()
                    : (page ?? 1)*ConfigWeb.PageSizeProducts + " - " + ((page ?? 1) + 1)*ConfigWeb.PageSizeProducts;
            return View(list);
        }

        public ActionResult ProductDetail(string nameAscii)
        {
            ViewData.Model = _productRepositoryFe.GetItemByAscii(nameAscii);
            return View();
        }

        public ActionResult OtherProducts(int prodId)
        {
            ViewData.Model = _productRepositoryFe.GetListOtherProducts(prodId, ConfigWeb.PageSizeOtherProducts);
            return View();
        }
    }
}