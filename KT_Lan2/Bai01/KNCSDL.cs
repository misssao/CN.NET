using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Bai01
{
    class KNCSDL
    {
        public SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-77FL8U47\SQLEXPRESS;Initial Catalog=QLHocSinh;Integrated Security=True");

        public List<DSKhoa> loadKhoa()
        {
            List<DSKhoa> khoas = new List<DSKhoa>();
            try
            {
                if(conn.State == ConnectionState.Closed)
                    conn.Open();
                string cauLenh = "select * from Khoa";
                SqlCommand cmd = new SqlCommand(cauLenh, conn);
                SqlDataReader dtRead = cmd.ExecuteReader();
                while(dtRead.Read())
                {
                    DSKhoa dsKhoa = new DSKhoa();
                    dsKhoa.MKhoa = dtRead["MaKhoa"].ToString();
                    dsKhoa.TKhoa = dtRead["TenKhoa"].ToString();
                    khoas.Add(dsKhoa);
                }
                dtRead.Close();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return khoas;
            }
            catch
            {
                return null;
            }
        }

        public List<string> loadLop(string mKhoa)
        {
            List<string> lops = new List<string>();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string cauLenh = "select TenLop from Lop where MaKhoa = '"+mKhoa+"'";
                SqlCommand cmd = new SqlCommand(cauLenh, conn);
                SqlDataReader dtRead = cmd.ExecuteReader();
                while (dtRead.Read())
                {
                    lops.Add(dtRead["TenLop"].ToString());
                }
                dtRead.Close();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return lops;
            }
            catch
            {
                return null;
            }
        }

        public bool ktTrung(string mKhoa)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string cauLenh = "select count(*) from Khoa where MaKhoa = '" + mKhoa + "'";
                SqlCommand cmd = new SqlCommand(cauLenh, conn);
                int dem = (int)cmd.ExecuteScalar();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                if (dem > 0)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool themKhoa(string mKhoa, string tKhoa)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string cauLenh = "insert into Khoa values ('"+mKhoa+"', N'"+tKhoa+"')";
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

        public bool suaKhoa(string mKhoa, string tKhoa)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string cauLenh = "update Khoa set TenKhoa = N'"+tKhoa+"' where MaKhoa = '"+mKhoa+"' ";
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

        public bool xoaKhoa(string mKhoa)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string cauLenh = "delete from Khoa where MaKhoa = '"+mKhoa+"'";
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
