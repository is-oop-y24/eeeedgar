namespace Backups.Backup
{
    public class StorageProperties
    {
        public StorageProperties(string relativePath)
        {
            RelativePath = relativePath;
        }

        public string RelativePath { get; }
    }
}