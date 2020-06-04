using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace music
{
    class Musics
    {
        private List<Music> musicArr;
        private int length = 0;

        #region 文件法获取歌曲列表
        public void getmusicByFile(String musicPath)
        {
            List<Music> objArr = new List<Music>();
            string[] files = Directory.GetFiles(musicPath);

            for (int i = 0; i < files.Length; i++)
            {
                string musicName = files[i].Split('\\')[1].Split('.')[0].Split('-')[0];
                string author = files[i].Split('\\')[1].Split('.')[0].Split('-')[1];
                String mid = i.ToString();
                objArr.Add(new Music(musicName, author,files[i],mid));
            }
            this.musicArr = objArr;
            this.length = files.Length;
        }
        #endregion

        #region 数据库获取歌曲列表
        public void setMusics(String type)
        {
            SqlFun sqlFun = new SqlFun();
            if (type.Equals("hot") == true)
            {
                this.musicArr = sqlFun.getHot();
                this.length = sqlFun.getLength();
            }
            else if (type.Equals("recommend") == true)
            {
                //this.musicArr = sqlFun.getRecommend();
                this.musicArr = sqlFun.getRecommend();
                this.length = sqlFun.getLength();
            }
            else
            {
                this.musicArr = sqlFun.getListMusics(type);
                this.length = sqlFun.getLength();
            }
        }
        #endregion


        public List<Music> getMusics()
        {
            return this.musicArr;
        }

        public int getLength()
        {
            return this.length;
        }
    }
}
