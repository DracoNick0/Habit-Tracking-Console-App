using Habit_Tracking_Console_App.Backend.Objects;
using Habit_Tracking_Console_App.Frontend;

namespace Habit_Tracking_Console_App.Backend.Threads
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
            bool checkIfBigger = Console.WindowWidth <= IO.maxStringLength + 1;

            while (running)
            {
                if (Console.WindowWidth != previousWidth)
                {
                    previousWidth = Console.WindowWidth;

                    if (Console.WindowWidth <= IO.maxStringLength + 1 || checkIfBigger && Console.WindowWidth > IO.maxStringLength + 1)
                    {
                        checkIfBigger = Console.WindowWidth <= IO.maxStringLength + 1;
                        ActionLogger.ExecuteStoredActions();
                    }
                }

                Thread.Sleep(200);
            }
        }
    }
}
