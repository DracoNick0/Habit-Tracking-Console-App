namespace Habit_Tracking_Console_App.Model
{
    class TimeDependentVarUpdater
    {
        public TimeDependentVarUpdater()
        {
            Task.Run(() => this.WaitAndUpdate());
        }

        private void WaitAndUpdate()
        {
            while (true)
            {
                DateTime now = DateTime.Now;
                DateTime dayFromNow = DateTime.Now.AddDays(1);
                DateTime tomorrowStart = new DateTime(dayFromNow.Year, dayFromNow.Month, dayFromNow.Day, 0, 0, 0);

                TimeSpan timeTillTomorrow = tomorrowStart - now;
                double msTillTomorrow = timeTillTomorrow.TotalMilliseconds;

                // Debug messages
                // Console.WriteLine(timeTillTomorrow);
                // Console.WriteLine(now + timeTillTomorrow);

                // Wait
                Thread.Sleep((int)Math.Ceiling(msTillTomorrow) + 1000);

                // Update
                this.UpdateTimeDependentVariables();
            }
        }

        private void UpdateTimeDependentVariables()
        {

        }
    }
}
