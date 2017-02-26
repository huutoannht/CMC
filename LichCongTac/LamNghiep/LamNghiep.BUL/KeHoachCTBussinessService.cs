using LamNghiep.DAL.DataServices;
using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.BUL.Bussiness
{
    public class KeHoachCTBussinessService
    {
        public List<KeHoachCongTacModel> GetKeHoachCongTac(JQueryDataTableParamModel param, string distinct = null)
        {
            KeHoachCTDataService keHoachCTDataService = new KeHoachCTDataService();

            List<KeHoachCT> listKeHoachCT = keHoachCTDataService.GetKeHoachCongTac(param);

            List<KeHoachCT> listKeHoachCTMonday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTTuesday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTWednesday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTThursday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTFriday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTSaturday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTSunday = new List<KeHoachCT>();

            List<KeHoachCongTacModel> listKeHoachCTModel = new List<KeHoachCongTacModel>();
            foreach (var item in listKeHoachCT)
            {
                KeHoachCongTacModel keHoachCongTacModel = new KeHoachCongTacModel();
                keHoachCongTacModel.Ngay = item.Ngay;
                if ("Monday".Equals(item.Ngay))
                {
                    listKeHoachCTMonday.Add(item);
                }
                if ("Tuesday".Equals(item.Ngay))
                {
                    listKeHoachCTTuesday.Add(item);
                }
                if ("Wednesday".Equals(item.Ngay))
                {
                    listKeHoachCTWednesday.Add(item);
                }
                if ("Thursday ".Equals(item.Ngay))
                {
                    listKeHoachCTThursday.Add(item);
                }
                if ("Friday".Equals(item.Ngay))
                {
                    listKeHoachCTFriday.Add(item);
                }
                if ("Saturday".Equals(item.Ngay))
                {
                    listKeHoachCTSaturday.Add(item);
                }
                if ("Sunday".Equals(item.Ngay))
                {
                    listKeHoachCTSunday.Add(item);
                }
            }

            KeHoachCongTacModel listKeHoachCTDayMondayModel = new KeHoachCongTacModel();
            listKeHoachCTDayMondayModel.Ngay = "Thứ 2";
            listKeHoachCTDayMondayModel.ListCongTac = listKeHoachCTMonday;
            listKeHoachCTModel.Add(listKeHoachCTDayMondayModel);

            KeHoachCongTacModel listKeHoachCTDayTuesdayModel = new KeHoachCongTacModel();
            listKeHoachCTDayTuesdayModel.Ngay = "Thứ 3";
            listKeHoachCTDayTuesdayModel.ListCongTac = listKeHoachCTTuesday;
            listKeHoachCTModel.Add(listKeHoachCTDayTuesdayModel);

            KeHoachCongTacModel listKeHoachCTDayWednesdayModel = new KeHoachCongTacModel();
            listKeHoachCTDayWednesdayModel.Ngay = "Thứ 4";
            listKeHoachCTDayWednesdayModel.ListCongTac = listKeHoachCTWednesday;
            listKeHoachCTModel.Add(listKeHoachCTDayWednesdayModel);

            KeHoachCongTacModel listKeHoachCTDayThursdayModel = new KeHoachCongTacModel();
            listKeHoachCTDayThursdayModel.Ngay = "Thứ 5";
            listKeHoachCTDayThursdayModel.ListCongTac = listKeHoachCTThursday;
            listKeHoachCTModel.Add(listKeHoachCTDayThursdayModel);

            KeHoachCongTacModel listKeHoachCTDayFridayModel = new KeHoachCongTacModel();
            listKeHoachCTDayFridayModel.Ngay = "Thứ 6";
            listKeHoachCTDayFridayModel.ListCongTac = listKeHoachCTFriday;
            listKeHoachCTModel.Add(listKeHoachCTDayFridayModel);

            KeHoachCongTacModel listKeHoachCTDaySaturdayModel = new KeHoachCongTacModel();
            listKeHoachCTDaySaturdayModel.Ngay = "Thứ 7";
            listKeHoachCTDaySaturdayModel.ListCongTac = listKeHoachCTSaturday;
            listKeHoachCTModel.Add(listKeHoachCTDaySaturdayModel);

            KeHoachCongTacModel listKeHoachCTDaySundayModel = new KeHoachCongTacModel();
            listKeHoachCTDaySundayModel.Ngay = "Chủ nhật";
            listKeHoachCTDaySundayModel.ListCongTac = listKeHoachCTSunday;
            listKeHoachCTModel.Add(listKeHoachCTDaySundayModel);

            //Display kehoachcongtac from screen PhongBan
            if (!string.IsNullOrEmpty(distinct))
            {
                PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
                foreach (var itemKeHoachCTModel in listKeHoachCTModel)
                {
                    itemKeHoachCTModel.ListCongTac = phongBanBussinessService.GetDistinctList(itemKeHoachCTModel.ListCongTac);
                }
            }
            return listKeHoachCTModel;
        }

        public int AddKeHoachCT(KeHoachCT keHoach)
        {
            KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();
            return ketHoachCTDataService.Add(keHoach);
        }
        public int AddKeHoachCTCanBo(KeHoachCT keHoach, string[] user)
        {
            KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();
            int result = 0;
            foreach (var item in user)
            {
                keHoach.UserName = item;
                result = result + ketHoachCTDataService.Add(keHoach);
            }
            return result;
        }
        public List<KeHoachCT> GetKeHoachCaNhan(JQueryDataTableParamModel param)
        {
            KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();
            return ketHoachCTDataService.GetKeHoachCaNhan(param);
        }
        public int GetCountListKeHoachCaNhan(JQueryDataTableParamModel param)
        {
            KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();
            return ketHoachCTDataService.GetCountListKeHoachCaNhan(param);
        }
        public List<KeHoachCT> GetKeHoachById(int id)
        {
            KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();
            return ketHoachCTDataService.GetKeHoachById(id);
        }
        public List<KeHoachCongTacModel> GetThamDuAndChuTri(string userName, DateTime startDate, DateTime endDate)
        {
            KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();

            List<KeHoachCT> listKeHoachCT = ketHoachCTDataService.GetThamDuAndChuTri(userName, startDate, endDate);

            List<KeHoachCT> listKeHoachCTMonday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTTuesday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTWednesday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTThursday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTFriday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTSaturday = new List<KeHoachCT>();
            List<KeHoachCT> listKeHoachCTSunday = new List<KeHoachCT>();

            List<KeHoachCongTacModel> listKeHoachCTModel = new List<KeHoachCongTacModel>();
            foreach (var item in listKeHoachCT)
            {
                KeHoachCongTacModel keHoachCongTacModel = new KeHoachCongTacModel();
                keHoachCongTacModel.Ngay = item.Ngay;
                if ("Monday".Equals(item.Ngay))
                {
                    listKeHoachCTMonday.Add(item);
                }
                if ("Tuesday".Equals(item.Ngay))
                {
                    listKeHoachCTTuesday.Add(item);
                }
                if ("Wednesday".Equals(item.Ngay))
                {
                    listKeHoachCTWednesday.Add(item);
                }
                if ("Thursday ".Equals(item.Ngay))
                {
                    listKeHoachCTThursday.Add(item);
                }
                if ("Friday".Equals(item.Ngay))
                {
                    listKeHoachCTFriday.Add(item);
                }
                if ("Saturday".Equals(item.Ngay))
                {
                    listKeHoachCTSaturday.Add(item);
                }
                if ("Sunday".Equals(item.Ngay))
                {
                    listKeHoachCTSunday.Add(item);
                }
            }
            KeHoachCongTacModel listKeHoachCTDayMondayModel = new KeHoachCongTacModel();
            listKeHoachCTDayMondayModel.Ngay = "Thứ 2";
            listKeHoachCTDayMondayModel.ListCongTac = listKeHoachCTMonday;
            listKeHoachCTModel.Add(listKeHoachCTDayMondayModel);

            KeHoachCongTacModel listKeHoachCTDayTuesdayModel = new KeHoachCongTacModel();
            listKeHoachCTDayTuesdayModel.Ngay = "Thứ 3";
            listKeHoachCTDayTuesdayModel.ListCongTac = listKeHoachCTTuesday;
            listKeHoachCTModel.Add(listKeHoachCTDayTuesdayModel);

            KeHoachCongTacModel listKeHoachCTDayWednesdayModel = new KeHoachCongTacModel();
            listKeHoachCTDayWednesdayModel.Ngay = "Thứ 4";
            listKeHoachCTDayWednesdayModel.ListCongTac = listKeHoachCTWednesday;
            listKeHoachCTModel.Add(listKeHoachCTDayWednesdayModel);

            KeHoachCongTacModel listKeHoachCTDayThursdayModel = new KeHoachCongTacModel();
            listKeHoachCTDayThursdayModel.Ngay = "Thứ 5";
            listKeHoachCTDayThursdayModel.ListCongTac = listKeHoachCTThursday;
            listKeHoachCTModel.Add(listKeHoachCTDayThursdayModel);

            KeHoachCongTacModel listKeHoachCTDayFridayModel = new KeHoachCongTacModel();
            listKeHoachCTDayFridayModel.Ngay = "Thứ 6";
            listKeHoachCTDayFridayModel.ListCongTac = listKeHoachCTFriday;
            listKeHoachCTModel.Add(listKeHoachCTDayFridayModel);

            KeHoachCongTacModel listKeHoachCTDaySaturdayModel = new KeHoachCongTacModel();
            listKeHoachCTDaySaturdayModel.Ngay = "Thứ 7";
            listKeHoachCTDaySaturdayModel.ListCongTac = listKeHoachCTSaturday;
            listKeHoachCTModel.Add(listKeHoachCTDaySaturdayModel);

            KeHoachCongTacModel listKeHoachCTDaySundayModel = new KeHoachCongTacModel();
            listKeHoachCTDaySundayModel.Ngay = "Chủ nhật";
            listKeHoachCTDaySundayModel.ListCongTac = listKeHoachCTSunday;
            listKeHoachCTModel.Add(listKeHoachCTDaySundayModel);

            //Display kehoachcongtac from screen PhongBan
           
                PhongBanBussinessService phongBanBussinessService = new PhongBanBussinessService();
                foreach (var itemKeHoachCTModel in listKeHoachCTModel)
                {
                    itemKeHoachCTModel.ListCongTac = phongBanBussinessService.GetDistinctList(itemKeHoachCTModel.ListCongTac);
                }
            return listKeHoachCTModel;
        }
        public object GetLichCongTac()
        {
            KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();
            List<KeHoachCT> listKeHoach = ketHoachCTDataService.GetLichCongTac();
            List<Event> listEvent = new List<Event>();
            Event eventKeHoach = null;
            foreach (var item in listKeHoach)
            {
                eventKeHoach = new Event();
                eventKeHoach.description = item.TenKeHoach;
                eventKeHoach.start = item.TuNgayModel.ToString();
                eventKeHoach.end = item.DenNgayModel.ToString();
                eventKeHoach.title = item.HoTen;
                eventKeHoach.location = item.DiaDiem;
                eventKeHoach.attend = item.ThamDu;
                eventKeHoach.color = item.Color;
                listEvent.Add(eventKeHoach);
            }
            return listEvent;
        }
        #region Function For Update KeHoach
        public List<KeHoachCT> GetKeHoachForUpdate(int id)
        {
            KeHoachCTDataService keHoachCTDataService = new KeHoachCTDataService();
            return keHoachCTDataService.GetKeHoachForUpdate(id);
        }
        public List<ThamDuModel> GetThamDuByIdKeHoach(int id)
        {
            KeHoachCTDataService keHoachCTDataService = new KeHoachCTDataService();
            return keHoachCTDataService.GetThamDuByIdKeHoach(id);
        }
        public List<KhachMoiModel> GetKhachMoiByIdKeHoach(int id)
        {
            KeHoachCTDataService keHoachCTDataService = new KeHoachCTDataService();
            return keHoachCTDataService.GetKhachMoiByIdKeHoach(id);
        }
        public int EditKeHoachCT(KeHoachCT keHoachCT)
        {
            KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();
            return ketHoachCTDataService.Save(keHoachCT);
        }
        #endregion

        #region Function Delete KeHoach
        public int DeleteKeHoachCongTac(int id)
        {
            KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();
            return ketHoachCTDataService.DeleteKeHoachCongTac(id);
        }
        #endregion

      

       
    }
}
