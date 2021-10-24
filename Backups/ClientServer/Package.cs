using System;
using System.Text;

namespace Backups.ClientServer
{
    public static class Package
    {
        public static int ByteSize => 128;

        public static string Encode(byte[] package)
        {
            return Encoding.Default.GetString(package);
        }

        public static string EncodeByChar(byte[] package)
        {
            string res = string.Empty;
            foreach (byte c in package)
            {
                res += (char)c;
            }

            return res;
        }

        public static void WriteByChar(byte[] package)
        {
            foreach (byte c in package)
            {
                Console.Write((char)c);
            }

            Console.WriteLine();
        }
    }
}