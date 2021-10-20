namespace Backups.Repo
{
    public class RepositoryProperties
    {
        public RepositoryProperties(string pathToInit)
        {
            string[] p = pathToInit.Split('/');
            AbsolutePath = string.Empty;
            for (int i = 0; i < p.Length - 1; i++)
            {
                AbsolutePath += $"{p[i]}/";
            }
        }

        public string AbsolutePath { get; }
    }
}