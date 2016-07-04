using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Base;
using Web365Business.Front_End.IRepository;

namespace Web365.Controllers
{
    public class ContactUsController : Controller
    {
        private IOtherRepositoryFE _otherRepositoryFe;
        private IProductRepositoryFE _productRepositoryFe;

        public ContactUsController(IOtherRepositoryFE otherRepositoryFe, IProductRepositoryFE productRepositoryFe)
        {
            this._otherRepositoryFe = otherRepositoryFe;
            this._productRepositoryFe = productRepositoryFe;
        }
        // GET: ContactUs
        public ActionResult Index(int? prodId)
        {
            if (prodId.HasValue)
                ViewData.Model = _productRepositoryFe.GetItemById(prodId.Value);
            return View();
        }

        [HttpPost]
        public ActionResult AddContact(tblContact model)
        {
            try
            {
                model.IsDeleted = false;
                model.IsViewed = true;
                model.DateCreated = DateTime.Now;
                model.DateUpdated = DateTime.Now;
                if (_otherRepositoryFe.AddContact(model))
                {
                    return Json(new { error = false, message = "Cảm ơn bạn đã gửi thông tin liên hệ cho chúng tôi, chúng tôi sẽ liên hệ lại với bạn trong thời gian ngắn nhất" }, JsonRequestBehavior.AllowGet); 
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
            }
            return Json(new { error = true, message = "Đã có lỗi xảy ra" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegPromotionNews(string email, int groupId)
        {
            try
            {
                var model = new tblReceiveInfo()
                {
                    Email = email,
                    GroupID =  groupId,
                    DateCreated = DateTime.Now,
                    IsDeleted = false
                };
                _otherRepositoryFe.AddEmailPromotion(model);
                return Json(
                    new
                    {
                        error = false, 
                        message = "Hệ thống tiếp nhận thông tin đăng ký của quý khách, chúng tôi sẽ gửi khuyến mãi cho quý khách mỗi khi có sự kiện."
                    }
                    , JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
            }
            return Json(new { error = true, message = "Đã có lỗi xảy ra" }, JsonRequestBehavior.AllowGet);
        }
    }
}