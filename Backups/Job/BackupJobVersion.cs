using System.Collections.Generic;

namespace Backups.Job
{
    public class BackupJobVersion
    {
        public BackupJobVersion()
        {
            JobObjects = new List<JobObject>();
        }

        public List<JobObject> JobObjects { get; }
    }
}