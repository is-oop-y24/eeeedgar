using System.Collections.Generic;
using System.Linq;
using Backups.Repo;

namespace BackupsExtra.ClearingRestorePoints
{
    public class AlgorithmByAmount
    {
        // todo find a better name
        private readonly List<RestorePoint> _restorePoints;
        public AlgorithmByAmount(List<RestorePoint> restorePoints, int amount)
        {
            _restorePoints = restorePoints;
            Amount = amount;
        }

        public int Amount { get; }

        public List<RestorePoint> SelectExceededRestorePoints()
        {
            var exceededRestorePoints = new List<RestorePoint>();
            var copiedPointArray = new RestorePoint[_restorePoints.Count];
            _restorePoints.CopyTo(copiedPointArray);
            var points = copiedPointArray.ToList();
            points.Sort((p1, p2) => p1.DateTime.CompareTo(p2.DateTime));
            return points.GetRange(0, _restorePoints.Count - Amount);
        }
    }
}