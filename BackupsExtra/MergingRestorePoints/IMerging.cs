using Backups.Repo;

namespace BackupsExtra.MergingRestorePoints
{
    public interface IMerging
    {
        RestorePoint Execute();
        RestorePoint RestorePoint1();
        RestorePoint RestorePoint2();
    }
}