using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.DTO
{
    [Serializable]
    public class PhongBan
    {
        public string UserName { get; set; }
        public int MaPhongBan { get; set; }
        public string HoTen { get; set; }
        public string TenPhongBan { get; set; }

        public string TenChucVu { get; set; }

        public bool Selected { get; set; }
    }
}
