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
using System.Drawing.Drawing2D;


namespace music
{
    public partial class Form1 : Form
    {
        SqlFun sqlfun = new SqlFun();
        MP3Player mp3 = new MP3Player();
        Form2 form2 = new Form2();
        Form3 form3 = new Form3();
        Musics musics = new Musics();
        List<Music> musicArr = null;
        List<MusicList> list = null;

        String user = "";
        int musicId = 0;
        int musicsLength = 0;
        string[] song = null;

        bool listReload = false;

        public Form1()
        {
            InitializeComponent();
            this.button6.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.BorderSize = 0;

            listView2_Draw("hot");
            contextMenuStrip2_Draw();
        }

        #region 绘制歌曲列表
        private void listView2_Draw(String type)
        {
            this.listView2.Clear();
            this.listView2.Columns.Add("", 30, HorizontalAlignment.Left); //一步添加
            this.listView2.Columns.Add("歌曲名", 280, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("歌手", 155, HorizontalAlignment.Center); //一步添加

            this.listView2.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            this.listView2.SmallImageList = this.imageList1;
            musics.setMusics(type);
            musicArr = musics.getMusics();
            musicsLength = musics.getLength();

            Random rd = new Random();
            foreach (Music m in musicArr)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = rd.Next(1, 5);
                lvi.SubItems.Add(m.getMusicName());
                lvi.SubItems.Add(m.getAuthor());
                this.listView2.Items.Add(lvi);
            }
            this.listView2.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }
        #endregion

