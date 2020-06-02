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
        Musics musics = new Musics("mp3");
        List<Music> musicArr = null;
        
        int musicId = 0;
        int musicsLength = 0;

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
                listView1.Items.Add(lvi);
            }
            this.listView1.EndUpdate();
            
            this.listView2.Columns.Add("", 30, HorizontalAlignment.Left); //一步添加
            this.listView2.Columns.Add("歌曲名", 280, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("歌手", 155, HorizontalAlignment.Center); //一步添加

            this.listView2.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            this.listView2.SmallImageList = this.imageList1;
            musicArr = musics.musicArr;
            musicsLength = musics.length;

            Random rd = new Random();
            foreach (Music m in musicArr)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = rd.Next(1,5);
                lvi.SubItems.Add(m.getMusicName());
                lvi.SubItems.Add(m.getAuthor());
                this.listView2.Items.Add(lvi);
            }
            this.listView2.EndUpdate();  //结束数据处理，UI界面一次性绘制。
            
        }

        #region 用户登录窗口
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            form2.Show();
            form2.button1.Click += button1_Click;
            form2.button2.Click += button2_Click;
            form2.button4.Click += button4_Click;

        }
        #endregion

        #region 创建用户歌单
        private void button4_Click(object sender, EventArgs e)
        {
            //this.Show();
            HttpUitls http = new HttpUitls();
            //MessageBox.Show(http.Get("http://mp3.wedlaa.com/index.php?c=music&a=song&key=%E5%91%8A%E7%99%BD%E6%B0%94%E7%90%83&filter=name&type=kugou&page=1"));
        }
        #endregion

        #region 热歌榜
        private void button1_Click(object sender, EventArgs e)
        {
            this.Show();
            pictureBox2.ImageLocation = "images/hot.jpg";
            //pictureBox2.ImageLocation = "images/bg1.jpg";
            label2.Text = "热歌榜";
        }
        #endregion

        #region 推荐榜
        private void button2_Click(object sender, EventArgs e)
        {
            this.Show();
            pictureBox2.ImageLocation = "images/tuijian.jpg";
            label2.Text = "推荐榜";
        }
        #endregion

        #region 播放全部
        private void button5_Click(object sender, EventArgs e)
        {
            Music music = musicArr[0];
            mp3.FilePath = music.getMusicPath();//"http://fdfs.xmcdn.com/group80/M05/EC/0E/wKgPEV6qW8PzQ2YmAB5_f6QbHQk419.mp3";
            label3.Text = music.getMusicName();
            MessageBox.Show("ok");
            mp3.Play();
        }
        #endregion

        #region 播放/暂停
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

        #region 查看歌词
        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Application.ExitThread();
            form3.Show();
            form3.label3.Text = label3.Text;
            form3.musicArr = musicArr;
            form3.musicsLength = musicsLength;
            form3.button1.Click += button1_Click;
            form3.button2.Click += button2_Click;
            form3.button3.Click += button3_Click;
            form3.button10.Click += button10_Click;
            form3.musicId = musicId;
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
