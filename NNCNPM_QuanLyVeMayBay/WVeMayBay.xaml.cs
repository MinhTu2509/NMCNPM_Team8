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
using NNCNPM_QuanLyVeMayBay.MyUserControls;
using System.Data;
using System.Windows.Threading;
using System.Text.RegularExpressions;

namespace NNCNPM_QuanLyVeMayBay
{
    /// <summary>
    /// Interaction logic for WVeMayBay.xaml
    /// </summary>
    public partial class WVeMayBay : Window
    {
        bool IsHKExist;
        DispatcherTimer timer = new DispatcherTimer();
        private void WChonChuyenBay_Thoat(object sender, EventArgs e)
        {
            if (TB_MaChuyenBay.Text != "")
                TB_HangVe.Text = "";
            this.TB_MaChuyenBay.Text = (sender as WChonChuyenBay).MaChuyenBayIsSelected;
            LoadThanhTien();
            (sender as Window).Close();
        }
        private void WChonHangVe_Thoat(object sender, EventArgs e)
        {
            this.TB_HangVe.Text = (sender as WChonHangVe).TenHangVeIsSelected;
            LoadThanhTien();
            (sender as Window).Close();
        }
        private void HuyVe(object sender, EventArgs e)
        {
            HuyVe();
        }
        private void LoadAfterHuyVe(object sender, EventArgs e)
        {
            Load();
        }
        void HuyVe()
        {
            string query = "DELETE VEMAYBAY WHERE VEMAYBAY.Loaive = 'Ve Dat Cho' AND VEMAYBAY.MaChuyenBay IN ( SELECT CHUYENBAY.MaChuyenBay FROM CHUYENBAY, THAMSO WHERE (DATEDIFF(HOUR, CURRENT_TIMESTAMP, CHUYENBAY.NgayBay)) < THAMSO.ThoiGianHuyVe)";
            DataProvider.Instance.ExecuteNonQuery(query);
        }
        void LoadThanhTien()
        {
            if (TB_MaChuyenBay.Text != "" && TB_HangVe.Text != "")
            {
                double giatien = Convert.ToDouble(DataProvider.Instance.ExecuteScalar("SELECT CHUYENBAY.GiaVe * HANGVE.TiLe FROM CHUYENBAY, HANGVE WHERE CHUYENBAY.MaChuyenBay = '" + TB_MaChuyenBay.Text + "'  AND HANGVE.TenHangVe = N'" + TB_HangVe.Text + "' ").ToString());
                TB_ThanhTien.Text = String.Format("{0:0,0}", giatien);
            }
            else TB_ThanhTien.Text = "";
        }
        public WVeMayBay()
        {
            InitializeComponent();
            List<string> Loaive = new List<string>();
            Loaive.Add("Da thanh toan");
            Loaive.Add("Chua thanh toan");
            CB_LoaiVe.ItemsSource = Loaive;
            IsHKExist = false;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 3, 0);
            timer.Start();
            HuyVe();
            Load();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            HuyVe();
            Load();
        }

