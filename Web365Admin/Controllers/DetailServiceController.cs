using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain.Service;

namespace Web365Admin.Controllers
{
    public class DetailServiceController : BaseController
    {
        private IDetailServiceDABEReponsitory _dabe;
        private IGroupServiceDABERepository groupServiceDabe;

        public DetailServiceController(IDetailServiceRepositoryBE dabe, IDetailServiceDABEReponsitory detailServiceDabe, IGroupServiceDABERepository groupServiceDabe)
        {
            this.baseRepository = dabe;
            this.groupServiceDabe = groupServiceDabe;
            _dabe = detailServiceDabe;
        }

        // GET: DetailService
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
            var obj = new DetailServiceItem();

            if (id.HasValue)
                obj = _dabe.GetItemById<DetailServiceItem>(id.Value);

            var listService = groupServiceDabe.GetListForTree<object>();

            return Json(new
            {
                data = obj,
                listService
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblServiceDetail objSubmit)
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
                var obj = _dabe.GetById<tblServiceDetail>(objSubmit.Id);

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