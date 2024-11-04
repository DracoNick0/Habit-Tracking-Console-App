using Habit_Tracking_Console_App.Backend.Logic;
using Habit_Tracking_Console_App.Backend.Objects;
using System.Globalization;

namespace Habit_Tracking_Console_App.Frontend.PrintHelpers
{
    class IO
    {
        public const int maxStringLength = 50;

        /// <summary>
        /// Writes a prompt to the console and logs the action.
        /// </summary>
        /// <param name="prompt">The message displayed to the user.</param>
        public static void Write(string prompt)
        {
            Console.Write($"{prompt}");
            ActionLogger.AddAction(() => Console.Write($"{prompt}"));
        }

        /// <summary>
        /// Writelines a prompt to the console and logs the action.
        /// </summary>
        /// <param name="prompt">The message displayed to the user.</param>
        public static void WriteLine(string prompt)
        {
            Console.WriteLine($"{prompt}");
            ActionLogger.AddAction(() => Console.WriteLine($"{prompt}"));
        }

        /// <summary>
        /// Clears the console and clears all stored actions in the action logger.
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
            ActionLogger.ClearStoredActions();
        }

        /// <summary>
        /// Reads a line of input from the console and returns it as a string?.
        /// </summary>
        /// <returns>Input from the user.</returns>
        public static string? ReadLine()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Prints a prompt to the console with a space preceding the message.
        /// </summary>
        /// <param name="prompt"></param>
        public static void Msg(params string[] message)
        {
            if (message.Length != 0)
            {
                foreach (string line in message)
                {
                    WriteLine($" {line}");
                }
            }
        }

        /// <summary>
        /// Calls StorableMsgForWindow and logs the action.
        /// </summary>
        /// <param name="message">The main message to display.</param>
        /// <param name="cutoff">Optional. A string to append at the end if the final message exceeds the maximum length.</param>
        /// <param name="trail">Optional. A string to display at the end of the formatted message.</param>
        /// <param name="filler">Optional. A character used to fill the space if the message is shorter than the defined length.</param>
        /// <param name="maxLength">Optional. The maximum length of the output message. If set to -1, it is calculated based on the message length and trailing string.</param>
        public static void MsgForWindow(string message, string cutoff = "", string trail = "", char filler = ' ', int maxLength = maxStringLength)
        {
            StorableMsgForWindow(message, cutoff, trail, filler, maxLength);

            ActionLogger.AddAction(() => StorableMsgForWindow(message, cutoff, trail, filler, maxLength));
        }

        /// <summary>
        /// Formats and writes a message to the console, ensuring it fits within the current window width or maximum length. Can include a trailing message and custom filler text.
        /// </summary>
        /// <param name="message">The main message to display.</param>
        /// <param name="cutoff">Optional. A string to append at the end if the final message exceeds the maximum length.</param>
        /// <param name="trail">Optional. A string to display at the end of the formatted message.</param>
        /// <param name="filler">Optional. A character used to fill the space if the message is shorter than the defined length.</param>
        /// <param name="maxLength">Optional. The maximum length of the output message. If set to -1, it is calculated based on the message length and trailing string.</param>
        public static void StorableMsgForWindow(string message, string cutoff = "", string trail = "", char filler = ' ', int maxLength = maxStringLength)
        {
            string finalMessage = message;

            if (maxLength == -1)
            {
                maxLength = finalMessage.Length + trail.Length;
            }

            maxLength = Math.Min(maxLength, Console.WindowWidth - 1); // Formats the final string to either the window width or the user defined maxLength.
            int fillerCount = maxLength - finalMessage.Length - trail.Length;

            if (fillerCount >= 0)
            {
                string fillerStr = new string(filler, fillerCount); // Makes string that will fill the empty space.
                finalMessage = finalMessage + fillerStr + trail;
            }
            else if (fillerCount < 0)
            {
                finalMessage += trail;
                finalMessage = finalMessage.Substring(0, maxLength - cutoff.Length); // Make space for cutoff
                finalMessage += cutoff;
            }

            Console.WriteLine($" {finalMessage}");
        }

