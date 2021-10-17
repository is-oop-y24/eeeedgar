using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace Backups.Entities
{
    public class BackupJob
    {
        public BackupJob(string backupPath, bool makeZipStorageSplit, TcpListener server = null)
        {
            BackupPath = backupPath;
            if (server != null)
            {
                if (makeZipStorageSplit)
                    throw new NotImplementedException();
                else
                    ExternalZipStorage = new ExternalSingleZipStorage();
            }
            else
            {
                if (makeZipStorageSplit)
                    LocalZipStorage = new SplitZipStorage();
                else
                    LocalZipStorage = new SingleZipStorage();
            }

            JobObjects = new List<JobObject>();
            RestorePoints = new List<RestorePoint>();
            if (!Directory.Exists(BackupPath))
                Directory.CreateDirectory(backupPath);
            Server = server;
        }

        public List<JobObject> JobObjects { get; }
        public List<RestorePoint> RestorePoints { get; }
        public string BackupPath { get; }
        public IZipStorage LocalZipStorage { get; }
        public IExternalZipStorage ExternalZipStorage { get; }

        public TcpListener Server { get; }

        public void AddJobObject(string filePath)
        {
            var jobObject = new JobObject(filePath);
            JobObjects.Add(jobObject);
        }

        public void RemoveJobObject(string filePath)
        {
            JobObjects.Remove(JobObjects.Find(o => o.FilePath.Equals(filePath)));
        }

        public void MakeBackup()
        {
            string backupPath = $"{BackupPath}/{ArchiveName()}";
            RestorePoints.Add(new RestorePoint(backupPath));
            LocalZipStorage.Create(JobObjects, backupPath);
        }

        public void MakeExternalBackup()
        {
            // RestorePoints.Add(new RestorePoint());
            ExternalZipStorage.Create(JobObjects, Server);
        }

        private string ArchiveName()
        {
            DateTime dateTime = DateTime.Now;
            return
                $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}-{dateTime.Hour}-{dateTime.Minute}-{dateTime.Second}";
        }
    }
}