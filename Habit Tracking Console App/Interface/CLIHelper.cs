using Habit_Tracking_Console_App.Backend;

namespace Habit_Tracking_Console_App.Interface
{
    class CLIHelper
    {
        public const int maxStringLength = 50;

        public static void Write(string prompt)
        {
            Console.Write($" {prompt}");
            ActionLogger.AddAction(() => Console.Write($" {prompt}"));
        }

        public static void WriteLine(string prompt)
        {
            Console.WriteLine($" {prompt}");
            ActionLogger.AddAction(() => Console.WriteLine($" {prompt}"));
        }

        public static void Clear()
        {
            Console.Clear();
            ActionLogger.ClearStoredActions();
        }

        public static string? ReadLine()
        {
            return Console.ReadLine();
        }

        public static void Msg(string prompt)
        {
            WriteLine($" {prompt}");
        }

        public static void MsgForWindow(string message, string cutoff = "", string trail = "", char filler = ' ', int maxLength = maxStringLength)
        {
            ActionLogger.AddAction(() => StorableMsgForWindow(message, cutoff, trail, filler, maxLength));

            StorableMsgForWindow(message, cutoff, trail, filler, maxLength);
        }

        public static void StorableMsgForWindow(string message, string cutoff = "", string trail = "", char filler = ' ', int maxLength = maxStringLength)
        {
            string finalMessage = message;

            if (maxLength == -1)
            {
                maxLength = finalMessage.Length + trail.Length;
            }

            maxLength = Math.Min(maxLength, Console.WindowWidth - 1); // Formats the final string to either the window width or the user defined maxLength.
            int fillerCount = maxLength - finalMessage.Length - trail.Length;

            if (fillerCount > 0)
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

        public static void Error(string errorMsg)
        {
            Msg($"Error: {errorMsg}");
        }

        public static void Info(string infoMsg)
        {
            Msg($"<{infoMsg}>");
        }

        public static void Prompt(params string[] prompt)
        {
            if (prompt.Length != 0)
            {
                foreach (string line in prompt)
                {
                    Msg($"{line}");
                }
            }
            Write(" > ");
        }

        public static string PromptForNotNullInput(params string[] prompt)
        {
            string? userInput = null;

            Prompt(prompt);
            while ((userInput = ReadLine()) == null)
            {
                Clear();
                Error("Input was null, try again!");
                Prompt(prompt);
            }

            Clear();
            return userInput;
        }

        public static string PromptForNotEmptyInput(params string[] prompt)
        {
            string? userInput = null;

            Prompt(prompt);
            while (string.IsNullOrEmpty(userInput = ReadLine()))
            {
                Clear();
                Error("Input was empty, try again!");
                Prompt(prompt);
            }

            Clear();
            return userInput;
        }

        public static bool PromptForTrueFalseInput(params string[] prompt)
        {
            string? userInput = null;

            while (true)
            {
                userInput = PromptForNotEmptyInput(prompt);
                userInput = userInput.ToLower();

                switch (userInput)
                {
                    case string s when s == "true" || s == "y" || s == "yes":
                        Clear();
                        return true;
                    case string s when s == "false" || s == "n" || s == "no":
                        Clear();
                        return false;
                    default:
                        Error("Input was not valid, try again!");
                        break;
                }
            }
        }

        public static int PromptForIntInput(params string[] prompt)
        {
            string? userInput = null;
            int output;

            Prompt(prompt);
            while (string.IsNullOrEmpty(userInput = ReadLine()) || !int.TryParse(userInput, out output))
            {
                Clear();
                Error("Input was not valid, try again!");
                Prompt(prompt);
            }

            Clear();
            return output;
        }
    }
}
