using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TonyBlogs.DTO.UserFunction;
using TonyBlogs.IService;

namespace TonyBlogs.WebApp.Areas.Admin.Controllers
{
    public class FunctionController : BaseAuthController
    {
        private IUserFunctionService _funcService;

        public FunctionController(IUserFunctionService funcService)
        {
            this._funcService = funcService;
        }

        public ActionResult Index()
        {
            var list = _funcService.GetAllValidFunciton();

            return View(list);
        }

        public ActionResult AjaxGetAllFunctions()
        {
            var list = _funcService.GetAllValidFunciton();

            return PartialView(list);
        }

        public ActionResult AjaxGetFuncEditDTO(long funcID, long parentID)
        {
            var result = _funcService.GetFunctionEditDTO(funcID, parentID);

            return PartialView(result);
        }

        public ActionResult AjaxAddOrEditFunction(UserFunctionEditDTO dto)
        {
            var result = _funcService.AddOrEditFunction(dto);

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxDeleteFunction(long funcID)
        {
            var result = _funcService.DeleteFunction(funcID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
