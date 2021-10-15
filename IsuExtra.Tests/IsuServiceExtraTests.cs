using System;
using Isu.Entities;
using Isu.Tools;
using IsuExtra.Entities;
using IsuExtra.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuServiceExtraTests
    {
        private IsuServiceExtra _isuServiceExtra;

        [SetUp]
        public void Setup()
        {
            _isuServiceExtra = new IsuServiceExtra();
        }
        
        [Test]
        public void AddGroupWithPrefix_RightMegaFacultyContainsGroup()
        {
            const string megaFacultyName = "TINT";
            const char prefix = 'M';
            string groupName = prefix + "3105";
                
            MegaFaculty megaFaculty = _isuServiceExtra.AddMegaFaculty(megaFacultyName);
            _isuServiceExtra.AddPrefixToMegaFaculty(megaFaculty, prefix);
            _isuServiceExtra.AddGroup(groupName);
            Assert.True(megaFaculty.IsuService.FindGroup(groupName) != null);
        }
        
        [Test]
        public void AddSimilarPrefixesToDifferentMegaFaculties_ThrowException()
        {
            const string firstMegaFacultyName = "TINT";
            const string secondMegaFacultyName = "KTU";
            const char prefix = 'M';
                
            MegaFaculty firstMegaFaculty = _isuServiceExtra.AddMegaFaculty(firstMegaFacultyName);
            MegaFaculty secondMegaFaculty = _isuServiceExtra.AddMegaFaculty(secondMegaFacultyName);
            _isuServiceExtra.AddPrefixToMegaFaculty(firstMegaFaculty, prefix);
            Assert.Catch<IsuException>(() =>
            {
                _isuServiceExtra.AddPrefixToMegaFaculty(secondMegaFaculty, prefix);
            });
        }

        [Test]
        public void AddOverlappingSchedule_ThrowException()
        {
            const string firstLessonStart = "8:20";
            const string secondLessonStart = "9:00";
            const uint classroomNumber = 229;
            const string megaFacultyName = "TINT";
            const char prefix = 'M';
            string groupName = prefix + "3105";
            
            MegaFaculty megaFaculty = _isuServiceExtra.AddMegaFaculty(megaFacultyName);
            _isuServiceExtra.AddPrefixToMegaFaculty(megaFaculty, prefix);
            Group group = _isuServiceExtra.AddGroup(groupName);
            var lesson1Start = DateTime.Parse(firstLessonStart);
            var lesson2Start = DateTime.Parse(secondLessonStart);
            var lesson1 = new Lesson(lesson1Start, classroomNumber);
            var lesson2 = new Lesson(lesson2Start, classroomNumber);

            _isuServiceExtra.AddLesson(group, lesson1);
            Assert.Catch<IsuException>(() =>
            {
                _isuServiceExtra.AddLesson(group, lesson2);
            });
        }

        [Test]
        public void AddStudentToExtraDisciplineGroupThatOverlapsOrdinaryGroupSchedule_ThrowException()
        {
            const string firstLessonStart = "8:20";
            const string secondLessonStart = "9:00";
            const uint classroomNumber = 229;
            const string firstMegaFacultyName = "TINT";
            const string secondMegaFacultyName = "KTU";
            const char firstPrefix = 'M';
            const char secondPrefix = 'K';
            string groupName = firstPrefix + "3205";
            const string extraDisciplineGroupName = "DDD4.1";
            const string studentName = "edgar";
            
            
            MegaFaculty firstMegaFaculty = _isuServiceExtra.AddMegaFaculty(firstMegaFacultyName);
            MegaFaculty secondMegaFaculty = _isuServiceExtra.AddMegaFaculty(secondMegaFacultyName);
            _isuServiceExtra.AddPrefixToMegaFaculty(firstMegaFaculty, firstPrefix);
            _isuServiceExtra.AddPrefixToMegaFaculty(secondMegaFaculty, secondPrefix);
            Group group = _isuServiceExtra.AddGroup(groupName);
            ExtraDisciplineGroup edGroup = _isuServiceExtra.AddExtraDisciplineGroup(secondMegaFaculty, extraDisciplineGroupName);
            var lesson1Start = DateTime.Parse(firstLessonStart);
            var lesson2Start = DateTime.Parse(secondLessonStart);
            var lesson1 = new Lesson(lesson1Start, classroomNumber);
            var lesson2 = new Lesson(lesson2Start, classroomNumber);
            _isuServiceExtra.AddLesson(group, lesson1);
            _isuServiceExtra.AddLesson(edGroup, lesson2);

            Student student = firstMegaFaculty.IsuService.AddStudent(group, studentName);
            Assert.Catch<IsuException>(() =>
            {
                _isuServiceExtra.AddStudentToExtraDisciplineGroup(edGroup, student);
            });
        }

        [Test]
        public void AddGroupWithNonAssociatedPrefix_ThrowException()
        {
            const string megaFacultyName = "TINT";
            const char prefix = 'M';
            const char nonExistingPrefix = 'S';
            const int courseNumber = 2;
            string groupName = nonExistingPrefix + "3" + courseNumber + "05";

            MegaFaculty megaFaculty = _isuServiceExtra.AddMegaFaculty(megaFacultyName);
            _isuServiceExtra.AddPrefixToMegaFaculty(megaFaculty, prefix);

            Assert.Catch<IsuException>(() =>
            {
                _isuServiceExtra.AddGroup(groupName);
            });
        }
        
        [Test]
        public void AddStudentToExtraDisciplineGroupThatAlreadyBelongsToAnotherOne_ThrowException()
        {
            const string megaFacultyName = "TINT";
            const char prefix = 'M';
            const int courseNumber = 2;
            string groupName = prefix + "3" + courseNumber + "05";
            const string edGroup1Name = "ed1";
            const string edGroup2Name = "ed2";
            const string studentName = "edgar";

            MegaFaculty megaFaculty = _isuServiceExtra.AddMegaFaculty(megaFacultyName);
            _isuServiceExtra.AddPrefixToMegaFaculty(megaFaculty, prefix);
            Group group = _isuServiceExtra.AddGroup(groupName);
            Student student = megaFaculty.IsuService.AddStudent(group, studentName);
            ExtraDisciplineGroup extraDisciplineGroup = _isuServiceExtra.AddExtraDisciplineGroup(megaFaculty, edGroup1Name);
            Assert.Catch<IsuException>(() =>
            {
                _isuServiceExtra.AddStudentToExtraDisciplineGroup(extraDisciplineGroup, student);
            });
        }
        
        [Test]
        public void AddStudentToExtraDisciplineGroupBelongsHisMegaFaculty_ThrowException()
        {
            const string megaFacultyName = "TINT";
            const char firstPrefix = 'M';
            string groupName = firstPrefix + "3205";
            const string extraDisciplineGroupName = "DDD4.1";
            const string studentName = "edgar";
            
            
            MegaFaculty megaFaculty = _isuServiceExtra.AddMegaFaculty(megaFacultyName);
            _isuServiceExtra.AddPrefixToMegaFaculty(megaFaculty, firstPrefix);
            Group group = _isuServiceExtra.AddGroup(groupName);
            ExtraDisciplineGroup edGroup = _isuServiceExtra.AddExtraDisciplineGroup(megaFaculty, extraDisciplineGroupName);
            
            Student student = megaFaculty.IsuService.AddStudent(group, studentName);
            Assert.Catch<IsuException>(() =>
            {
                _isuServiceExtra.AddStudentToExtraDisciplineGroup(edGroup, student);
            });
        }
        
        [Test]
        public void ChangeStudentExtraDiscipline_DisciplineChanged()
        {
            const string firstMegaFacultyName = "TINT";
            const string secondMegaFacultyName = "KTU";
            const string thirdMegaFacultyName = "NoZH";
            const char firstPrefix = 'M';
            const char secondPrefix = 'K';
            const char thirdPrefix = 'N';
            string groupName = firstPrefix + "3205";
            const string extraDisciplineGroupName = "DDD4.1";
            const string studentName = "edgar";
            
            
            MegaFaculty firstMegaFaculty = _isuServiceExtra.AddMegaFaculty(firstMegaFacultyName);
            MegaFaculty secondMegaFaculty = _isuServiceExtra.AddMegaFaculty(secondMegaFacultyName);
            MegaFaculty thirdMegaFaculty = _isuServiceExtra.AddMegaFaculty(thirdMegaFacultyName);
            _isuServiceExtra.AddPrefixToMegaFaculty(firstMegaFaculty, firstPrefix);
            _isuServiceExtra.AddPrefixToMegaFaculty(secondMegaFaculty, secondPrefix);
            _isuServiceExtra.AddPrefixToMegaFaculty(thirdMegaFaculty, thirdPrefix);
            Group group = _isuServiceExtra.AddGroup(groupName);
            ExtraDisciplineGroup edGroup1 = _isuServiceExtra.AddExtraDisciplineGroup(secondMegaFaculty, extraDisciplineGroupName);
            ExtraDisciplineGroup edGroup2 = _isuServiceExtra.AddExtraDisciplineGroup(thirdMegaFaculty, extraDisciplineGroupName);
            
            Student student = firstMegaFaculty.IsuService.AddStudent(group, studentName);
            _isuServiceExtra.AddStudentToExtraDisciplineGroup(edGroup1, student);
            _isuServiceExtra.RejectOfExtraDiscipline(student); _isuServiceExtra.AddStudentToExtraDisciplineGroup(edGroup2, student);
            Assert.Contains(student, edGroup2.Students);
            CollectionAssert.DoesNotContain(edGroup1.Students, student);
        }
    }
}