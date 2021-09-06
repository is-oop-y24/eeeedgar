using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService(22);
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            string studentName = "Ivan";
            Group group = _isuService.AddGroup("M3105");
            Student student = _isuService.AddStudent(group, studentName);
            Assert.Contains(student, group.Students);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            string studentBaseName = "kriper200";
            Group nonRubberGroup = _isuService.AddGroup("M3114");
            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i < 30; i++)
                {
                    string studentName = studentBaseName + i.ToString();
                    _isuService.AddStudent(nonRubberGroup, studentName);
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("omg");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            string studentName = "Ivan";
            Group oldGroup = _isuService.AddGroup("M3105");
            Student student = _isuService.AddStudent(oldGroup, studentName);
            Group newGroup = _isuService.AddGroup("M3200");
            _isuService.ChangeStudentGroup(student, newGroup);
            Assert.Contains(student, newGroup.Students);
            CollectionAssert.DoesNotContain(oldGroup.Students, student);
        }
    }
}