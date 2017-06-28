using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TonyBlogs.IService;

namespace TonyBlogs.WebApp.Controllers
{
    public class HomeController : Controller
    {

        IBlogArticleService BlogArticleServive;

        public HomeController(IBlogArticleService BlogArticleServive)
        {
            this.BlogArticleServive = BlogArticleServive;
        }

        public ActionResult Index()
        {
            int b = 0;
            int c = 1 / b;

            var list = BlogArticleServive.QueryWhere(m => m.ID > 0);

            return View(list);
        }

    }
}
