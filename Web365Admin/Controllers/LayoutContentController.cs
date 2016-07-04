﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Utility;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365Domain;

namespace Web365Admin.Controllers
{
    public class LayoutContentController : BaseController
    {

        private ILayoutContentRepositoryBE layoutContentRepository;

        // GET: /Admin/ProductType/

        public LayoutContentController(ILayoutContentRepositoryBE _layoutContentRepository)
        {
            this.layoutContentRepository = _layoutContentRepository;
            this.baseRepository = _layoutContentRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = layoutContentRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

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
            var obj = new LayoutContentItem();

            if (id.HasValue)
                obj = layoutContentRepository.GetItemById<LayoutContentItem>(id.Value);

            return Json(new
            {
                data = obj
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblLayoutContent objSubmit)
        {

            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                layoutContentRepository.Add(objSubmit);
            }
            else
            {             
                var obj = layoutContentRepository.GetById<tblLayoutContent>(objSubmit.ID);
                
                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                layoutContentRepository.Update(obj);
            }

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
