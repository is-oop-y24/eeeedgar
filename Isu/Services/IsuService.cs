using System.Collections.Generic;
using Isu.Entities;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private List<Group> _groups = new List<Group>();
        private int _currentId = 0;

        public IsuService()
        {
        }

        public Group AddGroup(string name)
        {
            var group = new Group(name);
            _groups.Add(
                new Group(name)); // по ссылке? если создается копия, то возвращает копию группы
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(_currentId++, name);
            group.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            foreach (Group group in _groups)
            {
                Student student = group.FindStudent(id);
                if (student != null)
                    return student;
            }

            return null; // todo обработать? return copy?
        }

        public Student FindStudent(string name)
        {
            foreach (Group group in _groups)
            {
                Student student = group.FindStudent(name);
                if (student != null)
                    return student;
            }

            return null; // todo обработать?
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.Properties.Name == groupName)
                {
                    return group.Students;
                }
            }

            return null; // todo обработать?
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var students = new List<Student>();
            foreach (Group group in _groups)
            {
                if (group.Properties.CourseNumber == courseNumber)
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
                if (group.Properties.Name == groupName)
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
                if (group.Properties.CourseNumber == courseNumber)
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
                if (group.FindStudent(student) != null)
                {
                    group.RemoveStudent(student);
                    break;
                }
            }

            newGroup.AddStudent(student);
        }
    }
}
