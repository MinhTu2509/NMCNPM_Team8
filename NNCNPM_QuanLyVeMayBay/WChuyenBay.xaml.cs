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
    /// Interaction logic for WChuyenBay.xaml
    /// </summary>
    public partial class WChuyenBay : Window
    {
        private VMChuyenbay vmChuyenBay;
        List<string> MaSanBay = new List<string>();
        DataTable dt_TTChuyenBay;
        DataTable dt = new DataTable();
        public WChuyenBay()
        {
            InitializeComponent();
            vmChuyenBay = new VMChuyenbay();
        }

        private void LoadVeSanBay()
        {
            DataTable table;
            table = DataProvider.Instance.ExecuteQuery("select * from SANBAY");
            foreach (DataRow i in table.Rows)
            {
                vmChuyenBay.dataSanBayTrungGian.Add(new DataSanBayTrungGian(i.ItemArray[0].ToString(), i.ItemArray[1].ToString(), 0));
                CB_SanBayDen.Items.Add(i.ItemArray[1].ToString());
                CB_SanBayDi.Items.Add(i.ItemArray[1].ToString());
                MaSanBay.Add(i.ItemArray[0].ToString());
            }

            dtg_SanBayTrungGian.ItemsSource = vmChuyenBay.dataSanBayTrungGian;
            dtg_SanBayTrungGian.CanUserAddRows = false;
            dtg_SanBayTrungGian.Columns[0].IsReadOnly = true;

            table.Clear();
            table = DataProvider.Instance.ExecuteQuery("select * from HANGVE");
            
            foreach(DataRow i in table.Rows)
            {
                vmChuyenBay.dataLoaiVe.Add(new DataLoaiVe(i.ItemArray[0].ToString(), i.ItemArray[1].ToString(), i.ItemArray[2].ToString(), 0));
            }
            dtg_LoaiVe.ItemsSource = vmChuyenBay.dataLoaiVe;
            dtg_LoaiVe.CanUserAddRows = false;
            dtg_LoaiVe.Columns[0].IsReadOnly = true;
            dtg_LoaiVe.Columns[1].IsReadOnly = true;

        }
        private void IsNumberic_only(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            double val;
            e.Handled = !double.TryParse(fullText, out val);
        }

        private void bt_Them_Click(object sender, RoutedEventArgs e)
        {
 
            if (tb_MaChuyenBay.Text == "" || CB_SanBayDi.Text == "" || CB_SanBayDen.Text == "" || DP_NgayBay.Text == "" || TP_NgayBay.Text == "" || tb_ThoiGianBay.Text == "" || tb_PhutBay.Text == "" || tb_GiaVe.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataTable table;
            table = DataProvider.Instance.ExecuteQuery("select [SoLuongSanBayTGToiDa],[ThoiGianBayToiThieu], [ThoiGianDungToiThieu] ,[ThoiGianDungToiDa]from THAMSO");
            int SoSanBayTrungGian = Int32.Parse(table.Rows[0].ItemArray[0].ToString());
            int ThoiGianBayToiThieu = Int32.Parse(table.Rows[0].ItemArray[1].ToString());
            int ThoiGianDungToiThieu = Int32.Parse(table.Rows[0].ItemArray[2].ToString());
            int ThoiGianDungToiDa = Int32.Parse(table.Rows[0].ItemArray[3].ToString());
            int flytime = Int32.Parse(tb_ThoiGianBay.Text) * 60 + Int32.Parse(tb_PhutBay.Text);

            if (Int32.Parse(tb_ThoiGianBay.Text.ToString()) < 0 || Int32.Parse(tb_PhutBay.Text.ToString())<0)
            {
                MessageBox.Show("Thời gian bay phải lớn hơn 0!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DP_NgayBay.SelectedDate < DateTime.Today)
            {
                MessageBox.Show("Ngày bay phải lớn hơn ngày hôm nay!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CB_SanBayDen.Text == CB_SanBayDi.Text)
            {
                MessageBox.Show("Sân bay đi phải khác sân bay đến! ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(flytime< ThoiGianBayToiThieu)
            {
                MessageBox.Show("Thời gian bay tối thiểu là "+ThoiGianBayToiThieu+" phút ! ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (DataSanBayTrungGian i in vmChuyenBay.dataSanBayTrungGian)
            {
                if (i.ThoiGianDung != 0)
                {
                    SoSanBayTrungGian -= 1;
                    if (i.TenSanBay == CB_SanBayDi.Text || i.TenSanBay == CB_SanBayDen.Text)
                    {
                        MessageBox.Show("Sân bay trung gian không được trùng sân bay đi và đến!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    int temp;
                    if (i.ThoiGianDung < ThoiGianDungToiThieu || i.ThoiGianDung > ThoiGianDungToiDa || !Int32.TryParse(i.ThoiGianDung.ToString(), out temp))
                    {
                        MessageBox.Show("Thời gian dừng phải lớn hơn " + ThoiGianDungToiThieu + " và nhỏ hơn " + ThoiGianDungToiDa + "!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                if (SoSanBayTrungGian < 0)
                {
                    MessageBox.Show("Số sân bay trung gian vượt quá giới hạn cho phép!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            foreach (DataLoaiVe i in vmChuyenBay.dataLoaiVe)
            {
                int temp;
                if (i.SoLuong < 0 || !Int32.TryParse(i.SoLuong.ToString(), out temp))
                {
                    MessageBox.Show("Số lượng vé phải lớn hơn hoặc bằng 0!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            var txt = this.DP_NgayBay.SelectedDate.Value.Date.ToString("yyyy-MM-dd") +" "+ TP_NgayBay.Text.ToString() + ":00";
            string command = "insert into CHUYENBAY values('" + tb_MaChuyenBay.Text + "'" +
                ",'" + MaSanBay.ElementAt(CB_SanBayDi.SelectedIndex).ToString() + "'" +
                ", '" + MaSanBay.ElementAt(CB_SanBayDen.SelectedIndex).ToString() + "'" +
                ", '" + tb_GiaVe.Text + "','" + txt + "', '" + flytime.ToString() + "')";
            DataProvider.Instance.ExecuteNonQuery(command);
           
            foreach(DataLoaiVe i in vmChuyenBay.dataLoaiVe)
            {
                if(i.SoLuong > 0)
                {
                    command = "Insert into SOLUONGVE values ('"+tb_MaChuyenBay.Text+"','"+i.MaLoaiVe+"','"+i.SoLuong+"',0)";
                    DataProvider.Instance.ExecuteNonQuery(command);
                }
            }

            foreach (DataSanBayTrungGian i in vmChuyenBay.dataSanBayTrungGian)
            {
                if (i.ThoiGianDung > 0)
                {
                    command = "Insert into TRUNGGIAN values ('" + tb_MaChuyenBay.Text + "','" + i.MaSanBay + "','" + i.ThoiGianDung + "','"+i.NghiChu+"')";
                    DataProvider.Instance.ExecuteNonQuery(command);
                }
            }
            MessageBox.Show("Thêm lịch trình bay thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadDSChuyenBay();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDSChuyenBay();
            LoadVeSanBay();
            EDP_SanBay.IsExpanded = true;
        }
        private void LoadDSChuyenBay()
        {
            dt_TTChuyenBay = DataProvider.Instance.ExecuteQuery("select MaChuyenBay as'Mã chuyến bay',SB1.TenSanBay as 'Sân bay đi',SB2.TenSanBay as 'Sân bay đến', REPLACE(CONVERT(varchar(20), GiaVe, 1), '.00', '') as 'Giá vé',NgayBay as 'Ngày-Giờ', ThoiGianBay as 'Thời gian bay(phút)' from CHUYENBAY,SANBAY SB1,SANBAY SB2 where CHUYENBAY.MaSanBayDi=SB1.MaSanBay and CHUYENBAY.MaSanBayDen=SB2.MaSanBay", new object[] { "chuyenbay" });
            dtg_DSChuyenBay.ItemsSource = dt_TTChuyenBay.AsDataView();
            dtg_DSChuyenBay.IsReadOnly = true;
            dtg_DSChuyenBay.CanUserAddRows = false;
            if (dt_TTChuyenBay.Rows.Count + 1 > 999)
                tb_MaChuyenBay.Text = "MH-" + DateTime.Today.ToString("yyyy") + DateTime.Today.ToString("MM") + "-" + (dt_TTChuyenBay.Rows.Count + 1);
            else if (dt_TTChuyenBay.Rows.Count + 1 > 99)
                tb_MaChuyenBay.Text = "MH-" + DateTime.Today.ToString("yyyy") + DateTime.Today.ToString("MM") + "-0" + (dt_TTChuyenBay.Rows.Count + 1);
            else if (dt_TTChuyenBay.Rows.Count + 1 > 9)
                tb_MaChuyenBay.Text = "MH-" + DateTime.Today.ToString("yyyy") + DateTime.Today.ToString("MM") + "-00" + (dt_TTChuyenBay.Rows.Count + 1);
            else 
                tb_MaChuyenBay.Text = "MH-" + DateTime.Today.ToString("yyyy") + DateTime.Today.ToString("MM") + "-000" + (dt_TTChuyenBay.Rows.Count + 1);
        }

        private void EDP_ChuyenBay_Expanded(object sender, RoutedEventArgs e)
        {
            EDP_SanBay.IsExpanded = false;
        }

        private void EDP_SanBay_Expanded(object sender, RoutedEventArgs e)
        {
                EDP_ChuyenBay.IsExpanded = false;
        }

        private void dtg_DSChuyenBay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dtg_DSChuyenBay.SelectedItem == null)
                return;
             DataRowView i = dtg_DSChuyenBay.SelectedItem as DataRowView;
            
            DateTime t = DateTime.Now;
            int n = DateTime.Compare(t, DateTime.Parse(i.Row.ItemArray[4].ToString()));
            if (n < 0)
            {
                WChiTietChuyenBay f = new WChiTietChuyenBay(i.Row.ItemArray[0].ToString(), i.Row.ItemArray[3].ToString(), i.Row.ItemArray[5].ToString(), 1);
                f.ShowDialog();
            }
            if (n >= 0)
            {
                WChiTietChuyenBay f = new WChiTietChuyenBay(i.Row.ItemArray[0].ToString(), i.Row.ItemArray[3].ToString(), i.Row.ItemArray[5].ToString(), 0);
                f.ShowDialog();
            }
        }
    }
}
