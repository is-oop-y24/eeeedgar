using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Job;
using Backups.Repo;
using Backups.Zippers;
using BackupsExtra.ClearingRestorePoints;
using BackupsExtra.Commands;
using BackupsExtra.JobExtra;
using BackupsExtra.MergingRestorePoints;
using BackupsExtra.RepoExtra;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            BackupExtraTest();
        }

        private static void ShowLogs()
        {
            {
                // single merge
                List<JobObject> jobObjects;
                var jobObject1 = new JobObject(@"path1");
                var jobObject2 = new JobObject(@"path2");
                var jobObject3 = new JobObject(@"path3");
                jobObjects = new List<JobObject>
                {
                    jobObject1,
                    jobObject2,
                    jobObject3,
                };
                {
                    var storage1 = new Storage(string.Empty);
                    var storage2 = new Storage(string.Empty);

                    storage1.JobObjects.Add(jobObjects[0]);
                    storage1.JobObjects.Add(jobObjects[1]);

                    storage2.JobObjects.Add(jobObjects[1]);
                    storage2.JobObjects.Add(jobObjects[2]);

                    var storages1 = new List<Storage> { storage1 };
                    var storages2 = new List<Storage> { storage2 };

                    var dateTime1 = DateTime.Parse("7/30/2002");
                    var dateTime2 = DateTime.Parse("8/22/1998");

                    var restorePoint1 = new RestorePoint(storages1, dateTime1, Guid.NewGuid());
                    var restorePoint2 = new RestorePoint(storages2, dateTime2, Guid.NewGuid());

                    var merging = new SingleStorageRestorePointsPairMerging(restorePoint1, restorePoint2);

                    var command = new MergeCommand(merging, DateTime.Now);
                    RestorePoint restorePoint = command.Execute();
                    string log = command.Log();
                    Console.WriteLine(log);
                }

                Console.WriteLine("-------------");
                {
                    // split merge
                    var storage1 = new Storage(string.Empty);
                    var storage2 = new Storage(string.Empty);
                    var storage3 = new Storage(string.Empty);

                    storage1.JobObjects.Add(jobObjects[0]);
                    storage2.JobObjects.Add(jobObjects[1]);
                    storage3.JobObjects.Add(jobObjects[2]);

                    var storages = new List<Storage> { storage1, storage2, storage3 };
                    var storages1 = new List<Storage> { storage1, storage2 };
                    var storages2 = new List<Storage> { storage2, storage3 };

                    var dateTime1 = DateTime.Parse("7/30/2002");
                    var dateTime2 = DateTime.Parse("8/22/1998");

                    var restorePoint1 = new RestorePoint(storages1, dateTime1, Guid.NewGuid());
                    var restorePoint2 = new RestorePoint(storages2, dateTime2, Guid.NewGuid());

                    var merging = new SplitStorageRestorePointsPairMerging(restorePoint1, restorePoint2);
                    var command = new MergeCommand(merging, DateTime.Now);
                    RestorePoint restorePoint = command.Execute();
                    string log = command.Log();
                    Console.WriteLine(log);
                }

                Console.WriteLine("-------------");
                {
                    // date selection
                    var storage1 = new Storage(string.Empty);
                    var storage2 = new Storage(string.Empty);
                    var storage3 = new Storage(string.Empty);

                    storage1.JobObjects.Add(jobObjects[0]);
                    storage2.JobObjects.Add(jobObjects[1]);
                    storage3.JobObjects.Add(jobObjects[2]);

                    var storages = new List<Storage> { storage1, storage2, storage3 };
                    var storages1 = new List<Storage> { storage1, storage2 };
                    var storages2 = new List<Storage> { storage2, storage3 };

                    var dateTime1 = DateTime.Parse("7/30/2002");
                    var dateTime2 = DateTime.Parse("8/22/1998");

                    var restorePoint1 = new RestorePoint(storages1, dateTime1, Guid.NewGuid());
                    var restorePoint2 = new RestorePoint(storages2, dateTime2, Guid.NewGuid());

                    var restorePoints = new List<RestorePoint> { restorePoint1, restorePoint2 };
                    var selection = new OutdatedRestorePointsSelection(restorePoints, DateTime.Parse("1/1/2000"));
                    var command = new SelectExceededCommand(selection, DateTime.Now);
                    List<RestorePoint> exceededRestorePoints = command.Execute();
                    string log = command.Log();
                    Console.WriteLine(log);
                }

                Console.WriteLine("-------------");
                {
                    // amount selection
                    var storage1 = new Storage(string.Empty);
                    var storage2 = new Storage(string.Empty);
                    var storage3 = new Storage(string.Empty);

                    storage1.JobObjects.Add(jobObjects[0]);
                    storage2.JobObjects.Add(jobObjects[1]);
                    storage3.JobObjects.Add(jobObjects[2]);

                    var storages = new List<Storage> { storage1, storage2, storage3 };
                    var storages1 = new List<Storage> { storage1, storage2 };
                    var storages2 = new List<Storage> { storage2, storage3 };

                    var dateTime1 = DateTime.Parse("7/30/2002");
                    var dateTime2 = DateTime.Parse("8/22/1998");

                    var restorePoint1 = new RestorePoint(storages1, dateTime1, Guid.NewGuid());
                    var restorePoint2 = new RestorePoint(storages2, dateTime2, Guid.NewGuid());

                    var restorePoints = new List<RestorePoint> { restorePoint1, restorePoint2 };
                    var selection = new OverTheNumberLimitRestorePointsSelection(restorePoints, 1);
                    var command = new SelectExceededCommand(selection, DateTime.Now);
                    List<RestorePoint> exceededRestorePoints = command.Execute();
                    string log = command.Log();
                    Console.WriteLine(log);
                }
            }
        }

        private static void BackupExtraTest()
        {
            const string localRepositoryPath = @"D:\\OOP\\lab-5\\repo";
            const string temporaryFilesDirectoryPath = @"D:\\OOP\\lab-5\\temp";

            var repository = new LocalRepository(localRepositoryPath);
            var repositoryExtra = new LocalRepositoryExtra(repository);
            var zipper = new SplitStorageCreator(temporaryFilesDirectoryPath);

            var backupJobExtra = new BackupJobExtra(new StorageConditions(), new SplitStorageListMerging(), repositoryExtra, zipper);
            backupJobExtra.StorageConditions.SetNumberLimit(3);
            var jobObject1 = new JobObject(@"D:\OOP\lab-5\data\1.txt");
            var jobObject2 = new JobObject(@"D:\OOP\lab-5\data\2.txt");
            var jobObject3 = new JobObject(@"D:\OOP\lab-5\data\3.txt");
            var jobObject4 = new JobObject(@"D:\OOP\lab-5\data\4.txt");

            backupJobExtra.Job.AddJobObject(jobObject1);
            backupJobExtra.CreateBackup(DateTime.Parse("1/1/2000"));

            backupJobExtra.Job.RemoveJobObject(jobObject1);
            backupJobExtra.Job.AddJobObject(jobObject2);
            backupJobExtra.CreateBackup(DateTime.Parse("2/1/2000"));

            backupJobExtra.Job.RemoveJobObject(jobObject2);
            backupJobExtra.Job.AddJobObject(jobObject3);
            backupJobExtra.CreateBackup(DateTime.Parse("3/1/2000"));

            backupJobExtra.Job.RemoveJobObject(jobObject3);
            backupJobExtra.Job.AddJobObject(jobObject4);
            backupJobExtra.CreateBackup(DateTime.Parse("4/1/2000"));

            Console.WriteLine(backupJobExtra.Job.Repository.GetRestorePoints().Count);
            foreach (RestorePoint restorePoint in backupJobExtra.Job.Repository.GetRestorePoints())
            {
                Console.WriteLine(restorePoint.Id);
                foreach (Storage storage in restorePoint.Storages)
                {
                    Console.WriteLine(storage.Path);
                }
            }

            Guid id = backupJobExtra.Job.Repository.GetRestorePoints().Last().Id;
            backupJobExtra.RepositoryExtra.DeleteRestorePoint(id);
        }
    }
}
