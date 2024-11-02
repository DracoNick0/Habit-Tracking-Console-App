using Habit_Tracking_Console_App.View;

namespace Habit_Tracking_Console_App.ViewModel
{
    class MainCommandHandler
    {
        HabitInterface habitInterface;

        public MainCommandHandler()
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
                    case "delete":
                        DeleteCommand(userInput, inputArgs);
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
            CLIHelper.MsgForWindow("", "", "", '-', int.MaxValue);
            CLIHelper.Msg("- help: displays a list of commands for the user to input");
            CLIHelper.Msg("- exit: saves and exits the program");
            CLIHelper.Msg("- show <item>: displays all items in category(eg. habit, task)");
            CLIHelper.Msg("- create <item>: creates an item(eg. habit, task)");
            CLIHelper.Msg("- delete <item>: deletes an item(eg. habit, task)");
            CLIHelper.Msg("- edit <item>: creates an item(eg. habit, task)");
        }

        private void CreateCommand(string userInput, params string[] inputArgs)
        {
            switch (inputArgs[1])
            {
                case "habit":
                    habitInterface.PromptForHabitCreation();
                    break;
                default:
                    InvalidArgument(userInput, inputArgs, 1);
                    break;
            }
        }

        private void DeleteCommand(string userInput, params string[] inputArgs)
        {
            switch (inputArgs[1])
            {
                case "habit":
                    habitInterface.PromptForHabitDelete();
                    break;
                default:
                    InvalidArgument(userInput, inputArgs, 1);
                    break;
            }
        }

        private void ViewCommand(string userInput, params string[] inputArgs)
        {
            switch (inputArgs[1])
            {
                case "habit":
                    habitInterface.PromptForHabitView();
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
                    habitInterface.DisplayAllHabits();
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
                    habitInterface.PromptForHabitEdit();
                    break;
                default:
                    InvalidArgument(userInput, inputArgs, 1);
                    break;
            }
        }

        private void ExitCommand()
        {
            habitInterface.SaveHabits();
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
