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

                CLIHelper.Clear();
                switch (inputArgs[0])
                {
                    case "help":
                        HelpCommand();
                        break;
                    case "create":
                        CreateCommand(userInput, inputArgs);
                        break;
                    case "show":
                        ShowCommand(userInput, inputArgs);
                        break;
                    case "edit":
                        EditCommand(userInput, inputArgs);
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
            CLIHelper.WriteLine(new string('-', Console.WindowWidth - 1));
            CLIHelper.Msg("- help: displays a list of commands for the user to input");
            CLIHelper.Msg("- exit: exit the program");
            CLIHelper.Msg("- show <item>: displays all items in category(eg. habit, task)");
            CLIHelper.Msg("- create <item>: creates an item(eg. habit, task)");
            CLIHelper.Msg("- edit <item>: creates an item(eg. habit, task)");
        }

        private void CreateCommand(string userInput, params string[] inputArgs)
        {
            switch (inputArgs[1])
            {
                case "habit":
                    this.habitInterface.PromptForHabitCreation();
                    break;
                default:
                    InvalidArgument(userInput, inputArgs, 1);
                    break;
            }
        }

        private void ShowCommand(string userInput, params string[] inputArgs)
        {
            switch (inputArgs[1])
            {
                case "habit":
                    this.habitInterface.DisplayAllHabits(true, true, true, true);
                    break;
                default:
                    InvalidArgument(userInput, inputArgs, 1);
                    break;
            }
        }

        private void EditCommand(string userInput, params string[] inputArgs)
        {
            switch (inputArgs[1])
            {
                case "habit":
                    this.habitInterface.PromptForHabitEdit();
                    break;
                default:
                    InvalidArgument(userInput, inputArgs, 1);
                    break;
            }
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
