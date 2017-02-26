using LamNghiep.Common;
using LamNghiep.Common.EnumData;
using LamNghiep.DTO;
using LamNghiep.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.DAL.DataServices
{
    public class PhongBanDataService : IPhongBanDataService
    {


        public IList<KeHoachCT> FindAll()
        {
            throw new NotImplementedException();
        }

        public int Add(KeHoachCT keHoachCT)
        {
            const string storeName = "st_InsertKeHoachCT";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@TenKeHoach",keHoachCT.TenKeHoach),
                     new SqlParameter("@UserName",keHoachCT.UserName),
                     new SqlParameter("@TuNgay",keHoachCT.TuNgay),
                     new SqlParameter("@DenNgay",keHoachCT.DenNgay),
                     new SqlParameter("@KeHoachCongTac",keHoachCT.KeHoachCongTac),
                     new SqlParameter("@ThamDu",keHoachCT.ThamDu),
                     new SqlParameter("@DiaDiem",keHoachCT.DiaDiem),
                     new SqlParameter("@MucDo",keHoachCT.MucDo),
                     new SqlParameter("@LapLai",keHoachCT.LapLai),
                };
                return conn.ExecNonQueryProc(storeName, objectParam);
            }
        }

        public int Save(KeHoachCT aggregate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(KeHoachCT aggregate)
        {
            throw new NotImplementedException();
        }

        //public List<KeHoachCT> GetKeHoachCaNhan(string userName)
        //{
        //    const string storeName = "st_getKeHoachCaNhan";
        //    using (var conn = new AdoHelper())
        //    {
        //        SqlParameter[] objectParam = new SqlParameter[]{
        //             new SqlParameter("@UserName",userName),
        //        };
        //        SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
        //        // convert table result to List object and return
        //        return DataReaderExtensions.DataReaderToObjectList<KeHoachCT>(reader);
        //    }
        //}

        public List<KeHoachCT> GetKeHoachCanBo(string userName)
        {
            const string storeName = "st_getKeHoachCanBo";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@UserName",userName),
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<KeHoachCT>(reader);
            }
        }
        public List<PhongBan> GetNguoiDungPhongBan()
        {
            const string storeName = "st_getNguoiDungPhongBan";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<PhongBan>(reader);
            }
        }
        public List<PhongBan> GetNguoiDungPhongBanById(string userName)
        {
            const string storeName = "st_getNguoiDungPhongBan";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@UserName",userName),
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<PhongBan>(reader);
            }
        }
        public List<PhongBan> GetAllNguoiDung()
        {
            const string storeName = "st_getAllNguoiDung";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<PhongBan>(reader);
            }
        }
        public List<PhongBan> GetNguoiDungLanhDao()
        {
            const string storeName = "st_getNguoiDungLanhDao";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<PhongBan>(reader);
            }
        }
        public List<KeHoachCT> GetCurWeekKHCTPhongBan()
        {
            throw new NotImplementedException();
        }

        public List<PhongBan> GetAllPhongBan()
        {
            const string storeName = "st_getTenPhongBan";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<PhongBan>(reader);
            }
        }

        public List<ChucVuModel> GetChucVuTheoPhongBan(int idPhongBan)
        {
            const string storeName = "st_getChucVuTheoPhongBan";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@IdPhongBan",idPhongBan)
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<ChucVuModel>(reader);
            }
        }

        public int AddNguoiDung(User user)
        {
            const string storeName = "st_InsertNguoiDung";
            int role=(int)RoleEnum.User;
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@UserName",user.UserName),
                    new SqlParameter("@Password",user.Password),
                    new SqlParameter("@HoTen",user.HoTen),
                    new SqlParameter("@DiaChi",user.DiaChi),
                    new SqlParameter("@SoDT",user.SoDT),
                    new SqlParameter("@ChucVu",user.IdChucVu),
                    new SqlParameter("@CreateBy",user.CreateBy),
                    new SqlParameter("@Role",EnumExtensions.GetEnumDescription((RoleEnum) role))
                };
                return conn.ExecNonQueryProc(storeName, objectParam);
            }
        }

        public bool CheckUserName(string userName)
        {
            const string storeName = "st_checkUserName";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@UserName",userName)
                };
                return conn.ExecScalarProc(storeName, objectParam).ToString()=="1";
            }
        }

        public List<User> GetListNguoiDung(JQueryDataTableParamModel param)
        {
            const string query = @"st_getNguoiDung";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@DisplayLength",param.iDisplayLength),
                      new SqlParameter("@DisplayStart",param.iDisplayStart),
                      new SqlParameter("@SortCol",param.ISortCol_0),
                      new SqlParameter("@SortDir",param.SSortDir_0),
                      new SqlParameter("@Search",param.sSearch),
                };
                SqlDataReader reader = conn.ExecDataReaderProc(query, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<User>(reader);
            }
        }
        public int GetCountListNguoiDung(JQueryDataTableParamModel param)
        {
            const string query = @"st_getCountListNguoiDung";
            using (var conn = new AdoHelper())
            {

                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@Search",param.sSearch==null? (object)DBNull.Value:param.sSearch) 
                };
                return int.Parse(conn.ExecScalar(query, objectParam).ToString());
            }
        }

        public List<User> getNguoiDungByUserName(string userName)
        {
            const string storeName = "st_getNguoiDungByUserName";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@UserName",userName)
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<User>(reader);
            }
        }

        public int EditNguoiDung(User user)
        {
            const string storeName = "st_UpdateNguoiDung";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@UserName",user.UserName),
                    new SqlParameter("@HoTen",user.HoTen),
                    new SqlParameter("@DiaChi",user.DiaChi),
                    new SqlParameter("@SoDT",user.SoDT),
                    new SqlParameter("@ChucVu",user.IdChucVu),
                    new SqlParameter("@UpdateBy",user.UpdateBy)
                };
                return conn.ExecNonQueryProc(storeName, objectParam);
            }
        }

        public bool DeleteNguoiDung(string userName)
        {
            const string storeName = "st_DeleteNguoiDungByUserName";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@UserName",userName)
                };
                int result= conn.ExecNonQueryProc(storeName, objectParam);
                return (result == 1);
            }
        }

        public bool ChangePassWord(string userName, string passWord,string updateBy)
        {
            const string storeName = "st_UpdateNguoiDungPassword";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@UserName",userName),
                    new SqlParameter("@PassWord",passWord),
                    new SqlParameter("@UpdateBy",updateBy)
                };
                int result = conn.ExecNonQueryProc(storeName, objectParam);
                return (result == 1);
            }
        }
    }
}
