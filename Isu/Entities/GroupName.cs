using System.Globalization;
using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        public GroupName(string value)
        {
            CheckGroupNameValidity(value);
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

        public char Prefix
        {
            get
            {
                if (Value.Length < 1)
                    throw new IsuException("WRONG GROUP NAME LENGTH");
                return Value[0];
            }
        }

        public void CheckGroupNameValidity(string name)
        {
            CheckGroupNameLength(name);
            CheckGroupNameHigherEducationDegree(name);
            CheckGroupNameNumber(name);
        }

        public void CheckGroupNameLength(string name)
        {
            if (name.Length != 5)
                throw new IsuException("INVALID_GROUP_NAME: length must be 5");
        }

        public void CheckGroupNameNumber(string name)
        {
            if (!int.TryParse(name.Substring(3, 2), NumberStyles.Integer, new NumberFormatInfo(), out int groupNumber))
                throw new IsuException("INVALID_GROUP_NAME: last two symbols must be numbers");

            if (name.Substring(3, 1) == "-")
                throw new IsuException("INVALID_GROUP_NAME: forth symbol can't be a '-'");
        }

        public void CheckGroupNameHigherEducationDegree(string name)
        {
            if (name.Substring(1, 1) != "3")
                throw new IsuException("INVALID_GROUP_NAME: higher education degree is bachelor, first two symbols must be '3'");
        }
    }
}