using System;
using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class Schedule
    {
        private List<Lesson> _lessons;

        public Schedule()
        {
            _lessons = new List<Lesson>();
        }

        public void PlanLesson(DateTime lessonBeginTime)
        {
            _lessons.Add(new Lesson(lessonBeginTime));
        }
    }
}