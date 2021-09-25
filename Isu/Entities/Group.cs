using System.Collections.Generic;
using System.Globalization;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private const int MaxGroupSize = 20;
        private Group(string name)
        {
            CheckNameValidity(name);
            Name = name;
            Students = new List<Student>();
        }

        public string Name { get; }
        public int CourseNumber => int.Parse(Name.Substring(2, 1));

        public List<Student> Students { get; }

        public static Group CreateInstance(string name)
        {
            return new Group(name);
        }

        public void AddStudent(Student student)
        {
            if (Students.Count >= MaxGroupSize)
                throw new IsuException("MAX_GROUP_SIZE_REACHED");
            Students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
        }

        private void CheckNameValidity(string name)
        {
            CheckNameLength(name);
            CheckNameSpecialty(name);
            CheckNameNumber(name);
        }

        private void CheckNameLength(string name)
        {
            if (name.Length != 5)
                throw new IsuException("INVALID_GROUP_NAME: length must be 5");
        }

        private void CheckNameNumber(string name)
        {
            if (!int.TryParse(name.Substring(2, 1), NumberStyles.Integer, new NumberFormatInfo(), out int groupNumber))
                throw new IsuException("INVALID_GROUP_NAME: last two symbols must be numbers");

            if (name.Substring(3, 1) == "-")
                throw new IsuException("INVALID_GROUP_NAME: forth symbol can't be a '-'");
        }

        private void CheckNameSpecialty(string name)
        {
            if (name.Substring(0, 2) != "M3")
                throw new IsuException("INVALID_GROUP_NAME: first two symbols must be 'M3'");
        }
    }
}