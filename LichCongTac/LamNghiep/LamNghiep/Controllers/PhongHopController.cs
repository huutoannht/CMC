using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LamNghiep.BUL.Bussiness;
using LamNghiep.DTO;
using System.Globalization;

namespace LamNghiep.Controllers
{
    public class PhongHopController : Controller
    {
        //
        // GET: /DatPhong/
        public ActionResult Index(string ngay)
        
        {
            DateTime myDate = DateTime.Now;
            try
            {
                 myDate = DateTime.ParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {

                myDate = DateTime.Today;
            }
           
            PhongBussinessService phongBussinessService = new PhongBussinessService();
            List<Phong> listPhong = phongBussinessService.GetPhong(myDate);
            ViewBag.Ngay = myDate.ToString("dd/MM/yyyy");
            return View(listPhong);
        }

        //
        // GET: /DatPhong/Details/5
        public ActionResult GetDatPhongByUser()
        {
            return View();
        }
        public ActionResult Details()
        {
            return View();
        }

        //
        // GET: /DatPhong/Details/5
        public JsonResult GetDatPhongByUserJson(JQueryDataTableParamModel param)
        {
            PhongBussinessService phongBussinessService = new PhongBussinessService();
            string user = "admin";
            List<Phong> listPhong = phongBussinessService.GetPhongByUser(user);

            int sortColumnIndex = param.ISortCol_0;
            string order = param.SSortDir_0;

            string orderBy = string.Empty;

            switch (sortColumnIndex)
            {
                case 0:
                    orderBy = "TePhong";
                    break;
                case 1:
                    orderBy = "Ngay";
                    break;

            }
            foreach (var item in listPhong)
            {
                item.TuGio = item.TuGio + " - " + item.DenGio;
            }


            int totalRecords = 0;

            totalRecords = 1;

            // return jon datatable
            return Json(new
            {
                sEcho = param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = listPhong
            }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /DatPhong/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DatPhong/Create
        [HttpPost]
        public JsonResult Create(string tugio, string dengio, string ngay, string maphong,string cuochop, string noidung)
        {
            try
            {
                PhongBussinessService phongBussinessService = new PhongBussinessService();
                Phong phong=new Phong();
                phong.Id = Int32.Parse(maphong);
                phong.NguoiDat="admin";
                phong.TuGio = tugio;
                phong.DenGio = dengio;
                phong.CuocHop = cuochop;
                phong.NoiDung = noidung;

                DateTime myDate = DateTime.ParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                phong.Ngay = myDate;
                phongBussinessService.DatPhong(phong);
                 return Json("OK", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("NoOK", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult CheckDatPhong(string tugio, string dengio, string ngay, string maphong)
        {
            try
            {
                PhongBussinessService phongBussinessService = new PhongBussinessService();
                Phong phong = new Phong();
                phong.Id = Int32.Parse(maphong);
                phong.NguoiDat = "admin";
                phong.TuGio = tugio;
                phong.DenGio = dengio;

                DateTime myDate = DateTime.ParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                phong.Ngay = myDate;
                int result=phongBussinessService.CheckDatPhong(phong);
                if (result ==0)
                {
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("NoOK", JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("NotOK", JsonRequestBehavior.AllowGet);
            }
        }
        //
        // GET: /DatPhong/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DatPhong/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DatPhong/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DatPhong/Delete/5
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
    }
}
