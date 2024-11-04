using Habit_Tracking_Console_App.Frontend.PrintHelpers;
using Habit_Tracking_Console_App.Backend.Storage;

namespace Habit_Tracking_Console_App.Backend.Logic.Commander
{
    /// <summary>
    /// Handles the main commands input by the user.
    /// </summary>
    class CommandHandler
    {
        private DynamicStorageManager dynamicStorage;
        private HabitInterface habitInterface;
        private CommandExecutor commands;
        private Action topText;

        public CommandHandler(DynamicStorageManager dynamicStorageManger)
        {
            dynamicStorage = dynamicStorageManger;
            habitInterface = new HabitInterface();
            commands = new CommandExecutor(dynamicStorage, habitInterface);
            topText = () => Console.Write("");
        }

        public void Run()
        {
            string userInput;

            do
            {
                topText.Invoke();
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
                            HelpInvoked();
                            break;
                        case "exit":
                            ExitInvoked();
                            return false;
                        // Following cases require 2 arguments.
                        case "create":
                            CreateInvoked(command, inputArgs);
                            break;
                        case "delete":
                            DeleteInvoked(command, inputArgs);
                            break;
                        case "view":
                            ViewInvoked(command, inputArgs);
                            break;
                        case "edit":
                            EditInvoked(command, inputArgs);
                            break;
                        case "do":
                            DoInvoked(command, inputArgs);
                            break;
                        case "undo":
                            UndoInvoked(command, inputArgs);
                            break;
                        default:
                            commands.InvalidCommand(command);
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
                        commands.PromptAndCreateHabit();
                        break;
                    default:
                        commands.InvalidArgument(command, inputArgs, 1);
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
                        commands.PromptAndDeleteHabit();
                        break;
                    default:
                        commands.InvalidArgument(command, inputArgs, 1);
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
                    case "habits":
                        topText = () => habitInterface.DisplayAllHabits(dynamicStorage.getHabits());
                        break;
                    default:
                        commands.InvalidArgument(command, inputArgs, 1);
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
                        commands.PromptAndEditHabit();
                        break;
                    default:
                        commands.InvalidArgument(command, inputArgs, 1);
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
                        commands.PromptAndDoHabit();
                        break;
                    default:
                        commands.InvalidArgument(command, inputArgs, 1);
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
                        commands.PromptAndUndoHabit();
                        break;
                    default:
                        commands.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Saves all habits and closes the program.
        /// </summary>
        private void ExitInvoked()
        {
            dynamicStorage.Save();
        }
    }
}
