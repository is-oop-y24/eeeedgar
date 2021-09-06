using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        public GroupName(string name)
        {
            try
            {
                Specialty = new Specialty(name[0].ToString() + name[1].ToString());
                CourseNumber = new CourseNumber(name[2].ToString());
                GroupNumber = new GroupNumber(name[3].ToString() + name[4].ToString());
            }
            catch (IsuException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Specialty Specialty { get; }
        public CourseNumber CourseNumber { get; }
        public GroupNumber GroupNumber { get; }
        public string Name => Specialty.Value + CourseNumber.Value + GroupNumber.Value;
    }
}