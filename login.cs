using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 串口控制
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1" && textBox2.Text == "1")
            {
                MessageBox.Show("用户登录权限为管理员", "提示");
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }
            else if (textBox1.Text == "222222" && textBox2.Text == "222222")
            {
                MessageBox.Show("用户登录权限为操作员", "提示");
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
