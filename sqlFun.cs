using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace music
{
    class SqlFun
    {
        private int length = 0;
        private int listLength = 0;
        private bool loginState = false;
        private String loginMsg = "";

        #region 获取链接字符串
        public static string GetConnectionString()
        {
            string strServer = AppDomain.CurrentDomain.BaseDirectory + "SqlConfig.txt";
            return File.ReadAllText(strServer);
        }
        #endregion

        #region 建立数据库链接
        public static MySqlConnection CreateConn()
        {
            string ConString = GetConnectionString();
            MySqlConnection conn = new MySqlConnection(ConString);//连接数据库
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句  
                Console.WriteLine("已经建立连接");
                //在这里使用代码对数据库进行增删查改  
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("建立连接失败!");
                Console.WriteLine(ex.Message);
            }
            return conn;
        }
        #endregion

        #region 获取热门歌曲
        public List<Music> getHot()
        {
            this.length = 0;
            List<Music> objArr = new List<Music>();
            String basePath = "mp3\\";
            MySqlConnection conn = CreateConn();
            string sqlQuery = "SELECT * FROM `music` where hot = 1";
            MySqlCommand comm = new MySqlCommand(sqlQuery, conn);
            MySqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                String musicName = dr.GetValue(2).ToString();
                String author = dr.GetValue(3).ToString();
                String path = basePath + dr.GetValue(1).ToString();
                String mid = dr.GetValue(0).ToString();
                objArr.Add(new Music(musicName, author, path, mid));
                this.length += 1;
            }
            conn.Close();
            return objArr;
        }
        #endregion

        #region 获取推荐歌曲
        public List<Music> getRecommend()
        {
            this.length = 0;
            List<Music> objArr = new List<Music>();
            String basePath = "mp3\\";
            MySqlConnection conn = CreateConn();
            string sqlQuery = "SELECT * FROM `music` where recommend = 1";
            MySqlCommand comm = new MySqlCommand(sqlQuery, conn);
            MySqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                String musicName = dr.GetValue(2).ToString();
                String author = dr.GetValue(3).ToString();
                String path = basePath + dr.GetValue(1).ToString();
                String mid = dr.GetValue(0).ToString();
                objArr.Add(new Music(musicName, author, path, mid));
                this.length += 1;
            }
            conn.Close();
            return objArr;
        }
        #endregion

        #region 登录/注册
        public void userLogin(String username, String pass)
        {
            bool flag = false;
            this.loginState = false;
            MySqlConnection conn = CreateConn();
            string sqlQuery = "SELECT password FROM user where userName = \"" + username + "\"";
            MySqlCommand comm = new MySqlCommand(sqlQuery, conn);
            MySqlDataReader dr = comm.ExecuteReader();
            if (dr.Read())
            {
                if (dr.GetValue(0).ToString().Equals(pass) == true)
                {
                    this.loginState = true;
                    this.loginMsg = "登录成功";
                }
                else
                {
                    this.loginMsg = "账号或密码输入错误";
                }
                flag = true;
            }
            conn.Close();

            if(!flag)
            {
                conn = CreateConn();
                string sql = "INSERT INTO `user` SET userName = \"" + username + "\", PASSWORD = \"" + pass + "\"";
                comm = new MySqlCommand(sql, conn);
                int i = comm.ExecuteNonQuery();
                if (i == 1)
                {
                    this.loginState = true;
                    this.loginMsg = "注册成功";
                }
                else
                {
                    this.loginMsg = "注册失败";
                }
            }

            conn.Close();
        }
        #endregion

        #region 获取歌单
        public List<MusicList> getList(String user)
        {
            this.listLength = 0;
            List<MusicList> objArr = new List<MusicList>();
            MySqlConnection conn = CreateConn();
            string sqlQuery = "SELECT * FROM `musicList` where user = \"" + user + "\"";
            MySqlCommand comm = new MySqlCommand(sqlQuery, conn);
            MySqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                String id = dr.GetValue(0).ToString();
                String name = dr.GetValue(1).ToString();
                objArr.Add(new MusicList(id,name));
                this.listLength += 1;
            }
            conn.Close();
            return objArr;
        }
        #endregion

        #region 创建歌单
        public int createList(String name, String user)
        {
            MySqlConnection conn = CreateConn();
            string sql = "INSERT INTO `musicList` SET name = \"" + name + "\", user = \"" + user + "\"";
            MySqlCommand comm = new MySqlCommand(sql, conn);
            int i = comm.ExecuteNonQuery();

            conn.Close();
            return i;
        }
        #endregion

        #region 删除歌单
        public int delList(String id)
        {
            MySqlConnection conn = CreateConn();
            string sql = "DELETE FROM musicList WHERE id =  \"" + id + "\"";
            MySqlCommand comm = new MySqlCommand(sql, conn);
            int i = comm.ExecuteNonQuery();

            conn.Close();
            return i;
        }
        #endregion

        #region 获取歌单歌曲
        public List<Music> getListMusics(String lid)
        {
            this.length = 0;
            List<Music> objArr = new List<Music>();
            String basePath = "mp3\\";
            MySqlConnection conn = CreateConn();
            string sqlQuery = "SELECT * FROM music a LEFT JOIN userMusic b ON a.`id` = b.`mid` WHERE b.`lid` = \"" + lid + "\" ORDER BY b.id DESC";
            MySqlCommand comm = new MySqlCommand(sqlQuery, conn);
            MySqlDataReader dr = comm.ExecuteReader();

            while (dr.Read())
            {
                String mid = dr.GetValue(0).ToString();
                String path = basePath + dr.GetValue(1).ToString();
                String musicName = dr.GetValue(2).ToString();
                String author = dr.GetValue(3).ToString();
                objArr.Add(new Music(musicName, author, path, mid));
                this.length += 1;
            }
            conn.Close();
            return objArr;
        }
        #endregion

        #region 添加歌曲到歌单
        public int setMusicToList(String mid, String lid)
        {
            MySqlConnection conn = CreateConn();
            string sql = "INSERT INTO `userMusic` SET mid = \"" + mid + "\", lid = \"" + lid + "\"";
            MySqlCommand comm = new MySqlCommand(sql, conn);
            int i = comm.ExecuteNonQuery();

            conn.Close();
            return i;
        }
        #endregion

        #region 从歌单删除歌曲
        public int delMusicToList(String mid, String lid)
        {
            MySqlConnection conn = CreateConn();
            string sql = "DELETE FROM usermusic WHERE `mid` = " + mid + " AND lid = " + lid;
            MySqlCommand comm = new MySqlCommand(sql, conn);
            int i = comm.ExecuteNonQuery();

            conn.Close();
            return i;
        }
        #endregion

        public int getLength()
        {
            return this.length;
        }
        public int getListLength()
        {
            return this.listLength;
        }

        public bool getLoginState()
        {
            return this.loginState;
        }

        public String getLoginMsg()
        {
            return this.loginMsg;
        }
    }
}
