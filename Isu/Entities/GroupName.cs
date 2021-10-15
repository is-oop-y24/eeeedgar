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

        private void CheckGroupNameValidity(string name)
        {
            if (!IsValidGroupNameLength(name))
                throw new IsuException("INVALID_GROUP_NAME: length must be 5");
            if (!IsValidGroupNameNumber(name))
                throw new IsuException("INVALID_GROUP_NAME: last two symbols must be numbers");
            if (!IsValidGroupNameHigherEducationDegree(name))
                throw new IsuException("INVALID_GROUP_NAME: higher education degree is bachelor, first two symbols must be '3'");
        }

        private bool IsValidGroupNameLength(string name)
        {
            return name.Length == 5;
        }

        private bool IsValidGroupNameNumber(string name)
        {
            if (!int.TryParse(name.Substring(3, 2), NumberStyles.Integer, new NumberFormatInfo(), out int groupNumber))
                return false;
            return name.Substring(3, 1) != "-";
        }

        private bool IsValidGroupNameHigherEducationDegree(string name)
        {
            return name.Substring(1, 1) == "3";
        }
    }
}