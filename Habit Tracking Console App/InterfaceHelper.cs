namespace Habit_Tracking_Console_App
{
    class InterfaceHelper
    {
        public static bool ConfirmInput(string input)
        {
            while (true)
            {
                string? userInput = string.Empty;
                Console.WriteLine("Is " + input + " correct? (y/n)");
                userInput = Console.ReadLine();

                if (userInput != null)
                {
                    userInput = userInput.ToLower();

                    switch (userInput)
                    {
                        case "y":
                            return true;
                        case "n":
                            return false;
                        default:
                            break;
                    }
                    return true;
                }
            }
        }

        public static string PromptForNotNullInput(string prompt)
        {
            string? userInput = null;

            Console.Write(prompt);
            while ((userInput = Console.ReadLine()) == null)
            {
                Console.WriteLine("Input was null, try again!");
                Console.Write(prompt);
            }

            return userInput;
        }

        public static string PromptForNotEmptyInput(string prompt)
        {
            string? userInput = null;

            Console.Write(prompt);
            while ((userInput = Console.ReadLine()) == string.Empty || userInput == null)
            {
                Console.Clear();
                Console.WriteLine("Input was empty, try again!");
                Console.Write(prompt);
            }

            return userInput;
        }

        public static bool PromptTrueFalseInput()
        {
            string? userInput = null;

            while (true)
            {
                Console.Clear();

                //switch
            }

        }

    }
}
