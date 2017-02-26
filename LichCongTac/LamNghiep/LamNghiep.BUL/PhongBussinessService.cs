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
    public class PhongBussinessService 
    {
        public List<Phong> GetPhong(DateTime ngay)
        {
            PhongDataService phongData = new PhongDataService();
            List<Phong> listPhong=phongData.GetPhongHop();
            if (listPhong!=null &&listPhong.Count>0)
            {
                foreach (var item in listPhong)
                {
                    item.ListPhong= phongData.GetPhongHopTheoNgay(ngay, item.Id);
                }
            }
            return listPhong;
        }

        public int DatPhong(Phong phong)
        {
            PhongDataService phongData = new PhongDataService();
            return phongData.DatPhong(phong);
        }

        public int CheckDatPhong(Phong phong)
        {
            PhongDataService phongData = new PhongDataService();
            return phongData.CheckDatPhong(phong);
        }

        public List<Phong> GetPhongByUser(string user)
        {
            PhongDataService phongData = new PhongDataService();
            return phongData.GetPhongByUser(user);
        }

        public object GetInfoPhong()
        {
            PhongDataService phongData = new PhongDataService();
            List<Phong> listPhong = phongData.GetInfoPhong();
          
            List<Event> lstEvent = new List<Event>();
            foreach (var item in listPhong)
            {
                Event even = new Event();
                even.date = item.Ngay.ToString();
                even.start = item.TuNgay.ToString();
                even.end = item.DenNgay.ToString();
                even.id = item.Id.ToString();
                even.title = item.TenPhong;
                even.color = item.Color;
                even.name = item.CuocHop;
                lstEvent.Add(even);
            }
            return lstEvent;
        }

      
    }
}
