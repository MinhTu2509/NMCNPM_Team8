using System;
using System.Collections.Generic;
using System.Data;
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

namespace NNCNPM_QuanLyVeMayBay
{
    /// <summary>
    /// Interaction logic for WChiTietChuyenBay.xaml
    /// </summary>
    public partial class WChiTietChuyenBay : Window
    {
        DataTable datatable_Ghe= new DataTable();
        DataTable datatable_TrungGian = new DataTable();
        public WChiTietChuyenBay()
        {
            InitializeComponent();
        }
        public WChiTietChuyenBay( string machuyenbay,string giave, string thoigianbay,int trangthai) // 0 là chưa bay, 1 là bay rồi
        {
        
            InitializeComponent();
            tbk_MaChuyenBay.Text = machuyenbay;
            tbk_giave.Text = giave +" VNĐ";
            tbk_thoigianbay.Text = thoigianbay+" phút";
            string ExeCommand = "select TenHangVe as 'Tên hạng vé', TongSoGhe as 'Tổng số ghế',  coalesce(TongSoGhe- VeDaMua,0) as 'Số ghế trống' , SoGheDaDat as 'Số ghế đặt' " +
                " from(" +
                " select MaHangVe, count(Loaive) as 'VeDaMua'" +
                " from VEMAYBAY" +
                " where Loaive = 'Ve mua' and MaChuyenBay = '"+machuyenbay+"'" +
                " group by MaHangVe) as table1" +
                " right join (" +
                " select HANGVE.TenHangVe, TongSoGhe , SoGheDaDat , HANGVE.MaHangVe" +
                " from SOLUONGVE, HANGVE" +
                " where SOLUONGVE.MaHangVe= HANGVE.MaHangVe and MaChuyenBay = '" + machuyenbay + "'" +
                " ) as table2" +
                " on table1.MaHangVe = table2.MaHangVe";
            
            datatable_Ghe = DataProvider.Instance.ExecuteQuery(ExeCommand, null);

            datagrid_Ghe.ItemsSource=datatable_Ghe.AsDataView();

            datatable_TrungGian = DataProvider.Instance.ExecuteQuery("select SANBAY.TenSanBay as 'Tên sân bay',TRUNGGIAN.ThoiGianDung as 'Thời gian dừng'  from TRUNGGIAN,SANBAY where MaChuyenBay='" + machuyenbay+"' AND TRUNGGIAN.MaSanBay=SANBAY.MaSanBay ", null);
            if (datatable_TrungGian.Rows.Count > 0)
            { datagrid_TrungGian.ItemsSource = datatable_TrungGian.AsDataView(); }
            else
            {
                datagrid_TrungGian.Visibility = Visibility.Collapsed;
                lb_thongbao.Visibility = Visibility.Visible;
            }    
            if(trangthai == 0)
            {
                lb_TrangThai1.Visibility = Visibility.Visible;

            }
            if (trangthai == 1)
            {
                lb_TrangThai0.Visibility = Visibility.Visible;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
