using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TonyBlogs.Common;
using TonyBlogs.Common.Cache;
using TonyBlogs.Common.Cookie;
using TonyBlogs.DTO.UserFunction;
using TonyBlogs.DTO.UserInfo;
using TonyBlogs.Framework;
using TonyBlogs.IService;

namespace TonyBlogs.WebApp.Filters
{
    public class AuthFilter : FilterAttribute, IActionFilter
    {
        private bool isCheckAuth;

        public AuthFilter(bool isCheckAuth = true)
        {
            this.isCheckAuth = isCheckAuth;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;

            bool isSuccess = VerifyCookie(request, response);
            if (!isSuccess)
            {
                RedirectToLogin(response);
                return;
            }

            var userObj = GetUserObj(request);
            if (userObj == null)
            {
                RedirectToLogin(response);
                return;
            }

            var menuItem = GetCurrentMenuItem(filterContext);

            bool validpurviewResult = ValidPurview(userObj, menuItem);
            if (validpurviewResult == false)
            {
                RedirectToLogin(response);
                return;
            }

            UserContext.CurrentUser = userObj;
            MenuContext.CurrentMenu = menuItem ?? new UserFunctionMenuItemDTO();
        }

        private bool VerifyCookie(HttpRequestBase request, HttpResponseBase response)
        {

            if (!request.Cookies.AllKeys.Contains(CookieNameConfigInfo.CookieName)
                || !request.Cookies.AllKeys.Contains(CookieNameConfigInfo.CacheKeyCookieName))
            {
                return false;
            }

            var cookie = request.Cookies[CookieNameConfigInfo.CookieName];
            string cacheKey = Base64Helper.Base64Decode(request.Cookies[CookieNameConfigInfo.CacheKeyCookieName].Value);

            ICacheManager cache = ContainerManager.Resolve<ICacheManager>();
            string cacheCookieValue = cache.Get<string>(cacheKey);
            if (cacheCookieValue != cookie.Value)
            {
                return false;
            }

            return true;
        }

        private UserObj GetUserObj(HttpRequestBase request)
        {
            long userId = 0;
            string cookieCacheKey = request.Cookies[CookieNameConfigInfo.CacheKeyCookieName].Value;
            string cacheKey = Base64Helper.Base64Decode(cookieCacheKey);

            long.TryParse(cacheKey.Split('_')[2], out userId);
            IUserInfoService userService = ContainerManager.Resolve<IUserInfoService>();

            var userObj = userService.GetUserObj(userId, isFromCache: true);
            return userObj;
        }

        private bool ValidPurview(UserObj userObj, UserFunctionMenuItemDTO menuItem)
        {
            if (menuItem == null)
            {
                return true;
            }
            else
            {
                string funcId = menuItem.ID.ToString();
                return userObj.PurviewFuncIDs.Contains(funcId);
            }
            
        }

        private UserFunctionMenuItemDTO GetCurrentMenuItem(ActionExecutingContext filterContext)
        {
            UserFunctionMenuItemDTO dto = null;

            string controller = filterContext.RouteData.Values["controller"] == null ? "" : filterContext.RouteData.Values["controller"].ToString().ToLower();
            string action = filterContext.RouteData.Values["action"] == null ? "" : filterContext.RouteData.Values["action"].ToString().ToLower();
            string area = filterContext.RouteData.DataTokens["area"] == null ? "" : filterContext.RouteData.DataTokens["area"].ToString().ToLower();

            if (!string.IsNullOrWhiteSpace(controller) && !string.IsNullOrWhiteSpace(action))
            {
                IUserFunctionService userFuncService = ContainerManager.Resolve<IUserFunctionService>();
                var funcList = userFuncService.GetAllFuncitonMenuList();

                dto = funcList.FirstOrDefault(m => m.AreaName.ToLower() == area
                    && m.ControllerName.ToLower() == controller
                    && m.ActionName.ToLower() == action);
            }

            return dto;
        }

        private void RedirectToLogin(HttpResponseBase response)
        {
            if (isCheckAuth)
            {
                response.Redirect("/admin", true); 
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}