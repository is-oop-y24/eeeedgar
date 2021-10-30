using Backups.Repo;

namespace Backups.TemporaryLocalData
{
    public class TemporaryLocalStorage
    {
        public TemporaryLocalStorage(Storage storage, string temporaryPath)
        {
            TemporaryPath = temporaryPath;
            Storage = storage;
        }

        public string TemporaryPath { get; }
        public Storage Storage { get; }
    }
}