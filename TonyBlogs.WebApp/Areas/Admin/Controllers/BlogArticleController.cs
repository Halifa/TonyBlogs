using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TonyBlogs.DTO;
using TonyBlogs.DTO.BlogArticle;
using TonyBlogs.IService;

namespace TonyBlogs.WebApp.Areas.Admin.Controllers
{
    public class BlogArticleController : BaseController
    {
        private IBlogArticleService _blogArticleService;

        public BlogArticleController(IBlogArticleService blogArticleService)
        {
            this._blogArticleService = blogArticleService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddOrEdit(long? blogID)
        {
            long parseBlogID = blogID.HasValue ? blogID.Value : 0;
            var dto = _blogArticleService.GetBlogArticleEditDTO(parseBlogID);

            return View(dto);
        }

        public ActionResult AjaxGetList(JQueryDataTableSearchDTO searchDTO)
        {
            var listDTO = _blogArticleService.GetList(searchDTO, UserContext.CurrentUser);

            return Json(new
            {
                sEcho = searchDTO.sEcho,
                iTotalRecords = listDTO.TotalRecords,
                iTotalDisplayRecords = listDTO.TotalRecords,
                aaData = listDTO.List
            },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxAddOrEdit(BlogArticleEditDTO dto)
        {
            var result = _blogArticleService.AddOrEditBlogArticle(dto, UserContext.CurrentUser);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxDelete(long blogID)
        {
            var result = _blogArticleService.Delete(blogID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
