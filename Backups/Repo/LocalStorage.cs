namespace Backups.Repo
{
    public class LocalStorage
    {
        public LocalStorage(Storage storage, string temporaryPath)
        {
            TemporaryPath = temporaryPath;
            Storage = storage;
        }

        public string TemporaryPath { get; }
        public Storage Storage { get; }
    }
}