using System;
using System.Collections.Generic;
using Backups.Repo;
using BackupsExtra.Commands;

namespace BackupsExtra.MergingRestorePoints
{
    public class SingleStorageListMerging : IListMerging
    {
        private string _log;

        public SingleStorageListMerging()
        {
            _log = string.Empty;
        }

        public RestorePoint Execute(List<RestorePoint> restorePoints, DateTime time)
        {
            RestorePoint result = null;
            foreach (RestorePoint t in restorePoints)
            {
                var merging = new SingleStorageRestorePointsPairMerging(result, t);
                var command = new MergeCommand(merging, time, "Single");
                result = command.Execute();
                _log += command.Log();
            }

            return result;
        }

        public string Log()
        {
            return _log;
        }
    }
}