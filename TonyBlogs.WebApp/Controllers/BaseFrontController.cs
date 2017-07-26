using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TonyBlogs.WebApp.Filters;

namespace TonyBlogs.WebApp.Controllers
{
    [AuthFilter(false)]
    public class BaseFrontController : BaseController
    {

    }
}
