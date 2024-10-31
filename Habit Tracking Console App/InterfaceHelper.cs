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



    }
}
