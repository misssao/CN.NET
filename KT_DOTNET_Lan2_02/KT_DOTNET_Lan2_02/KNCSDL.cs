using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace KT_DOTNET_Lan2_02
{
    public class KNCSDL
    {
        SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-77FL8U47\SQLEXPRESS;Initial Catalog=QLHocSinh;Integrated Security=True");
        DataSet ds = new DataSet();
        SqlDataAdapter da;

        public KNCSDL() { }

        public DataTable loadKhoa()
        {
            string cauLenh = "select * from KHOA";

            da = new SqlDataAdapter(cauLenh, cn);

            da.Fill(ds, "KHOA");

            DataColumn[] keys = new DataColumn[1];
            keys[0] = ds.Tables["KHOA"].Columns["MaKhoa"];
            ds.Tables["KHOA"].PrimaryKey = keys;

            return ds.Tables["KHOA"];
        }

        public List<string> loadTenLop(string mKhoa)
        {
            if (cn.State == ConnectionState.Closed)
                cn.Open();

            string cauLenh = "select * from LOP where MaKhoa = '"+mKhoa+"'";

            SqlCommand cm = new SqlCommand(cauLenh, cn);

            SqlDataReader dr = cm.ExecuteReader();
            List<string> Lops = new List<string>();
            while (dr.Read())
                Lops.Add(dr["TenLop"].ToString());
            dr.Close();

            if (cn.State == ConnectionState.Open)
                cn.Close();
            return Lops;
        }

        public bool ktTrungMaKhoa(string mKhoa)
        {
            DataTable dt = loadKhoa();
            foreach (DataRow row in dt.Rows)
            {
                if (row["MaKhoa"].ToString() == mKhoa)
                    return false;
            }
            return true;
        }

        public bool ThemKhoa(string mKhoa, string tKhoa)
        {
            try
            {
                DataRow dr = ds.Tables["KHOA"].NewRow();

                dr[0] = mKhoa;
                dr[1] = tKhoa;

                ds.Tables["KHOA"].Rows.Add(dr);

                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                da.Update(ds, "KHOA");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SuaKhoa(string mKhoa, string tKhoa)
        {
            try
            {
                DataRow dr = ds.Tables["KHOA"].Rows.Find(mKhoa);

                dr[1] = tKhoa;

                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                da.Update(ds, "KHOA");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool XoaKhoa(string mKhoa)
        {
            try
            {
                DataRow dr = ds.Tables["KHOA"].Rows.Find(mKhoa);

                if (dr != null)
                    dr.Delete();

                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                da.Update(ds, "KHOA");

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
