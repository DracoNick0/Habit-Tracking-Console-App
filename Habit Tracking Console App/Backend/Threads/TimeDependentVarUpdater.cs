namespace Habit_Tracking_Console_App.Backend.Threads
{
    class TimeDependentVarUpdater
    {
        public event EventHandler DateChanged = delegate { };

        public TimeDependentVarUpdater()
        {
            Task.Run(() => WaitAndUpdate());
        }

        private void WaitAndUpdate()
        {
            while (true)
            {
                // Update
                UpdateTimeDependentVariables();

                DateTime now = DateTime.Now;
                DateTime dayFromNow = now.AddDays(1);
                DateTime tomorrowStart = new DateTime(dayFromNow.Year, dayFromNow.Month, dayFromNow.Day, 0, 0, 0);

                TimeSpan timeTillTomorrow = tomorrowStart - now;
                double msTillTomorrow = timeTillTomorrow.TotalMilliseconds;

                // Debug messages
                // Console.WriteLine(timeTillTomorrow);
                // Console.WriteLine(now + timeTillTomorrow);

                // Wait
                Thread.Sleep((int)Math.Ceiling(msTillTomorrow) + 1000);
            }
        }

        private void UpdateTimeDependentVariables()
        {
            this.DateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
