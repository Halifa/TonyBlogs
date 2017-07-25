using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TonyBlogs.DTO.UserInfo;
using TonyBlogs.IService;

namespace TonyBlogs.WebApp.Areas.Admin.Controllers
{
    public class UserInfoController : BaseAuthController
    {
        private IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            this._userInfoService = userInfoService;
        }

        public ActionResult Index()
        {
            var dto = _userInfoService.GetUserInfoSearchDTO();

            return View(dto);
        }

        public ActionResult AjaxGetList(UserInfoSearchDTO searchDTO)
        {
            var listDTO = _userInfoService.GetUserInfoList(searchDTO);

            return Json(new
            {
                sEcho = searchDTO.sEcho,
                iTotalRecords = listDTO.TotalRecords,
                iTotalDisplayRecords = listDTO.TotalRecords,
                aaData = listDTO.List
            },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxGetEditDTO(long userID)
        {
            var result = _userInfoService.GetUserInfoEditDTO(userID);

            return PartialView(result);
        }

        public ActionResult AjaxAddOrEdit(UserInfoEditDTO dto)
        {
            var result = _userInfoService.AddOrEditUserInfo(dto);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxDelete(long userID)
        {
            var result = _userInfoService.DeleteUserInfo(userID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
