using LamNghiep.BUL.Bussiness;
using LamNghiep.Common;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LamNghiep.Controllers
{
    public class CTCaNhanController : AppController
    {
        #region ActionResult
        [Authorize(Roles = "User, Admin")]
        public ActionResult Index()
        {
            ViewBag.TuanLe = this.GetListWeek();
            return View();
        }
       [Authorize(Roles = "User, Admin")]
        public ActionResult MyCalendar(string search)
        {
            DateTime startDate = DateTimeExtensions.FirstDateOfWeek(DateTime.Now.Year, DateTimeExtensions.WeekNumber(DateTime.Today), System.Globalization.CultureInfo.CurrentCulture);
            DateTime endDate = startDate.AddDays(6);
            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;
            ViewBag.search = search;
            return View();
        }
        public ActionResult Details()
        {
            return View();
        }

        //
        // GET: /CTCaNhan/Create
         [Authorize(Roles = "User, Admin")]
        public ActionResult Create()
        {
            KeHoachCT keHoachCT = new KeHoachCT();
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            keHoachCT.ListPhongBan = phongBanBussinessService.GetAllNguoiDung();
            keHoachCT.ListPhongBanDistinct = phongBanBussinessService.GetNguoiDungDistinct(keHoachCT.ListPhongBan);
            keHoachCT.MucDo = 3;
            return View(keHoachCT);
        }
        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public ActionResult Create(KeHoachCT keHoachCT, string[] thamdu, string[] chutri)
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
                ketHoachCTBussinessService.AddKeHoachCT(keHoachCT);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
       [Authorize(Roles = "User, Admin")]
        public ActionResult ViewTuanToi()
        {
            JQueryDataTableParamModel param = new JQueryDataTableParamModel();
            param.CanBo = StringExtensions.GetVariableSql(CurrentUser.Name);
            param.StartDate = DateTimeExtensions.FirstDateOfWeek(DateTime.Now.Year, DateTimeExtensions.WeekNumber(DateTime.Today) + 1, System.Globalization.CultureInfo.CurrentCulture);
            param.EndDate = param.StartDate.AddDays(6);

            KeHoachCTBussinessService keHoachCTBussinessService = new KeHoachCTBussinessService();
            List<KeHoachCongTacModel> listKeHoachCT = keHoachCTBussinessService.GetKeHoachCongTac(param);
            return View(listKeHoachCT);
        }
        [Authorize(Roles = "User, Admin")]
        public ActionResult ViewTuanNay()
        {
            JQueryDataTableParamModel param = new JQueryDataTableParamModel();
            param.CanBo = StringExtensions.GetVariableSql(CurrentUser.Name);
            param.StartDate = DateTimeExtensions.FirstDateOfWeek(DateTime.Now.Year, DateTimeExtensions.WeekNumber(DateTime.Today), System.Globalization.CultureInfo.CurrentCulture);
            param.EndDate = param.StartDate.AddDays(6);

            KeHoachCTBussinessService keHoachCTBussinessService = new KeHoachCTBussinessService();
            List<KeHoachCongTacModel> listKeHoachCT = keHoachCTBussinessService.GetKeHoachCongTac(param);
            return View(listKeHoachCT);
        }
        [Authorize(Roles = "User, Admin")]
        public ActionResult ViewThamDuChuTri()
        {
            JQueryDataTableParamModel param = new JQueryDataTableParamModel();
            param.CanBo = CurrentUser.Name;
            param.StartDate = DateTimeExtensions.FirstDateOfWeek(DateTime.Now.Year, DateTimeExtensions.WeekNumber(DateTime.Today), System.Globalization.CultureInfo.CurrentCulture);
            param.EndDate = param.StartDate.AddDays(6);

            KeHoachCTBussinessService keHoachCTBussinessService = new KeHoachCTBussinessService();
            List<KeHoachCongTacModel> listKeHoachCT = keHoachCTBussinessService.GetThamDuAndChuTri(param.CanBo, param.StartDate,param.EndDate);
            return View(listKeHoachCT);
        }
        // POST: /CTCaNhan/Edit/5
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
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
                    null :keHoachCTBussinessService.GetKhachMoiByIdKeHoach(id).FindAll(t => t.Kieu == 1);
                //ChuTri : type =2
                keHoachCT.ListKhachMoiChuTri = keHoachCTBussinessService.GetKhachMoiByIdKeHoach(id) == null ?
                  null : keHoachCTBussinessService.GetKhachMoiByIdKeHoach(id).FindAll(t => t.Kieu == 2);
                
                //Check and fill selected for User in PhongBan
                //ThamDu : type =1
                keHoachCT.ListPhongBanThamDu = new List<PhongBan>();
                List<PhongBan>  listPhongBanThamDuOld = keHoachCT.ListPhongBan;

                List<PhongBan> listPhongBanChuTriOld = ListExtensions.DeepCopy(listPhongBanThamDuOld);

                List<ThamDuModel> listKhachMoiModel = listThamDuModel.FindAll(t => t.Kieu == 1);
                keHoachCT.ListPhongBanThamDu = phongBanBussinessService.CheckSelectedThamDu(listPhongBanThamDuOld, listKhachMoiModel);
                //ChuTri : type =2
                keHoachCT.ListPhongBanChuTri = new List<PhongBan>();
               
                List<ThamDuModel>  listChutriModel = listThamDuModel.FindAll(t => t.Kieu == 2);
                keHoachCT.ListPhongBanChuTri = phongBanBussinessService.CheckSelectedThamDu(listPhongBanChuTriOld, listChutriModel);
               

                return View(listkeHoachCT[0]);
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [Authorize(Roles = "User, Admin")]
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

                    return RedirectToAction("MyCalendar");
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
        //
        // GET: /CTCaNhan/Delete/5
        [Authorize(Roles = "User, Admin")]
        public JsonResult Delete(int id)
        {
            KeHoachCTBussinessService keHoachCTBussinessService = new KeHoachCTBussinessService();
            keHoachCTBussinessService.DeleteKeHoachCongTac(id);
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion



        #region JSON RESULT

        public JsonResult GetJsonMyCalendar(JQueryDataTableParamModel param)
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

            if (param.StartDate != null && param.EndDate != null)
            {
                param.StartDate = DateTimeExtensions.FirstDateOfWeek(DateTime.Now.Year, DateTimeExtensions.WeekNumber(DateTime.Today), System.Globalization.CultureInfo.CurrentCulture);
                param.EndDate = param.StartDate.AddDays(6);
            }
            if (string.IsNullOrEmpty(param.sSearch))
            {
                param.StartDate = Convert.ToDateTime("01/01/1990");
                param.EndDate = Convert.ToDateTime("01/01/2017");
            }
            List<KeHoachCT> listKeHoachCT = keHoachCTBussinessService.GetKeHoachCaNhan(param);
            int totalRecords = keHoachCTBussinessService.GetCountListKeHoachCaNhan(param); ;
            // return jon datatable
            return Json(new
            {
                sEcho = param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = listKeHoachCT
            }, JsonRequestBehavior.AllowGet);
        }

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
            param.CanBo = StringExtensions.GetVariableSql(CurrentUser.Name);

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
        #endregion

        #region Unilities

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
        #endregion

        public ActionResult GeneratePDF()
        {
            return new Rotativa.ActionAsPdf("Index");
        }
        //

    }
}
