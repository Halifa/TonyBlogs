using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TonyBlogs.Common.Log;
using TonyBlogs.DTO;
using TonyBlogs.Framework;

namespace TonyBlogs.WebApp.Filters
{
    public class ExcepFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception exp = filterContext.Exception;

            //获取ex的第一级内部异常
            Exception innerEx = exp.InnerException == null ? exp : exp.InnerException;
            //循环获取内部异常直到获取详细异常信息为止
            while (innerEx.InnerException != null)
            {
                innerEx = innerEx.InnerException;
            }

            ILogger _logger = ContainerManager.Resolve<ILogger>();  
            _logger.Error(innerEx.Message, innerEx);

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult() { Data = new ExecuteResult { IsSuccess = false, Message = "请求发生错误，请联系管理员" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                ViewResult vireResult = new ViewResult();
                vireResult.ViewName = "/Views/Shared/Error.cshtml";
                filterContext.Result = vireResult;
            }

            //告诉MVC框架异常被处理
            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
        }
    }
}