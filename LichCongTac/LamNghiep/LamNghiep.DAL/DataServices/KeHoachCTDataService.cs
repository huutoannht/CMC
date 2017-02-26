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
    public class KeHoachCTDataService : IKeHoachCTDataService
    {
        public List<KeHoachCT> GetKeHoachCongTac(JQueryDataTableParamModel param)
        {
            const string storeName = "st_GetKetHoachCongTac";
            string query = @"SELECT DATENAME(dw,TuNgay) as Ngay, [IdKeHoach]
	                      ,khct.TenKeHoach
	                      ,nd.HoTen
                          ,nd.ThuTu
                          ,khct.[UserName]
                          ,TuNgay AS ThoiGian
                          ,[KeHoachCongTac]
                           ,[TuGio] +[TuNgay] AS TuNgayModel
                          ,[DenGio]+[TuNgay] AS DenNgayModel
                          ,[DiaDiem]
	                      ,TuGio
	                      ,DenGio
	                      ,DenNgay AS DenNgayView
	                      ,cv.VietTat
	                      ,cv.TenChucVu as ChucVu
	                      ,CASE MucDo 
						  WHEN 1 THEN N'Rất quan trọng'
						  WHEN 2 THEN N'Quan trọng'
						  WHEN 3 THEN N'Bình thường'
						  END AS MucDoView
  FROM [KeHoachCongTac] khct  LEFT JOIN [dbo].[NguoiDung] nd
								  on khct.UserName=nd.UserName
							 LEFT JOIN ChucVu cv
								  on cv.IdChucVu=nd.ChucVu
								   ";

            @query = @query + " WHERE khct.IsDeleted=0 AND ((TuNgay BETWEEN '" + param.StartDate + "' AND '" + param.EndDate + "') OR "
                        + "(DenNgay BETWEEN '" + param.StartDate + "' AND '" + param.EndDate + "')) ";


            if (!string.IsNullOrEmpty(param.CanBo))
            {
                @query = @query + " AND  khct.UserName IN (" + param.CanBo + ")";
            }
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                param.sSearch = param.sSearch.Trim();
                @query = @query + " AND ( khct.TenKeHoach LIKE N'%" + param.sSearch +
                "%' OR khct.KeHoachCongTac LIKE N'%" + param.sSearch +
                 "%' OR khct.DiaDiem LIKE N'%" + param.sSearch
                 + "%' OR nd.HoTen LIKE N'%" + param.sSearch
                 + "%')";
            }

            @query = @query + " ORDER BY nd.ThuTu ASC ";

            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@Query",@query),
                   
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<KeHoachCT>(reader);
            }
        }
        public IList<KeHoachCT> FindAll()
        {
            throw new NotImplementedException();
        }

        public int Add(KeHoachCT keHoachCT)
        {
            const string storeName = "st_InsertKeHoachCT";
            const string storeNameThamDu = "st_InsertThamDu";
            int idKeHoach = 0;
            using (var conn = new AdoHelper())
            {
                conn.BeginTransaction();
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@TenKeHoach",keHoachCT.TenKeHoach),
                     new SqlParameter("@UserName",keHoachCT.UserName),
                     new SqlParameter("@TuNgay",keHoachCT.TuNgayModel),
                     new SqlParameter("@TuGio",keHoachCT.TuGio),
                     new SqlParameter("@DenGio",keHoachCT.DenGio),
                     new SqlParameter("@CaNgay",keHoachCT.CaNgay),
                     new SqlParameter("@DenNgay",keHoachCT.DenNgayModel),
                     new SqlParameter("@KeHoachCongTac",keHoachCT.KeHoachCongTac),
                     new SqlParameter("@DiaDiem",keHoachCT.DiaDiem),
                     new SqlParameter("@MucDo",keHoachCT.MucDo),
                     new SqlParameter("@LapLai",keHoachCT.LapLai),
                     new SqlParameter("@Deadline",keHoachCT.Deadline),
                     new SqlParameter("@Type",keHoachCT.Type),
                };
                idKeHoach = int.Parse(conn.ExecScalarProc(storeName, objectParam).ToString());
                if (keHoachCT.ThamDu != null && (keHoachCT.ThamDu.Count() > 0))
                {
                    foreach (var item in keHoachCT.ThamDu)
                    {
                        SqlParameter[] objectThamDu = new SqlParameter[]{
                        new SqlParameter("@IdKeHoach",idKeHoach),
                        new SqlParameter("@NguoiThamDu",item),
                        new SqlParameter("@Kieu",1),//Tham du
                    };
                        conn.ExecNonQueryProc(storeNameThamDu, objectThamDu);
                    }
                }
                if (keHoachCT.ChuTri!=null && (keHoachCT.ChuTri.Count() > 0))
                {
                    foreach (var item in keHoachCT.ChuTri)
                    {
                        SqlParameter[] objectThamDu = new SqlParameter[]{
                        new SqlParameter("@IdKeHoach",idKeHoach),
                        new SqlParameter("@NguoiThamDu",item),
                        new SqlParameter("@Kieu",2),//Chu Tri
                    };
                        conn.ExecNonQueryProc(storeNameThamDu, objectThamDu);
                    }
                }
                
                conn.Commit();
            }
            return idKeHoach;
        }

        public int Save(KeHoachCT keHoachCT)
        {
            const string storeName = "st_UpdateKeHoachCT";
            const string storeNameThamDu = "st_InsertThamDu";
            const string storeNameDeleteThamDu = "st_DeleteThamDu";
            int idKeHoach = 0;
            using (var conn = new AdoHelper())
            {
                conn.BeginTransaction();
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@IdKeHoach",keHoachCT.IdKeHoach),
                     new SqlParameter("@TenKeHoach",keHoachCT.TenKeHoach),
                     new SqlParameter("@UserName",keHoachCT.UserName),
                     new SqlParameter("@TuNgay",keHoachCT.TuNgayModel),
                     new SqlParameter("@TuGio",keHoachCT.TuGio),
                     new SqlParameter("@DenGio",keHoachCT.DenGio),
                     new SqlParameter("@DenNgay",keHoachCT.DenNgayModel),
                     new SqlParameter("@KeHoachCongTac",keHoachCT.KeHoachCongTac),
                     new SqlParameter("@DiaDiem",keHoachCT.DiaDiem),
                     new SqlParameter("@MucDo",keHoachCT.MucDo),
                     new SqlParameter("@LapLai",keHoachCT.LapLai),
                     new SqlParameter("@Deadline",keHoachCT.Deadline),
                };
                conn.ExecNonQueryProc(storeName, objectParam);

                SqlParameter[] objectDeleteThamDu = new SqlParameter[]{
                        new SqlParameter("@IdKeHoach",keHoachCT.IdKeHoach),
                    };
                conn.ExecNonQueryProc(storeNameDeleteThamDu, objectDeleteThamDu);
                if (keHoachCT.ThamDu != null && (keHoachCT.ThamDu.Count() > 0))
                {
                    foreach (var item in keHoachCT.ThamDu)
                    {
                        SqlParameter[] objectThamDu = new SqlParameter[]{
                        new SqlParameter("@IdKeHoach",keHoachCT.IdKeHoach),
                        new SqlParameter("@NguoiThamDu",item),
                        new SqlParameter("@Kieu",1),//Tham du
                    };
                        conn.ExecNonQueryProc(storeNameThamDu, objectThamDu);
                    }
                }
                if (keHoachCT.ChuTri != null && (keHoachCT.ChuTri.Count() > 0))
                {
                    foreach (var item in keHoachCT.ChuTri)
                    {
                        SqlParameter[] objectThamDu = new SqlParameter[]{
                        new SqlParameter("@IdKeHoach",keHoachCT.IdKeHoach),
                        new SqlParameter("@NguoiThamDu",item),
                        new SqlParameter("@Kieu",2),//Chu Tri
                    };
                        conn.ExecNonQueryProc(storeNameThamDu, objectThamDu);
                    }
                }
                conn.Commit();
            }
            return idKeHoach;
        }

        public bool Delete(KeHoachCT aggregate)
        {
            throw new NotImplementedException();
        }

        public List<KeHoachCT> GetThamDuAndChuTri(string userName,DateTime startDate,DateTime endDate)
        {
            const string storeName = "st_getThamDuAndChuTri";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@UserName",userName),
                     new SqlParameter("@StartDate",startDate),
                     new SqlParameter("@EndDate",endDate),
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<KeHoachCT>(reader);
            }
        }
        public List<KeHoachCT> GetKeHoachCaNhan(JQueryDataTableParamModel param)
        {
            const string storeName = "st_getKeHoachCaNhan";
            
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@DisplayLength",param.iDisplayLength),
                     new SqlParameter("@DisplayStart",param.iDisplayStart),
                     new SqlParameter("@SortCol",param.ISortCol_0),
                     new SqlParameter("@SortDir",param.SSortDir_0),
                     new SqlParameter("@Search",param.sSearch),
                     new SqlParameter("@UserName",param.CanBo),
                     new SqlParameter("@StartDate",param.StartDate),
                     new SqlParameter("@EndDate",param.EndDate),
                     new SqlParameter("@Type",param.Type)
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<KeHoachCT>(reader);
            }
        }
        public int GetCountListKeHoachCaNhan(JQueryDataTableParamModel param)
        {
            const string storeName = "st_getCountListKeHoachCaNhan";

            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@Search",param.sSearch),
                     new SqlParameter("@UserName",param.CanBo),
                     new SqlParameter("@StartDate",param.StartDate),
                     new SqlParameter("@EndDate",param.EndDate),
                };
                return int.Parse(conn.ExecScalarProc(storeName, objectParam).ToString());
            }
        }
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

        public List<KeHoachCT> GetKeHoachById(int id)
        {
            const string storeName = "st_getKeHoachById";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@Id",id),
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<KeHoachCT>(reader);
            }
        }

        public List<KeHoachCT> GetLichCongTac()
        {
            const string storeName = "st_getKeHoach";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<KeHoachCT>(reader);
            }
        }
        public List<KeHoachCT> GetKeHoachForUpdate(int id)
        {
            const string storeName = "st_getForUpdateKeHoach";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@Id",id),
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<KeHoachCT>(reader);
            }
        }
        public List<KhachMoiModel> GetKhachMoiByIdKeHoach(int id)
        {
            const string storeName = "st_getKhachMoiByIdKeHoach";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@IdKeHoach",id),
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<KhachMoiModel>(reader);
            }
        }
        public List<ThamDuModel> GetThamDuByIdKeHoach(int id)
        {
            const string storeName = "st_getThamDuByIdKeHoach";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                     new SqlParameter("@IdKeHoach",id),
                };
                SqlDataReader reader = conn.ExecDataReaderProc(storeName, objectParam);
                // convert table result to List object and return
                return DataReaderExtensions.DataReaderToObjectList<ThamDuModel>(reader);
            }
        }
        public int DeleteKeHoachCongTac(int id)
        {
            const string storeName = "st_deleteKeHoach";
            using (var conn = new AdoHelper())
            {
                SqlParameter[] objectParam = new SqlParameter[]{
                    new SqlParameter("@IdKeHoach",id)
                };
                return conn.ExecNonQueryProc(storeName, objectParam);
            }
        }
    }
}
