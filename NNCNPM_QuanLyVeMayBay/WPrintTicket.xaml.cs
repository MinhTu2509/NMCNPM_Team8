using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace NNCNPM_QuanLyVeMayBay
{
    /// <summary>
    /// Interaction logic for WPrintTicket.xaml
    /// </summary>
    public partial class WPrintTicket : Window
    {
        string MaVeMayBay;
        public WPrintTicket()
        {
            InitializeComponent();
        }
        public WPrintTicket(string MaVeMayBay)
        {
            this.MaVeMayBay = MaVeMayBay;
            InitializeComponent();
            Load();
        }
        void Load()
        {
            DataTable table = new DataTable();
            table.Clear();
            string query = "SELECT CHUYENBAY.MaChuyenBay as 'Mã chuyến bay', SB1.TenSanBay as 'Sân bay đi', SB2.TenSanBay as 'Sân bay đến', CHUYENBAY.NgayBay, DATEADD(mi,CHUYENBAY.ThoiGianBay, CHUYENBAY.NgayBay) as 'Giờ đến', HANGVE.TenHangVe as 'Hạng vé', CHUYENBAY.ThoiGianBay "
                            + "FROM CHUYENBAY, SANBAY SB1, SANBAY SB2, VEMAYBAY, HANGVE "
                            + "WHERE CHUYENBAY.MaSanBayDi = SB1.MaSanBay AND CHUYENBAY.MaSanBayDen = SB2.MaSanBay AND CHUYENBAY.MaChuyenBay = VEMAYBAY.MaChuyenBay AND VEMAYBAY.MaVe = 'VE0000' AND VEMAYBAY.MaHangVe = HANGVE.MaHangVe";
            table = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow i in table.Rows)
            {
                TB_NgayBay.Text = ConvertDateTime(Convert.ToDateTime(i.ItemArray[3].ToString()));
                TB_MaChuyenBay.Text = i.ItemArray[0].ToString();
                TB_NoiDi.Text = i.ItemArray[1].ToString();
                TB_NoiDen.Text = i.ItemArray[2].ToString();
                TB_GioBay.Text = Convert.ToDateTime(i.ItemArray[3].ToString()).ToString("HH:mm:ss");
                TB_GioDen.Text = Convert.ToDateTime(i.ItemArray[4].ToString()).ToString("HH:mm:ss");
                TB_HangVe.Text = i.ItemArray[5].ToString();
                TB_ThoiGianBay.Text = i.ItemArray[6].ToString();
            }
            query = "SELECT COUNT(*) FROM TRUNGGIAN WHERE TRUNGGIAN.MaChuyenBay = '" + TB_MaChuyenBay + "'";
            TB_SBTrungGian.Text = DataProvider.Instance.ExecuteScalar(query).ToString();
            table.Clear();
            query = "SELECT HANHKHACH.HoTen, HANHKHACH.CMND, HANHKHACH.SoDienThoai FROM HANHKHACH, VEMAYBAY WHERE HANHKHACH.CMND = VEMAYBAY.CMND AND VEMAYBAY.MaVe = '" + MaVeMayBay + "'";
            table = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow i in table.Rows)
            {
                TB_TenHanhKhach.Text = i.ItemArray[0].ToString();
                TB_CMND.Text = i.ItemArray[1].ToString();
                TB_SDT.Text = i.ItemArray[2].ToString();
                //TenHanhKhach.Inlines.Clear();
                //TenHanhKhach.Inlines.Add(i.ItemArray[0].ToString());
                //CMND.Inlines.Clear();
                //CMND.Inlines.Add(i.ItemArray[1].ToString());
                //SDT.Inlines.Clear();
                //SDT.Inlines.Add(i.ItemArray[2].ToString());
            }
        }
        string ConvertDateTime(DateTime dateTime)
        {
            string date = "";
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    date = "Thứ hai";
                    break;
                case DayOfWeek.Tuesday:
                    date = "Thứ ba";
                    break;
                case DayOfWeek.Wednesday:
                    date = "Thứ tư";
                    break;
                case DayOfWeek.Thursday:
                    date = "Thứ năm";
                    break;
                case DayOfWeek.Friday:
                    date = "Thứ sáu";
                    break;
                case DayOfWeek.Saturday:
                    date = "Thứ bảy";
                    break;
                case DayOfWeek.Sunday:
                    date = "Chủ nhật";
                    break;
                default:
                    date = "";
                    break;

            }
            date = date + ", Ngày " + dateTime.Day.ToString() + " Tháng " + dateTime.Month.ToString() + " Năm " + dateTime.Year.ToString();
            return date;
        }
        public void PrintTicket()
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource idp = FlowDoc;
                pd.PrintDocument(idp.DocumentPaginator, "Flow Document");
            }
        }
    }

}
