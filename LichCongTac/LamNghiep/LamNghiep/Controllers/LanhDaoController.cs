using LamNghiep.BUL.Bussiness;
using LamNghiep.Common;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LamNghiep.Controllers
{
    public class LanhDaoController : Controller
    {
        //
        // GET: /CTCaNhan/
        public ActionResult Index()
        {
            KeHoachCT keHoachCT = new KeHoachCT();
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            keHoachCT.ListPhongBan = phongBanBussinessService.GetNguoiDungLanhDao();
            keHoachCT.ListPhongBanDistinct = phongBanBussinessService.GetNguoiDungDistinct(keHoachCT.ListPhongBan);
            ViewBag.TuanLe = this.GetListWeek();
            return View(keHoachCT);
        }
        #region Utilities
        public List<SelectListItem> GetListWeek()
        {
            List<WeekOfYearModel> listWeekOfYearModel = DateTimeExtensions.GetWeekOfYear();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in listWeekOfYearModel)
            {
                switch (item.IsWeek)
                {
                    case (int)WeekEnum.ThisWeek:
                        items.Add(new SelectListItem { Text = "Tuần này (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = true });
                        break;
                    case (int)WeekEnum.LastWeek:
                        items.Add(new SelectListItem { Text = "Tuần qua (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = false });
                        break;
                    case (int)WeekEnum.NextWeek:
                        items.Add(new SelectListItem { Text = "Tuần tới (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = false });
                        break;
                    default:
                        items.Add(new SelectListItem { Text = "Tuần " + item.CodeWeek + " (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = false });
                        break;
                }
            }
            return items;
        }
        #endregion


        public JsonResult GetJson(JQueryDataTableParamModel param)
        {
            // check login

            int sortColumnIndex = param.ISortCol_0;
            string order = param.SSortDir_0;

            string orderBy = string.Empty;

            switch (sortColumnIndex)
            {
                case 0:
                    orderBy = "Name";
                    break;
                case 1:
                    orderBy = "Description";
                    break;
                case 2:
                    orderBy = "Code";
                    break;
                case 3:
                    orderBy = "CreateDate";
                    break;
            }

            KeHoachCTBussinessService keHoachCTBussinessService = new KeHoachCTBussinessService();
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            if (!string.IsNullOrEmpty(param.CanBo) && !"null".Equals(param.CanBo))
            {
                param.CanBo = StringExtensions.GetCanBo(param.CanBo);
            }
            else
            {
                param.CanBo = StringExtensions.GetListPhongBanOrLanhDao(phongBanBussinessService.GetNguoiDungLanhDao());
            }

            if (param.TuanLe == 0)
            {
                param.StartDate = DateTimeExtensions.FirstDateOfWeek(DateTime.Now.Year, DateTimeExtensions.WeekNumber(DateTime.Today), System.Globalization.CultureInfo.CurrentCulture);
                param.EndDate = param.StartDate.AddDays(6);
            }
            else
            {
                param.StartDate = DateTimeExtensions.FirstDateOfWeek(DateTime.Now.Year, param.TuanLe, System.Globalization.CultureInfo.CurrentCulture);
                param.EndDate = param.StartDate.AddDays(6);
            }
            List<KeHoachCongTacModel> listKeHoachCT = keHoachCTBussinessService.GetKeHoachCongTac(param, "distict");
            int totalRecords = 0;

            totalRecords = 1;

            // return jon datatable
            return Json(new
            {
                sEcho = param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = listKeHoachCT
            }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /PhongBan/Details/5
        public JsonResult Details(int id)
        {
            KeHoachCTBussinessService ketHoachCTBussinessService = new KeHoachCTBussinessService();
            List<KeHoachCT> listKeHoach = ketHoachCTBussinessService.GetKeHoachById(id);
            return Json(listKeHoach[0], JsonRequestBehavior.AllowGet);
        }
    }
}
