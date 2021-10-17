namespace Backups.Entities
{
    public class JobObject
    {
        public JobObject(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; }

        public string FileNameWithoutExtension => FilePath.Split('/')[^1].Split('.')[0];
        public string FileExtension => FilePath.Split('/')[^1].Split('.')[1];
    }
}