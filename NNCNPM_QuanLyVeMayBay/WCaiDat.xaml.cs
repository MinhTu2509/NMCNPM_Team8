using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for WCaiDat.xaml
    /// </summary>
    public partial class WCaiDat : Window
    {
        DataTable datatable_ChuyenBay;
        DataTable datatable_HangVe;
        public WCaiDat()
        {
            InitializeComponent();
        }

        private void IsNumberic_only(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            double val;
            e.Handled = !double.TryParse(fullText, out val);
        }

        private void BTN_Them_Click(object sender, RoutedEventArgs e)
        {
            if (TB_MaSanBay.Text == "" || TB_TenSanBay.Text == "")
            {
                MessageBox.Show("không thể để trống thông tin");
                return;
            }
            if (datatable_ChuyenBay.Rows.Contains(TB_MaSanBay.Text))
            {
                MessageBox.Show("Mã sân bay không được trùng");
                return;
            }
            string command = "insert into SANBAY values (N'" + TB_MaSanBay.Text + "','" + TB_TenSanBay.Text.ToString() + "')";
            DataProvider.Instance.ExecuteNonQuery(command);
            TB_SanBayToiDa.Text = datatable_ChuyenBay.Rows.Count.ToString();
            command = "update THAMSO set SOSANBAY = '" + TB_SanBayToiDa.Text + "'";
            DataProvider.Instance.ExecuteNonQuery(command);
            MessageBox.Show("Thêm sân bay thành công!");
            datatable_ChuyenBay.Rows.Add(TB_MaSanBay.Text, TB_TenSanBay.Text);
            TB_MaSanBay.Text = TB_TenSanBay.Text = "";
        }

        private void BTN_Luu_Click(object sender, RoutedEventArgs e)
        {

            if (TB_TGHuyVe.Text == "" || TB_TGDMin.Text == "" || TB_TGDMax.Text == "" || TB_TGDatVeMax.Text == "" || TB_TGBMin.Text == "" || TB_SBTGToiDa.Text == "")
            {
                MessageBox.Show("Warning : Có ô có dữ liệu trống !");
                return;
            }
            if (Int32.Parse(TB_TGHuyVe.Text) >= Int32.Parse(TB_TGDatVeMax.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Int32.Parse(TB_TGBMin.Text) < 0 || Int32.Parse(TB_TGDatVeMax.Text) < 0 || Int32.Parse(TB_TGDMax.Text) < 0 || Int32.Parse(TB_TGDMin.Text) < 0 || Int32.Parse(TB_TGHuyVe.Text) < 0 || Int32.Parse(TB_SBTGToiDa.Text) < 0)
            {
                MessageBox.Show("Giá trị phải lớn hơn 0 !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string command = "update THAMSO set SoLuongSanBayTGToiDa = '" + TB_SBTGToiDa.Text + "'"
            + ", ThoiGianBayToiThieu = '" + TB_TGBMin.Text + "'"
            + ", ThoiGianDungToiThieu = '" + TB_TGDMin.Text + "'"
            + ", ThoiGianDungToiDa = '" + TB_TGDMax.Text + "'"
            + ", ThoiGianDatVe = '" + TB_TGDatVeMax.Text + "'"
            + ", ThoiGianHuyVe = '" + TB_TGHuyVe.Text + "'";
            DataProvider.Instance.ExecuteNonQuery(command);
            MessageBox.Show("Sửa thông tin thành công");
        }

        private void BTN_ThemHangVe_Click(object sender, RoutedEventArgs e)
        {
            if (TB_TenHangVe.Text == "" || TB_TiLe.Text == "")
            {
                MessageBox.Show("không thể để trống thông tin");
                return;
            }
            if (datatable_HangVe.Rows.Contains(TB_MaSanBay.Text))
            {
                MessageBox.Show("Mã sân bay không được trùng");
                return;
            }

            string command = "insert into HANGVE values ('" + TB_MaHangVe.Text + "',N'" + TB_TenHangVe.Text + "'," + TB_TiLe.Text + ")";

            DataProvider.Instance.ExecuteNonQuery(command);
            command = "update THAMSO set SoHangVe = '" + TB_SanBayToiDa.Text + "'";
            DataProvider.Instance.ExecuteNonQuery(command);
            MessageBox.Show("Thêm Hạng vé thành công!");
            datatable_HangVe.Rows.Add(TB_MaHangVe.Text, TB_TenHangVe.Text, TB_TiLe.Text);
            TB_TenHangVe.Text = TB_TiLe.Text = "";
            TB_SoHangVe.Text = datatable_HangVe.Rows.Count.ToString();
            if (Int32.Parse(TB_SoHangVe.Text) >= 999)
                TB_MaHangVe.Text = "HV" + (Int32.Parse(TB_SoHangVe.Text) + 1).ToString();
            else if (Int32.Parse(TB_SoHangVe.Text) >= 99)
                TB_MaHangVe.Text = "HV0" + (Int32.Parse(TB_SoHangVe.Text) + 1).ToString();
            else if (Int32.Parse(TB_SoHangVe.Text) >= 9)
                TB_MaHangVe.Text = "HV00" + (Int32.Parse(TB_SoHangVe.Text) + 1).ToString();
            else
                TB_MaHangVe.Text = "HV000" + (Int32.Parse(TB_SoHangVe.Text) + 1).ToString();

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datatable_ChuyenBay = DataProvider.Instance.ExecuteQuery("select MaSanBay as 'Mã sân bay', TenSanBay as 'Tên sân bay' from SANBAY");
            datatable_ChuyenBay.PrimaryKey = new DataColumn[] { datatable_ChuyenBay.Columns["Mã sân bay"] };
            DGV_SanBay.ItemsSource = datatable_ChuyenBay.AsDataView();
            TB_SanBayToiDa.Text = datatable_ChuyenBay.Rows.Count.ToString();
            if (Int32.Parse(TB_SanBayToiDa.Text) >= 999)
                TB_MaSanBay.Text = "AP" + (Int32.Parse(TB_SanBayToiDa.Text) + 1).ToString();
            else if (Int32.Parse(TB_SanBayToiDa.Text) >= 99)
                TB_MaSanBay.Text = "AP0" + (Int32.Parse(TB_SanBayToiDa.Text) + 1).ToString();
            else if (Int32.Parse(TB_SanBayToiDa.Text) >= 9)
                TB_MaSanBay.Text = "AP00" + (Int32.Parse(TB_SanBayToiDa.Text) + 1).ToString();
            else
                TB_MaSanBay.Text = "AP000" + (Int32.Parse(TB_SanBayToiDa.Text) + 1).ToString();

            datatable_HangVe = DataProvider.Instance.ExecuteQuery("select MaHangVe as 'Mã hạng vé', TenHangVe as 'Tên hạng vé', TiLe as 'Tỉ lệ' from HANGVE");
            datatable_HangVe.PrimaryKey = new DataColumn[] { datatable_HangVe.Columns["Mã hạng vé"] };
            DGV_HangVe.ItemsSource = datatable_HangVe.AsDataView();
            TB_SoHangVe.Text = datatable_HangVe.Rows.Count.ToString();

            if (Int32.Parse(TB_SoHangVe.Text) >= 999)
                TB_MaHangVe.Text = "HV" + (Int32.Parse(TB_SoHangVe.Text) + 1).ToString();
            else if (Int32.Parse(TB_SoHangVe.Text) >= 99)
                TB_MaHangVe.Text = "HV0" + (Int32.Parse(TB_SoHangVe.Text) + 1).ToString();
            else if (Int32.Parse(TB_SoHangVe.Text) >= 9)
                TB_MaHangVe.Text = "HV00" + (Int32.Parse(TB_SoHangVe.Text) + 1).ToString();
            else
                TB_MaHangVe.Text = "HV000" + (Int32.Parse(TB_SoHangVe.Text) + 1).ToString();

            DataTable temp;
            temp = DataProvider.Instance.ExecuteQuery("select * from THAMSO");
            TB_SBTGToiDa.Text = temp.Rows[0].ItemArray[1].ToString();
            TB_TGBMin.Text = temp.Rows[0].ItemArray[2].ToString();
            TB_TGDMin.Text = temp.Rows[0].ItemArray[3].ToString();
            TB_TGDMax.Text = temp.Rows[0].ItemArray[4].ToString();
            TB_TGDatVeMax.Text = temp.Rows[0].ItemArray[6].ToString();
            TB_TGHuyVe.Text = temp.Rows[0].ItemArray[7].ToString();

            EPD_QDChuyenBay.IsExpanded = true;
            EPD_DSHangVe.IsExpanded = true;
            datatable_HangVe.Columns[0].ReadOnly = true;
        }

        private void BTN_ApDung_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRow i in datatable_HangVe.Rows)
            {
                float t = 0;
                if (!float.TryParse(i.ItemArray[2].ToString(), out t))
                {
                    MessageBox.Show("Tỉ lệ phải là số");
                    return;
                }
            }
            foreach (DataRow i in datatable_HangVe.Rows)
            {
                string command = "update HANGVE set TenHangVe = N'" + i.ItemArray[1].ToString() + "', TiLe = " + i.ItemArray[2].ToString() + " where MaHangVe = '" + i.ItemArray[0].ToString() + "'";
                DataProvider.Instance.ExecuteNonQuery(command);
            }
            MessageBox.Show("Chỉnh sửa thành công!");
        }


        private void EPD_QDChuyenBay_Collapsed(object sender, RoutedEventArgs e)
        {
            if (EPD_DSHangVe.IsExpanded == true)
                EPD_DSSanBay.IsExpanded = false;
        }

        private void EPD_DSHangVe_Collapsed(object sender, RoutedEventArgs e)
        {
            if (EPD_DSSanBay.IsExpanded == true)
                EPD_QDChuyenBay.IsExpanded = false;
        }

        private void EPD_DSSanBay_Collapsed(object sender, RoutedEventArgs e)
        {
            if (EPD_QDChuyenBay.IsExpanded == true)
                EPD_DSHangVe.IsExpanded = false;
        }

    }
}
