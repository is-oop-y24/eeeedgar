using System.Collections.Generic;
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
            foreach (Group gr in _groups)
            {
                if (gr.Name() == name)
                    throw new IsuException();
            }

            Group group = new Group(name);
            _groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count == _maxGroupSize)
                throw new IsuException();
            var student = new Student(_currentId++, name);
            group.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            foreach (Group group in _groups)
            {
                foreach (Student student in group.Students)
                {
                    if (student.Id == id)
                        return student;
                }
            }

            return null;
        }

        public Student FindStudent(string name)
        {
            foreach (Group group in _groups)
            {
                foreach (Student student in group.Students)
                {
                    if (student.Name == name)
                        return student;
                }
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.Name() == groupName)
                {
                    return group.Students;
                }
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var students = new List<Student>();
            foreach (Group group in _groups)
            {
                if (group.GroupName.CourseNumber == courseNumber)
                {
                    foreach (Student student in group.Students)
                    {
                        students.Add(student);
                    }
                }
            }

            return students;
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.Name() == groupName)
                {
                    return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var groups = new List<Group>();
            foreach (Group group in _groups)
            {
                if (group.GroupName.CourseNumber == courseNumber)
                {
                    groups.Add(group);
                }
            }

            return groups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (Group group in _groups)
            {
                if (group.Students.Remove(student))
                {
                    newGroup.AddStudent(student);
                    return;
                }
            }
        }
    }
}