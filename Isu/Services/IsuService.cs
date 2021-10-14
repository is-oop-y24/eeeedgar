using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private string _prefix;

        public IsuService(int lastCourseNumber = 4, string prefix = "")
        {
            _prefix = prefix;
            CourseNumbers = new List<CourseNumber>();
            for (int courseNumber = 1; courseNumber <= lastCourseNumber; courseNumber++)
            {
                CourseNumbers.Add(new CourseNumber(courseNumber));
            }
        }

        private List<CourseNumber> CourseNumbers { get; }

        public Group AddGroup(string name)
        {
            if (!_prefix.Equals(string.Empty))
            {
                if (name.Length < 1 || !name.Substring(0, 1).Equals(_prefix))
                    throw new IsuException("WRONG PREFIX");
            }

            var group = new Group(name);
            int courseNumber = group.Name.CourseNumber;
            CourseNumber course = GetCourse(courseNumber);
            course.AddGroup(group);
            return group;
        }

        public Group AddGroup(GroupName groupName)
        {
            var group = new Group(groupName);
            int courseNumber = group.Name.CourseNumber;
            CourseNumber course = GetCourse(courseNumber);
            course.AddGroup(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = Student.CreateInstance(name, group);
            group.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            foreach (
                Student student in CourseNumbers
                .SelectMany(courseNumber => courseNumber.Groups, (courseNumber, @group) => @group.Students
                .Find(st => st.Id == id))
                .Where(student => student != null))
            {
                return student;
            }

            throw new IsuException("INVALID_STUDENT_ID");
        }

        public Student FindStudent(string name)
        {
            return (from courseNumber in CourseNumbers from @group in courseNumber.Groups select @group.Students.Find(st => st.Name == name))
                .FirstOrDefault(student => student != null);
        }

        public List<Student> FindStudents(string groupName)
        {
            return FindGroup(groupName).Students;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return
                FindGroups(courseNumber)
                .SelectMany(group => group.Students)
                .ToList();
        }

        public Group FindGroup(string groupName)
        {
            CourseNumber course = GetCourse(GetGroupCourseNumber(groupName));
            Group group = course.Groups.Find(gr => gr.Name.Value.Equals(groupName));
            if (group == null)
                throw new IsuException("NONEXISTENT_GROUP");
            return group;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return courseNumber.Groups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            student.Group.RemoveStudent(student);
            newGroup.AddStudent(student);
            student.Group = newGroup;
        }

        private CourseNumber GetCourse(int courseNumber)
        {
            CourseNumber course = CourseNumbers.Find(c => c.Number == courseNumber);
            if (course == null)
                throw new IsuException("INVALID_COURSE_NUMBER");
            return course;
        }

        private int GetGroupCourseNumber(string groupName)
        {
            if (!int.TryParse(groupName.Substring(2, 1), NumberStyles.Integer, new NumberFormatInfo(), out int courseNumber))
            {
                throw new IsuException("INVALID_GROUP_NAME: course must be a number");
            }

            return courseNumber;
        }
    }
}