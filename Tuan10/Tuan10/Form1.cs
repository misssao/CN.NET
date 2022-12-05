using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tuan10
{
    public partial class Form1 : Form
    {
        DataProvider provider;
        public Form1()
        {
            InitializeComponent();
            provider = new DataProvider();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadKhoaVaoCB();
            textBox1.Enabled = textBox2.Enabled = maskedTextBox1.Enabled = false;
            dataGridView1.DataSource = provider.loadSinhVien();
            addDatabingding(provider.Ds.Tables["SinhVien"]);
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            if(dataGridView1.Rows.Count > 0)
            {
                maskedTextBox1.Text = DateTime.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString()).ToString("dd/MM/yyyy");
            }
        }

        void loadKhoaVaoCB()
        {
            comboBox1.DataSource = provider.loadKhoa();
            comboBox1.DisplayMember = "TenKhoa";
            comboBox1.ValueMember = "MaKhoa";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.DataSource = provider.loadLopTuKhoa(comboBox1.SelectedValue.ToString());
            comboBox2.DisplayMember = "TenLop";
            comboBox2.ValueMember = "MaLop";
            btnXoa.Enabled = btnSua.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = provider.loadListSinhVienTuLop(comboBox2.SelectedValue.ToString());
        }

        void addDatabingding(DataTable tb)
        {
            textBox1.DataBindings.Clear();
            textBox2.DataBindings.Clear();
            maskedTextBox1.DataBindings.Clear();
            comboBox2.DataBindings.Clear();

            textBox1.DataBindings.Add("Text", tb, "MaSinhVien");
            textBox2.DataBindings.Add("Text", tb, "HoTen");
            maskedTextBox1.DataBindings.Add("Text", tb, "NgaySinh");
            comboBox2.DataBindings.Add("Text", tb, "MaLop"); 
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            { 
                maskedTextBox1.Text = DateTime.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString()).ToString("dd/MM/yyyy");
            }
            catch
            {
                maskedTextBox1.Text = "";
            }
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.ReadOnly = false;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].ReadOnly = true;
            }
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(provider.luu())
            {
                addDatabingding(provider.Ds.Tables["SinhVien"]);
                MessageBox.Show("Thanh cong");
                btnLuu.Enabled = false;
            }
            else 
            {
                MessageBox.Show("That bai");
                provider.loadSinhVien().Clear();
                dataGridView1.Refresh();
                dataGridView1.DataSource = provider.loadSinhVien();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            dataGridView1.ReadOnly = false;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (provider.xoa(dataGridView1.CurrentRow.Cells[0].Value.ToString()) > 0)
                {
                    MessageBox.Show("Thanh cong");
                    provider.loadSinhVien().Clear();
                    dataGridView1.Refresh();
                    dataGridView1.DataSource = provider.loadSinhVien();
                }
                else
                    MessageBox.Show("That bai");
            }
        }
    }
}
