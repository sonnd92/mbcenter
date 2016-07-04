﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Utility;
using Web365Base;
using Web365Domain;
using System;
using Web365Business.Back_End.IRepository;

namespace Web365Admin.Controllers
{
    public class UserRoleController : BaseController
    {

        private IUserRoleRepositoryBE userRoleRepository;

        // GET: /Admin/ProductType/

        public UserRoleController(IUserRoleRepositoryBE _userRoleRepository)
        {
            this.baseRepository = _userRoleRepository;
            this.userRoleRepository = _userRoleRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = userRoleRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

            return Json(new
            {
                total = total,
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditForm(string id)
        {
            var obj = new AspNetRoleItem();

            var list = userRoleRepository.GetListForTree<object>();

            if (!string.IsNullOrEmpty(id))
                obj = userRoleRepository.GetItemById<AspNetRoleItem>(id);

            return Json(new
            {
                data = obj,
                list = list
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(AspNetRoles objSubmit)
        {

            if (string.IsNullOrEmpty(objSubmit.Id))
            {
                objSubmit.Id = Guid.NewGuid().ToString();
                userRoleRepository.Add(objSubmit);
            }
            else
            {
                var obj = userRoleRepository.GetById<AspNetRoles>(objSubmit.Id);

                UpdateModel(obj);

                userRoleRepository.Update(obj);
            }

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PageForRole(string roleId, string listPage)
        {
            userRoleRepository.PageForRole(roleId, Web365Utility.Web365Utility.StringToArrayInt(listPage));

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
