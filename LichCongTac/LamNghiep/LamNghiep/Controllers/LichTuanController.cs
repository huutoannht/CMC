using LamNghiep.BUL.Bussiness;
using LamNghiep.Common;
using LamNghiep.Common.EnumData;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LamNghiep.Controllers
{
    public class LichTuanController : AppController
    {
        public ActionResult Index()
        {
            ViewBag.TuanLe = this.GetListWeek();
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            KeHoachCT keHoachCT = new KeHoachCT();
            keHoachCT.DenNgay = DateTime.Now.ToString("dd/MM/yyyy");
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            List<KeHoachCT> listKeHoachCT = new List<KeHoachCT>();
            listKeHoachCT.Add(keHoachCT);

            ViewBag.TuanLe = this.GetListWeek();
            var listPhongBan = phongBanBussinessService.GetAllNguoiDung();
            var listPhongBanDistinct = phongBanBussinessService.GetNguoiDungDistinct(listPhongBan);
            ViewBag.ListPhongBan = listPhongBan;
            ViewBag.ListPhongBanDistinct = listPhongBanDistinct;

            return View(listKeHoachCT);
        }
        [HttpPost]
        public ActionResult Create(List<KeHoachCT> listKeHoachCT, string TuanLe)
        {
            string userName = CurrentUser.Name;
            try
            {
                KeHoachCTBussinessService ketHoachCTBussinessService = new KeHoachCTBussinessService();
                int tuanLe = !string.IsNullOrEmpty(TuanLe) ? Int32.Parse(TuanLe) : DateTimeExtensions.WeekNumber(DateTime.Today);
                var tuanLeData = DateTimeExtensions.FirstDateOfWeek(DateTime.Now.Year, tuanLe, System.Globalization.CultureInfo.CurrentCulture);
                foreach (var keHoachCT in listKeHoachCT)
                {
                    keHoachCT.UserName = userName;
                    keHoachCT.TuNgayModel = tuanLeData;
                    keHoachCT.DenNgayModel = tuanLeData.AddDays(6);
                    keHoachCT.Type = (int)TypeCalendarEnum.LichTuan;
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

                //Get tuan le
                ViewBag.TuanLe = this.GetListWeekEdit(DateTimeExtensions.WeekOfYearISO8601(DateTime.ParseExact(keHoachCT.DenNgay,"dd/MM/yyyy", CultureInfo.InvariantCulture)));

                return View(listkeHoachCT[0]);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // POST: LichTuan/Edit/5
        [HttpPost]
        public ActionResult Edit(KeHoachCT keHoachCT, string[] thamdu, string[] chutri, string TuanLe)
        {
            try
            {
                string userName = CurrentUser.Name;
                try
                {
                    KeHoachCTBussinessService ketHoachCTBussinessService = new KeHoachCTBussinessService();
                    keHoachCT.ThamDu = thamdu;
                    keHoachCT.ChuTri = chutri;

                    int tuanLe = !string.IsNullOrEmpty(TuanLe) ? Int32.Parse(TuanLe) : DateTimeExtensions.WeekNumber(DateTime.Today);
                    var tuanLeData = DateTimeExtensions.FirstDateOfWeek(DateTime.Now.Year, tuanLe, System.Globalization.CultureInfo.CurrentCulture);

                    keHoachCT.TuNgayModel = tuanLeData;
                    keHoachCT.DenNgayModel = tuanLeData.AddDays(6);

                    keHoachCT.UserName = userName;
                    ketHoachCTBussinessService.EditKeHoachCT(keHoachCT);

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    throw ex;
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
        private List<SelectListItem> GetListWeek()
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
        private List<SelectListItem> GetListWeekEdit(int selected)
        {
            List<WeekOfYearModel> listWeekOfYearModel = DateTimeExtensions.GetWeekOfYear();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in listWeekOfYearModel)
            {
                if (item.IsWeek == (int)WeekEnum.ThisWeek)
                {
                    if (item.CodeWeek == selected)
                    {
                        items.Add(new SelectListItem { Text = "Tuần này (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = true });
                    }
                    else
                    {
                        items.Add(new SelectListItem { Text = "Tuần này (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = false });
                    }
                    goto Outer;
                }
                if (item.IsWeek == (int)WeekEnum.LastWeek)
                {
                    if (item.CodeWeek == selected)
                    {
                        items.Add(new SelectListItem { Text = "Tuần qua (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = true });
                    }
                    else
                    {
                        items.Add(new SelectListItem { Text = "Tuần qua (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = false });
                    }
                    goto Outer;
                }
                if (item.IsWeek == (int)WeekEnum.NextWeek)
                {
                    if (item.CodeWeek == selected)
                    {
                        items.Add(new SelectListItem { Text = "Tuần tới (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = false });
                    }
                    else
                    {
                        items.Add(new SelectListItem { Text = "Tuần tới (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = false });
                    }
                    goto Outer;
                }
                else
                {
                    if (item.CodeWeek == selected)
                    {
                        items.Add(new SelectListItem { Text = "Tuần " + item.CodeWeek + " (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = true });
                    }
                    else
                    {
                        items.Add(new SelectListItem { Text = "Tuần " + item.CodeWeek + " (" + item.NameWeek + ")", Value = item.CodeWeek.ToString(), Selected = false });
                    }
                }
                Outer:
                continue;

            }
            return items;
        }
        #region GetJSON
        public JsonResult GetJson(JQueryDataTableParamModel param)
        {
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
            param.Type = (int)TypeCalendarEnum.LichTuan;
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
