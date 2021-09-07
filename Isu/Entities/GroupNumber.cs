using System.Globalization;
using Isu.Tools;

namespace Isu.Entities
{
    public class GroupNumber
    {
        public GroupNumber(string value)
        {
            if (!int.TryParse(value, NumberStyles.Integer, new NumberFormatInfo(), out int numberValue))
            {
                throw new IsuException("Error: group number must be nonnegative number.\n");
            }

            if (numberValue < 0)
            {
                throw new IsuException("Error: group number can't be negative.\n");
            }

            Value = value;
        }

        public string Value { get; }
    }
}