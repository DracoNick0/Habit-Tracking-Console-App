using System.Runtime.CompilerServices;

namespace Habit_Tracking_Console_App.Backend
{
    class CommandHandler
    {
        HabitManager habitManager;
        TaskManager taskManager;

        public CommandHandler()
        {
            habitManager = new HabitManager();
            taskManager = new TaskManager();
        }

        public bool ExecuteCommand(string? userInput)
        {
            if (userInput != null)
            {
                // Change user input to be digestable by the program
                userInput = userInput.ToLower();
                string[] inputArgs = userInput.Split(' ');

                Console.Clear();
                switch (inputArgs[0])
                {
                    case "help":
                        HelpCommand();
                        break;
                    case "create":
                        switch (inputArgs[1])
                        {
                            case "habit":
                                habitManager.CreateHabit();
                                break;
                            case "task":
                                taskManager.AddTask();
                                break;
                            default:
                                InvalidArguument(userInput, inputArgs, 1);
                                break;
                        }
                        break;
                    case "exit":
                        ExitCommand();
                        return false;
                    default:
                        InvalidCommand(userInput);
                        break;
                }
            }

            return true;
        }

        private void HelpCommand()
        {
            Console.WriteLine(" Commands:");
            Console.WriteLine("  - help: displays a list of commands for the user to input");
            Console.WriteLine("  - exit: exit the program");
        }

        private void ExitCommand()
        {
            Console.WriteLine(" <See you again!>");
        }

        private void InvalidCommand(string command)
        {
            Console.Error.WriteLine(" <The command \"" + command + "\" is not valid, try again!>");
        }

        private void InvalidArguument(string command, string[] inputArgs, int index)
        {
            Console.Error.WriteLine(" <The argument \"" + inputArgs[index] + "\" in \"" + command + "\" is not valid, try again!>");
        }
    }
}
