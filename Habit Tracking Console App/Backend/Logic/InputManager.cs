using Habit_Tracking_Console_App.Backend.Objects;
using Habit_Tracking_Console_App.Frontend;
using System.Globalization;

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

        /// <summary>
        /// Prompts the user for input until given integer input.
        /// </summary>
        /// <param name="prompt">One or more strings to display as the prompt.</param>
        /// <returns>Integer user input.</returns>
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


        /// <summary>
        /// Prompts the user for input until given mm/dd/yyyy input.
        /// </summary>
        /// <param name="prompt">One or more strings to display as the prompt.</param>
        /// <returns>DateTime user input.</returns>
        public static DateOnly GetDateInput(params string[] prompt)
        {
            string userInput;
            DateOnly output;

            while (!DateOnly.TryParseExact(userInput = IO.PromptForNotEmptyInput(prompt), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out output))
            {
                IO.Clear();
                IO.InvalidInput(userInput, "date");
            }

            IO.Clear();
            return output;
        }

        /// <summary>
        /// Prompts the user for input until given a recurrence input.
        /// </summary>
        /// <param name="prompt">One or more strings to display as the prompt.</param>
        /// <returns>Recurrence user input.</returns>
        public static RecurrenceEnum GetRecurrenceInput(params string[] prompt)
        {
            while (true)
            {
                string userInput;
                switch (userInput = IO.PromptForNotEmptyInput(prompt))
                {
                    case "none":
                        return RecurrenceEnum.none;
                    case "daily":
                        return RecurrenceEnum.daily;
                    case "weekly":
                        return RecurrenceEnum.weekly;
                    case "monthly":
                        return RecurrenceEnum.monthly;
                    case "yearly":
                        return RecurrenceEnum.yearly;
                    default:
                        IO.Clear();
                        IO.InvalidInput(userInput, "recurrence");
                        break;
                }
            }
        }
    }
}
