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

        public ActionResult Index(BlogArticleSearchDTO dto)
        {
            dto = dto ?? new BlogArticleSearchDTO();
            return View("index",dto);
        }

        public ActionResult Blog()
        {
            return Index(new BlogArticleSearchDTO() { Category = "技术博文" });
        }

        public ActionResult Essay()
        {
            return Index(new BlogArticleSearchDTO() { Category = "随笔日志" });
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
