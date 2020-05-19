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
        public List<Music> getmusics(String musicPath)
        {
            List<Music> objArr = new List<Music>();
            string[] files = Directory.GetFiles(musicPath);

            for (int i = 0; i < files.Length; i++)
            {
                string musicName = files[i].Split('\\')[1].Split('.')[0].Split('-')[0];
                string author = files[i].Split('\\')[1].Split('.')[0].Split('-')[1];
                objArr.Add(new Music(musicName, author));
            }
            return objArr;
        }
    }
}
