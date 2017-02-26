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
    public class DanhBaDataService : IDanhBaDataService
    {
        public List<DanhBa> GetDanhBa()
        {
            const string storeName = "st_getDanhBa";
            using (var conn = new AdoHelper())
            {
                 SqlParameter[] objectParam = new SqlParameter[]{
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return  DataReaderExtensions.DataReaderToObjectList<DanhBa>(reader);
            }
        }
        public int Save(DanhBa danhBa)
        {
            const string storeName = "st_updateDanhBa";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@NoiDung",danhBa.NoiDung),
                     new SqlParameter("@UserName",danhBa.UserName),
                     new SqlParameter("@Id",danhBa.Id),
                };
                return conn.ExecNonQueryProc(storeName, objectParam);
            }
        }



        public IList<DanhBa> FindAll()
        {
            throw new NotImplementedException();
        }

        public int Add(DanhBa aggregate)
        {
            throw new NotImplementedException();
        }

        

        public bool Delete(DanhBa aggregate)
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
