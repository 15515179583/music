using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace music
{
    public partial class Form2 : Form
    {
        private String user = "登录/注册";
        SqlFun sqlfun = new SqlFun();
        public Form2()
        {
            InitializeComponent();
        }

        #region 登录验证
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "")
            {
                String username = textBox1.Text.Trim();
                String pass = textBox2.Text.Trim();
                if (textBox2.TextLength < 8)
                {
                    MessageBox.Show("密码长度应大于10位，请重新输入", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sqlfun.userLogin(username, pass);
                    if (sqlfun.getLoginState())
                    {
                        this.user = username;
                        MessageBox.Show(sqlfun.getLoginMsg(), "登录成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(sqlfun.getLoginMsg(), "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("请输入账号和密码","登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Form2_Closing);
        }
        private void Form2_Closing(object sender, CancelEventArgs e)
        {
            Application.ExitThread(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public String getUser()
        {
            return this.user;
        }
    }
}
