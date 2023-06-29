using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NNCNPM_QuanLyVeMayBay.MyUserControls
{
    public class ControlUC
    {
        #region Make singleton
        private static ControlUC instance;
        public static ControlUC Instance
        {
            get
            {
                if (instance == null) instance = new ControlUC(); return instance;
            }
            private set { instance = value; }
        }

        private ControlUC() { }
        #endregion

        /// <summary>
        /// Hàm lấy window từ userContol
        /// </summary>
        /// <param name="uc"></param>
        /// <returns></returns>
        public FrameworkElement GetWindowParent(UserControl uc)
        {
            FrameworkElement parent = uc;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}
