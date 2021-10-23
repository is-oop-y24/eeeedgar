namespace Backups.Repo
{
    public class LocalRepositoryProperties
    {
        public LocalRepositoryProperties(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}