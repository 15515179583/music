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
    public partial class Form3 : Form
    {
        MP3Player mp3 = new MP3Player();
        Form2 form2 = new Form2();
        public Form3()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form3_Closing);
            this.button6.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.BorderSize = 0;
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.BeginUpdate();
            for (int i = 0; i < 8; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = 0;
                lvi.Text = "item" + i;
                lvi.SubItems.Add("第2列");
                listView1.Items.Add(lvi);
            }
            this.listView1.EndUpdate();
            mp3.FilePath = "mp3/不再联系-夏天Alex.mp3";
            
        }
        private void Form3_Closing(object sender, CancelEventArgs e)
        {
            Application.ExitThread();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Application.ExitThread();
            form2.Show();
            form2.button4.Click += button4_Click;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Show();
            //listView1 加载数据
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            /*pictureBox2.ImageLocation = "images/hot.jpg";
            //pictureBox2.ImageLocation = "images/bg1.jpg";
            label2.Text = "热歌榜";*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            /*pictureBox2.ImageLocation = "images/tuijian.jpg";
            label2.Text = "推荐榜";*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ok");
            mp3.Play();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.button7.Font.Size == 12)
            {
                mp3.Pause();
                this.button7.Font = new System.Drawing.Font(button7.Font.FontFamily,11); ;
                this.button7.BackgroundImage = new Bitmap("images/bofangjian.png");
            }
            else 
            {
                mp3.Resume();
                this.button7.Font = new System.Drawing.Font(button7.Font.FontFamily, 12); ;
                this.button7.BackgroundImage = new Bitmap("images/zanting.png");
            }
            
        }

    }
}
