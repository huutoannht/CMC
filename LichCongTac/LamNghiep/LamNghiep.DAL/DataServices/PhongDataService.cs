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
    public class PhongDataService : IPhongDataService
    {
        public List<Phong> GetPhongHop()
        {
            const string storeName = "st_getPhong";
            using (var conn = new AdoHelper())
            {
                 SqlParameter[] objectParam = new SqlParameter[]{
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return  DataReaderExtensions.DataReaderToObjectList<Phong>(reader);
            }
        }
        public List<Phong> GetPhongHopTheoNgay(DateTime ngay, int id)
        {
            const string storeName = "st_getPhongTheoNgay";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@Ngay",ngay),
                     new SqlParameter("@Id",id),
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<Phong>(reader);
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

        public int DatPhong(Phong phong)
        {
            const string storeName = "st_DatPhong";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@MaPhong",phong.Id),
                    new SqlParameter("@TuGio",phong.TuGio),
                    new SqlParameter("@DenGio",phong.DenGio),
                    new SqlParameter("@Ngay",phong.Ngay),
                    new SqlParameter("@CuocHop",phong.CuocHop),
                    new SqlParameter("@NoiDung",phong.NoiDung),
                    new SqlParameter("@NguoiDat",phong.NguoiDat),
                };
                return conn.ExecNonQueryProc(storeName, objectParam);
            }
        }

        public int CheckDatPhong(Phong phong)
        {
            const string storeName = "st_CheckDatPhong";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@MaPhong",phong.Id),
                    new SqlParameter("@TuGio",phong.TuGio),
                    new SqlParameter("@DenGio",phong.DenGio),
                    new SqlParameter("@Ngay",phong.Ngay)
                };
                return Int32.Parse(conn.ExecScalarProc(storeName, objectParam).ToString());
            }
        }

        public List<Phong> GetPhongByUser(string user)
        {
            const string storeName = "st_GetDatPhongByUser";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@User",user)
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<Phong>(reader);
            }
        }

        public List<Phong> GetInfoPhong()
        {
            const string storeName = "st_GetInfoPhong";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<Phong>(reader);
            }
        }
    }
}
