namespace Habit_Tracking_Console_App.Model
{
    class ConsoleResizeMonitor
    {
        private static bool running = true;

        public ConsoleResizeMonitor()
        {
            Task.Run(() => MonitorConsoleResize());
        }

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
