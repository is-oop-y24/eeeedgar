using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class CourseNumber
    {
        public CourseNumber(string value)
        {
            try
            {
                int v = int.Parse(value);
                if (v is >= 1 and <= 4)
                {
                    Value = value;
                }
                else
                {
                    throw new IsuException("Exception: wrong course number");
                }
            }
            catch (IsuException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string Value { get; } // aka "1"
    }
}