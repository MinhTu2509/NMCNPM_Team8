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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            TB_UserName.Text = Properties.Settings.Default.Username;
            TB_UserPass.Password = Properties.Settings.Default.Password;
            
        }

        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            table.Clear();

            string command = "select * from USER_ACOUNT where UserName = '" + TB_UserName.Text + "'";
            table = DataProvider.Instance.ExecuteQuery(command);
            if (table.Rows.Count < 1)
            {
                L_Message.Content = "Sai tên đăng nhập!";
                return;
            }

            MD5 mh = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(TB_UserPass.Password);
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            if (sb.ToString() == table.Rows[0].ItemArray[1].ToString())
            {
                if (CB_NhoPass.IsChecked == true)
                {
                    Properties.Settings.Default.Username = TB_UserName.Text;
                    Properties.Settings.Default.Password = TB_UserPass.Password;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.Username = "";
                    Properties.Settings.Default.Password = "";
                    Properties.Settings.Default.Save();
                }
                MainWindow mainWindow = new MainWindow(TB_UserName.Text);
                mainWindow.Owner = this;
                mainWindow.Show();
                L_Message.Content = "";
                this.Hide();
            }
            else
            {
                L_Message.Content = "Sai mật khẩu !";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
