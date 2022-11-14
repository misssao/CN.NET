using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLySinhVien
{
    public class KNCSDL
    {
        SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-77FL8U47\SQLEXPRESS;Initial Catalog=QLHocSinh;Integrated Security=True");
        DataSet ds = new DataSet();

        public KNCSDL() { }
    
        public List<string> loadTenKhoa()
        {
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            string cauLenh = "select TenKhoa from KHOA";
            SqlCommand cm = new SqlCommand(cauLenh, cn);
            SqlDataReader dr = cm.ExecuteReader();
            List<string> Khoas = new List<string>();
            while(dr.Read())
                Khoas.Add(dr["TenKhoa"].ToString());
            dr.Close();
            if (cn.State == ConnectionState.Open)
                cn.Close();
            return Khoas;
        }

        public DataTable loadLop()
        {
            string cauLenh = "select * from LOP";
            SqlDataAdapter da = new SqlDataAdapter(cauLenh, cn);
            da.Fill(ds, "LOP");
            DataColumn[] keys = new DataColumn[1];
            keys[0] = ds.Tables["LOP"].Columns["MaLop"];
            ds.Tables["LOP"].PrimaryKey = keys;
            return ds.Tables["LOP"];
        }
        SqlDataAdapter da_HocSinh;
        public DataTable loadHocSinh()
        {
            string cauLenh = "select * from SINHVIEN";
            da_HocSinh = new SqlDataAdapter(cauLenh, cn);
            da_HocSinh.Fill(ds, "SINHVIEN");
            DataColumn[] keys = new DataColumn[1];
            keys[0] = ds.Tables["SINHVIEN"].Columns["MaSV"];
            ds.Tables["SINHVIEN"].PrimaryKey = keys;
            return ds.Tables["SINHVIEN"];
        }

        public string getTenLop(string tLop)
        {
            string ten = "";
            foreach (DataRow row in ds.Tables["Lop"].Rows )
	        {
                if (row["TenLop"].ToString() == tLop)
                {
                    ten = row["MaLop"].ToString();
                    break;
                }
	        }
            return ten;
        }

        public bool themHocSinh(string mSV, string hTen, string gTinh, DateTime nSinh, string dChi, string mLop)
        {
            try
            {
                DataRow dr = ds.Tables["SINHVIEN"].NewRow();
                dr[0] = mSV;
                dr[1] = hTen;
                dr[2] = gTinh;
                dr[3] = nSinh;
                dr[4] = dChi;
                dr[5] = mLop;
                ds.Tables["SINHVIEN"].Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(da_HocSinh);
                da_HocSinh.Update(ds, "SINHVIEN");
                return true;
            }
            catch
            {
                return false;
            }
        } 
    }
}
