namespace Backups.Backup
{
    public class BackupsProperties
    {
        public BackupsProperties(string path, bool isSplitCompression, bool isItTest = false)
        {
            Path = path;
            IsSplitCompression = isSplitCompression;
            IsItTest = isItTest;
        }

        public string Path { get; }
        public bool IsSplitCompression { get; set; }
        public bool IsItTest { get; set; }
    }
}