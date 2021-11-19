using Backups.Repo;

namespace BackupsExtra.MergingRestorePoints
{
    public class SingleRestorePointsMerging
    {
        private readonly RestorePoint _restorePoint1;
        private readonly RestorePoint _restorePoint2;

        public SingleRestorePointsMerging(RestorePoint restorePoint1, RestorePoint restorePoint2)
        {
            _restorePoint1 = restorePoint1;
            _restorePoint2 = restorePoint2;
        }

        public RestorePoint Execute()
        {
            return _restorePoint1.DateTime > _restorePoint2.DateTime ? _restorePoint1 : _restorePoint2;
        }
    }
}