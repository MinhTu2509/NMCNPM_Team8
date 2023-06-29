using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for WThemTaiKhoan.xaml
    /// </summary>
    public partial class WThemTaiKhoan : Window
    {
        public WThemTaiKhoan()
        {
            InitializeComponent();
        }

        private void BTN_Huy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BNT_XacNhan_Click(object sender, RoutedEventArgs e)
        {          
            if(TB_UserName.Text == string.Empty)
            {
                L_Message.Content = "Phải điền tên đăng nhập!";
            }

            string command = "select * from USER_ACOUNT where UserName = '" + TB_UserName.Text + "'";

            DataTable table = new DataTable();
            table.Clear();
            table = DataProvider.Instance.ExecuteQuery(command);
            if(table.Rows.Count > 0)
            {
                L_Message.Content = "Tên tài khoản đã được sử dụng";
                return;
            }

            MD5 mh = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes("1");
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            command = "insert into USER_ACOUNT values ('" + TB_UserName.Text + "','" + sb.ToString() + "')";
            DataProvider.Instance.ExecuteNonQuery(command);
            MessageBox.Show("Thêm tài khoản thành công! ");
            this.Close();
        }
    }
}
