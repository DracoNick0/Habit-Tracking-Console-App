using Habit_Tracking_Console_App.Storage;
using Habit_Tracking_Console_App.PrintHelpers;

namespace Habit_Tracking_Console_App.Commander
{
    /// <summary>
    /// Handles the main commands input by the user.
    /// </summary>
    class CommandHandler
    {
        private DynamicStorageManager dynamicStorage;
        private HabitInterface habitInterface;
        private Commands commands;
        private Action topText;

        public CommandHandler()
        {
            this.dynamicStorage = new DynamicStorageManager();
            this.habitInterface = new HabitInterface();
            this.commands = new Commands(this.dynamicStorage, this.habitInterface);
            this.topText = (() => Console.Write(""));
        }

        public void Run()
        {
            string userInput;

            do
            {
                this.topText.Invoke();
                userInput = CLIHelper.PromptForNotEmptyInput("Enter \"help\" to print a list of commands.");
            } while (ExecuteCommand(userInput));
        }

        /// <summary>
        /// Executes the functions associated with the command imputed.
        /// </summary>
        /// <param name="userInput">The command.</param>
        /// <returns>False to close program, true to keep running.</returns>
        private bool ExecuteCommand(string userInput)
        {
            if (userInput != null)
            {
                // Alter user input to be command ready.
                string command = userInput.ToLower();
                string[] inputArgs = command.Split(' ');

                CLIHelper.Clear();
                if (inputArgs.Length > 0)
                {
                    switch (inputArgs[0])
                    {
                        // Following cases require 1 argument.
                        case "help":
                            this.HelpInvoked();
                            break;
                        case "exit":
                            this.ExitInvoked();
                            return false;
                        // Following cases require 2 arguments.
                        case "create":
                            this.CreateInvoked(command, inputArgs);
                            break;
                        case "delete":
                            this.DeleteInvoked(command, inputArgs);
                            break;
                        case "view":
                            this.ViewInvoked(command, inputArgs);
                            break;
                        case "edit":
                            this.EditInvoked(command, inputArgs);
                            break;
                        case "do":
                            this.DoInvoked(command, inputArgs);
                            break;
                        case "undo":
                            this.UndoInvoked(command, inputArgs);
                            break;
                        default:
                            this.commands.InvalidCommand(command);
                            break;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Displays a list of commands the user can enter.
        /// Text should be given and executed in View. ***************************************************************************************************
        /// </summary>
        private void HelpInvoked()
        {
            CLIHelper.Msg("Available Commands:");
            CLIHelper.MsgForWindow("", "", "", '-', int.MaxValue);
            CLIHelper.Msg("- exit: saves and exits the program");
            CLIHelper.Msg("- show <item>: displays all items in category(eg. habit, task)");
            CLIHelper.Msg("- create <item>: creates an item(eg. habit, task)");
            CLIHelper.Msg("- delete <item>: deletes an item(eg. habit, task)");
            CLIHelper.Msg("- edit <item>: creates an item(eg. habit, task)");
        }

        /// <summary>
        /// Calls functions that are under the category of 'create'.
        /// </summary>
        /// <param name="command">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void CreateInvoked(string command, params string[] inputArgs)
        {
            if (inputArgs.Length > 1)
            {
                switch (inputArgs[1])
                {
                    case "habit":
                        this.commands.PromptAndCreateHabit();
                        break;
                    default:
                        this.commands.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Calls functions that are under the category of 'delete'.
        /// </summary>
        /// <param name="command">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param> ***************************************************************************
        private void DeleteInvoked(string command, params string[] inputArgs)
        {
            if (inputArgs.Length > 1)
            {
                switch (inputArgs[1])
                {
                    case "habit":
                        this.commands.PromptAndDeleteHabit();
                        break;
                    default:
                        this.commands.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Calls functions that are under the category of 'show'.
        /// </summary>
        /// <param name="command">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void ViewInvoked(string command, params string[] inputArgs)
        {
            if (inputArgs.Length > 1)
            {
                switch (inputArgs[1])
                {
                    case "habit":
                        this.topText = (() => this.habitInterface.DisplayAllHabits(this.dynamicStorage.getHabits()));
                        break;
                    default:
                        this.commands.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Calls functions that are under the category of 'edit'.
        /// </summary>
        /// <param name="command">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void EditInvoked(string command, params string[] inputArgs)
        {
            if (inputArgs.Length > 1)
            {
                switch (inputArgs[1])
                {
                    case "habit":
                        this.commands.PromptAndEditHabit();
                        break;
                    default:
                        this.commands.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Calls functions that are under the category of 'do'.
        /// </summary>
        /// <param name="command">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void DoInvoked(string command, params string[] inputArgs)
        {
            if (inputArgs.Length > 1)
            {
                switch (inputArgs[1])
                {
                    case "habit":
                        this.commands.PromptAndDoHabit();
                        break;
                    default:
                        this.commands.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Calls functions that are under the category of 'undo'.
        /// </summary>
        /// <param name="command">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void UndoInvoked(string command, params string[] inputArgs)
        {
            if (inputArgs.Length > 1)
            {
                switch (inputArgs[1])
                {
                    case "habit":
                        this.commands.PromptAndUndoHabit();
                        break;
                    default:
                        this.commands.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Saves all habits and closes the program.
        /// </summary>
        private void ExitInvoked()
        {
            this.dynamicStorage.Save();
        }
    }
}
