using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace music
{
    class Music
    {
        private String musicName;
        private String author;
        private String path;
        public Music(String musicName,String author, String musicPath)
        {
            this.musicName = musicName;
            this.author = author;
            this.path = musicPath.Replace('\\','/');
        }
        public String getMusicName() 
        {
            return this.musicName;
        }
        public String getAuthor()
        {
            return this.author;
        }
        public String getMusicPath()
        {
            return this.path;
        }
    }
}
