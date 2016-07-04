using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Domain.Service;

namespace Web365Admin.Controllers
{
    public class ServiceController : BaseController
    {
        private IServiceDABERepository _dabe;
        private IDetailServiceDABEReponsitory _dabeDetail;

        public ServiceController(IServiceRepositoryBE serviceRepository, IServiceDABERepository dabe, IDetailServiceDABEReponsitory dabeDetail)
        {
            this.baseRepository = serviceRepository;
            _dabe = dabe;
            _dabeDetail = dabeDetail;
        }

        // GET: Service
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = _dabe.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

            return Json(new
            {
                total = total,
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }   

        [HttpGet]
        public ActionResult EditForm(int? id)
        {
            var obj = new ServiceItem();

            if (id.HasValue)
                obj = _dabe.GetItemById<ServiceItem>(id.Value);

            return Json(new
            {
                data = obj
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblService objSubmit)
        {

            if (objSubmit.Id == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                _dabe.Add(objSubmit);

            }
            else
            {
                var obj = _dabe.GetById<tblService>(objSubmit.Id);

                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                _dabe.Update(obj);
            }

            return Json(new
            {
                Error = false

            }, JsonRequestBehavior.AllowGet);
        }
    }
}