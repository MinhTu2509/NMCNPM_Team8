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

namespace NNCNPM_QuanLyVeMayBay.MyUserControls
{
    /// <summary>
    /// Interaction logic for TicketUC.xaml
    /// </summary>
    public partial class TicketUC : UserControl
    {
        public string MaVe;
        public event EventHandler HuyVe;
        public event EventHandler LoadAfterHuyVe;
        public TicketUC()
        {
            InitializeComponent();
        }
        public event EventHandler LoadDSVeMayBay;


        private void btn_Book_Click(object sender, RoutedEventArgs e)
        {
            //Window window = (Window)ControlUC.Instance.GetWindowParent(this);

            //window.Background = Brushes.Red;
            //this.Background = Brushes.Red;
        }

        #region Method lấy Window từ UserControl


        #endregion

        private void BTN_Ban_Click(object sender, RoutedEventArgs e)
        {
            HuyVe(this, new EventArgs());
            string query = "SELECT COUNT(*) FROM VEMAYBAY WHERE VEMAYBAY.LoaiVe = 'Chua thanh toan' AND VEMAYBAY.MaVe = '"+MaVe+"'";
            int count = 0;
            count = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query));
            if(count>0)
            {
                query = "UPDATE VEMAYBAY SET Loaive = 'Da thanh toan' where VEMAYBAY.MaVe = '" + MaVe + "' ";
                DataProvider.Instance.ExecuteNonQuery(query);
                MessageBoxResult result = MessageBox.Show("Bạn có muốn in vé không?", "Thông báo", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    WPrintTicket printTicket = new WPrintTicket(MaVe);
                    printTicket.PrintTicket();
                }
            }
            else
            {
                MessageBox.Show("Vé này đã bị hủy", "Thông báo", MessageBoxButton.OK);
            } 
                
            LoadAfterHuyVe(this, new EventArgs());
        }
    }
}
