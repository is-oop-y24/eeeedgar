using System.Globalization;
using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        public GroupName(string name)
        {
            Specialty = new Specialty(name.Substring(0, 2));
            if (!int.TryParse(name.Substring(2, 1), NumberStyles.Integer, new NumberFormatInfo(), out int courseNumber))
            {
                throw new IsuException("Error: Course number must be a number from 1 to 4.\n");
            }

            if (courseNumber is > 4 or < 1)
            {
                throw new IsuException("Error: Course number must be from 1 to 4.\n");
            }

            CourseNumber = (CourseNumber)courseNumber;
            GroupNumber = new GroupNumber(name.Substring(3, 2));
        }

        public Specialty Specialty { get; }
        public CourseNumber CourseNumber { get; }
        public GroupNumber GroupNumber { get; }
        public string Name => $"{Specialty.Value + (int)CourseNumber + GroupNumber.Value}";
    }
}