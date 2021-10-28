using Backups.Job;
using Backups.Repo;
using Backups.Zippers;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            const string localRepoPath = @"D:\oop\lab-3\repository";
            const string restorerPath = @"D:\oop\lab-3\temporaryFiles";
            var repository = new LocalRepository(localRepoPath);
            var restorer = new SplitStorageCreator(restorerPath);
            var job = new BackupJob(repository, restorer);
            var jobObject1 = new JobObject(@"D:\OOP\lab-3\BackupJob\CurrentVersion\1.txt");
            var jobObject2 = new JobObject(@"D:\OOP\lab-3\BackupJob\CurrentVersion\2.txt");
            job.AddJobObject(jobObject1);
            job.AddJobObject(jobObject2);
            job.CreateBackup();
        }
    }
}
