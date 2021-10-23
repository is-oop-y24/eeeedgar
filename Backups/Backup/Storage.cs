namespace Backups.Backup
{
    public class Storage
    {
        public Storage(string relativePath)
        {
            Properties = new StorageProperties(relativePath);
        }

        public StorageProperties Properties { get; }
    }
}