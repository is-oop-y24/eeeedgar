namespace Backups.Backup
{
    public class BackupsProperties
    {
        public BackupsProperties(string relativePath)
        {
            RelativePath = relativePath;
        }

        public string RelativePath { get; }
    }
}