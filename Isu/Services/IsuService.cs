using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private List<Group> _groups;
        private int _currentId;
        private int _maxGroupSize;

        public IsuService(int maxGroupSize)
        {
            _groups = new List<Group>();
            _currentId = 0;
            _maxGroupSize = maxGroupSize;
        }

        public Group AddGroup(string name)
        {
            if (_groups.Find(gr => gr.Name() == name) != null)
            {
                throw new IsuException();
            }

            var group = new Group(name);
            _groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count >= _maxGroupSize)
                throw new IsuException();
            var student = new Student(_currentId++, name);
            group.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            return _groups.Select(group => group.Students.Find(student => student.Id == id))
                .FirstOrDefault(st => st != null);
        }

        public Student FindStudent(string name)
        {
            return _groups.Select(group => group.Students.Find(student => student.Name == name))
                .FirstOrDefault(st => st != null);
        }

        public List<Student> FindStudents(string groupName)
        {
            return _groups.Find(group => group.Name() == groupName)?.Students;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var students = new List<Student>();
            foreach (Group group in FindGroups(courseNumber))
            {
                students.AddRange(group.Students);
            }

            return students;
        }

        public Group FindGroup(string groupName)
        {
            return _groups.Find(group => group.Name() == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Where(group => Equals(group.GroupName.CourseNumber, courseNumber)).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (!_groups.Any(group => group.Students.Remove(student))) return;
            newGroup.AddStudent(student);
        }
    }
}
