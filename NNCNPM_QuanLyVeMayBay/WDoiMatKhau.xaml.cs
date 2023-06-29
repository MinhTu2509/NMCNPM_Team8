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
    /// Interaction logic for WDoiMatKhau.xaml
    /// </summary>
    public partial class WDoiMatKhau : Window
    {
        string UserName;
        public WDoiMatKhau( string Username = "")
        {
            InitializeComponent();
            this.UserName = Username;
        }

        private void BTN_Huy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BNT_XacNhan_Click(object sender, RoutedEventArgs e)
        {
            if (TB_Password.Text == "" || TB_NewPassword.Text == "")
            {
                L_Message.Content = "Mật khẩu bị trống";
                return;
            }
            if (TB_NewPassword.Text != TB_ReNewPassword.Text)
            {
                L_Message.Content = "Mật khẩu mới không khớp";
                return;
            }

            string command = "select * from USER_ACOUNT where UserName = '" + UserName + "'";

            DataTable table = new DataTable();
            table.Clear();
            table = DataProvider.Instance.ExecuteQuery(command);

            MD5 mh = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(TB_Password.Text);
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            if(sb.ToString() == table.Rows[0].ItemArray[1].ToString())
            {
                inputBytes = System.Text.Encoding.ASCII.GetBytes(TB_NewPassword.Text);
                hash = mh.ComputeHash(inputBytes);
                sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                command = "update USER_ACOUNT set UserPass = '" + sb.ToString() + "' where USERNAME = '" + UserName + "'";
                DataProvider.Instance.ExecuteNonQuery(command);
                MessageBox.Show("Đổi mật khẩu thành công");
                this.Close();
               
            }
            else
            {
                L_Message.Content = "Sai mật khẩu cũ!";
            }
        }
    }
}
