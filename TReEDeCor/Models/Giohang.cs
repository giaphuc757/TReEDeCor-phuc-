using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TReEDeCor.Models
{
    public class Giohang
    {
        DatabaseDataContext data = new DatabaseDataContext();
        public int idsp { get; set; }
        public string tensanpham { get; set; }
        public string anh { get; set; }
        public Double dongia { get; set; }
        public int soluong { get; set; }
        public Double tongtien { get { return soluong * dongia; } }

        public Giohang(int id)
        {
            idsp = id;
            SANPHAM s = data.SANPHAMs.Single(n => n.MaSP == id);
            tensanpham = s.TenSP;
            anh = s.AnhSP;
            dongia = double.Parse(s.Giaban.ToString());
            soluong = 1;
        }
    }
}