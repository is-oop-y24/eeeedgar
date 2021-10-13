using System;
using IsuExtra.Entities;

namespace IsuExtra
{
    internal class Program
    {
        private static void Main()
        {
            // var isuServiceExtra = new IsuServiceExtra();
            // MegaFaculty megaFaculty = isuServiceExtra.AddMegaFaculty("fitip");
            // isuServiceExtra.AddPrefixToMegaFaculty(megaFaculty, 'M');
            const string firstLessonStart = "8:20";
            const string secondLessonStart = "9:00";
            const string thirdLessonStart = "10:00";
            const uint classroomNumber = 229;

            var lesson1 = new Lesson(DateTime.Parse(firstLessonStart), classroomNumber);
            var lesson2 = new Lesson(DateTime.Parse(secondLessonStart), classroomNumber);
            var lesson3 = new Lesson(DateTime.Parse(thirdLessonStart), classroomNumber);

            Console.WriteLine(lesson1.BeginTime);
            Console.WriteLine(lesson1.EndTime);
        }
    }
}
