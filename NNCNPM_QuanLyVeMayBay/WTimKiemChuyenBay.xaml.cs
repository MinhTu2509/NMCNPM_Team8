using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NNCNPM_QuanLyVeMayBay
{
    /// <summary>
    /// Interaction logic for WTimKiemChuyenBay.xaml
    /// </summary>
    public partial class WTimKiemChuyenBay : Window
    {
        private DataTable datatable_ChuyenBay;
        private DataTable datatable_SanBayDi;
        private DataTable datatable_SanBayDen;
        DataTable dt = new DataTable();
        List<string> list_MaSBDi = new List<string>();
        List<string> list_MaSBDen = new List<string>();
        List<string> list_TenSBDi = new List<string>();
        List<string> list_TenSBDen = new List<string>();

        public WTimKiemChuyenBay()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void WTimKiemChuyenBay_Loaded(object sender, RoutedEventArgs e)

        {
            datatable_ChuyenBay = DataProvider.Instance.ExecuteQuery("select MaChuyenBay as'Chuyến bay',MaSanBayDi as 'Mã sân bay đi',SB1.TenSanBay as 'Sân bay đi',MaSanBayDen as 'Mã sân bay đến',SB2.TenSanBay as 'Sân bay đến', REPLACE(CONVERT(varchar(20), GiaVe, 1), '.00', '') as 'Giá vé',NgayBay as 'Ngày-Giờ khởi hành', ThoiGianBay as 'Thời gian bay(phút)' from CHUYENBAY,SANBAY SB1,SANBAY SB2 where CHUYENBAY.MaSanBayDi=SB1.MaSanBay and CHUYENBAY.MaSanBayDen=SB2.MaSanBay ORDER BY CHUYENBAY.NGAYBAY DESC", new object[] { "chuyenbay" });
            datatable_SanBayDi = DataProvider.Instance.ExecuteQuery("select distinct MaSanBayDi,TenSanBay from CHUYENBAY,SanBay where CHUYENBAY.MaSanBayDi=SANBAY.MaSanBay", new object[] { "chuyenbay" });
            datatable_SanBayDen = DataProvider.Instance.ExecuteQuery("select distinct MaSanBayDen,TenSanBay from CHUYENBAY,SanBay where CHUYENBAY.MaSanBayDen=SANBAY.MaSanBay", new object[] { "chuyenbay" });
            datagrid.ItemsSource = datatable_ChuyenBay.AsDataView();
            datagrid.IsReadOnly = true;
            datagrid.Columns[1].Visibility = Visibility.Collapsed;
            datagrid.Columns[3].Visibility = Visibility.Collapsed;
            list_MaSBDi.Add("Không chọn");
            list_MaSBDen.Add("Không chọn");
            list_TenSBDi.Add("Không chọn");
            list_TenSBDen.Add("Không chọn");
            foreach (DataRow dr in datatable_SanBayDi.Rows)
            {
                list_MaSBDi.Add(dr[0].ToString());
                list_TenSBDi.Add(dr[1].ToString());
            }

            foreach (DataRow dr in datatable_SanBayDen.Rows)
            {
                list_MaSBDen.Add(dr[0].ToString());
                list_TenSBDen.Add(dr[1].ToString());
            }

            cbb_SanBayDi.ItemsSource = list_TenSBDi;
            cbb_SanBayDen.ItemsSource = list_TenSBDen;
            cbb_SanBayDi.SelectedIndex = 0;
            cbb_SanBayDen.SelectedIndex = 0;
            if (datatable_ChuyenBay.Rows.Count > 0)
            {
                tb_GiaVeMax.Text = datatable_ChuyenBay.AsEnumerable().Max(row => Convert.ToInt32(row[5].ToString().Replace(",", ""))).ToString();
                tb_GiaVeMin.Text = datatable_ChuyenBay.AsEnumerable().Min(row => Convert.ToInt32(row[5].ToString().Replace(",", ""))).ToString();
                tb_TGBayMax.Text = datatable_ChuyenBay.AsEnumerable().Max(row => Convert.ToInt32(row[7])).ToString();
                tb_TGBayMin.Text = datatable_ChuyenBay.AsEnumerable().Min(row => Convert.ToInt32(row[7])).ToString();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            LoadSanBay();

        }
        void LoadSanBay()
        {         
            dt = datatable_ChuyenBay.Copy();
            if (tb_MaChuyenBay.Text.Length > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gt_tim = tb_MaChuyenBay.Text;
                    string ma = dt.Rows[i][0].ToString();
                    if (!ma.Contains(gt_tim))
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (cbb_SanBayDi.SelectedIndex != -1 && cbb_SanBayDi.SelectedValue.ToString() != "Không chọn")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][1].ToString() != list_MaSBDi[cbb_SanBayDi.SelectedIndex].ToString())
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (cbb_SanBayDen.SelectedIndex != -1 && cbb_SanBayDen.SelectedValue.ToString() != "Không chọn")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][3].ToString() != list_MaSBDen[cbb_SanBayDen.SelectedIndex].ToString())
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (dtp_NgayBay.Text.Length > 0)
            {
                DateTime date = dtp_NgayBay.SelectedDate.Value;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DateTime dateTime = DateTime.Parse(dt.Rows[i][6].ToString());

                    if (dateTime.ToString("dd/MM/yyyy") != date.ToString("dd/MM/yyyy"))
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (dtp_GioBay.Text != null)
            {
                DateTime date = dtp_GioBay.SelectedTime.Value;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DateTime dateTime = DateTime.Parse(dt.Rows[i][6].ToString());

                    if (dateTime.ToString("HH:mm:ss") != date.ToString("HH:mm:ss"))
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (tb_TGBayMin.Text.Length > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    double Time = double.Parse(dt.Rows[i][7].ToString());

                    if (double.Parse(tb_TGBayMin.Text) > Time)
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (tb_TGBayMax.Text.Length > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    double Time = double.Parse(dt.Rows[i][7].ToString());

                    if (double.Parse(tb_TGBayMax.Text) < Time)
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (tb_GiaVeMin.Text.Length > 0)
            {

                double GiaVeTim = double.Parse(tb_GiaVeMin.Text.Replace(",", ""));
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    double GiaVe = double.Parse(dt.Rows[i][5].ToString().Replace(",", ""));

                    if (GiaVeTim > GiaVe)
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (tb_GiaVeMax.Text.Length > 0)
            {
                double GiaVeTim = double.Parse(tb_GiaVeMax.Text.Replace(",", ""));
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    double GiaVe = double.Parse(dt.Rows[i][5].ToString().Replace(",", ""));

                    if (GiaVeTim < GiaVe)
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (ck_ghetrong.IsChecked == true)
            {
                DataTable datatable_SoGhe = DataProvider.Instance.ExecuteQuery("select sum(TongSoGhe) as 'Tổng số ghế', sum(SoGheDaDat) as 'Số ghế đã đặt',MaChuyenBay from SOLUONGVE group by MaChuyenBay");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string s = datatable_ChuyenBay.Rows[i][0].ToString();
                    int a = 0, b = 0;
                    string c;
                    for (int j = 0; j < datatable_SoGhe.Rows.Count; j++)
                    {

                        a = int.Parse(datatable_SoGhe.Rows[j][0].ToString());
                        b = int.Parse(datatable_SoGhe.Rows[j][1].ToString());
                        c = datatable_SoGhe.Rows[j][2].ToString();
                        if (b >= a && c == s)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
                datagrid.ItemsSource = dt.AsDataView();
                datagrid.Columns[1].Visibility = Visibility.Collapsed;
                datagrid.Columns[3].Visibility = Visibility.Collapsed;
            }
            else
            {
                datagrid.ItemsSource = dt.AsDataView();
                datagrid.Columns[1].Visibility = Visibility.Collapsed;
                datagrid.Columns[3].Visibility = Visibility.Collapsed;
            }
            if (dt.Rows.Count == 0)
            {
                datagrid.ItemsSource = null;

            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            LoadSanBay();
        }

        private void cbb_SanBayDi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadSanBay();
        }

        private void cbb_SanBayDen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadSanBay();
        }

        private void dtp_NgayBay_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadSanBay();
        }

        private void dtp_GioBay_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            LoadSanBay();
        }

        private void tb_TGBayMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadSanBay();
        }

        private void tb_TGBayMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadSanBay();

        }

        private void tb_GiaVeMin_TextChanged(object sender, TextChangedEventArgs e)
        {

            tb_GiaVeMin.Text = String.Format("{0:###,###,##0}", tb_GiaVeMin.Text);
            if (tb_GiaVeMin.Text != "")
            {
                decimal number;
                number = decimal.Parse(tb_GiaVeMin.Text, System.Globalization.NumberStyles.Currency);
                tb_GiaVeMin.Text = number.ToString("#,#");
                tb_GiaVeMin.SelectionStart = tb_GiaVeMin.Text.Length;

            }
            LoadSanBay();
        }

        private void tb_GiaVeMax_TextChanged(object sender, TextChangedEventArgs e)
        {

            tb_GiaVeMax.Text = String.Format("{0:###,###,##0}", tb_GiaVeMax.Text);
            if (tb_GiaVeMax.Text != "")
            {
                decimal number;
                number = decimal.Parse(tb_GiaVeMax.Text, System.Globalization.NumberStyles.Currency);
                tb_GiaVeMax.Text = number.ToString("#,#");
                tb_GiaVeMax.SelectionStart = tb_GiaVeMax.Text.Length;

            }
            LoadSanBay();
        }

        private void tb_MaChuyenBay_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadSanBay();
        }

        private void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagrid.SelectedItem == null)
                return;
            DateTime t = DateTime.Now;
            DataRowView i = datagrid.SelectedItem as DataRowView;

            int n =DateTime.Compare(t, DateTime.Parse(i.Row.ItemArray[6].ToString()));
            if (n < 0)
            {
                WChiTietChuyenBay f = new WChiTietChuyenBay(i.Row.ItemArray[0].ToString(), i.Row.ItemArray[5].ToString(), i.Row.ItemArray[7].ToString(), 1);
                f.ShowDialog();
            }
            if(n>=0)
            {
                WChiTietChuyenBay f = new WChiTietChuyenBay(i.Row.ItemArray[0].ToString(), i.Row.ItemArray[5].ToString(), i.Row.ItemArray[7].ToString(), 0);
                f.ShowDialog();
            }    
        }
    }
}
