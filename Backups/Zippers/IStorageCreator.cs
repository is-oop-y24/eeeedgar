using System.Collections.Generic;
using Backups.Job;
using Backups.TemporaryLocalData;

namespace Backups.Zippers
{
    public interface IStorageCreator
    {
        List<TemporaryLocalStorage> Compress(List<JobObject> jobObjects);
    }
}