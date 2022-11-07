using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Bai02
{
    class KNCSDL
    {
        public SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-77FL8U47\SQLEXPRESS;Initial Catalog=QLHocSinh;Integrated Security=True");

        public List<string> loadKhoa()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                List<string> khoas = new List<string>();
                string cauLenh = "select TenKhoa from Khoa";
                SqlCommand cmd = new SqlCommand(cauLenh, conn);
                SqlDataReader read = cmd.ExecuteReader();
                while(read.Read())
                {
                    khoas.Add(read["TenKhoa"].ToString());
                }
                read.Close();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return khoas;
            }
            catch
            {
                return null;
            }
        }

        public List<string> loadLop()
        {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                List<string> lops = new List<string>();
                string cauLenh = "select TenLop from Lop";
                SqlCommand cmd = new SqlCommand(cauLenh, conn);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    lops.Add(read["TenLop"].ToString());
                }
                read.Close();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return lops;
        }

        public List<HocSinh> loadHocSinh()
        {
            List<HocSinh> dsHocSinh = new List<HocSinh>();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            DataTable dt = new DataTable();
            string cauLenh = "select * from SinhVien";
            SqlCommand cmd = new SqlCommand(cauLenh, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                HocSinh hs = new HocSinh();
                hs.mSV = item["MaSV"].ToString();
                hs.hTen = item["HoTen"].ToString();
                hs.gTinh = item["GioiTinh"].ToString();
                hs.nSinh = (DateTime)item["NgaySinh"];
                hs.dChi = item["DiaChi"].ToString();
                hs.mLop = item["MaLop"].ToString();
                dsHocSinh.Add(hs);
            }
            return dsHocSinh;
        }

        public bool themHocSinh(string mSV, string hTen, string gTinh, DateTime nSinh, string dChi, string mLop)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string cauLenh = "insert into SinhVien values ('"+mSV+"', N'"+hTen+"', '"+gTinh+"', '"+nSinh+"', N'"+dChi+"', '"+mLop+"')";
                SqlCommand cmd = new SqlCommand(cauLenh, conn);
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool suaHocSinh(string mSV, string hTen, string gTinh, DateTime nSinh, string dChi, string mLop)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string cauLenh = "update SinhVien set HoTen = N'"+hTen+"', GioiTinh = '"+gTinh+"', NgaySinh = '"+nSinh+"', DiaChi = N'"+dChi+"', MaLop = '"+mLop+"' where MaSV = '"+mSV+"'";
                SqlCommand cmd = new SqlCommand(cauLenh, conn);
                cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
