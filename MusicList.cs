using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace music
{
    class MusicList
    {
        private String id;
        private String name;

        public MusicList(String id, String name)
        {
            this.id = id;
            this.name = name;
        }

        public String getId()
        {
            return this.id;
        }

        public String getName()
        {
            return this.name;
        }
    }
}
