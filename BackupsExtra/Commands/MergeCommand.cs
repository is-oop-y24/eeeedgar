using System;
using Backups.Repo;
using BackupsExtra.MergingRestorePoints;
using BackupsExtra.Tools;

namespace BackupsExtra.Commands
{
    public class MergeCommand
    {
        private readonly IMerging _merging;
        private readonly DateTime _time;
        private RestorePoint _resultRestorePoint;
        public MergeCommand(IMerging merging, DateTime time)
        {
            _merging = merging;
            _time = time;
            _resultRestorePoint = null;
        }

        public RestorePoint Execute()
        {
            _resultRestorePoint = _merging.Execute();
            return _resultRestorePoint;
        }

        public string Log()
        {
            if (_resultRestorePoint is null)
                throw new BackupsExtraException("attempt to get log before execution");
            return
                new LogTemplate().Merge("Single", _merging.RestorePoint1(), _merging.RestorePoint2(), _resultRestorePoint, _time);
        }
    }
}