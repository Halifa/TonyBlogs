using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TonyBlogs.DTO.UserPurview;
using TonyBlogs.IService;

namespace TonyBlogs.WebApp.Areas.Admin.Controllers
{
    public class PurviewController : Controller
    {
        private IUserPurviewService _purviewService;

        public PurviewController(IUserPurviewService purviewService)
        {
            this._purviewService = purviewService;
        }

        public ActionResult Index()
        {
            var searchDTO = new UserPurviewSearchDTO();

            return View(searchDTO);
        }

        public ActionResult AjaxGetList(UserPurviewSearchDTO dto)
        {
            var listDTO = _purviewService.GetPurviewList(dto);

            return Json(new
                    {
                        sEcho = dto.sEcho,
                        iTotalRecords = listDTO.TotalRecords,
                        iTotalDisplayRecords = listDTO.TotalRecords,
                        aaData = listDTO.List
                    },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxGetEditDTO(long purviewID)
        {
            var result = _purviewService.GetPurviewEditDTO(purviewID);

            return PartialView(result);
        }

        public ActionResult AjaxAddOrEdit(UserPurviewEditDTO dto)
        {
            var result = _purviewService.AddOrEditPurview(dto);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxDelete(long purviewID)
        {
            var result = _purviewService.DeletePurview(purviewID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
