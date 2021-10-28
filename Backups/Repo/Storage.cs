using System.Collections.Generic;
using Backups.Job;

namespace Backups.Repo
{
    public class Storage
    {
        public Storage()
        {
            JobObjects = new List<JobObject>();
        }

        public List<JobObject> JobObjects { get; }
    }
}