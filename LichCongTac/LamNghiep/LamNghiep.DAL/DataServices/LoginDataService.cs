using LamNghiep.Common;
using LamNghiep.DTO;
using LamNghiep.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.DAL.DataServices
{
    public class LoginDataService : ILoginDataService
    {
        public List<User> CheckLogin(User user)
        {
            const string storeName = "st_CheckLogin";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@UserName",user.UserName),
                    new SqlParameter("@Password",user.Password)
                };

                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<User>(reader);

            }
        }



        public IList<User> FindAll()
        {
            throw new NotImplementedException();
        }

        public int Add(User aggregate)
        {
            throw new NotImplementedException();
        }

        public int Save(User aggregate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
