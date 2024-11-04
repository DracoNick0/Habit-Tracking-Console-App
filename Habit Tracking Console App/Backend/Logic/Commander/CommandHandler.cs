using Task_Tracking_Console_App.Frontend.PrintHelpers;
using Task_Tracking_Console_App.Backend.Storage;
using Habit_Tracking_Console_App.Frontend.PrintHelpers;

namespace Task_Tracking_Console_App.Backend.Logic.Commander
{
    /// <summary>
    /// Handles the main commands input by the user.
    /// </summary>
    class CommandHandler
    {
        private DynamicStorageManager dynamicStorage;
        private TaskInterface taskInterface;
        private CommandExecutor commands;
        private Action topText;

        public CommandHandler(DynamicStorageManager dynamicStorageManger)
        {
            dynamicStorage = dynamicStorageManger;
            taskInterface = new TaskInterface();
            commands = new CommandExecutor(dynamicStorage, taskInterface);
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
        /// </summary>
        private void HelpInvoked()
        {
            CLIHelper.Msg("Available Commands:");
            CLIHelper.MsgForWindow("", "", "", '-', int.MaxValue);
            CLIHelper.Msg("- exit: saves and exits the program");
            CLIHelper.Msg("- show <item>: displays all items in category(eg. task, habit)");
            CLIHelper.Msg("- create <item>: creates an item(eg. task, habit)");
            CLIHelper.Msg("- delete <item>: deletes an item(eg. task, habit)");
            CLIHelper.Msg("- edit <item>: creates an item(eg. task, habit)");
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
                    case "task":
                        commands.PromptAndCreateTask();
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
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void DeleteInvoked(string command, params string[] inputArgs)
        {
            if (inputArgs.Length > 1)
            {
                switch (inputArgs[1])
                {
                    case "task":
                        commands.PromptAndDeleteTask();
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
                    case "task":
                    case "tasks":
                        topText = () => taskInterface.DisplayAllTasks(dynamicStorage.getTasks());
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
                    case "task":
                        commands.PromptAndEditTask();
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
                    case "task":
                        commands.PromptAndDoTask();
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
                    case "task":
                        commands.PromptAndUndoTask();
                        break;
                    default:
                        commands.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Saves all tasks and closes the program.
        /// </summary>
        private void ExitInvoked()
        {
            dynamicStorage.Save();
        }
    }
}
