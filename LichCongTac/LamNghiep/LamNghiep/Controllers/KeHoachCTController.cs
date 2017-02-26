using LamNghiep.BUL.Bussiness;
using LamNghiep.Common;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LamNghiep.Controllers
{
    public class KeHoachCTController : AppController
    {
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public ActionResult Index()
        {
            KeHoachCT keHoachCT = new KeHoachCT();
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            keHoachCT.ListPhongBan = phongBanBussinessService.GetNguoiDungLanhDao();
            ViewBag.TuanLe = this.GetListWeek();
            ViewBag.LanhDao = keHoachCT.ListPhongBan;

            KeHoachCTBussinessService ketHoachCTBussinessService = new KeHoachCTBussinessService();
            JQueryDataTableParamModel param = new JQueryDataTableParamModel();
            param.CanBo = StringExtensions.GetVariableSql(CurrentUser.Name);

            param.CanBo = StringExtensions.GetListPhongBanOrLanhDao(keHoachCT.ListPhongBan);
            param.StartDate = DateTimeExtensions.FirstDateOfWeek(DateTime.Now.Year, DateTimeExtensions.WeekNumber(DateTime.Today), System.Globalization.CultureInfo.CurrentCulture);
            param.EndDate = param.StartDate.AddDays(6);
            //Print day of week
            GetDayOfWeek(param.StartDate);
            return View(ketHoachCTBussinessService.GetKeHoachCongTac(param,"distinct"));
        }
        
       
        public List<SelectListItem> GetListWeek()
        {
            List<WeekOfYearModel> listWeekOfYearModel = DateTimeExtensions.GetWeekOfYear();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in listWeekOfYearModel)
            {
                switch (item.IsWeek)
                {
                    case (int)WeekEnum.ThisWeek:
                        items.Add(new SelectListItem { Text = "Tuần này ("+ item.NameWeek+")", Value = item.CodeWeek.ToString(), Selected = true });
                        break;
                    case (int)WeekEnum.LastWeek:
                        items.Add(new SelectListItem { Text = "Tuần qua (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = false });
                        break;
                    case (int)WeekEnum.NextWeek:
                        items.Add(new SelectListItem { Text = "Tuần tới (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = false });
                        break;
                    default:
                        items.Add(new SelectListItem { Text = "Tuần "+item.CodeWeek+" (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = false });
                        break;
                }
            }
            return items;
        }
        [ActionName("LichCongTac")]
        [Authorize(Roles = "User, Admin")]
        public ActionResult LichCongTac()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(string[] lanhDao, int tuanLe,string search)
        {
            KeHoachCT keHoachCT = new KeHoachCT();
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            keHoachCT.ListPhongBan = phongBanBussinessService.GetNguoiDungLanhDao();
            ViewBag.TuanLe = this.GetListWeek();
            ViewBag.KeHoachCT = keHoachCT.ListPhongBan;

            KeHoachCTBussinessService ketHoachCTBussinessService = new KeHoachCTBussinessService();
            JQueryDataTableParamModel param = new JQueryDataTableParamModel();
            param.CanBo = "'" + CurrentUser.Name + "'";

            if (lanhDao == null|| string.IsNullOrEmpty(lanhDao[0]))
            {
                param.CanBo = StringExtensions.GetListPhongBanOrLanhDao(keHoachCT.ListPhongBan);
            }
            else
            {
                param.CanBo = StringExtensions.GetStringListUser(lanhDao);
            }
            param.StartDate = DateTimeExtensions.FirstDateOfWeek(DateTime.Now.Year,tuanLe,System.Globalization.CultureInfo.CurrentCulture);
            param.EndDate = param.StartDate.AddDays(6);
            param.sSearch = search;
            //Print day of week
            GetDayOfWeek(param.StartDate);
            List<KeHoachCongTacModel> listKeHoachCongTacModel = ketHoachCTBussinessService.GetKeHoachCongTac(param);
            return View(listKeHoachCongTacModel);
        }
        private void GetDayOfWeek(DateTime dateTime)
        {
            DayOfWeekModel dayOfWeek = DateTimeExtensions.GetDayOfWeek(dateTime);
            ViewBag.DayOfWeekModel = dayOfWeek;
        }
    }
}