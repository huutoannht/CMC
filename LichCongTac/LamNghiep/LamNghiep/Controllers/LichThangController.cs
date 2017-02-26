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
    public class LichThangController : AppController
    {
        public ActionResult Index()
        {
            ViewBag.Month = this.GetListMonth(DateTime.Now.Year);
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {

            KeHoachCT keHoachCT = new KeHoachCT();
            keHoachCT.Deadline = DateTime.Now;
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            List<KeHoachCT> listKeHoachCT = new List<KeHoachCT>();
            listKeHoachCT.Add(keHoachCT);

            ViewBag.Month = this.GetListMonth(DateTime.Now.Year);
            var listPhongBan = phongBanBussinessService.GetAllNguoiDung();
            var listPhongBanDistinct = phongBanBussinessService.GetNguoiDungDistinct(listPhongBan);
            ViewBag.ListPhongBan = listPhongBan;
            ViewBag.ListPhongBanDistinct = listPhongBanDistinct;

            return View(listKeHoachCT);
        }
        [HttpPost]
        public ActionResult Create(List<KeHoachCT> listKeHoachCT, string thang)
        {
            string userName = CurrentUser.Name;
            try
            {
                KeHoachCTBussinessService ketHoachCTBussinessService = new KeHoachCTBussinessService();
                int tuanLe = !string.IsNullOrEmpty(thang) ? Int32.Parse(thang) : DateTimeExtensions.WeekNumber(DateTime.Today);
                DateTime tuNgay = DateTime.Now;
                tuNgay = new DateTime(DateTime.Now.Year, int.Parse(thang), 1);
                foreach (var keHoachCT in listKeHoachCT)
                {
                    keHoachCT.UserName = userName;
                    keHoachCT.TuNgayModel = tuNgay;
                    keHoachCT.DenNgayModel = tuNgay.AddMonths(1).AddDays(-1);
                    ketHoachCTBussinessService.AddKeHoachCT(keHoachCT);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // GET: LichTuan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LichTuan/Create

        // GET: LichTuan/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                KeHoachCTBussinessService keHoachCTBussinessService = new KeHoachCTBussinessService();
                PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
                List<KeHoachCT> listkeHoachCT = keHoachCTBussinessService.GetKeHoachForUpdate(id);
                KeHoachCT keHoachCT = listkeHoachCT[0];
                //Get All User
                keHoachCT.ListPhongBan = phongBanBussinessService.GetAllNguoiDung();

                //Get List PhongBan
                keHoachCT.ListPhongBanDistinct = phongBanBussinessService.GetNguoiDungDistinct(keHoachCT.ListPhongBan);


                List<ThamDuModel> listThamDuModel = keHoachCTBussinessService.GetThamDuByIdKeHoach(id);
                //Get List KhachMoi
                //ThamDu : type =1
                keHoachCT.ListKhachMoiThamDu = keHoachCTBussinessService.GetKhachMoiByIdKeHoach(id) == null ?
                    null : keHoachCTBussinessService.GetKhachMoiByIdKeHoach(id).FindAll(t => t.Kieu == 1);
                //ChuTri : type =2
                keHoachCT.ListKhachMoiChuTri = keHoachCTBussinessService.GetKhachMoiByIdKeHoach(id) == null ?
                  null : keHoachCTBussinessService.GetKhachMoiByIdKeHoach(id).FindAll(t => t.Kieu == 2);

                //Check and fill selected for User in PhongBan
                //ThamDu : type =1
                keHoachCT.ListPhongBanThamDu = new List<PhongBan>();
                List<PhongBan> listPhongBanThamDuOld = keHoachCT.ListPhongBan;

                List<PhongBan> listPhongBanChuTriOld = ListExtensions.DeepCopy(listPhongBanThamDuOld);

                List<ThamDuModel> listKhachMoiModel = listThamDuModel.FindAll(t => t.Kieu == 1);
                keHoachCT.ListPhongBanThamDu = phongBanBussinessService.CheckSelectedThamDu(listPhongBanThamDuOld, listKhachMoiModel);
                //ChuTri : type =2
                keHoachCT.ListPhongBanChuTri = new List<PhongBan>();

                List<ThamDuModel> listChutriModel = listThamDuModel.FindAll(t => t.Kieu == 2);
                keHoachCT.ListPhongBanChuTri = phongBanBussinessService.CheckSelectedThamDu(listPhongBanChuTriOld, listChutriModel);


                return View(listkeHoachCT[0]);
            }
            catch
            {
                return View();
            }
        }

        // POST: LichTuan/Edit/5
        [HttpPost]
        public ActionResult Edit(KeHoachCT keHoachCT, string[] thamdu, string[] chutri)
        {
            try
            {
                string userName = CurrentUser.Name;
                try
                {
                    KeHoachCTBussinessService ketHoachCTBussinessService = new KeHoachCTBussinessService();
                    keHoachCT.ThamDu = thamdu;
                    keHoachCT.ChuTri = chutri;

                    keHoachCT.TuNgayModel = DateTimeExtensions.ConvertDateTime(keHoachCT.TuNgay);
                    keHoachCT.DenNgayModel = DateTimeExtensions.ConvertDateTime(keHoachCT.DenNgay);
                    keHoachCT.UserName = userName;
                    ketHoachCTBussinessService.EditKeHoachCT(keHoachCT);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: LichTuan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LichTuan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        private List<SelectListItem> GetListMonth(int year)
        {
            List<MonthOfYearModel> listWeekOfYearModel = DateTimeExtensions.GetMonthOfYear(year);
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in listWeekOfYearModel)
            {

                items.Add(new SelectListItem { Text = "Tháng " + item.CodeMonth + " (" + item.NameMonth + ")", Value = item.CodeMonth.ToString(), Selected = false });
            }
            return items;
        }
        #region GetJSON
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
            param.CanBo = CurrentUser.Name;

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
            List<KeHoachCT> listKeHoachCT = keHoachCTBussinessService.GetKeHoachCaNhan(param);
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
        #endregion
    }
}
