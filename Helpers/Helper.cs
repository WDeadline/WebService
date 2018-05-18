using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WebService.Helpers
{
    public class Helper
    {
        public Helper()
        {
        }

        public string GetMD5(string chuoi)
        {
            string str_md5 = "";
            byte[] mang = Encoding.UTF8.GetBytes(chuoi);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);

            foreach (byte b in mang)
            {
                str_md5 += b.ToString("x2");
            }

            return str_md5;
        }
    }
}
