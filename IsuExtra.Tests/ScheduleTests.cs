using System;
using IsuExtra.Entities;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class ScheduleTests
    {
        private Schedule _schedule;
        
        [SetUp]
        public void Setup()
        {
            _schedule = new Schedule();
        }

        [Test]
        public void MakeNearbyLessons_Overlap()
        {
            const string firstLessonStart = "8:20";
            const string secondLessonStart = "9:00";
            const string thirdLessonStart = "10:00";
            const uint classroomNumber = 229;

            var lesson1 = new Lesson(DateTime.Parse(firstLessonStart), classroomNumber);
            var lesson2 = new Lesson(DateTime.Parse(secondLessonStart), classroomNumber);
            var lesson3 = new Lesson(DateTime.Parse(thirdLessonStart), classroomNumber);
            //Assert.True(lesson1.DoesOverlap(lesson2));
            Assert.True(lesson2.DoesOverlap(lesson1));
            //Assert.False(lesson1.DoesOverlap(lesson3));
            
        }
    }
}