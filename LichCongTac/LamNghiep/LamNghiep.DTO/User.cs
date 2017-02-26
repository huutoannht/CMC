using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace LamNghiep.DTO
{
    public partial class User
    
    {
        [Required(ErrorMessage="Tên đăng nhập không được để trống")]
        [Remote("CheckUserName", "NguoiDung", HttpMethod = "POST", ErrorMessage = "Tên đăng nhập này đã tồn tại.")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6 đến 50 ký tự")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6 đến 50 ký tự")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("Password", ErrorMessage = "Mật khẩu không trùng nhau.")]
        public string ConfirmPassword { get; set; }
         [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string HoTen { get; set; }

        public string  DiaChi { get; set; }
           [Required(ErrorMessage = "Chức vụ không được để trống")]
        public int IdChucVu { get; set; }

        public string  SoDT { get; set; }

        #region Extensions
        public string TenChucVu { get; set; }
        public string TenPhongBan { get; set; }
        #endregion

        public int TotalCount { get; set; }

        public int IdPhongBan { get; set; }

        public string UpdateBy { get; set; }

        public string CreateBy { get; set; }

        public string Role { get; set; }
        public string VietTat { get; set; }
    }
}
