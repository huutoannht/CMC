using LamNghiep.BUL.Bussiness;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace LamNghiep.Controllers
{
    public class HomeController : AppController
    {
        
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(CurrentUser.Name))
            {
                return RedirectToAction("LamNghiep");
            }
            ViewBag.IsAdmin = CurrentUser.IsInRole("Admin");
            return View();
        }

        public ActionResult LamNghiep()
        {
            
            return View();
        }

      

        public ActionResult NavList()
        {
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            var listPhongBan = phongBanBussinessService.GetAllNguoiDung();
            string IdPhongBan = CurrentUser.Claims.Where(c=>c.Type== "IdPhongBan").FirstOrDefault().Value.ToString();
            string maChucVu = CurrentUser.Claims.Where(c => c.Type == "MaChucVu").FirstOrDefault().Value.ToString();
            if (maChucVu=="TP" || maChucVu == "CVP")
            {

            }
            var listPhongBanDistinct = phongBanBussinessService.GetNguoiDungDistinct(listPhongBan);
            ViewBag.ListPhongBan = listPhongBan;
            ViewBag.ListPhongBanDistinct = listPhongBanDistinct;
            return View();
        }
    }
}