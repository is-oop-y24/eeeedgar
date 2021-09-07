using System.Globalization;
using Isu.Tools;

namespace Isu.Entities
{
    public class GroupNumber
    {
        public GroupNumber(string value)
        {
            if (int.TryParse(value, NumberStyles.Integer, new NumberFormatInfo(), out int v))
            {
                if (v is >= 0 and <= 99)
                {
                    Value = value;
                }
                else
                {
                    throw new IsuException("Error: wrong group number (2).\n");
                }
            }
            else
            {
                throw new IsuException("Error: wrong group number (1).\n");
            }
        }

        public string Value { get; }
    }
}