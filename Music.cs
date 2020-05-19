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
        public Music(String musicName,String author)
        {
            this.musicName = musicName;
            this.author = author;
        }
        public String getMusicName() 
        {
            return this.musicName;
        }
        public String getAuthor()
        {
            return this.author;
        }
    }
}
