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
    /// Interaction logic for WChonHangVe.xaml
    /// </summary>
    public partial class WChonHangVe : Window
    {
        string MaChuyenBay;
        public string TenHangVeIsSelected;
        public event EventHandler Thoat;
        public WChonHangVe()
        {
            InitializeComponent();
            this.MaChuyenBay = "";
            TenHangVeIsSelected = "";
            load();
        }

        public WChonHangVe(string MaChuyenBay)
        {
            InitializeComponent();
            this.MaChuyenBay = MaChuyenBay;
            TenHangVeIsSelected = "";
            load();
        }    
        void load()
        {
            DataTable table = new DataTable();
            table.Clear();

            string query = "SELECT HANGVE.TenHangVe as 'Tên hạng vé', HANGVE.TiLe as 'Tỉ lệ', SOLUONGVE.TongSoGhe as 'Tổng số ghế', SOLUONGVE.SoGheDaDat as 'Số ghế đã đặt' FROM HANGVE, SOLUONGVE WHERE HANGVE.MaHangVe = SOLUONGVE.MaHangVe AND SOLUONGVE.MaChuyenBay = '"+MaChuyenBay+"'";
            table = DataProvider.Instance.ExecuteQuery(query);
            DG_DSHangVe.ItemsSource = table.DefaultView;
        }

        private void DG_DSHangVe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView dt = (DataRowView)DG_DSHangVe.SelectedItem;
            if (dt == null)
                return;
            TenHangVeIsSelected = dt["Tên hạng vé"].ToString();
            int TongSoGhe = Convert.ToInt32(dt["Tổng số ghế"].ToString());
            int SoGheDaDat = Convert.ToInt32(dt["Số ghế đã đặt"].ToString());
            if (TongSoGhe - SoGheDaDat <= 0)
            {
                MessageBox.Show("Đã hết ghế", "Thông báo", MessageBoxButton.OK);
                return;
            }
            Thoat(this, new EventArgs());
        }
    }
}
