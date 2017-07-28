using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TonyBlogs.DTO.BlogArticle;
using TonyBlogs.DTO.BlogComment;
using TonyBlogs.IService;

namespace TonyBlogs.WebApp.Controllers
{
    public class BlogDetailController : BaseFrontController
    {
        private IBlogArticleService _blogArticleService;
        private IBlogCommentService _blogCommentService;

        public BlogDetailController(IBlogArticleService blogArticleService,
            IBlogCommentService blogCommentService)
        {
            this._blogArticleService = blogArticleService;
            this._blogCommentService = blogCommentService;
        }

        public ActionResult Index(long blogID)
        {
            BlogArticleDetailPageDTO dto = this._blogArticleService.GetBlogArticleDetail(blogID);

            dto.IsLogin = UserContext.CurrentUser == null;

            string ip = GetClientIp();
            //异步处理访问数
            Task.Factory.StartNew(() => _blogArticleService.AddBlogTraffic(blogID, ip));

            return View(dto);
        }

        public ActionResult AjaxGetCoommentList(long blogID)
        {
            var list = this._blogCommentService.GetCommentList(blogID);


            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxAddComment(BlogCommentAddPageDTO dto)
        {
            var result = _blogCommentService.AddComment(dto, UserContext.CurrentUser);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
