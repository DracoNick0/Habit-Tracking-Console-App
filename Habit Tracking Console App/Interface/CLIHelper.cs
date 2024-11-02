namespace Habit_Tracking_Console_App.Interface
{
    class CLIHelper
    {
        public static void Msg(string prompt)
        {
            Console.WriteLine($" {prompt}");
        }

        public static void MsgForWindow(string message, string cutoff = "", string trail = "", int maxLength = -1, char filler = ' ')
        {
            if (maxLength == -1)
            {
                maxLength = message.Length + trail.Length;
            }

            maxLength = Math.Min(maxLength, Console.WindowWidth - 1); // Formats the final string to either the window width or the user defined maxLength.
            int fillerCount = maxLength - message.Length - trail.Length;

            if (fillerCount > 0)
            {
                string fillerStr = new string(filler, fillerCount); // Makes string that will fill the empty space.
                message = message + fillerStr + trail;
            }
            else if (fillerCount < 0)
            {
                message += trail;
                message = message.Substring(0, maxLength - cutoff.Length); // Make space for cutoff
                message += cutoff;
            }

            CLIHelper.Msg($"{message}");
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
            Console.Write(" > ");
        }

        public static string PromptForNotNullInput(params string[] prompt)
        {
            string? userInput = null;

            Prompt(prompt);
            while ((userInput = Console.ReadLine()) == null)
            {
                Console.Clear();
                Error("Input was null, try again!");
                Prompt(prompt);
            }

            Console.Clear();
            return userInput;
        }

        public static string PromptForNotEmptyInput(params string[] prompt)
        {
            string? userInput = null;

            Prompt(prompt);
            while (string.IsNullOrEmpty(userInput = Console.ReadLine()))
            {
                Console.Clear();
                Error("Input was empty, try again!");
                Prompt(prompt);
            }

            Console.Clear();
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
                        Console.Clear();
                        return true;
                    case string s when s == "false" || s == "n" || s == "no":
                        Console.Clear();
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
            while (string.IsNullOrEmpty(userInput = Console.ReadLine()) || !int.TryParse(userInput, out output))
            {
                Console.Clear();
                Error("Input was not valid, try again!");
                Prompt(prompt);
            }

            Console.Clear();
            return output;
        }
    }
}
