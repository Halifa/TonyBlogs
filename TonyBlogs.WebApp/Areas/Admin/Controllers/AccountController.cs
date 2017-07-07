using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TonyBlogs.Common.Cookie;
using TonyBlogs.DTO;
using TonyBlogs.DTO.Account;
using TonyBlogs.IService;

namespace TonyBlogs.WebApp.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountServcie;

        public AccountController(IAccountService accountService)
        {
            this._accountServcie = accountService;
        }


        public ActionResult Index()
        {
            AccountLoginDTO dto = new AccountLoginDTO();

            return View(dto);
        }

        public ActionResult Login(AccountLoginDTO dto)
        {
            var result = _accountServcie.Login(dto);
            if (result.IsSuccess == true)
            {
                Response.Cookies.Add(new HttpCookie(CookieNameConfigInfo.CookieName, result.CookieValue) { Domain = CookieNameConfigInfo.DomainName });
                Response.Cookies.Add(new HttpCookie(CookieNameConfigInfo.CacheKeyCookieName, result.CookieValueCacheKey) { Domain = CookieNameConfigInfo.DomainName });
            }

            return Json(new ExecuteResult(result.IsSuccess, result.Message), 
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            if (Request.Cookies.AllKeys.Contains(CookieNameConfigInfo.CacheKeyCookieName))
            {
                string cacheKey = Request.Cookies[CookieNameConfigInfo.CacheKeyCookieName].Value;
                _accountServcie.Logout(cacheKey);
            }

            return Redirect("/admin");
        }    

    }
}
