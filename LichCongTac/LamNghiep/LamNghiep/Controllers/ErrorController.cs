using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LamNghiep.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult PageNotFound()
        {
            return View();
        }
        public ActionResult ErrorPage()
        {
            return View();
        }
        public ActionResult Denied()
        {
            return View();
        }
	}
}