using Habit_Tracking_Console_App.Backend.Objects;
using Habit_Tracking_Console_App.Frontend.PrintHelpers;

namespace Habit_Tracking_Console_App.Backend.Logic
{
    class InputManager
    {
        /// <summary>
        /// Prompts the user for input until given a boolean input.
        /// </summary>
        /// <param name="prompt">One or more strings to display as the prompt.</param>
        /// <returns>Boolean user input.</returns>
        public static bool GetBoolInput(params string[] prompt)
        {
            string userInput;

            while (true)
            {
                userInput = IO.PromptForNotEmptyInput(prompt).ToLower();

                switch (userInput)
                {
                    case string s when s == "true" || s == "t" || s == "y" || s == "yes" || s == "1":
                        IO.Clear();
                        return true;
                    case string s when s == "false" || s == "f" || s == "y" || s == "no" || s == "0":
                        IO.Clear();
                        return false;
                    default:
                        IO.InvalidInput(userInput, "boolean");
                        break;
                }
            }
        }

        public static int GetIntInput(params string[] prompt)
        {
            string userInput;
            int output;

            while (!int.TryParse(userInput = IO.PromptForNotEmptyInput(prompt), out output))
            {
                IO.Clear();
                IO.InvalidInput(userInput, "integer");
            }

            IO.Clear();
            return output;
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
