using System.Collections.Generic;

namespace Backups.Job
{
    public class BackupJobVersion
    {
        public BackupJobVersion(string directoryName)
        {
            JobObjects = new List<JobObject>();
            Properties = new BackupJobVersionProperties(directoryName);
        }

        public List<JobObject> JobObjects { get; }
        public BackupJobVersionProperties Properties { get; }
    }
}