using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class GroupNumber
    {
        public GroupNumber(string value)
        {
            try
            {
                int v = int.Parse(value);
                if (v is >= 0 and <= 99)
                {
                    Value = value;
                }
                else
                {
                    throw new IsuException("Exception: wrong group number");
                }
            }
            catch (IsuException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string Value { get; }
    }
}