using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai01
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
            List<DSKhoa> khoas = dt.loadKhoa();
            List<string> lops;
            foreach (DSKhoa item in khoas)
            {
                TreeNode node =  treeView1.Nodes.Add(item.TKhoa);
                lops = dt.loadLop(item.MKhoa);
                foreach (string s in lops)
                {
                    node.Nodes.Add(s);
                }
                comboBox1.Items.Add(item.TKhoa);
            }
            treeView1.ExpandAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dt.ktTrung(textBox1.Text))
            {
                if (dt.themKhoa(textBox1.Text, textBox2.Text))
                    MessageBox.Show("Them thanh cong");
                else
                    MessageBox.Show("That bai");
            }
            else
                MessageBox.Show("Da trung");
        }

        private void button2_Click(object sender, EventArgs e)
        {
                if (dt.suaKhoa(textBox1.Text, textBox2.Text))
                    MessageBox.Show("Thanh cong");
                else
                    MessageBox.Show("That bai");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dt.xoaKhoa(textBox1.Text))
                MessageBox.Show("Thanh cong");
            else
                MessageBox.Show("That bai");
        }
    }
}
