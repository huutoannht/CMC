using LamNghiep.BUL.Bussiness;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LamNghiep.Controllers
{
    
    public class NguoiDungController : AppController
    {
        //
        // GET: /NguoiDung/
        [AdminAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AdminAuthorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.PhongBan = this.GetPhongBan();
            return View();
        }
        [HttpPost]
        [AdminAuthorize(Roles = "Admin")]
        public ActionResult Create(User user)
        {
            user.CreateBy = this.CurrentUser.Name;
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            phongBanBussinessService.AddNguoiDung(user);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [AdminAuthorize(Roles = "Admin")]
        public ActionResult Edit(string userName)
        {
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            List<User> listUser=phongBanBussinessService.getNguoiDungByUserName(userName);
            if (listUser==null||listUser.Count==0)
            {
               return HttpNotFound();
            }
            ViewBag.PhongBan = this.GetPhongBan(listUser[0].IdPhongBan);
            ViewBag.IdChucVu = this.GetChucVu(listUser[0].IdChucVu, listUser[0].IdPhongBan);
            return View(listUser[0]);
        }
        [HttpPost]
        [AdminAuthorize(Roles = "Admin")]
        public ActionResult Edit(User user)
        {
            user.UpdateBy = this.CurrentUser.Name;
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            phongBanBussinessService.EditNguoiDung(user);

            return RedirectToAction("Index");
        }
        [HttpGet]
        [AdminAuthorize(Roles = "Admin")]
        public JsonResult Delete(string userName)
        {
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            bool check = phongBanBussinessService.DeleteNguoiDung(userName);
            return Json(!check, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChucVuTheoPhongBan(int id)
        {
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            List<ChucVuModel> listChucVu = phongBanBussinessService.GetChucVuTheoPhongBan(id);
            return Json(listChucVu, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckUserName(string UserName)
        {
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            bool check= phongBanBussinessService.CheckUserName(UserName);
            return Json(!check, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChangePassWord(string userName,string passWord)
        {
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            bool check = phongBanBussinessService.ChangePassWord(userName, passWord,this.CurrentUser.Name);
            return Json(!check, JsonRequestBehavior.AllowGet);
        }
        private List<SelectListItem> GetPhongBan(int selected=-1)
        {
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            List<PhongBan> listPhongBan = phongBanBussinessService.GetAllPhongBan();
            List<SelectListItem> listItemPhongBan = new List<SelectListItem>();
            foreach (var item in listPhongBan)
            {
                if (item.MaPhongBan == selected)
                {
                    listItemPhongBan.Add(new SelectListItem { Text = item.TenPhongBan, Value = item.MaPhongBan.ToString(),Selected=true });
                }
                else
                {
                    listItemPhongBan.Add(new SelectListItem { Text = item.TenPhongBan, Value = item.MaPhongBan.ToString() });
                }
               
            }
            return listItemPhongBan;
        }
        private List<SelectListItem> GetChucVu(int selected ,int idPhongBan)
        {
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            List<ChucVuModel> listChucVu = phongBanBussinessService.GetChucVuTheoPhongBan(idPhongBan);
            List<SelectListItem> listItemChucVu= new List<SelectListItem>();
            foreach (var item in listChucVu)
            {
                if (item.IdChucVu == selected)
                {
                    listItemChucVu.Add(new SelectListItem { Text = item.TenChucVu, Value = item.IdChucVu.ToString(), Selected = true });
                }
                else
                {
                    listItemChucVu.Add(new SelectListItem { Text = item.TenChucVu, Value = item.IdChucVu.ToString() });
                }

            }
            return listItemChucVu;
        }
        #region Json result

        public JsonResult GetJsonNguoiDung(JQueryDataTableParamModel param)
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

            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();


            List<User> listUser = phongBanBussinessService.GetListNguoiDung(param);
            int totalRecords = 0;

            totalRecords = phongBanBussinessService.GetCountListNguoiDung(param);

            // return jon datatable
            return Json(new
            {
                sEcho = param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = listUser
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}