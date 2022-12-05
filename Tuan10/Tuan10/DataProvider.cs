using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Tuan10
{
    public class DataProvider
    {
        SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-77FL8U47\SQLEXPRESS;Initial Catalog=QLSINHVIEN;Integrated Security=True");
        DataSet ds = new DataSet();

        public DataSet Ds
        {
            get { return ds; }
            set { ds = value; }
        }

        public DataProvider() { }

        public DataTable loadKhoa()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Khoa", cn);
            da.Fill(ds, "Khoa");
            return ds.Tables["Khoa"];
           
        }

        public DataTable loadLopTuKhoa(string mKhoa)
        {
            DataTable tb = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Lop where MaKhoa = '" + mKhoa + "'", cn);
            da.Fill(tb);
            return tb;
        }

        public DataTable loadListSinhVienTuLop(string mLop)
        {
            DataTable tb = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from SinhVien where MaLop = '" + mLop + "'", cn);
            da.Fill(tb);
            return tb;
        }

        public DataTable loadSinhVien()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from SinhVien", cn);
            da.Fill(ds, "SinhVien");
            return ds.Tables["SinhVien"];
        }

        public bool luu()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from SinhVien", cn);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "SinhVien");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int xoa(string mSV)
        {
            cn.Open();
            SqlCommand cb = new SqlCommand("delete from SinhVien where MaSinhVien = '"+mSV+"'", cn);
            int n = cb.ExecuteNonQuery();
            cn.Close();
            return n;
        }
    }
}
