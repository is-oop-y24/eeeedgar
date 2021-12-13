using System;
using System.Linq;
using Backups.Job;
using Backups.Repo;
using Backups.Zippers;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            const string restorerPath = @"D:\oop\lab-3\temporaryFiles";

            // const string repoPath = @"D:\OOP\lab-3\Repository";
            var repository = new RemoteRepository("127.0.0.1", 1234);

            // var repository = new LocalRepository(repoPath);
            var restorer = new SplitStorageCreator(restorerPath);
            var job = new BackupJob(repository, restorer);
            var jobObject1 = new JobObject(@"D:\OOP\lab-3\BackupJob\CurrentVersion\1.txt");
            var jobObject2 = new JobObject(@"D:\OOP\lab-3\BackupJob\CurrentVersion\2.txt");
            job.AddJobObject(jobObject1);
            job.AddJobObject(jobObject2);
            job.CreateBackup();
            Console.WriteLine(job.Repository.GetRestorePoints().First().Storages.First().Path);
        }
    }
}
