using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TonyBlogs.WebApp.Filters;

namespace TonyBlogs.WebApp.Areas.Admin.Controllers
{
    [AuthFilter]
    public class BaseAuthController : BaseController
    {

    }
}