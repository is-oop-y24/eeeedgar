namespace Backups.Backup
{
    public class StorageProperties
    {
        public StorageProperties(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}