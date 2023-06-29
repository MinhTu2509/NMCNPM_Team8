using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCNPM_QuanLyVeMayBay.DAO
{
    public class BaoCaoDoanhThuDAO
    {
        #region Make singleton
        private static BaoCaoDoanhThuDAO instance;
        public static BaoCaoDoanhThuDAO Instance
        {
            get
            {
                if (instance == null) instance = new BaoCaoDoanhThuDAO(); return instance;
            }
            private set { instance = value; }
        }

        private BaoCaoDoanhThuDAO() { }
        #endregion


        /// <summary>
        /// Trả về số chuyến bay của từng tháng trong năm
        /// </summary>
        /// <param name="nam"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, int>> LaySoChuyenBayTheoNam(int nam)
        {
            List<KeyValuePair<string, int>> soChuyenBay = new List<KeyValuePair<string, int>>();

            for (int i = 1; i < 13; i++)
            {
                string query = string.Format("SELECT COUNT(cb.MaChuyenBay) FROM VEMAYBAY vmb, CHUYENBAY cb WHERE vmb.MaChuyenBay = cb.MaChuyenBay AND YEAR(cb.NgayBay) = {0} AND MONTH(cb.NgayBay) = {1}", nam, i);

                int _soChuyenBay = (int)DataProvider.Instance.ExecuteScalar(query);

                soChuyenBay.Add(new KeyValuePair<string, int>(i.ToString(), _soChuyenBay));
            }

            return soChuyenBay;
        }


        /// <summary>
        /// Trả về doanh thu của từng tháng trong năm
        /// </summary>
        /// <param name="nam"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, int>> LayDoanhThuTheoNam(int nam)
        {
            List<KeyValuePair<string, int>> doanhThu = new List<KeyValuePair<string, int>>();

            for (int i = 1; i < 13; i++)
            {
                string query = string.Format("SELECT SUM(vmb.GiaTien) FROM VEMAYBAY vmb, CHUYENBAY cb WHERE vmb.LoaiVe = 'Da thanh toan' AND vmb.MaChuyenBay = cb.MaChuyenBay AND YEAR(cb.NgayBay) = {0} AND MONTH(cb.NgayBay) = {1}", nam, i);

                try
                {
                    System.Decimal __doanhthu = (System.Decimal)DataProvider.Instance.ExecuteScalar(query);

                    int _doanhThu = Convert.ToInt32(__doanhthu);

                    doanhThu.Add(new KeyValuePair<string, int>(i.ToString(), _doanhThu));
                }
                catch
                {
                    doanhThu.Add(new KeyValuePair<string, int>(i.ToString(), 0));
                }
            }

            return doanhThu;
        }


        /// <summary>
        /// Trả về doanh thu của từng chuyến bay trong tháng 
        /// </summary>
        /// <param name="thang"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, int>> LayDoanhThuTheoThang(int nam, int thang)
        {
            List<KeyValuePair<string, int>> doanhThu = new List<KeyValuePair<string, int>>();

            string query = string.Format("SELECT cb.MaChuyenBay, REPLACE(CONVERT(varchar(20),  REPLACE(CONVERT(varchar(20), SUM(vmb.GiaTien), 1), '.00', ''), 1), '.00', '')  DoanhThu FROM(SELECT * FROM CHUYENBAY cb WHERE MONTH(cb.NgayBay) = {0} AND YEAR(cb.NgayBay) = {1}) cb LEFT JOIN VeMayBay vmb ON cb.MaChuyenBay = vmb.MaChuyenBay GROUP BY cb.MaChuyenBay ", thang, nam);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                string maChuyenBay = (string)row["MaChuyenBay"];

                System.Decimal _doanhThu = 0;
                int __doanhThu = 0;
                try
                {
                    _doanhThu = (System.Decimal)row["DoanhThu"];
                    __doanhThu = Convert.ToInt32(_doanhThu);
                }
                catch
                {

                }

                doanhThu.Add(new KeyValuePair<string, int>(maChuyenBay, __doanhThu));
            }

            return doanhThu;
        }


        /// <summary>
        /// Hàm trả về số vé từng chuyến bay trong tháng
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="thang"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, int>> LaySoVeTheoThang(int nam, int thang)
        {
            List<KeyValuePair<string, int>> soVe = new List<KeyValuePair<string, int>>();

            string query = string.Format("SELECT cb.MaChuyenBay, COUNT(vmb.MaChuyenBay) SoVe FROM(SELECT * FROM CHUYENBAY cb WHERE MONTH(cb.NgayBay) = {0} AND YEAR(cb.NgayBay) = {1}) cb LEFT JOIN VeMayBay vmb ON vmb.LoaiVe = 'Da thanh toan' AND cb.MaChuyenBay = vmb.MaChuyenBay GROUP BY cb.MaChuyenBay ", thang, nam);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                string maChuyenBay = (string)row["MaChuyenBay"];

                System.Decimal _soVe = 0;
                int __soVe = 0;
                try
                {
                    _soVe = (System.Decimal)row["SoVe"];
                    __soVe = Convert.ToInt32(_soVe);
                }
                catch
                {

                }

                soVe.Add(new KeyValuePair<string, int>(maChuyenBay, __soVe));
            }

            return soVe;
        }

        /// <summary>
        /// Hàm xuất file exel doanh thu theo tháng 
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="thang"></param>
        public void XuatFileExelTheoThang(int nam, int thang)
        {
            string query = string.Format("SELECT cb.MaChuyenBay, Count(vmb.MaChuyenBay) SoVe,  REPLACE(CONVERT(varchar(20),  REPLACE(CONVERT(varchar(20), SUM(vmb.GiaTien), 1), '.00', ''), 1), '.00', '') DoanhThu FROM(SELECT * FROM CHUYENBAY cb WHERE MONTH(cb.NgayBay) = {0} AND YEAR(cb.NgayBay) = {1}) cb LEFT JOIN VeMayBay vmb ON vmb.LoaiVe = 'Da thanh toan' AND cb.MaChuyenBay = vmb.MaChuyenBay GROUP BY cb.MaChuyenBay ", thang, nam);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < data.Columns.Count + 1; i++)

                {
                    xcelApp.Cells[1, i] = data.Columns[i - 1].ColumnName;
                }
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        xcelApp.Cells[i + 2, j + 1] = data.Rows[i][j].ToString();
                    }
                }

                xcelApp.Columns.AutoFit();
                xcelApp.Visible = true;
            }
            else
            {

            }
        }

        /// <summary>
        /// Hàm xuất file exel doanh thu theo năm 
        /// </summary>
        /// <param name="nam"></param>
        public void XuatFileExelTheoNam(int nam)
        {
            DataTable data = new DataTable();

            for (int i = 1; i < 13; i++)
            {
                string query = string.Format("SELECT COUNT(cb.MaChuyenBay) 'Số Chuyến Bay',  REPLACE(CONVERT(varchar(20), SUM(vmb.GiaTien), 1), '.00', '') 'Doanh Thu' FROM VEMAYBAY vmb, CHUYENBAY cb WHERE vmb.LoaiVe = 'Da thanh toan' AND vmb.MaChuyenBay = cb.MaChuyenBay AND YEAR(cb.NgayBay) = {0} AND MONTH(cb.NgayBay) = {1}", nam, i);

                DataTable dataRow = DataProvider.Instance.ExecuteQuery(query);

                data.Merge(dataRow);

            }


            if (data.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);
                xcelApp.Cells[1, 1] = "Tháng";

                //Thêm cột tháng
                for (int i = 1; i < 13; i++)
                {
                    xcelApp.Cells[i + 1, 1] = i.ToString();
                }

                //Thêm tên các cột trong Datatable
                for (int i = 1; i < data.Columns.Count + 1; i++)
                {
                    xcelApp.Cells[1, i + 1] = data.Columns[i - 1].ColumnName;
                }

                //Thêm giá trị
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        xcelApp.Cells[i + 2, j + 2] = data.Rows[i][j].ToString();
                    }
                }

                xcelApp.Columns.AutoFit();
                xcelApp.Visible = true;
            }
            else
            {

            }
        }

        /// <summary>
        /// Hàm trả về Datatable doanh thu theo tháng 
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="thang"></param>
        public DataTable BaoCaoDoanhThuTheoThang(int nam, int thang)
        {
            string query = string.Format("SELECT cb.MaChuyenBay 'Mã chuyến bay', Count(vmb.MaChuyenBay) 'Số vé',   REPLACE(CONVERT(varchar(20), SUM(vmb.GiaTien), 1), '.00', '') 'Doanh thu' FROM(SELECT * FROM CHUYENBAY cb WHERE MONTH(cb.NgayBay) = {0} AND YEAR(cb.NgayBay) = {1}) cb LEFT JOIN VeMayBay vmb ON vmb.LoaiVe = 'Da thanh toan' AND cb.MaChuyenBay = vmb.MaChuyenBay GROUP BY cb.MaChuyenBay ", thang, nam);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            return data;
        }
    }
}