        void Load()
        {
            SPN_ShowList.Children.Clear();
            DataTable table = new DataTable();
            table.Clear();
            string query = "select VEMAYBAY.MaVe, HANGVE.TenHangVe, CHUYENBAY.MaChuyenBay, CHUYENBAY.NgayBay, SB1.TenSanBay AS SBDi, SB2.TenSanBay AS ABDen, HANHKHACH.CMND, HANHKHACH.HoTen, HANHKHACH.SoDienThoai, VEMAYBAY.GiaTien, VEMAYBAY.LoaiVe "
                            + "from CHUYENBAY, SANBAY SB1, SANBAY SB2, HANGVE, VEMAYBAY, HANHKHACH "
                            + "WHERE CHUYENBAY.MaSanBayDi = SB1.MaSanBay AND CHUYENBAY.MaSanBayDen = SB2.MaSanBay "
                            + "AND VEMAYBAY.MaChuyenBay = CHUYENBAY.MaChuyenBay AND VEMAYBAY.MaHangVe = HANGVE.MaHangVe AND VEMAYBAY.CMND = HANHKHACH.CMND "
                            + "AND VEMAYBAY.CMND LIKE '%" + TB_TimVe.Text.ToString() + "%' "
                            + "AND CHUYENBAY.NGAYBAY >= '"+DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")+"' "
                            + "ORDER BY CHUYENBAY.NGAYBAY DESC "; 
            table = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow i in table.Rows)
            {
                MyUserControls.TicketUC ticket = new TicketUC();

                if (!i.ItemArray[10].ToString().Equals("Da thanh toan"))
                {
                    Uri fileUri = new Uri("/Icon/icons8-booking-50.png", UriKind.Relative);
                    ticket.IMG_LoaiVe.Source = new BitmapImage(fileUri);
                    ticket.border_ticket.Background = Brushes.LightYellow;
                    ticket.border_ticket.BorderBrush = Brushes.Orange;
                }
                else
                {
                    ticket.BTN_Ban.Visibility = Visibility.Hidden;
                }
                ticket.MaVe = i.ItemArray[0].ToString();
                ticket.TB_HangVe.Text = i.ItemArray[1].ToString();
                ticket.TB_MaChuyenBay.Text = i.ItemArray[2].ToString();
                ticket.TB_NgayBay.Text = i.ItemArray[3].ToString();
                ticket.TB_SanBayDi.Text = i.ItemArray[4].ToString();
                ticket.TB_SanBayDen.Text = i.ItemArray[5].ToString();
                ticket.TB_CMND.Text = i.ItemArray[6].ToString();
                ticket.TB_TenHanhKhach.Text = i.ItemArray[7].ToString();
                ticket.TB_SDT.Text = i.ItemArray[8].ToString();
                string Tien = i.ItemArray[9].ToString();
                double tien = Convert.ToDouble(Tien);
                ticket.TB_ThanhTien.Text = String.Format("{0:0,0}", tien);
                ticket.HuyVe += HuyVe;
                ticket.LoadAfterHuyVe += LoadAfterHuyVe;

                SPN_ShowList.Children.Add(ticket);
            }
        }
        void SetDefaultControls()
        {
            for (int i = 0; i < 10; i++)
            {
                MyUserControls.TicketUC ticket = new TicketUC();

                SPN_ShowList.Children.Add(ticket);
            }

        }
        private void TB_TimVe_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load();
        }

        private void BTN_MaChuyenBay_Click(object sender, RoutedEventArgs e)
        {
            if (DP_NgayBay.Text == "")
            {
                MessageBox.Show("Chưa nhập ngày bay", "Thông báo", MessageBoxButton.OK);
                return;
            }
            WChonChuyenBay chonChuyenBay = new WChonChuyenBay(DP_NgayBay.SelectedDate.Value);
            chonChuyenBay.Thoat += WChonChuyenBay_Thoat;
            //chonChuyenBay.Left = this.Left + this.Width;
            //chonChuyenBay.Top = this.Top + this.Height;
            chonChuyenBay.ShowDialog();


        }

        private void BTN_HangVe_Click(object sender, RoutedEventArgs e)
        {
            if (TB_MaChuyenBay.Text == "")
            {
                MessageBox.Show("Chưa nhập mã chuyến bay", "Thông báo", MessageBoxButton.OK);
                return;
            }
            WChonHangVe chonHangVe = new WChonHangVe(TB_MaChuyenBay.Text);
            chonHangVe.Thoat += WChonHangVe_Thoat;
            chonHangVe.ShowDialog();
        }

        private void TB_CMND_TextChanged(object sender, TextChangedEventArgs e)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM HANHKHACH WHERE HANHKHACH.CMND = '" + TB_CMND.Text + "'";
            count = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query).ToString());
            if (count == 1)
            {
                IsHKExist = true;
                DataTable dataTable = new DataTable();
                dataTable.Clear();
                dataTable = DataProvider.Instance.ExecuteQuery("SELECT * FROM HANHKHACH WHERE HANHKHACH.CMND = '" + TB_CMND.Text + "'");
                foreach (DataRow i in dataTable.Rows)
                {
                    TB_TenKH.Text = i.ItemArray[1].ToString();
                    TB_SDT.Text = i.ItemArray[2].ToString();
                }

            }
            else
            {
                IsHKExist = false;
                TB_TenKH.Text = "";
                TB_SDT.Text = "";
            }
        }

        private void BTN_HoanTat_Click(object sender, RoutedEventArgs e)
        {
            string ID = "";
            if (DP_NgayBay.Text == "")
            {
                MessageBox.Show("Chưa điền ngày bay", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (TB_MaChuyenBay.Text == "")
            {
                MessageBox.Show("Chưa chọn chuyến bay", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (TB_HangVe.Text == "")
            {
                MessageBox.Show("Chưa chọn hạng vé", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (TB_CMND.Text == "")
            {
                MessageBox.Show("Chưa điền CMND/CCCD", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (TB_TenKH.Text == "")
            {
                MessageBox.Show("Chưa điền tên hành khách", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (TB_SDT.Text == "")
            {
                MessageBox.Show("Chưa điền số điện thoại", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (TB_SDT.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại điền không đúng!", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (CB_LoaiVe.Text == "")
            {
                MessageBox.Show("Chưa điền Loại vé", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (!IsHKExist)
            {
                try
                {
                    string query = "INSERT INTO HANHKHACH VALUES('" + TB_CMND.Text + "', N'" + TB_TenKH.Text + "','" + TB_SDT.Text + "')";
                    DataProvider.Instance.ExecuteNonQuery(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButton.OK);
                    return;
                }
            }
            try
            {
                int Soid = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM VEMAYBAY").ToString());
                string MaHangVe = DataProvider.Instance.ExecuteScalar("SELECT HANGVE.MaHangVe FROM HANGVE WHERE HANGVE.TenHangVe = N'" + TB_HangVe.Text + "'").ToString();
                string LoaiVe = "";
                ID = CreateMaVe(Soid);
                string query = "";
                if (CB_LoaiVe.Text == "Da thanh toan")
                {
                    LoaiVe = "Da thanh toan";
                }
                else LoaiVe = "Chua thanh toan";

                while (true)
                {
                    ID = CreateMaVe(Soid);
                    query = "SELECT COUNT(*) FROM VEMAYBAY WHERE VEMAYBAY.MaVe = '" + ID + "'";
                    int count = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query).ToString());
                    if (count == 1)
                        Soid++;
                    else break;
                }
                TB_ThanhTien.Text = TB_ThanhTien.Text.Replace(".", string.Empty);
                TB_ThanhTien.Text = TB_ThanhTien.Text.Replace(",", string.Empty);
                query = "INSERT INTO VEMAYBAY VALUES('" + ID + "','" + TB_MaChuyenBay.Text + "','" + TB_CMND.Text + "','" + MaHangVe + "'," + TB_ThanhTien.Text + ",'" + LoaiVe + "', '" + DateTime.Today.ToString("yyyy/MM/dd") + "')";
                DataProvider.Instance.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(CutExceptionMessage(ex.ToString()), "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (CB_LoaiVe.Text == "Da thanh toan")
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn in vé không?", "Thông báo", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    WPrintTicket printTicket = new WPrintTicket(ID);
                    printTicket.PrintTicket();
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Đã hoàn tất đặt vé", "Thông báo", MessageBoxButton.OK);
            }

            LoadEmpty();
            Load();
        }
        string CreateMaVe(int SoID)
        {
            string ID = "VE";
            int sochia = 1000;
            while (sochia > 0)
            {
                int n = SoID / sochia;
                ID = ID + n.ToString();
                SoID = SoID - n * sochia;
                sochia = sochia / 10;
            }
            return ID;
        }
        void LoadEmpty()
        {
            TB_MaChuyenBay.Text = "";
            TB_HangVe.Text = "";
            TB_CMND.Text = "";
            TB_TenKH.Text = "";
            TB_SDT.Text = "";
            CB_LoaiVe.Text = "";
            TB_ThanhTien.Text = "";
        }
        string CutExceptionMessage(string ex)
        {
            string result = ex;
            result = result.Substring(result.IndexOf("ERROR"));
            result = result.Substring(0, result.IndexOf("\n"));
            return result;
        }

        private void DP_NgayBay_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TB_MaChuyenBay.Text = "";
            TB_HangVe.Text = "";
            LoadThanhTien();
        }

        private void TB_CMND_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TB_SDT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TB_MaChuyenBay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
