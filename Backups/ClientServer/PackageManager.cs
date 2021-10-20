using System.Text;

namespace Backups.ClientServer
{
    public static class PackageManager
    {
        public static int ByteSize => 128;

        public static string ToString(byte[] package)
        {
            return Encoding.Default.GetString(package);
        }
    }
}