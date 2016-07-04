using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365Domain.Order;

namespace Web365.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepositoryFE _orderRepository;

        public OrderController(IOrderRepositoryFE _order)
        {
            _orderRepository = _order;
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Booking(BookingItem model)
        {   
            try
            {
                if (_orderRepository.AddBooking(model))
                {
                    return Json(new { error = false, message = "Đặt chỗ thành công" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
            }

            return Json(new { error = true, message = "Đặt chỗ không thành công, vui lòng thử lại sau" }, JsonRequestBehavior.AllowGet);
        }
    }
}