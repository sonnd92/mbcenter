using System.Collections.Generic;
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
    public class ArticleController : BaseController
    {
        
        private IArticleRepositoryBE articleRepository;
        private IArticleTypeRepositoryBE articleTypeRepository;

        // GET: /Admin/ProductType/

        public ArticleController(IArticleRepositoryBE _articleRepository,
            IArticleGroupRepositoryBE _articleGroupRepository,
            IArticleTypeRepositoryBE _articleTypeRepository,
            IArticleGroupMapRepositoryBE _articleGroupMapRepository)
        {
            this.baseRepository = _articleRepository;
            this.articleRepository = _articleRepository;
            this.articleTypeRepository = _articleTypeRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int? typeId, int? groupId, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = articleRepository.GetList(out total, name, typeId, groupId, currentRecord, numberRecord, propertyNameSort, descending);

            return Json(new
            {
                total = total,
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPropertyFilter()
        {
            var listArticleType = articleTypeRepository.GetListForTree<object>();

            return Json(new
            {
                listType = listArticleType
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditForm(int? id)
        {
            var obj = new ArticleItem();

            var listArticleType = articleTypeRepository.GetListForTree<object>();

            if (id.HasValue)
                obj = articleRepository.GetItemById<ArticleItem>(id.Value);

            return Json(new
            {
                data = obj,
                listType = listArticleType
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblArticle objSubmit)
        {

            if (objSubmit.ID == 0)
            {                
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                articleRepository.Add(objSubmit);

            }
            else
            {
                var obj = articleRepository.GetById<tblArticle>(objSubmit.ID);
                
                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                articleRepository.Update(obj);
            }

            //articleGroupMapRepository.ResetGroupOfNews(objSubmit.ID, Web365Utility.Web365Utility.StringToArrayInt(Request["groupID"]));

            articleRepository.ResetListPicture(objSubmit.ID, Request["listPictureId"]);

            return Json(new
            {
                Error = false

            }, JsonRequestBehavior.AllowGet);
        }
    }
}
