using LamNghiep.BUL.Bussiness;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LamNghiep.Controllers
{
    public class DanhBaController : Controller
    {
        //
        // GET: /DanhBa/
        [Authorize(Roles = "User, admin")]
        public ActionResult Index()
        {
            DanhBaBussinessService danhBaBussinessService = new DanhBaBussinessService();
            List<DanhBa> listDanhBa=danhBaBussinessService.GetDanhBa();
            if (listDanhBa.Count>0)
            {
                ViewBag.NoiDung = listDanhBa[0].NoiDung;
                ViewBag.Id = listDanhBa[0].Id;
            }
            return View();
        }
        [Authorize(Roles = "User, Admin")]
        public ActionResult Create()
        {

            DanhBaBussinessService danhBaBussinessService = new DanhBaBussinessService();
            List<DanhBa> listDanhBa = danhBaBussinessService.GetDanhBa();
            if (listDanhBa.Count > 0)
            {
                ViewBag.NoiDung = listDanhBa[0].NoiDung;
                ViewBag.Id = listDanhBa[0].Id;
            }
            return View();
        }
        //
        // POST: /DanhBa/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(DanhBa danhba)
        {
            try
            {
                DanhBaBussinessService danhBaBussinessService = new DanhBaBussinessService();
                danhba.UserName = System.Web.HttpContext.Current.User.Identity.Name ;
                danhBaBussinessService.CapNhatDanhBa(danhba);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
    }
}