        /// <summary>
        /// Writelines a message in the format of an error.
        /// </summary>
        /// <param name="errorMsg">The message.</param>
        public static void Error(string errorMsg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Msg($"Error: {errorMsg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Writelines a message in the format of an info message.
        /// </summary>
        /// <param name="infoMsg">The message.</param>
        public static void Info(string infoMsg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Msg($"({infoMsg})");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Prints a prompt and gets user input.
        /// </summary>
        /// <param name="prompt">One or more strings to display as the prompt.</param>
        public static string? Prompt(params string[] prompt)
        {
            Msg(prompt);
            Write(" > ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Prompts the user for input until given a non-null input.
        /// </summary>
        /// <param name="prompt">One or more strings to display as the prompt.</param>
        /// <returns>Non-null user input.</returns>
        public static string PromptForNotNullInput(params string[] prompt)
        {
            string? userInput = null;

            while ((userInput = Prompt(prompt)) == null)
            {
                Clear();
                Error("Input was null, try again!");
            }

            Clear();
            return userInput;
        }

        /// <summary>
        /// Prompts the user for input until given a non-empty input.
        /// </summary>
        /// <param name="prompt">One or more strings to display as the prompt.</param>
        /// <returns>Non-empty user input.</returns>
        public static string PromptForNotEmptyInput(params string[] prompt)
        {
            string? userInput = null;

            while (string.IsNullOrEmpty(userInput = Prompt(prompt)))
            {
                Clear();
                Error("Input was empty, try again!");
            }

            Clear();
            return userInput;
        }

        /// <summary>
        /// Prompts the user for input until given a boolean input.
        /// </summary>
        /// <param name="prompt">One or more strings to display as the prompt.</param>
        /// <returns>Boolean user input.</returns>
        public static bool PromptForBoolInput(params string[] prompt)
        {
            string userInput;

            while (true)
            {
                userInput = PromptForNotEmptyInput(prompt).ToLower();

                switch (userInput)
                {
                    case string s when s == "true" || s == "t" || s == "y" || s == "yes":
                        Clear();
                        return true;
                    case string s when s == "false" || s == "f" || s == "y" || s == "no":
                        Clear();
                        return false;
                    default:
                        Error("Input was not valid, try again!");
                        break;
                }
            }
        }

        /// <summary>
        /// Prompts the user for input until given an integer input.
        /// </summary>
        /// <param name="prompt">One or more strings to display as the prompt.</param>
        /// <returns>Integer user input.</returns>
        public static int PromptForIntInput(params string[] prompt)
        {
            string userInput;
            int output;

            while (!int.TryParse(userInput = PromptForNotEmptyInput(prompt), out output))
            {
                Clear();
                Error($"\"{userInput}\" is not a valid integer, try again!");
            }

            Clear();
            return output;
        }

        /// <summary>
        /// Prompts the user for input until given mm/dd/yyyy input.
        /// </summary>
        /// <param name="prompt">One or more strings to display as the prompt.</param>
        /// <returns>DateTime user input.</returns>
        public static DateTime PromptForDateInput(params string[] prompt)
        {
            string userInput;
            DateTime output;

            while (!DateTime.TryParseExact(userInput = PromptForNotEmptyInput(prompt), "mm/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out output))
            {
                Clear();
                Error($"\"{userInput}\" is an invalid date, try again!");
            }

            Clear();
            return output;
        }

        /// <summary>
        /// Prompts the user for the recurrence of a task.
        /// </summary>
        /// <returns>User input.</returns>
        public RecurrenceEnum PromptForRecurrence(string prompt)
        {
            while (true)
            {
                string userInput;
                switch (userInput = PromptForNotEmptyInput(prompt))
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
                        Clear();
                        Error($"{userInput} was not a valid recurrence, try again!");
                        break;
                }
            }
        }
    }
}
