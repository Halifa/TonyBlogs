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
    public class BaseController :Controller
    {
        /// <summary>
      /// 返回JsonResult
      /// </summary>
      /// <param name="data">数据</param>
      /// <param name="contentType">内容类型</param>
      /// <param name="contentEncoding">内容编码</param>
      /// <param name="behavior">行为</param>
      /// <returns>JsonReuslt</returns>
      protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
      {
          return new NewtonJsonResult(data, behavior, contentEncoding);

      }

      /// <summary>
      /// 返回JsonResult.24         /// </summary>
      /// <param name="data">数据</param>
      /// <param name="behavior">行为</param>
      /// <param name="format">json中dateTime类型的格式</param>
      /// <returns>Json</returns>
      protected JsonResult MyJson(object data, JsonRequestBehavior behavior,string format)
      {
          return new NewtonJsonResult(data, behavior, null, new JsonSerializerSettings() { DateFormatString = format });
      }

      /// <summary>
      /// 返回JsonResult42         /// </summary>
      /// <param name="data">数据</param>
      /// <param name="format">数据格式</param>
      /// <returns>Json</returns>
      protected JsonResult MyJson(object data, string format)
      {
          return new NewtonJsonResult(data, JsonRequestBehavior.DenyGet, null, new JsonSerializerSettings() { DateFormatString = format });
      }

    }
}