using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
namespace Barcode
{
    class Settings
    {
        public String inipath = Application.StartupPath + @"\config.ini";
        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        private static extern int GetPrivateProfileString(String sectionName, string KeyName, String defaultValue, StringBuilder returnedString, int Size, String fileName);
        [DllImport("Kernel32")]
        private static extern long WritePrivateProfileString(String section, String keyname, String value, String path);

        public StringBuilder sbusername;
        public String username { get; set; }

        public void readIni()
        {
            int resultSize;
            sbusername = new StringBuilder(50);
            resultSize = GetPrivateProfileString("SECTION", "username", "", sbusername, sbusername.Capacity, inipath);
            this.username = sbusername.ToString();
        }

        public void writeIni(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, inipath);
        }

        public string hashing(String passhash)
        {
            SHA1 sha = SHA1.Create();
            byte[] hashdata = sha.ComputeHash(Encoding.Default.GetBytes(passhash));
            StringBuilder value = new StringBuilder();
            for (int i = 0; i < hashdata.Length; i++)
            {
                value.Append(hashdata[i].ToString());
            }
            return value.ToString();
        }
    }
}
