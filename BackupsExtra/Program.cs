using System;
using System.Collections.Generic;
using Backups.Job;
using Backups.Repo;
using BackupsExtra.ClearingRestorePoints;
using BackupsExtra.Commands;
using BackupsExtra.MergingRestorePoints;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            ShowLogs();
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
                    var storage1 = new Storage();
                    var storage2 = new Storage();

                    storage1.JobObjects.Add(jobObjects[0]);
                    storage1.JobObjects.Add(jobObjects[1]);

                    storage2.JobObjects.Add(jobObjects[1]);
                    storage2.JobObjects.Add(jobObjects[2]);

                    var storages1 = new List<Storage> { storage1 };
                    var storages2 = new List<Storage> { storage2 };

                    var dateTime1 = DateTime.Parse("7/30/2002");
                    var dateTime2 = DateTime.Parse("8/22/1998");

                    var restorePoint1 = new RestorePoint(storages1, dateTime1, "okay", Guid.NewGuid());
                    var restorePoint2 = new RestorePoint(storages2, dateTime2, "okay", Guid.NewGuid());

                    var merging = new SingleStorageRestorePointsMerging(restorePoint1, restorePoint2);

                    var command = new MergeCommand(merging, DateTime.Now);
                    RestorePoint restorePoint = command.Execute();
                    string log = command.Log();
                    Console.WriteLine(log);
                }

                Console.WriteLine("-------------");
                {
                    // split merge
                    var storage1 = new Storage();
                    var storage2 = new Storage();
                    var storage3 = new Storage();

                    storage1.JobObjects.Add(jobObjects[0]);
                    storage2.JobObjects.Add(jobObjects[1]);
                    storage3.JobObjects.Add(jobObjects[2]);

                    var storages = new List<Storage> { storage1, storage2, storage3 };
                    var storages1 = new List<Storage> { storage1, storage2 };
                    var storages2 = new List<Storage> { storage2, storage3 };

                    var dateTime1 = DateTime.Parse("7/30/2002");
                    var dateTime2 = DateTime.Parse("8/22/1998");

                    var restorePoint1 = new RestorePoint(storages1, dateTime1, "okay", Guid.NewGuid());
                    var restorePoint2 = new RestorePoint(storages2, dateTime2, "okay", Guid.NewGuid());

                    var merging = new SplitStorageRestorePointsMerging(restorePoint1, restorePoint2);
                    var command = new MergeCommand(merging, DateTime.Now);
                    RestorePoint restorePoint = command.Execute();
                    string log = command.Log();
                    Console.WriteLine(log);
                }

                Console.WriteLine("-------------");
                {
                    // date selection
                    var storage1 = new Storage();
                    var storage2 = new Storage();
                    var storage3 = new Storage();

                    storage1.JobObjects.Add(jobObjects[0]);
                    storage2.JobObjects.Add(jobObjects[1]);
                    storage3.JobObjects.Add(jobObjects[2]);

                    var storages = new List<Storage> { storage1, storage2, storage3 };
                    var storages1 = new List<Storage> { storage1, storage2 };
                    var storages2 = new List<Storage> { storage2, storage3 };

                    var dateTime1 = DateTime.Parse("7/30/2002");
                    var dateTime2 = DateTime.Parse("8/22/1998");

                    var restorePoint1 = new RestorePoint(storages1, dateTime1, "okay", Guid.NewGuid());
                    var restorePoint2 = new RestorePoint(storages2, dateTime2, "okay", Guid.NewGuid());

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
                    var storage1 = new Storage();
                    var storage2 = new Storage();
                    var storage3 = new Storage();

                    storage1.JobObjects.Add(jobObjects[0]);
                    storage2.JobObjects.Add(jobObjects[1]);
                    storage3.JobObjects.Add(jobObjects[2]);

                    var storages = new List<Storage> { storage1, storage2, storage3 };
                    var storages1 = new List<Storage> { storage1, storage2 };
                    var storages2 = new List<Storage> { storage2, storage3 };

                    var dateTime1 = DateTime.Parse("7/30/2002");
                    var dateTime2 = DateTime.Parse("8/22/1998");

                    var restorePoint1 = new RestorePoint(storages1, dateTime1, "okay", Guid.NewGuid());
                    var restorePoint2 = new RestorePoint(storages2, dateTime2, "okay", Guid.NewGuid());

                    var restorePoints = new List<RestorePoint> { restorePoint1, restorePoint2 };
                    var selection = new OverTheNumberLimitRestorePointsSelection(restorePoints, 1);
                    var command = new SelectExceededCommand(selection, DateTime.Now);
                    List<RestorePoint> exceededRestorePoints = command.Execute();
                    string log = command.Log();
                    Console.WriteLine(log);
                }
            }
        }
    }
}
