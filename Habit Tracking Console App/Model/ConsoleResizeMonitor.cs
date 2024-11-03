namespace Habit_Tracking_Console_App.Model
{
    /// <summary>
    /// Monitors the console window size.
    /// </summary>
    class ConsoleResizeMonitor
    {
        private static bool running = true;

        /// <summary>
        /// Creates a thread that runs parallel to the program.
        /// </summary>
        public ConsoleResizeMonitor()
        {
            Task.Run(() => MonitorConsoleResize());
        }

        /// <summary>
        /// Continuously checks if the size of the window has changed, and if it does, it executes all stored actions.
        /// </summary>
        private void MonitorConsoleResize()
        {
            int previousWidth = Console.WindowWidth;
            int previousHeight = Console.WindowHeight;

            while (running)
            {
                if (Console.WindowWidth != previousWidth || Console.WindowHeight != previousHeight)
                {
                    previousWidth = Console.WindowWidth;
                    previousHeight = Console.WindowHeight;

                    ActionLogger.ExecuteStoredActions();
                }

                Thread.Sleep(100);
            }
        }
    }
}
