using System;
using System.IO;
using System.Threading.Tasks;

namespace music
{
    class sqlFun
    {
        public static string GetConnectionString()
        {
            string strServer = AppDomain.CurrentDomain.BaseDirectory + "SqlConfig.txt";
            return File.ReadAllText(strServer);
        }
    }
}
