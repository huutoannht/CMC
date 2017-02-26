using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.DTO
{
    public class JQueryDataTableParamModel
    {
        public string SEcho { get; set; }
        public string sSearch { get; set; }
        public int iDisplayLength { get; set; }
        public int iDisplayStart { get; set; }
        public int iColumns { get; set; }
        public int iSortingCols { get; set; }
        public string sColumns { get; set; }

        public int ISortCol_0 { get; set; }

        public string SSortDir_0 { get; set; }

        public string CanBo { get; set; }
        public int TuanLe { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Type { get; set; }
    }
}
