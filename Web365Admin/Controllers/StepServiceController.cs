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
    public class StepServiceController : BaseController
    {
        private IStepServiceDABERepository _dabe;
        private IServiceDABERepository dabeServiceDabeRepository;

        public StepServiceController(IStepServiceRepositoryBE serviceRepository,IStepServiceDABERepository dabeRepository, IServiceDABERepository dabeServiceDabeRepository)
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
            var obj = new StepOfServiceItem();

            if (id.HasValue)
                obj = _dabe.GetItemById<StepOfServiceItem>(id.Value);

            var listService = dabeServiceDabeRepository.GetListForTree<object>();

            return Json(new
            {
                data = obj,
                listService = listService
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblStepOfService objSubmit)
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
                var obj = _dabe.GetById<tblStepOfService>(objSubmit.Id);

                UpdateModel(obj);

                _dabe.Update(obj);
            }

            _dabe.ResetListPicture(objSubmit.Id, Request["listPictureId"]);

            return Json(new
            {
                Error = false

            }, JsonRequestBehavior.AllowGet);
        }
    }
}