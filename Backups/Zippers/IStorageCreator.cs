using System.Collections.Generic;
using Backups.Job;
using Backups.Repo;

namespace Backups.Zippers
{
    public interface IStorageCreator
    {
        List<LocalStorage> Compress(List<JobObject> jobObjects);
    }
}