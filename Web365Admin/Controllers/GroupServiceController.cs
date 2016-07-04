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
    public class GroupServiceController : BaseController
    {
        private IGroupServiceDABERepository _dabe;
        private IServiceDABERepository dabeServiceDabeRepository;

        public GroupServiceController(IGroupServiceRepositoryBE serviceRepository,IGroupServiceDABERepository dabeRepository, IServiceDABERepository dabeServiceDabeRepository)
        {
            this.baseRepository = serviceRepository;
            _dabe = dabeRepository;
            this.dabeServiceDabeRepository = dabeServiceDabeRepository;
        }

        // GET: StepService
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
            var obj = new GroupOfServiceItem();

            if (id.HasValue)
                obj = _dabe.GetItemById<GroupOfServiceItem>(id.Value);

            var listService = dabeServiceDabeRepository.GetListForTree<object>();

            return Json(new
            {
                data = obj,
                listService = listService
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblGroupOfService objSubmit)
        {

            if (objSubmit.Id == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                _dabe.Add(objSubmit);

            }
            else
            {
                var obj = _dabe.GetById<tblGroupOfService>(objSubmit.Id);

                UpdateModel(obj);

                _dabe.Update(obj);
            }

            return Json(new
            {
                Error = false

            }, JsonRequestBehavior.AllowGet);
        }
    }
}