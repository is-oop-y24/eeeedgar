namespace Backups.ClientServer
{
    public class ServerFile
    {
        public ServerFile(string relativeName, byte[] data)
        {
            RelativeName = relativeName;
            Data = data;
        }

        public string RelativeName { get; }
        public byte[] Data { get; }
    }
}