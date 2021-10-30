namespace Backups.Server
{
    public class ServerFile
    {
        public ServerFile(string name, byte[] data)
        {
            Name = name;
            Data = data;
        }
        
        public string Name { get; }
        public byte[] Data { get; }
    }
}