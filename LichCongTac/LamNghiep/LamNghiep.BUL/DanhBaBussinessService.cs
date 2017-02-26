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
    public class DanhBaBussinessService 
    {
        public int CapNhatDanhBa(DanhBa danhBa)
        {
            DanhBaDataService danhBaData = new DanhBaDataService();
            return danhBaData.Save(danhBa);
        }
        public List<DanhBa> GetDanhBa()
        {
            DanhBaDataService danhBaData = new DanhBaDataService();
            return danhBaData.GetDanhBa();
        }

    }
}
