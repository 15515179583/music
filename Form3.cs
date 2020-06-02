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
        Musics musics = new Musics("mp3");
        internal int musicId = 0;
        internal int musicsLength = 0;
        internal List<Music> musicArr = null;
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
            musicId = rd.Next(0, musics.length);
            Music music = musicArr[musicId];
            mp3.FilePath = music.getMusicPath();
            label3.Text = music.getMusicName();
            mp3.Play();
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
        }
        #endregion

    }
}
