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
            const string megaFacultyName = "FITIP";
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
            const string firstMegaFacultyName = "FITIP";
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
            const string megaFacultyName = "FITIP";
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
            const string firstMegaFacultyName = "FITIP";
            const string secondMegaFacultyName = "KTU";
            const char firstPrefix = 'M';
            const char secondPrefix = 'K';
            const int courseNumber = 2;
            string groupName = firstPrefix + "3" + courseNumber + "05";
            const string extraDisciplineGroupName = "DDD4.1";
            const string studentName = "edgar";
            
            
            MegaFaculty firstMegaFaculty = _isuServiceExtra.AddMegaFaculty(firstMegaFacultyName);
            MegaFaculty secondMegaFaculty = _isuServiceExtra.AddMegaFaculty(secondMegaFacultyName);
            _isuServiceExtra.AddPrefixToMegaFaculty(firstMegaFaculty, firstPrefix);
            _isuServiceExtra.AddPrefixToMegaFaculty(firstMegaFaculty, secondPrefix);
            Group group = _isuServiceExtra.AddGroup(groupName);
            ExtraDisciplineGroup edGroup = _isuServiceExtra.AddExtraDisciplineGroup(secondMegaFaculty, courseNumber, extraDisciplineGroupName);
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
    }
}