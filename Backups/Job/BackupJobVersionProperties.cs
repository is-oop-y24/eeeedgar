namespace Backups.Job
{
    public class BackupJobVersionProperties
    {
        public BackupJobVersionProperties(string relativePath)
        {
            RelativePath = relativePath;
        }

        public string RelativePath { get; }
    }
}