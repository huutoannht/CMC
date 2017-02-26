using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.Common
{
    public static class StringExtensions
    {
        public static string GetVariableSql(string variable)
        {
            return "'" + variable + "'";
        }
        public static string GetStringListUser(string[] arrUser)
        {
            StringBuilder stbPhongBan = new StringBuilder();
            foreach (var item in arrUser)
            {
                stbPhongBan.Append("'" + item + "',");
            }
            string sLanhDao = stbPhongBan.ToString().Substring(0, stbPhongBan.Length - 1);
            return sLanhDao;
        }
        public static string GetListPhongBanOrLanhDao(List<PhongBan> listPhongBan)
        {
            StringBuilder stbPhongBan = new StringBuilder();
            foreach (var item in listPhongBan)
            {
                stbPhongBan.Append("'" + item.UserName + "',");
            }
            string sList = stbPhongBan.ToString().Substring(0, stbPhongBan.Length - 1);
            return sList;
        }
        public static string GetCanBo(string canBo)
        {
            if (!string.IsNullOrEmpty(canBo) && canBo != "null")
            {
                string[] arrayCanBo = canBo.Split(',');
                StringBuilder stbCanBo = new StringBuilder();
                foreach (var item in arrayCanBo)
                {
                    stbCanBo.Append("'" + item + "',");
                }
                string sCanBo = stbCanBo.ToString().Substring(0, stbCanBo.Length - 1);
                return sCanBo;
            }
            return null;
        }
      
    }
}
