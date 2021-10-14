using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private const int MaxGroupSize = 20;
        public Group(string name)
        {
            Name = new GroupName(name);
            Students = new List<Student>();
        }

        public Group(GroupName groupName)
        {
            Name = groupName;
            Students = new List<Student>();
        }

        public GroupName Name { get; }
        public List<Student> Students { get; }

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

        public override bool Equals(object obj)
        {
            return Equals(obj as Group);
        }

        public bool Equals(Group other)
        {
            return other != null && Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}