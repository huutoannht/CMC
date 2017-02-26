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
    public class PhongBanBussinessService
    {
        public List<KeHoachCT> GetDistinctList(List<KeHoachCT> listKeHoach)
        {
            listKeHoach = this.GetHeightKeHoach(listKeHoach);
            List<string> listUserName = new List<string>();
            foreach (var item in listKeHoach)
            {
                if (listUserName.Any(user => user == item.UserName))
                {
                    item.HoTen = "";
                    item.VietTat = null;
                }
                listUserName.Add(item.UserName);
            }
            listKeHoach = this.PrintHeightKeHoach(listKeHoach);
            return listKeHoach;
        }
        public List<KeHoachCT> PrintHeightKeHoach(List<KeHoachCT> listKeHoach)
        {
            foreach (var keHoach in listKeHoach)
            {
                switch (keHoach.HeightRow)
                {
                    case 1:
                        keHoach.LineHeight = 23;
                        break;
                    case 2:
                        keHoach.LineHeight = 58;
                        break;
                    case 3:
                        keHoach.LineHeight = 93;
                        break;
                    case 4:
                        keHoach.LineHeight = 128;
                        break;
                    case 5:
                        keHoach.LineHeight = 163;
                        break;
                    case 6:
                        keHoach.LineHeight = 198;
                        break;
                    case 7:
                        keHoach.LineHeight = 233;
                        break;
                    case 8:
                        keHoach.LineHeight = 268;
                        break;
                    default:
                        break;
                }
            }
            return listKeHoach;
        }
        private List<KeHoachCT> GetHeightKeHoach(List<KeHoachCT> listKeHoach)
        {
            foreach (var item in listKeHoach)
            {
                item.HeightRow = this.GetCountUserName(listKeHoach, item.UserName);
            }
            return listKeHoach;
        }
        private int GetCountUserName(List<KeHoachCT> listKeHoach, string userName)
        {
            return listKeHoach.Count(kh => kh.UserName == userName);
        }
        public int AddKeHoachCT(KeHoachCT keHoach)
        {
            KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();
            return ketHoachCTDataService.Add(keHoach);
        }

        //public List<KeHoachCT> GetKeHoachCaNhan(string userName)
        //{
        //    KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();
        //    return ketHoachCTDataService.GetKeHoachCaNhan(userName);
        //}

        public object GetLichCaNhan(JQueryDataTableParamModel param)
        {
            KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();
            param.CanBo = "'" + param.CanBo + "'";
            List<KeHoachCT> lstKeHoachCT = ketHoachCTDataService.GetKeHoachCongTac(param);
            List<Event> listEvent = new List<Event>();
            Event eventKeHoach = null;
            foreach (var item in lstKeHoachCT)
            {
                eventKeHoach = new Event();
                eventKeHoach.description = item.TenKeHoach;
                eventKeHoach.start = item.TuNgayModel.ToString();
                eventKeHoach.end = item.DenNgayModel.ToString();
                eventKeHoach.title = item.HoTen;
                eventKeHoach.allDay = item.CaNgay;
                listEvent.Add(eventKeHoach);
            }
            return listEvent;
        }
        public List<KeHoachCT> GetKeHoachCanBo(string userName)
        {
            KeHoachCTDataService ketHoachCTDataService = new KeHoachCTDataService();
            return ketHoachCTDataService.GetKeHoachCanBo(userName);
        }

        public List<PhongBan> GetNguoiDungPhongBan()
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.GetNguoiDungPhongBan();
        }
        public List<PhongBan> GetNguoiDungLanhDao()
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.GetNguoiDungLanhDao();
        }
        public List<PhongBan> GetAllNguoiDung()
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.GetAllNguoiDung();
        }

        public List<PhongBan> GetNguoiDungDistinct(List<PhongBan> listPhongBan)
        {
            var listDistinct = listPhongBan.GroupBy(phongban => phongban.MaPhongBan)
                         .Select(grpb => grpb.First())
                         .ToList();
            return listDistinct;
        }

        #region Function For Update KeHoach
        public List<PhongBan> CheckSelectedThamDu(List<PhongBan> listPhongBan, List<ThamDuModel> listThamDuModel)
        {
            List<PhongBan> listPhongBanNew = new List<PhongBan>();
            listPhongBanNew = listPhongBan;
            foreach (var itemPhongBan in listPhongBanNew)
            {
                itemPhongBan.Selected = false;
                if (listThamDuModel.Any(thamdu => thamdu.NguoiThamDu == itemPhongBan.UserName))
                {
                    itemPhongBan.Selected = true;
                }
            }
            return listPhongBanNew;
        }
        #endregion


        public List<PhongBan> GetAllPhongBan()
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.GetAllPhongBan();
        }


        public List<ChucVuModel> GetChucVuTheoPhongBan(int idPhongBan)
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.GetChucVuTheoPhongBan(idPhongBan);
        }

        public int AddNguoiDung(User user)
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.AddNguoiDung(user);
        }


        public bool CheckUserName(string userName)
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.CheckUserName(userName);
        }

        public List<User> GetListNguoiDung(JQueryDataTableParamModel param)
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.GetListNguoiDung(param);
        }

        public int GetCountListNguoiDung(JQueryDataTableParamModel param)
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.GetCountListNguoiDung(param);
        }

        public List<User> getNguoiDungByUserName(string username)
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.getNguoiDungByUserName(username);
        }

        public int EditNguoiDung(User user)
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.EditNguoiDung(user);
        }

        public bool DeleteNguoiDung(string userName)
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.DeleteNguoiDung(userName);
        }

        public bool ChangePassWord(string userName, string passWord,string updateBy)
        {
            PhongBanDataService phongBanDataService = new PhongBanDataService();
            return phongBanDataService.ChangePassWord(userName, passWord, updateBy);
        }
    }
}
