using System.Collections.Generic;
using Backups.Job;
using Backups.Repo;
using Backups.Zippers;

namespace Backups.Tests
{
    public class TestStorageCreator : IStorageCreator
    {
        public List<Storage> Compress(List<JobObject> jobObjects)
        {
            return new List<Storage>();
        }
    }
}