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
    }
}