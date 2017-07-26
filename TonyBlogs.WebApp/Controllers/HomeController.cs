using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TonyBlogs.DTO.BlogArticle;
using TonyBlogs.IService;

namespace TonyBlogs.WebApp.Controllers
{
    public class HomeController : BaseFrontController
    {

        private IBlogArticleService _blogArticleService;

        public HomeController(IBlogArticleService blogArticleService)
        {
            this._blogArticleService = blogArticleService;
        }

        public ActionResult Index()
        {
            var list = _blogArticleService.QueryWhere(m => m.ID > 0);

            return View(list);
        }

        public ActionResult AjaxGetPageList(BlogArticleSearchDTO searchDTO)
        {
            var result = _blogArticleService.GetListPage(searchDTO);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxGetViewRankList()
        {
            var result = _blogArticleService.GetViewRankList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
