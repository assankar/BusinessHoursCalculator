using System;

namespace BusinessHourCalculator
{
    class BusinessHourCalculator
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //string t1 = "2021-06-20 08:00:00";
            //DateTime dt1 = DateTime.Parse(t1);
            //string t2 = "2021-06-26 17:00:00";
            //DateTime dt2 = DateTime.Parse(t2);
            //Console.WriteLine((dt2 - dt1).Days);

            BusinessHourCalculator businessHourCalculator = new BusinessHourCalculator();
            string test1Start = "2021-06-21 08:00:00";
            string test1End = "2021-06-25 17:00:00";
            Console.WriteLine("Should be 40: " + businessHourCalculator.BusinessHours(test1Start, test1End));

            string test2Start = "2021-06-14 08:00:00";
            string test2End = "2021-06-25 17:00:00";
            Console.WriteLine("Should be 80: " + businessHourCalculator.BusinessHours(test2Start, test2End));

            string test3Start = "2021-06-13 08:00:00";
            string test3End = "2021-06-25 17:00:00";
            Console.WriteLine("Should be 80: " + businessHourCalculator.BusinessHours(test3Start, test3End));

            string test4Start = "2021-06-14 08:00:00";
            string test4End = "2021-06-26 17:00:00";
            Console.WriteLine("Should be 80: " + businessHourCalculator.BusinessHours(test4Start, test4End));

            string test5Start = "2021-06-13 08:00:00";
            string test5End = "2021-06-26 17:00:00";
            Console.WriteLine("Should be 80: " + businessHourCalculator.BusinessHours(test5Start, test5End));

            string test6Start = "2021-06-09 02:00:00";
            string test6End = "2021-06-29 13:00:00";
            Console.WriteLine("Should be 116: " + businessHourCalculator.BusinessHours(test6Start, test6End));

            string test7Start = "2021-05-28 02:00:00";
            string test7End = "2021-05-31 13:00:00";
            Console.WriteLine("Should be 12: " + businessHourCalculator.BusinessHours(test7Start, test7End));
        }
        public BusinessHourCalculator()
        {

        }

        public int BusinessHours(string str1, string str2)
        {
            DateTime start = DateTime.Parse(str1);
            DateTime end = DateTime.Parse(str2);
            TimeSpan reset = new TimeSpan(0, 0, 0);

            DayOfWeek startdayOfWeek = start.DayOfWeek;
            int daysInFirstWeek = 0;
            int hoursInFirstDay = 0;
            int hoursInFirstWeek;

            if(startdayOfWeek == DayOfWeek.Sunday)
            {
                daysInFirstWeek = 5;
                hoursInFirstDay = 0;
                hoursInFirstWeek = 40;
                start = start.AddDays(7);
                start = start.Date + reset;
            }
            else if(startdayOfWeek != DayOfWeek.Saturday)
            {
                daysInFirstWeek = DayOfWeek.Friday - startdayOfWeek;
                if(start.Hour > 9){
                    hoursInFirstDay = 17 - start.Hour;
                }
                else
                {
                    hoursInFirstDay = 8;
                }
                hoursInFirstWeek = daysInFirstWeek * 8 + hoursInFirstDay;
                start = start.AddDays(daysInFirstWeek + 2);
                start = start.Date + reset;
            }
            else
            {
                daysInFirstWeek = 0;
                hoursInFirstWeek = 0;
            }

            DayOfWeek enddayOfWeek = end.DayOfWeek;
            int daysInLastWeek = 0;
            int hoursInLastDay = 0;
            int hoursInLastWeek = 0;

            if ((end - start).Days > 5 || ((end-start).Days < 5 && end.DayOfWeek > start.DayOfWeek))
            {
                if (enddayOfWeek == DayOfWeek.Saturday)
                {
                    daysInLastWeek = 5;
                    hoursInLastDay = 0;
                    hoursInLastWeek = 40;
                    end = end.AddDays(-6);
                    end = end.Date + reset;

                }
                else if (enddayOfWeek != DayOfWeek.Sunday)
                {
                    daysInLastWeek = enddayOfWeek - DayOfWeek.Monday;
                    if (end.Hour < 17)
                    {
                        hoursInLastDay = end.Hour - 9;
                    }
                    else
                    {
                        hoursInLastDay = 8;
                    }
                    hoursInLastWeek = daysInLastWeek * 8 + hoursInLastDay;
                    end = end.AddDays((daysInLastWeek+1) * -1);
                    end = end.Date + reset;
                }
                else
                {
                    daysInLastWeek = 0;
                    hoursInLastWeek = 0;
                }
            }
            else
            {
                hoursInLastWeek = 0;
            }

            TimeSpan timeSpan = end - start;
            int businessWeeks = timeSpan.Days / 7;
            int businessHours = businessWeeks * 40;
            businessHours = businessHours + hoursInFirstWeek + hoursInLastWeek;

            return businessHours;
        }

        public int BruteForceBusinessHour(string str1, string str2)
        {
            DateTime start = DateTime.Parse(str1);
            DateTime end = DateTime.Parse(str2);

            int count = 0;

            for (var i = start; i < end; i = i.AddHours(1))
            {
                if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (i.TimeOfDay.Hours >= 8 && i.TimeOfDay.Hours < 17)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
