using System.Collections.Generic;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class ExtraDisciplineService
    {
        public ExtraDisciplineService(int lastCourseNumber)
        {
            Courses = new List<CourseNumber>();
            for (int courseNumber = 1; courseNumber <= lastCourseNumber; courseNumber++)
            {
                Courses.Add(new CourseNumber(courseNumber));
            }
        }

        public List<CourseNumber> Courses { get; }

        public ExtraDisciplineGroup AddExtraDisciplineGroup(int courseNumber, string name)
        {
            var extraDisciplineGroup = new ExtraDisciplineGroup(name);
            CourseNumber course = Courses.Find(c => c.Number.Equals(courseNumber));
            if (course == null) throw new IsuException("ATTEMPT TO ADD GROUP TO INVALID COURSE");
            course.AddGroup(extraDisciplineGroup);
            return extraDisciplineGroup;
        }
    }
}