namespace Backups.Backup
{
    public class RestorePointProperties
    {
        public RestorePointProperties(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}