using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.DTO
{
    public class WeekOfYearModel
    {
        public int CodeWeek { get; set; }
        public string NameWeek { get; set; }
        public bool IsCurSelected { get; set; }
        public int IsWeek { get; set; }
    }
    public class MonthOfYearModel
    {
        public int CodeMonth { get; set; }
        public string NameMonth { get; set; }
        public bool IsCurSelected { get; set; }
        public int IsMonth { get; set; }
    }
}
