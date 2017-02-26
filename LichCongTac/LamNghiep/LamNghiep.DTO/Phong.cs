using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.DTO
{
   public class Phong
    {
        public int Id { get; set; }
        public string TenPhong { get; set; }
        public bool TrangThai { get; set; }

        public string NguoiDat { get; set; }

        public string MaPhong { get; set; }

        public string TuGio { get; set; }

        public string DenGio { get; set; }

        public DateTime Ngay { get; set; }

        public string NgayView { get; set; }

        public string HoTen { get; set; }

        public string CuocHop { get; set; }
        public string NoiDung { get; set; }

        public string Color { get; set; }

        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }

        public List<Phong> ListPhong { get; set; }
    }
}
