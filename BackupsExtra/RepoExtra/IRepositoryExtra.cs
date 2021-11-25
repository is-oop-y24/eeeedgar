using System;
using Backups.Repo;

namespace BackupsExtra.RepoExtra
{
    public interface IRepositoryExtra
    {
        IRepository Repository();
        void DeleteRestorePoint(Guid id);
    }
}