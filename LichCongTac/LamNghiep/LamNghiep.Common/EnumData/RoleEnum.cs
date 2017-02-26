using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.Common.EnumData
{
    public enum RoleEnum
    {
        [Description("Admin")]
        Admin=0,
          [Description("User")]
        User=1
    }
    public enum ChucVuEnum
    {
        [Description("Lãnh đạo")]
        LanhDao = 0,
        [Description("Trưởng phòng")]
        TruongPhong = 1,
        [Description("Nhân viên")]
        NhanVien = 2
    }
    public enum TypeCalendarEnum
    {
        [Description("Admin")]
        LichTuan = 0,
        [Description("LichThang")]
        LichThang = 1,
        [Description("LichQuy")]
        LichQuy = 2,
        [Description("LichNam")]
        LichNam = 3
    }
    public static class EnumExtensions
    {
        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
