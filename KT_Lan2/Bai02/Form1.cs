using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai02
{
    public partial class Form1 : Form
    {
        KNCSDL dt;
        public Form1()
        {
            dt = new KNCSDL();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] head = { "STT", "Mã HS", "Tên HS", "Giới tính", "Ngày sinh", "Địa chỉ", "Lớp" };
            foreach (string item in head)
            {
                listView1.Columns.Add(item);
            }
            List<HocSinh> dsHocSinh = dt.loadHocSinh();
            int stt = 1;
            foreach (HocSinh item in dsHocSinh)
            {
                ListViewItem it = new ListViewItem(new[] { (stt++).ToString(), item.mSV, item.hTen, item.gTinh, item.nSinh.ToString(), item.dChi, item.mLop });
                listView1.Items.Add(it);
            }

            List<string> khoas = dt.loadKhoa();
            foreach (string item in khoas)
            {
                comboBox1.Items.Add(item);
            }

            List<string> lops = dt.loadLop();
            foreach (string item in lops)
            {
                comboBox2.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int check = 1;
            if (textBox1.Text == "")
            {
                check = 0;
                MessageBox.Show("Ban chua nhap ma sinh vien");
                textBox1.Focus();
            }
            else
            {
                DateTime hienTai = DateTime.Now;
                DateTime obj = dateTimePicker1.Value;
                if (hienTai.Year - obj.Year <= 17)
                {
                    check = 0;
                    MessageBox.Show("Tuoi phai lon hon 17");
                }
            }
            if (check == 1)
            {
                string mL = "";
                if (comboBox2.Text == "Lop 1")
                {
                    mL = "L1";
                }
                else if (comboBox2.Text == "Lop 2")
                {
                    mL = "L2";
                }
                else if (comboBox2.Text == "Lop 3")
                {
                    mL = "L3";
                }
                else if (comboBox2.Text == "Lop 4")
                {
                    mL = "L4";
                }
                string gTinh = checkBox1.Checked ? "Nam" : "Nu";
                DateTime dT = dateTimePicker1.Value;
                if (dt.themHocSinh(textBox1.Text, textBox2.Text, gTinh, dT, textBox3.Text, mL))
                    MessageBox.Show("Thanh cong");
                else
                    MessageBox.Show("That bai");
            }
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string mL = "";
            if (comboBox2.Text == "Lop 1")
            {
                mL = "L1";
            }
            else if (comboBox2.Text == "Lop 2")
            {
                mL = "L2";
            }
            else if (comboBox2.Text == "Lop 3")
            {
                mL = "L3";
            }
            else if (comboBox2.Text == "Lop 4")
            {
                mL = "L4";
            }
            string gTinh = checkBox1.Checked ? "Nam" : "Nu";
            DateTime dT = dateTimePicker1.Value;
            if (dt.suaHocSinh(textBox1.Text, textBox2.Text, gTinh, dT, textBox3.Text, mL))
                MessageBox.Show("Thanh cong");
            else
                MessageBox.Show("That bai");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                e.Cancel = true;
        }
    }
}
