using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KT_DOTNET_Lan2_02
{
    public partial class Form1 : Form
    {
        KNCSDL kn;
        public Form1()
        {
            kn = new KNCSDL();
            InitializeComponent();
        }

        public void loadTreeCombobox()
        {
            DataTable dt = kn.loadKhoa();
            foreach (DataRow row in dt.Rows)
            {
                comboBox1.Items.Add(row["TenKhoa"]);
                TreeNode tn = treeView1.Nodes.Add(row["TenKhoa"].ToString());
                List<string> lops = kn.loadTenLop(row["MaKhoa"].ToString());
                foreach (string s in lops)
                {
                    tn.Nodes.Add(s);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadTreeCombobox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                if (kn.ktTrungMaKhoa(textBox1.Text))
                {
                    if (kn.ThemKhoa(textBox1.Text, textBox2.Text))
                        MessageBox.Show("Thành công");
                    else
                        MessageBox.Show("Thất bại");
                }
                else
                    MessageBox.Show("Mã khoa bị trùng!");
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập mã");
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (kn.SuaKhoa(textBox1.Text, textBox2.Text))
                MessageBox.Show("Thành công");
            else
                MessageBox.Show("Thất bại");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (kn.XoaKhoa(textBox1.Text))
                MessageBox.Show("Thành công");
            else
                MessageBox.Show("Thất bại");
        }
    }
}