        #region 绘制歌单列表
        private void listView1_Draw(String user)
        {
            this.listView1.Clear();
            this.listView1.Columns.Add("我的歌单", 147, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("", 2, HorizontalAlignment.Center); //一步添加
            this.listView1.SmallImageList = this.imageList1;

            this.listView1.BeginUpdate();
            list = sqlfun.getList(user);
            for (int i = 0; i < sqlfun.getListLength(); i++)
            {
                
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = 0;
                lvi.Text = list[i].getName();
                listView1.Items.Add(lvi);
            }
            this.listView1.EndUpdate();
        }
        #endregion

        #region 绘制歌曲右键菜单
        private void contextMenuStrip2_Draw()
        {
            contextMenuStrip2.Items.Clear();
            ToolStripMenuItem item = new ToolStripMenuItem("播放", null, paly_Click);
            contextMenuStrip2.Items.Add(item);
            try 
            { 
                item = new ToolStripMenuItem("从歌单【" + list[listView1.SelectedIndices[0]].getName() +  "】移除", null);
                contextMenuStrip2.Items.Add(item);
            }
            catch 
            {
                if (sqlfun.getListLength() != 0)
                {
                    for (int i = 0; i < sqlfun.getListLength(); i++)
                    {
                        item = new ToolStripMenuItem("添加至【" + list[i].getName() + "】", null);
                        contextMenuStrip2.Items.Add(item);
                    }
                }
            }
        }
        #endregion

        #region 用户登录窗口
        private void button3_Click(object sender, EventArgs e)
        {

            this.button3.Text = form2.getUser();
            this.Hide();
            form2.Show();
            form2.button1.Click += button1_Click;
            form2.button2.Click += button2_Click;
            form2.button4.Click += main_show;
        }
        #endregion

        #region 显示首页
        private void main_show(object sender, EventArgs e)
        {
            this.Show();
        }
        #endregion

        #region 创建用户歌单
        private void button4_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text.Trim();
            if (name != ""&& name.Length <=5)
            {
                if (sqlfun.createList(name, user) == 1)
                {
                    MessageBox.Show("创建成功", "创建成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    listReload = true;
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("歌单创建失败", "创建失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("未输入歌单名称或歌单过长","创建失败",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 热歌榜
        private void button1_Click(object sender, EventArgs e)
        {
            this.Show();
            pictureBox2.ImageLocation = "images/hot.jpg";
            //pictureBox2.ImageLocation = "images/bg1.jpg";
            listView2_Draw("hot");
            label2.Text = "热歌榜";
            //listView1.
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                item.Selected = false;
            }
        }
        #endregion

        #region 推荐榜
        private void button2_Click(object sender, EventArgs e)
        {
            this.Show();
            listView2_Draw("recommend");
            pictureBox2.ImageLocation = "images/tuijian.jpg";
            label2.Text = "推荐榜";

            foreach (ListViewItem item in listView1.SelectedItems)
            {
                item.Selected = false;
            }
        }
        #endregion

        #region 用户歌单
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                contextMenuStrip2_Draw();
                listView2_Draw(list[listView1.SelectedIndices[0]].getId());
                label2.Text = list[listView1.SelectedIndices[0]].getName();
            }
            catch { }
        }
        #endregion

        #region 播放/暂停
        private void button7_Click(object sender, EventArgs e)
        {
            if (this.button7.Font.Size == 12)
            {
                mp3.Pause();
                this.button7.Font = new System.Drawing.Font(button7.Font.FontFamily, 11); ;
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
            form3.musicId = musicId;
            form3.song = song;

            form3.button1.Click += button1_Click;
            form3.button2.Click += button2_Click;

            form3.button3.Click += button3_Click;
            form3.button3.Text = button3.Text;
            form3.button3.Font = button3.Font;

        }
        #endregion

        #region 播放全部
        private void button5_Click(object sender, EventArgs e)
        {
            Music music = musicArr[0];
            mp3.FilePath = music.getMusicPath();//"http://fdfs.xmcdn.com/group80/M05/EC/0E/wKgPEV6qW8PzQ2YmAB5_f6QbHQk419.mp3";
            label3.Text = music.getMusicName();
            musicId = 0;
            MessageBox.Show("ok");
            mp3.Play();

            try
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory + "lrc/" + musicArr[musicId].getMusicName() + ".lrc";
                song = File.ReadAllLines(strPath);
            }
            catch { }
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

        # region 删除歌单
        private void 删除歌单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定删除吗？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //删除歌单
                try
                {
                    int i = sqlfun.delList(list[listView1.SelectedIndices[0]].getId().ToString());
                    if (i == 1)
                    {
                        listReload = true;
                        MessageBox.Show("删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("删除失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch { }

            }
            else
            {
                return;
            }
        }
        #endregion

        # region 播放键
        private void paly_Click(object sender, EventArgs e)
        {

            musicId = listView2.SelectedIndices[0];
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

        #region 动态刷新
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (user == "")
            {
                if (form2.getUser() != "登录/注册")
                {
                    user = form2.getUser();
                    button3.Text = user;
                    button3.Font = new Font("宋体", 15);
                    button3.Enabled = false;

                    /*显示歌单*/
                    listView1_Draw(user);
                    contextMenuStrip2_Draw();
                    //contextMenuStrip2.ItemAdded();

                    textBox1.Visible = true;
                    button4.Visible = true;
                }
            }
            if (listReload == true)
            {
                listView1_Draw(user);
                contextMenuStrip2_Draw();
                listReload = false;
            }
        }
        #endregion

        #region 右键弹出菜单功能实现
        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < contextMenuStrip2.Items.Count; i++)
            {
                try
                {
                    String rmLid = listView1.SelectedIndices[0].ToString();
                    if (i == 1 && contextMenuStrip2.Items[i].Selected == true)
                    {
                        String Mid = musicArr[listView2.SelectedIndices[0]].getMid();
                        String Lid = list[listView1.SelectedIndices[0]].getId();
                        int flag = sqlfun.delMusicToList(Mid, Lid);
                        if (flag == 1)
                        {
                            MessageBox.Show("音乐从歌单删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            //listView2_Draw(user);
                        }
                        else
                        {
                            MessageBox.Show("音乐从歌单删除失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch
                {
                    if (i != 0 && contextMenuStrip2.Items[i].Selected == true)
                    {
                        String Mid = musicArr[listView2.SelectedIndices[0]].getMid();
                        String Lid = list[i - 1].getId();
                        int flag = sqlfun.setMusicToList(Mid, Lid);
                        if (flag == 1)
                        {
                            MessageBox.Show("音乐添加到歌单成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            MessageBox.Show("音乐添加到歌单失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        #endregion

        #region 随机产生用户歌单图片
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            Random rd = new Random();
            String picId = rd.Next(1,4).ToString();
            if (picId == "1")
            {
                pictureBox2.ImageLocation = "images/musicList1.gif";
            }
            else
            {
                pictureBox2.ImageLocation = "images/musicList" + picId + ".jpg";
            }
        }
        #endregion
    }
}
