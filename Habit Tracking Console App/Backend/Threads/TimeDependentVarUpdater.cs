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
                DateOnly today = DateOnly.FromDateTime(now);
                DateTime midnight = today.ToDateTime(TimeOnly.MaxValue);

                TimeSpan timeTillMidnight = midnight - now;
                double msTillMidnight = timeTillMidnight.TotalMilliseconds;

                // Wait
                Thread.Sleep((int)Math.Ceiling(msTillMidnight));
            }
        }

        private void UpdateTimeDependentVariables()
        {
            this.DateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
