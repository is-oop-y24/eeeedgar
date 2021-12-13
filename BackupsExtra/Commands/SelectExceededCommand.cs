using System;
using System.Collections.Generic;
using Backups.Repo;
using BackupsExtra.ClearingRestorePoints;

namespace BackupsExtra.Commands
{
    public class SelectExceededCommand
    {
        private readonly IExceededRestorePointsSelection _selection;
        private readonly DateTime _date;
        private List<RestorePoint> _selected;
        private List<RestorePoint> _restorePoints;
        public SelectExceededCommand(IExceededRestorePointsSelection selection, DateTime date, List<RestorePoint> restorePoints)
        {
            _selection = selection;
            _date = date;
            _restorePoints = restorePoints;
            _selected = new List<RestorePoint>();
        }

        public List<RestorePoint> Execute()
        {
            _selected = _selection.Execute(_restorePoints);
            return _selected;
        }

        public string Log()
        {
            return new LogTemplate().ExceededSelection(_selected, _date);
        }
    }
}