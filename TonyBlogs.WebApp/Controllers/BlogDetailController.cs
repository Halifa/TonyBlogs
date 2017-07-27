using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TonyBlogs.DTO.BlogArticle;
using TonyBlogs.IService;

namespace TonyBlogs.WebApp.Controllers
{
    public class BlogDetailController : BaseFrontController
    {
        private IBlogArticleService _blogArticleService;

        public BlogDetailController(IBlogArticleService blogArticleService)
        {
            this._blogArticleService = blogArticleService;
        }

        public ActionResult Index(long blogID)
        {
            BlogArticleDetailPageDTO dto = this._blogArticleService.GetBlogArticleDetail(blogID);

            string ip = GetClientIp();
            //异步处理访问数
            Task.Factory.StartNew(() => _blogArticleService.AddBlogTraffic(blogID, ip));

            return View(dto);
        }

    }
}
