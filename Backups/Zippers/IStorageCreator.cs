using System.Collections.Generic;
using Backups.Job;
using Backups.Repo;

namespace Backups.Zippers
{
    public interface IStorageCreator
    {
        List<Storage> Compress(List<JobObject> jobObjects);
    }
}