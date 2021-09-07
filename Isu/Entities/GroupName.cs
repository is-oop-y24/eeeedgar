using System;
using System.Globalization;
using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        public GroupName(string name)
        {
            try
            {
                Specialty = new Specialty(name[..2]);
                int.TryParse(name.Substring(2, 1), NumberStyles.Integer, new NumberFormatInfo(), out int courseNumber);
                if (courseNumber is > 4 or < 1)
                    throw new IsuException();
                CourseNumber = (CourseNumber)courseNumber;
                GroupNumber = new GroupNumber(name.Substring(3, 2));
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
        public string Name
        {
            get
            {
                string value = Specialty.Value;
                value += (int)CourseNumber;
                value += GroupNumber.Value;
                return value;
            }
        }
    }
}