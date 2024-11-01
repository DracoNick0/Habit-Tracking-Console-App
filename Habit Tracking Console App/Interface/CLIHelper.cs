namespace Habit_Tracking_Console_App.Interface
{
    class CLIHelper
    {
        public static void Message(string prompt)
        {
            Console.WriteLine($" {prompt}");
        }

        public static void Error(string errorMsg)
        {
            Message($"Error: {errorMsg}");
        }

        public static void Info(string infoMsg)
        {
            Message($"<{infoMsg}>");
        }

        public static void Prompt(params string[] prompt)
        {
            if (prompt.Length != 0)
            {
                for (int i = 0; i < prompt.Length; i++)
                {
                    Message($"{prompt[i]}");
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
