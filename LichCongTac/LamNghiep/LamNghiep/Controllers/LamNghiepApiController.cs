using LamNghiep.BUL.Bussiness;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace LamNghiep.Controllers
{
    public class LamNghiepApiController : ApiController
    {

        [AcceptVerbs("GetPhongHop")]
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public object GetPhongHop()
        {
            PhongBussinessService phongBussinessService = new PhongBussinessService();
            return phongBussinessService.GetInfoPhong();
        }
        [AcceptVerbs("GetLichCongTac")]
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public object GetLichCongTac()
        {
            KeHoachCTBussinessService ketHoachCTBussinessService = new KeHoachCTBussinessService();
            return ketHoachCTBussinessService.GetLichCongTac();
        }
        [AcceptVerbs("GetLichCongTacCaNhan")]
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public object GetLichCongTacCaNhan()
        {
            JQueryDataTableParamModel param = new JQueryDataTableParamModel();
            var identity =  (ClaimsIdentity)User.Identity;
            string username = identity.Claims.First().Value;
            PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
            param.CanBo = username;
            param.StartDate = DateTime.Today.AddYears(-1);
            param.EndDate = DateTime.Today.AddYears(1);
            return phongBanBussinessService.GetLichCaNhan(param);
        }
    }
}