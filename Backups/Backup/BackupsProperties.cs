namespace Backups.Backup
{
    public class BackupsProperties
    {
        public BackupsProperties(string path, bool isSplitComprtession)
        {
            Path = path;
            IsSplitCompression = isSplitComprtession;
        }

        public string Path { get; }
        public bool IsSplitCompression { get; set; }
    }
}