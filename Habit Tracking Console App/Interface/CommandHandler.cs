using Habit_Tracking_Console_App.Backend;

namespace Habit_Tracking_Console_App.Interface
{
    class CommandHandler
    {
        HabitInterface habitInterface;

        public CommandHandler()
        {
            habitInterface = new HabitInterface();
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
                                habitInterface.PromptForHabitCreation();
                                break;
                            default:
                                InvalidArgument(userInput, inputArgs, 1);
                                break;
                        }
                        break;
                    case "show":
                        switch (inputArgs[1])
                        {
                            case "habit":
                                habitInterface.DisplayAllHabits();
                                break;
                            default:
                                InvalidArgument(userInput, inputArgs, 1);
                                break;
                        }
                        break;

                    case "edit":
                        switch (inputArgs[1])
                        {
                            case "habit":
                                habitInterface.PromptForHabitEdit();
                                break;
                            default:
                                InvalidArgument(userInput, inputArgs, 1);
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
            CLIHelper.Msg("Available Commands:");
            Console.WriteLine(new string('-', Console.WindowWidth - 1));
            CLIHelper.Msg("- help: displays a list of commands for the user to input");
            CLIHelper.Msg("- exit: exit the program");
            CLIHelper.Msg("- create <item>: creates an item(eg. habit, task)");
            CLIHelper.Msg("- show <item>: displays all items in category(eg. habit, task)");
        }

        private void ExitCommand()
        {
            CLIHelper.Msg("See you again!");
        }

        private void InvalidCommand(string command)
        {
            CLIHelper.Error($"The command \"{command}\" is not valid, try again!");
        }

        private void InvalidArgument(string command, string[] inputArgs, int index)
        {
            CLIHelper.Error($"The argument \"{inputArgs[index]}\" in \"{command}\" is not valid, try again!");
        }
    }
}
