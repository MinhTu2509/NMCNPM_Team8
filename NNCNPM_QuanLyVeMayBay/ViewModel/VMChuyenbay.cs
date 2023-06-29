using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCNPM_QuanLyVeMayBay
{
    class VMChuyenbay
    {
           public IList<DataLoaiVe> dataLoaiVe { get; set; }
        public IList<DataSanBayTrungGian> dataSanBayTrungGian { get; set; }
        public VMChuyenbay() {
            dataLoaiVe = new List<DataLoaiVe>();
            dataSanBayTrungGian = new List<DataSanBayTrungGian>();
        }

    }

    internal class DataLoaiVe
    {
        public string MaLoaiVe { get; set; }
        public string TenLoaiVe { get; set; }
        public string TiLe { get; set; }
        public int SoLuong { get; set; }
        public DataLoaiVe(string MaLoaiVe = "", string TenLoaiVe = "", string tile = "", int SoLuong = 0)
        {
            this.MaLoaiVe = MaLoaiVe;
            this.TenLoaiVe = TenLoaiVe;
            this.TiLe = tile;
            this.SoLuong = SoLuong;
        }
    }
    internal class DataSanBayTrungGian
    {
        public string MaSanBay { get; set; }
        public string TenSanBay { get; set; }
        public int ThoiGianDung { get; set; }
        public string NghiChu { get; set; }
        public DataSanBayTrungGian(string MaSanBay = "", string TenSanBay = "", int ThoiGianDung = 0, string NghiChu = "")
        {
            this.MaSanBay = MaSanBay;
            this.TenSanBay = TenSanBay;
            this.ThoiGianDung = ThoiGianDung;
            this.NghiChu = NghiChu;
        }
    }

}
