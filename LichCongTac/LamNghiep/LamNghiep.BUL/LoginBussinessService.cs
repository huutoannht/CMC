using LamNghiep.DAL.DataServices;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.BUL.Bussiness
{
    public class LoginBussinessService 
    {
        public List<User> CheckLogin(User user)
        {
            LoginDataService loginData = new LoginDataService();
            return loginData.CheckLogin(user);
        }

       
    }
}
