using System.Collections.Generic;
using Backups.Job;
using Backups.TemporaryLocalData;
using Backups.Zippers;

namespace Backups.Tests
{
    public class TestStorageCreator : IStorageCreator
    {
        public List<TemporaryLocalStorage> Compress(List<JobObject> jobObjects)
        {
            return new List<TemporaryLocalStorage>();
        }
    }
}