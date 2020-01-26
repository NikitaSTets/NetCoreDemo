using System;

namespace NetStandart.Calculator
{
    public class Calendar : ICalendar
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }
    }
}
