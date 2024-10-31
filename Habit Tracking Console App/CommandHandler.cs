namespace Habit_Tracking_Console_App
{
    class CommandHandler
    {
        public CommandHandler() { }

        public static bool ExecuteCommand(string? userInput) {
            if (userInput != null)
            {
                // Change user input to be digestable by the program
                userInput = userInput.ToLower();

                Console.Clear();
                switch (userInput)
                {
                    case "help":
                        Console.WriteLine("Commands:");
                        Console.WriteLine(" - help: displays a list of commands for the user to input");
                        Console.WriteLine(" - exit: exit the program");
                        break;
                    case "exit":
                        Console.WriteLine("See you again!");
                        return false;
                    default:
                        Console.Error.WriteLine("The command \"" + userInput + "\" is not valid, try again!");
                        break;
                }
            }

            return true;
        }
    }
}
