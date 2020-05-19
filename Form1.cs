using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace music
{
    public partial class Form1 : Form
    {
        MP3Player mp3 = new MP3Player();
        Form2 form2 = new Form2();
        Form3 form3 = new Form3();
        public Form1()
        {
            InitializeComponent();
            this.button6.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.BorderSize = 0;
            this.listView1.Columns.Add("我的歌单", 147, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("", 2, HorizontalAlignment.Center); //一步添加
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.BeginUpdate();
            for (int i = 0; i < 7; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = 0;
                lvi.Text = "默认歌单" + (i+1);
                //lvi.SubItems.Add("item"+i);
                listView1.Items.Add(lvi);
            }
            this.listView1.EndUpdate();
            mp3.FilePath = "mp3/不再联系-夏天Alex.mp3";
            this.listView2.Columns.Add("", 30, HorizontalAlignment.Left); //一步添加
            this.listView2.Columns.Add("歌曲名", 280, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("歌手", 155, HorizontalAlignment.Center); //一步添加
            this.listView2.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            this.listView2.SmallImageList = this.imageList1;
            List<Music> musicArr = new Musics().getmusics("mp3");
            Random rd = new Random();
            foreach (Music m in musicArr)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = rd.Next(1,5);
                lvi.SubItems.Add(m.getMusicName());
                lvi.SubItems.Add(m.getAuthor());
                this.listView2.Items.Add(lvi);
            }
            
            /*for (int i = 0; i < 10; i++)   //添加10行数据
            {
               ;
                ListViewItem lvi = new ListViewItem();

                lvi.ImageIndex = i%2+1; //通过与imageList绑定，显示imageList中第i项图标

               // lvi.SubItems.Add(music);
              //  lvi.SubItems.Add(author);
                this.listView2.Items.Add(lvi);
            }*/

            this.listView2.EndUpdate();  //结束数据处理，UI界面一次性绘制。
            
        }
        private void listView2_GetList(object sender, EventArgs e)
        {
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Application.ExitThread();
            form2.Show();
            form2.button1.Click += button1_Click;
            form2.button2.Click += button2_Click;
            form2.button4.Click += button4_Click;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Show();
            //listView1 加载数据
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Show();
            pictureBox2.ImageLocation = "images/hot.jpg";
            //pictureBox2.ImageLocation = "images/bg1.jpg";
            label2.Text = "热歌榜";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Show();
            pictureBox2.ImageLocation = "images/tuijian.jpg";
            label2.Text = "推荐榜";
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

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Application.ExitThread();
            form3.Show();
            form3.button1.Click += button1_Click;
            form3.button2.Click += button2_Click;
            form3.button3.Click += button3_Click;
            form3.button10.Click += button10_Click;
        }
    }
}
