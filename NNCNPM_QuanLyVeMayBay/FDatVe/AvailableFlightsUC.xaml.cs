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

namespace NNCNPM_QuanLyVeMayBay.UserControls
{
    /// <summary>
    /// Interaction logic for AvailableFlights.xaml
    /// </summary>
    public partial class AvailableFlights : UserControl
    {
        public AvailableFlights()
        {
            InitializeComponent();
        }
        public AvailableFlights(string idChuyenBay, string SanBayDi , string SanBayDen,string ThoiGianDi,string ThoiGianBay)
        {
            TBL_ID_ChuyenBay.Text = idChuyenBay;
            TBL_NoiDen.Text = SanBayDen;
            TBL_ThoiGianBay.Text = ThoiGianBay;
            TBL_NoiDi.Text = SanBayDi;
            TBL_ThoiGianDi.Text = ThoiGianDi;
            InitializeComponent();
        }
    }
}
