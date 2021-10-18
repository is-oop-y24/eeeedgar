using System;
using System.Collections.Generic;
using System.IO;
using Backups.ClientServer;

namespace Backups.Entities
{
    public class BackupJob
    {
        private Client _client;
        public BackupJob(string backupPath, bool makeZipStorageSplit, Client client = null)
        {
            BackupPath = backupPath;
            if (client != null)
            {
                _client = client;
                if (makeZipStorageSplit)
                    ExternalZipStorage = new ExternalSplitZipStorage();
                else
                    ExternalZipStorage = new ExternalSingleZipStorage();
            }
            else
            {
                if (!Directory.Exists(BackupPath))
                    Directory.CreateDirectory(backupPath);
                if (makeZipStorageSplit)
                    LocalZipStorage = new SplitZipStorage();
                else
                    LocalZipStorage = new SingleZipStorage();
            }

            JobObjects = new List<JobObject>();
        }

        public List<JobObject> JobObjects { get; }
        public string BackupPath { get; }
        public IZipStorage LocalZipStorage { get; }
        public IExternalZipStorage ExternalZipStorage { get; }

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
            LocalZipStorage.Create(JobObjects, backupPath);
        }

        public void MakeExternalBackup()
        {
            ExternalZipStorage.Create(JobObjects, _client);
        }

        private string ArchiveName()
        {
            DateTime dateTime = DateTime.Now;
            return
                $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}-{dateTime.Hour}-{dateTime.Minute}-{dateTime.Second}";
        }
    }
}