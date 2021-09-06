using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class Specialty
    {
        public Specialty(string value)
        {
            if (value == "M3")
            {
                Value = value;
            }
            else
            {
                throw new IsuException("Exception: wrong speciality");
            }
        }

        public string Value { get; } // aka "M31"
    }
}