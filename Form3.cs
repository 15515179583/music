using System;
using System.IO;
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
        internal int musicId = 0;
        internal int musicsLength = 0;
        internal List<Music> musicArr = null;
        internal string[] song = null;
        public Form3()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form3_Closing);
            this.button6.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.BorderSize = 0;
        }
        private void Form3_Closing(object sender, CancelEventArgs e)
        {
            Application.ExitThread();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            form2.Show();
            form2.button4.Click += button4_Click;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ok");
            mp3.Play();
        }

        #region 暂停/播放
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
        #endregion

        #region 随机切歌
        private void button9_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            musicId = rd.Next(0, musicsLength);
            Music music = musicArr[musicId];
            mp3.FilePath = music.getMusicPath();
            label3.Text = music.getMusicName();
            mp3.Play();

            try
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory + "lrc/" + musicArr[musicId].getMusicName() + ".lrc";
                song = File.ReadAllLines(strPath);
            }
            catch { }
        }
        #endregion

        #region 上一曲
        private void button6_Click(object sender, EventArgs e)
        {
            musicId -= 1;
            if (musicId < 0)
            {
                musicId = musicsLength - 1;
            }

            Music music = musicArr[musicId];
            mp3.FilePath = music.getMusicPath();
            label3.Text = music.getMusicName();
            mp3.Play();

            try
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory + "lrc/" + musicArr[musicId].getMusicName() + ".lrc";
                song = File.ReadAllLines(strPath);
            }
            catch { }
        }
        #endregion

        #region 下一曲
        private void button8_Click(object sender, EventArgs e)
        {
            musicId += 1;
            if (musicId == musicsLength)
            {
                musicId = 0;
            }
            Music music = musicArr[musicId];
            mp3.FilePath = music.getMusicPath();
            label3.Text = music.getMusicName();
            mp3.Play();

            try
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory + "lrc/" + musicArr[musicId].getMusicName() + ".lrc";
                song = File.ReadAllLines(strPath);
            }
            catch { }
        }
        #endregion

        #region 渲染歌词
        private void groupBox_Paint()
        {
            Graphics gobj = groupBox2.CreateGraphics();
            SolidBrush brush = new SolidBrush(BackColor);
            gobj.FillRectangle(brush, (float)10, (float)10, (float)500, (float)500);

            Font font = new Font("宋体", 15);
            brush.Color = Color.Red;
            
            gobj.DrawString("暂不提供动态歌词", font, brush, 200, 100);
            brush.Color = Color.Magenta;
            try { gobj.DrawString(song[0], font, brush, 20, 200); }
            catch { }
            
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            groupBox_Paint();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory + "lrc/" + musicArr[musicId].getMusicName() + ".lrc";
                MessageBox.Show(File.ReadAllText(strPath));
                //File.ReadAllLines(strPath);
            }
            catch { }
        }

    }
}
