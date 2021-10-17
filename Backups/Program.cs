using Backups.Entities;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var backupJob = new BackupJob("D:/OOP/lab-3/backups", true);
            backupJob.AddJobObject("D:/OOP/lab-3/cur-version/1.txt");
            backupJob.AddJobObject("C:/Users/edgar/Downloads/Telegram Desktop/lgd.pdf");
            backupJob.MakeBackup();
        }
    }
}
