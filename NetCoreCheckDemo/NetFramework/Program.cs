using System;
using NetStandart.Calculator;

namespace NetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            var calendar = new Calendar();
            var currentDate = calendar.GetCurrentDate();

            Console.WriteLine(currentDate);
        }
    }
}
