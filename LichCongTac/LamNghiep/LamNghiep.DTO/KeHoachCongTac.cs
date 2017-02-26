using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LamNghiep.DTO
{
    public class KeHoachCT
    {
        public int IdKeHoach { get; set; }
        public string Ngay { get; set; }
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Xin nhập tên kế hoạch.")]
        public string TenKeHoach { get; set; }
        public int ThuTu { get; set; }
        [Required(ErrorMessage = "Xin nhập ngày bắt đầu.")]
        public string TuNgay { get; set; }
        [Required(ErrorMessage = "Xin nhập ngày kết thúc.")]
        public string DenNgay { get; set; }

        public DateTime TuNgayModel { get; set; }
        public DateTime DenNgayModel { get; set; }

        [Required(ErrorMessage = "Xin nhập hạn hoàn thành.")]
        public DateTime Deadline { get; set; }

        public DateTime DenNgayView { get; set; }

        [Required(ErrorMessage = "Xin nhập nội dung công tác.")]
        public string KeHoachCongTac { get; set; }
        public string[] ThamDu { get; set; }
        public string[] ChuTri { get; set; }

        public string ChuTriModel { get; set; }
        public string KhachThamDu { get; set; }
        [Required(ErrorMessage = "Xin nhập địa điểm.")]
        public string DiaDiem { get; set; }
        public string ChucVu { get; set; }

        public DateTime ThoiGian { get; set; }
        public TimeSpan Gio { get; set; }

        public int NgayConLai { get; set; }
        [Required(ErrorMessage = "Xin nhập giờ bắt đầu.")]
        public string TuGio { get; set; }
        [Required(ErrorMessage = "Xin nhập giờ kết thúc.")]
        public string DenGio { get; set; }

        public int MucDo { get; set; }
        public bool CaNgay { get; set; }

        public bool LapLai { get; set; }
        public string UserName { get; set; }

        public string NguoiTao { get; set; }

        public string GioView { set; get; }
        public string MucDoView { get; set; }

        public string VietTat { get; set; }

        public int HeightRow { get; set; }
        public int LineHeight { get; set; }


        public List<PhongBan> ListPhongBan { get; set; }
        public List<PhongBan> ListPhongBanDistinct { get; set; }

        public string Color { get; set; }

        public int Type { get; set; }

        public string DanhGia { get; set; }

        public List<PhongBan> ListPhongBanThamDu { get; set; }

        public List<PhongBan> ListPhongBanChuTri { get; set; }

        public List<KhachMoiModel> ListKhachMoiThamDu { get; set; }

        public List<KhachMoiModel> ListKhachMoiChuTri { get; set; }
    }

}
