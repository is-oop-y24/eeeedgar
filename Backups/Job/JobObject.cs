namespace Backups.Job
{
    public class JobObject
    {
        public JobObject(string relativeFilePath)
        {
            RelativeFilePath = relativeFilePath;
        }

        public string RelativeFilePath { get; }

        public string FileNameWithoutExtension => RelativeFilePath.Split('/')[^1].Split('.')[0];
        public string FileExtension => RelativeFilePath.Split('/')[^1].Split('.')[1];
    }
}