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
    /// Interaction logic for WTrangChu.xaml
    /// </summary>
    public partial class WTrangChu : Window
    {
        string UserName = "";
        public WTrangChu(string name = "")
        {
            InitializeComponent();
            UserName = name;
            L_name.Text += name;
            LoadData();
            if (name != "admin")
                BTN_ThemTaiKhoan.Visibility = Visibility.Collapsed;
        }

        private void BTN_DoiMatKhau_Click(object sender, RoutedEventArgs e)
        {
            WDoiMatKhau wDoiMatKhau = new WDoiMatKhau(UserName);
            wDoiMatKhau.Show();
        }

        private void BTN_ThemTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            WThemTaiKhoan wThemTaiKhoan = new WThemTaiKhoan();
            wThemTaiKhoan.Show();
        }

        private void LoadData()
        {
            DataTable table = new DataTable();
            table.Clear();
            string command = "select COUNT(VEMAYBAY.MaVe), REPLACE(CONVERT(varchar(30), sum(VEMAYBAY.GiaTien), 1), '.00', '') from VEMAYBAY "
                             + "where YEAR(VEMAYBAY.NgayMua) = '"+DateTime.Today.Year+"' and MONTH(VEMAYBAY.NgayMua) = '"+ DateTime.Today.Month + "' and DAY(VEMAYBAY.NgayMua)= '"+ DateTime.Today.Day+ "' and VEMAYBAY.LoaiVe = 'Ve Mua'"
                            + " group by VEMAYBAY.NgayMua ";
            table = DataProvider.Instance.ExecuteQuery(command);
            if (table.Rows.Count < 1)
            {
                L_VeDaBan.Content = "0" + L_VeDaBan.Content;
                L_DoanhSo.Content = "0" + L_DoanhSo.Content;
            }
            else
            {
                L_VeDaBan.Content = table.Rows[0].ItemArray[0].ToString() + L_VeDaBan.Content;
                L_DoanhSo.Content = table.Rows[0].ItemArray[1].ToString() + " VNĐ" +  L_DoanhSo.Content;
            }
            table.Clear();
            command = "select COUNT(CHUYENBAY.MaChuyenBay)  from CHUYENBAY "
                             + "where YEAR(CHUYENBAY.NgayBay) = '" + DateTime.Today.Year + "' and MONTH(CHUYENBAY.NgayBay) = '" + DateTime.Today.Month + "' and DAY(CHUYENBAY.NgayBay)= '" + DateTime.Today.Day + "'"
                            + " group by CHUYENBAY.NgayBay ";
            table = DataProvider.Instance.ExecuteQuery(command);
            if (table.Rows.Count < 1)
            {
                L_ChuyenBay.Content = "0" + L_ChuyenBay.Content;
            }
            else
            {
                L_ChuyenBay.Content = table.Rows[0].ItemArray[0].ToString() + L_ChuyenBay.Content;
            }
        }

    }
}
