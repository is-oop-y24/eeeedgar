using System;
using Backups.Repo;

namespace Backups.TemporaryLocalData
{
    public class TemporaryLocalStorage
    {
        public TemporaryLocalStorage(Storage storage, string temporaryPath, Guid id = default)
        {
            Id = id == default ? Guid.NewGuid() : id;
            TemporaryPath = temporaryPath;
            Storage = storage;
        }

        public Guid Id { get; }
        public string TemporaryPath { get; }
        public Storage Storage { get; }
    }
}