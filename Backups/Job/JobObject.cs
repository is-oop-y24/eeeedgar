namespace Backups.Job
{
    public class JobObject
    {
        public JobObject(string path)
        {
            Properties = new JobObjectProperties(path);
        }

        public JobObjectProperties Properties { get; }
    }
}