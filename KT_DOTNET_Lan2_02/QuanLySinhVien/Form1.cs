using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        KNCSDL kn;
        public Form1()
        {
            kn = new KNCSDL();
            InitializeComponent();
        }

        public void loadKhoaLop()
        {
            List<string> khoas = kn.loadTenKhoa();
            foreach (string s in khoas)
            {
                comboBox1.Items.Add(s);
            }
            DataTable dtLop = kn.loadLop();
            foreach (DataRow row in dtLop.Rows)
            {
                comboBox2.Items.Add(row["TenLop"].ToString());
            }
            DataTable dtHocSinh = kn.loadHocSinh();
            int stt = 1;
            foreach (DataRow row in dtHocSinh.Rows)
            {
                string mSV = row["MaSV"].ToString();
                string hTen = row["HoTen"].ToString();
                string gTinh = row["GioiTinh"].ToString();
                string nSinh = row["NgaySinh"].ToString();
                string dChi = row["DiaChi"].ToString();
                string mLop = row["MaLop"].ToString();
                ListViewItem item = new ListViewItem(new[] { stt++.ToString(), mSV, hTen, gTinh, nSinh, dChi, mLop });
                listView1.Items.Add(item);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("STT");
            listView1.Columns.Add("Mã HS");
            listView1.Columns.Add("Tên HS");
            listView1.Columns.Add("Giới tính");
            listView1.Columns.Add("Ngày sinh");
            listView1.Columns.Add("Địa chỉ");
            listView1.Columns.Add("Lớp");
            loadKhoaLop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mL = kn.getTenLop(comboBox2.Text);
            string gTinh = checkBox1.Checked ? "Nam": "Nu";
            DateTime time = dateTimePicker1.Value;
            if (kn.themHocSinh(textBox1.Text, textBox2.Text, gTinh, time, textBox3.Text, mL))
                MessageBox.Show("Thành công");
            else
                MessageBox.Show("Thất bại");
        }
    }
}
