using System;
using System.Collections.Generic;
using Backups.Repo;
using BackupsExtra.Commands;

namespace BackupsExtra.MergingRestorePoints
{
    public class SplitStorageListMerging : IListMerging
    {
        private string _log;

        public SplitStorageListMerging()
        {
            _log = string.Empty;
        }

        public RestorePoint Execute(List<RestorePoint> restorePoints, DateTime time)
        {
            RestorePoint result = restorePoints[0];
            for (int p = 1; p < restorePoints.Count; p++)
            {
                var merging = new SplitStorageRestorePointsPairMerging(result, restorePoints[p]);
                var command = new MergeCommand(merging, time, "Split");
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