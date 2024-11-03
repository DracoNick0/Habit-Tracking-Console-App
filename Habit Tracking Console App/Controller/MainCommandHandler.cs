using Habit_Tracking_Console_App.View;

namespace Habit_Tracking_Console_App.ViewModel
{
    /// <summary>
    /// Handles the main commands input by the user.
    /// </summary>
    class MainCommandHandler
    {
        HabitInterface habitInterface;

        public MainCommandHandler()
        {
            this.habitInterface = new HabitInterface();
        }

        public void Main()
        {
            string? userInput = string.Empty;

            do
            {
                CLIHelper.Info("Enter \"help\" to print a list of commands.");
                CLIHelper.Prompt();

                // Wait for user input
                userInput = CLIHelper.ReadLine();

                // Execute the users input
            } while (ExecuteCommand(userInput));
        }

        /// <summary>
        /// Executes the functions associated with the command imputed.
        /// </summary>
        /// <param name="userInput">The command.</param>
        /// <returns>False to close program, true to keep running.</returns>
        private bool ExecuteCommand(string? userInput)
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

        /// <summary>
        /// Displays a list of commands the user can enter.
        /// Text should be given and executed in View. ***************************************************************************************************
        /// </summary>
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

        /// <summary>
        /// Calls functions that are under the category of 'create'.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
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

        /// <summary>
        /// Calls functions that are under the category of 'delete'.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void DeleteCommand(string userInput, params string[] inputArgs)
        {
            switch (inputArgs[1])
            {
                case "habit":
                    this.habitInterface.PromptForHabitDelete();
                    break;
                default:
                    InvalidArgument(userInput, inputArgs, 1);
                    break;
            }
        }

        /// <summary>
        /// Calls functions that are under the category of 'view'.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void ViewCommand(string userInput, params string[] inputArgs)
        {
            switch (inputArgs[1])
            {
                case "habit":
                    this.habitInterface.PromptForHabitView();
                    break;
                default:
                    InvalidArgument(userInput, inputArgs, 1);
                    break;
            }
        }

        /// <summary>
        /// Calls functions that are under the category of 'show'.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void ShowCommand(string userInput, params string[] inputArgs)
        {
            switch (inputArgs[1])
            {
                case "habit":
                    this.habitInterface.DisplayAllHabits();
                    break;
                default:
                    InvalidArgument(userInput, inputArgs, 1);
                    break;
            }
        }

        /// <summary>
        /// Calls functions that are under the category of 'edit'.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
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

        /// <summary>
        /// Saves all habits and closes the program.
        /// </summary>
        private void ExitCommand()
        {
            this.habitInterface.SaveHabits();
        }

        /// <summary>
        /// Prints an error notification regarding an invalid command.
        /// Text should be given and executed in View. ***************************************************************************************************
        /// </summary>
        /// <param name="command">The user inputted command.</param>
        private void InvalidCommand(string command)
        {
            CLIHelper.Error($"The command \"{command}\" is not valid, try again!");
        }

        /// <summary>
        /// Prints an error notification regarding an invalid argument.
        /// Text should be given and executed in View. ***************************************************************************************************
        /// </summary>
        /// <param name="command">The user inputted command.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void InvalidArgument(string command, string[] inputArgs, int index)
        {
            CLIHelper.Error($"The argument \"{inputArgs[index]}\" in \"{command}\" is not valid, try again!");
        }
    }
}
