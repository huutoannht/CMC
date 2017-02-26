using LamNghiep.BUL.Bussiness;
using LamNghiep.DTO;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace LamNghiep.Controllers
{
    public class DangNhapController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user)
        {
            LoginBussinessService loginBussinessService = new LoginBussinessService();
            List<User> listUser = loginBussinessService.CheckLogin(user);
            if (listUser == null || listUser.Count == 0)
            {
                return View(user);
            }
            else
            {
                var ident = new ClaimsIdentity(
          new[] { 
              // adding following 2 claim just for supporting default antiforgery provider
              new Claim(ClaimTypes.NameIdentifier, user.UserName),
              new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
              new Claim(ClaimTypes.Actor,user.UserName),
              // optionally you could add roles if any
              new Claim(ClaimTypes.Role, listUser[0].Role),
              new Claim("IdPhongBan", listUser[0].IdPhongBan.ToString()),
              new Claim("MaChucVu", listUser[0].VietTat),
          },
          DefaultAuthenticationTypes.ApplicationCookie);
                HttpContext.GetOwinContext().Authentication.SignIn(
                   new AuthenticationProperties { IsPersistent = false }, ident);
                return RedirectToAction("Index", "Home");
            }

        }
        //
        // GET: /Login/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Thoat()
        {
            //Removing Session
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


    }
}
