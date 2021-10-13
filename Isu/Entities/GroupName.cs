using System.Globalization;
using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        public GroupName(string value)
        {
            CheckNameValidity(value);
            Value = value;
        }

        public string Value { get; }

        public int CourseNumber
        {
            get
            {
                if (!int.TryParse(Value.Substring(2, 1), NumberStyles.Integer, new NumberFormatInfo(), out int courseNumber))
                    throw new IsuException("INVALID_GROUP_NAME: course number");
                return courseNumber;
            }
        }

        public static char Prefix(string name)
        {
            if (name.Length < 1)
                throw new IsuException("WRONG GROUP NAME LENGTH");
            return name[0];
        }

        private void CheckNameValidity(string name)
        {
            CheckNameLength(name);
            CheckHigherEducationDegree(name);
            CheckNameNumber(name);
        }

        private void CheckNameLength(string name)
        {
            if (name.Length != 5)
                throw new IsuException("INVALID_GROUP_NAME: length must be 5");
        }

        private void CheckNameNumber(string name)
        {
            if (!int.TryParse(name.Substring(3, 2), NumberStyles.Integer, new NumberFormatInfo(), out int groupNumber))
                throw new IsuException("INVALID_GROUP_NAME: last two symbols must be numbers");

            if (name.Substring(3, 1) == "-")
                throw new IsuException("INVALID_GROUP_NAME: forth symbol can't be a '-'");
        }

        private void CheckHigherEducationDegree(string name)
        {
            if (name.Substring(1, 1) != "3")
                throw new IsuException("INVALID_GROUP_NAME: higher education degree is bachelor, first two symbols must be '3'");
        }
    }
}