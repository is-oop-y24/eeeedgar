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
        public SelectExceededCommand(IExceededRestorePointsSelection selection, DateTime date)
        {
            _selection = selection;
            _date = date;
            _selected = new List<RestorePoint>();
        }

        public List<RestorePoint> Execute()
        {
            _selected = _selection.Execute();
            return _selected;
        }

        public string Log()
        {
            return new LogTemplate().ExceededSelection(_selected, _date);
        }
    }
}