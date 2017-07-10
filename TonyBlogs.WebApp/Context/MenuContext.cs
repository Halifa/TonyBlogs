using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TonyBlogs.DTO.UserFunction;



public static class MenuContext
{
    public static UserFunctionMenuItemDTO CurrentMenu
    {
        get
        {
            return HttpContext.Current.Items["FunctionMenu"] as UserFunctionMenuItemDTO;
        }

        set
        {
            HttpContext.Current.Items["FunctionMenu"] = value;
        }
    }

    public static string GenerateUrlPath(string area,string controller, string action)
    {
        string urlPath = string.Empty;
        if (!string.IsNullOrEmpty(controller) && !string.IsNullOrEmpty(action))
        {
            urlPath = UrlHelper.GenerateUrl(null, action, controller, null, null, null,
                        new RouteValueDictionary() { { "area", area } }, RouteTable.Routes,
                        HttpContext.Current.Request.RequestContext, true);
        }

        return urlPath;
    }
}
