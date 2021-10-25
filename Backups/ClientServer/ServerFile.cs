namespace Backups.ClientServer
{
    public class ServerFile
    {
        public ServerFile(string relativePath, byte[] data)
        {
            RelativePath = relativePath;
            Data = data;
        }

        public string RelativePath { get; }
        public byte[] Data { get; }
    }
}