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
            RestorePoint result = restorePoints[0];
            for (int p = 1; p < restorePoints.Count; p++)
            {
                var merging = new SingleStorageRestorePointsPairMerging(result, restorePoints[p]);
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