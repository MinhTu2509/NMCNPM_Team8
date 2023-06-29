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
    /// Interaction logic for WChonChuyenBay.xaml
    /// </summary>
    public partial class WChonChuyenBay : Window
    {
        DateTime date;
        public string MaChuyenBayIsSelected;
        public event EventHandler Thoat;
        public WChonChuyenBay()
        {
            InitializeComponent();
            date = DateTime.Today;
            MaChuyenBayIsSelected = "";
            load();
        }
        public WChonChuyenBay(DateTime time)
        {
            InitializeComponent();
            date = time;
            MaChuyenBayIsSelected = "";
            load();
        }
        void load()
        {
            DataTable table = new DataTable();
            table.Clear();

            string query = "SELECT CHUYENBAY.MaChuyenBay as 'Mã chuyến bay', SB1.TenSanBay as 'Sân bay đi', SB2.TenSanBay as 'Sân bay đến', convert(varchar,CHUYENBAY.NgayBay, 108) as 'Giờ bay', ThoiGianBay as 'Thời gian bay', SUM(TongSoGhe) as 'Tổng số ghế', SUM(SoGheDaDat) as 'Số ghế đã đặt', REPLACE(CONVERT(varchar(20), GiaVe, 1), '.00', '') as 'Giá vé' FROM CHUYENBAY, SANBAY SB1, SANBAY SB2, SOLUONGVE "
                          + "WHERE CHUYENBAY.MaChuyenBay = SOLUONGVE.MaChuyenBay AND CHUYENBAY.MaSanBayDi = SB1.MaSanBay AND CHUYENBAY.MaSanBayDen = SB2.MaSanBay "
                          +"AND CHUYENBAY.NgayBay >= '"+date.ToString("yyyy/MM/dd 00:00:00")+"' AND CHUYENBAY.NgayBay <= '"+date.ToString("yyyy/MM/dd 23:59:59")+"' "
                          +"AND SB1.TenSanBay LIKE N'%"+TB_TimNoiDi.Text+"%' AND SB2.TenSanBay LIKE N'%"+TB_TimNoiDen.Text+"%' "
                          + "GROUP BY CHUYENBAY.MaChuyenBay, SB1.TenSanBay, SB2.TenSanBay,convert(varchar,CHUYENBAY.NgayBay, 108), ThoiGianBay, GiaVe ";
            table = DataProvider.Instance.ExecuteQuery(query);
            DG_DSChuyenBay.ItemsSource = table.DefaultView;
            //DG_DSChuyenBay.IsReadOnly = true;
        }


        private void TB_TimNoiDi_TextChanged(object sender, TextChangedEventArgs e)
        {
            load();
        }

        private void TB_TimNoiDen_TextChanged(object sender, TextChangedEventArgs e)
        {
            load();
        }

        private void DG_DSChuyenBay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            DataRowView dt = (DataRowView)DG_DSChuyenBay.SelectedItem;
            if (dt == null)
                return;
            MaChuyenBayIsSelected = dt["Mã chuyến bay"].ToString();
            int TongSoGhe = Convert.ToInt32(dt["Tổng số ghế"].ToString());
            int SoGheDaDat = Convert.ToInt32(dt["Số ghế đã đặt"].ToString());
            if(TongSoGhe - SoGheDaDat<=0)
            {
                MessageBox.Show("Đã hết ghế", "Thông báo", MessageBoxButton.OK);
                return;
            }    
            Thoat(this, new EventArgs());
        }
    }
}
