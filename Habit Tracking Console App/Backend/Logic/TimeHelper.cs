namespace Habit_Tracking_Console_App.Backend.Logic
{
    class TimeHelper
    {
        public static TimeSpan TimeTillNextDay()
        {
            DateTime now = DateTime.Now;
            DateTime dayFromNow = now.AddDays(1);
            DateTime tomorrowStart = new DateTime(dayFromNow.Year, dayFromNow.Month, dayFromNow.Day, 0, 0, 0);

            return tomorrowStart - now;
        }

        public static TimeSpan TimeTillNextWeek()
        {
            DateTime now = DateTime.Now;

            int daysUntilMonday = (((int)DayOfWeek.Monday - (int)now.DayOfWeek + 7) % 7);
            if (daysUntilMonday == 0) daysUntilMonday = 7;

            DateTime startOfNextDay = now.Date.AddDays(daysUntilMonday);

            return startOfNextDay - now;
        }

        public static TimeSpan TimeTillNextMonth()
        {
            DateTime now = DateTime.Now;

            DateTime startOfNextMonth = new DateTime(now.AddMonths(1).Year, now.AddMonths(1).Month, 1);

            return startOfNextMonth - now;
        }

        public static TimeSpan TimeTillNextYear()
        {
            DateTime now = DateTime.Now;

            DateTime startOfNextMonth = new DateTime(now.AddYears(1).Year, 1, 1);

            return startOfNextMonth - now;
        }
    }
}
