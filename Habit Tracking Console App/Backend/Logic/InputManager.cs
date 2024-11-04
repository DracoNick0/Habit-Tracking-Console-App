using Habit_Tracking_Console_App.Backend.Objects;

namespace Habit_Tracking_Console_App.Backend.Logic
{
    class InputManager
    {
        public static bool GetBoolInput(params string[] prompt)
        {
            return false;
        }

        public static int GetIntegerInput(params string[] prompt)
        {
            return 0;
        }

        public static DateTime GetDateInput(params string[] prompt)
        {
            return DateTime.Now;
        }

        public static RecurrenceEnum GetRecurrenceInput(params string[] prompt)
        {
            return RecurrenceEnum.none;
        }
    }
}
