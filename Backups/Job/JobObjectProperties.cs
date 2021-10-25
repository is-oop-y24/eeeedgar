namespace Backups.Job
{
    public class JobObjectProperties
    {
        public JobObjectProperties(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}