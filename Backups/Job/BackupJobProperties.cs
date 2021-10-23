namespace Backups.Job
{
    public class BackupJobProperties
    {
        public BackupJobProperties(string path)
        {
            Path = path;
        }

        public string Path { get; }
        public string CurrentVersionDirectoryName => "CurrentVersion";
        public string BackupsDirectoryName => "Backups";
    }
}