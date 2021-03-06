using System;
using Backups.Repo;
using BackupsExtra.MergingRestorePoints;
using BackupsExtra.Tools;

namespace BackupsExtra.Commands
{
    public class MergeCommand
    {
        private readonly IPairMerging _pairMerging;
        private readonly DateTime _time;
        private RestorePoint _resultRestorePoint;
        private string _storageType;
        public MergeCommand(IPairMerging pairMerging, DateTime time, string storageType)
        {
            _pairMerging = pairMerging;
            _time = time;
            _storageType = storageType;
            _resultRestorePoint = null;
        }

        public RestorePoint Execute()
        {
            _resultRestorePoint = _pairMerging.Execute();
            return _resultRestorePoint;
        }

        public string Log()
        {
            if (_resultRestorePoint is null)
                throw new BackupsExtraException("attempt to get log before execution");
            return
                new LogTemplate().Merge(_storageType, _pairMerging.RestorePoint1, _pairMerging.RestorePoint2, _resultRestorePoint, _time);
        }
    }
}