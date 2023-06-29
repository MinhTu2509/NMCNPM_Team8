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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NNCNPM_QuanLyVeMayBay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool islogout = false;
        string UserName = "";
        int nowWindow = 0;
        public MainWindow(string name = "")
        {
            InitializeComponent();
            WTrangChu child = new WTrangChu(name);
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
            UserName = name;
            if (UserName.ToLower() != "admin")
            {
                BTN_CaiDat.Visibility = Visibility.Hidden;
            }
                    }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.GridTabWindow.Children.Clear();

        }

        private void bnt_ChuyenBay_Click(object sender, RoutedEventArgs e)
        {
            if (nowWindow == 1)
                return;
            BTN_ChuyenBay.Background = new SolidColorBrush(Color.FromArgb(255, 0,76,153));
            BTN_BaoCao.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_CaiDat.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_TrangChu.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_TimKiem.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_VeChuyenBay.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            nowWindow = 1;
            WChuyenBay child = new WChuyenBay();
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }

        private void bnt_VeMayBay_Click(object sender, RoutedEventArgs e)
        {
            if (nowWindow == 2)
                return;
            nowWindow = 2;
            BTN_VeChuyenBay.Background = new SolidColorBrush(Color.FromArgb(255, 0,76,153));
            BTN_BaoCao.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_CaiDat.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_ChuyenBay.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_TimKiem.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_TrangChu.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            WVeMayBay child = new WVeMayBay();
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }

        private void bnt_TimKiemChuyenBay_Click(object sender, RoutedEventArgs e)
        {
            if (nowWindow == 3)
                return;
            nowWindow = 3;
            BTN_TimKiem.Background = new SolidColorBrush(Color.FromArgb(255, 0,76,153));
            BTN_BaoCao.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_CaiDat.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_ChuyenBay.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_TrangChu.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_VeChuyenBay.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));

            WTimKiemChuyenBay child = new WTimKiemChuyenBay();
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }

        private void bnt_BaoCaoDoanhThu_Click(object sender, RoutedEventArgs e)
        {
            if (nowWindow == 4)
                return;
            nowWindow = 4;
            BTN_BaoCao.Background = new SolidColorBrush(Color.FromArgb(255, 0,76,153));
            BTN_TrangChu.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_CaiDat.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_ChuyenBay.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_TimKiem.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_VeChuyenBay.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));

            WBaoCaoDoanhThu child = new WBaoCaoDoanhThu();
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            islogout = true;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (islogout == false)
            {
                Environment.Exit(0);
            }
        }


        private void BTN_CaiDat_Click(object sender, RoutedEventArgs e)
        {
            if (nowWindow == 5)
                return;
            nowWindow = 5;
            BTN_CaiDat.Background = new SolidColorBrush(Color.FromArgb(255, 0,76,153));
            BTN_BaoCao.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_TrangChu.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_ChuyenBay.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_TimKiem.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_VeChuyenBay.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            WCaiDat child = new WCaiDat();
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }

        private void BTN_TrangChu_Click(object sender, RoutedEventArgs e)
        {
            if (nowWindow == 0)
                return;
            BTN_TrangChu.Background = new SolidColorBrush(Color.FromArgb(255, 0,76,153));
            BTN_BaoCao.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_CaiDat.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_ChuyenBay.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_TimKiem.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            BTN_VeChuyenBay.Background = new SolidColorBrush(Color.FromArgb(255, 0,30,60));
            nowWindow = 0;

            WTrangChu child = new WTrangChu(UserName);
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
