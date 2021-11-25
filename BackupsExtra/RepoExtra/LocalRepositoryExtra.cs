using System;
using System.IO;
using Backups.Repo;
using BackupsExtra.Tools;

namespace BackupsExtra.RepoExtra
{
    public class LocalRepositoryExtra : IRepositoryExtra
    {
        private LocalRepository _repository;

        public LocalRepositoryExtra(LocalRepository repository)
        {
            _repository = repository;
        }

        public IRepository Repository()
        {
            return _repository;
        }

        public void DeleteRestorePoint(Guid id)
        {
            RestorePoint restorePointToDelete = _repository.RestorePoints.Find(point => point.Id.Equals(id));
            if (restorePointToDelete == null)
                throw new BackupsExtraException("wrong restore point id");
            foreach (Storage storage in restorePointToDelete.Storages)
            {
                Console.WriteLine($"delete file {Path.Combine(_repository.LocationPath, restorePointToDelete.Id.ToString(), storage.Path)}");
                File.Delete(Path.Combine(_repository.LocationPath, restorePointToDelete.Id.ToString(), storage.Path));
            }

            Console.WriteLine($"delete dir {Path.Combine(_repository.LocationPath, restorePointToDelete.Id.ToString())}");
            Directory.Delete(Path.Combine(_repository.LocationPath, restorePointToDelete.Id.ToString()));
        }
    }
}