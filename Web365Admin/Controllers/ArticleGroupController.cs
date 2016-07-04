﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Utility;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365Domain;
using System;
namespace Web365Admin.Controllers
{
    public class ArticleGroupController : BaseController
    {

        private IArticleGroupRepositoryBE articleGroupRepository;

        // GET: /Admin/ProductType/

        public ArticleGroupController(IArticleGroupRepositoryBE _articleGroupRepository)
        {
            this.baseRepository = _articleGroupRepository;
            this.articleGroupRepository = _articleGroupRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = articleGroupRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

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
            var obj = new ArticleGroupItem();

            var listProductType = articleGroupRepository.GetListForTree<object>();

            if (id.HasValue)
                obj = articleGroupRepository.GetItemById<ArticleGroupItem>(id.Value);

            return Json(new
            {
                data = obj,
                listType = listProductType
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblGroupArticle objSubmit)
        {
            
            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                articleGroupRepository.Add(objSubmit);
            }
            else
            {
                var obj = articleGroupRepository.GetById<tblGroupArticle>(objSubmit.ID);
                
                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                articleGroupRepository.Update(obj);
            }

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
