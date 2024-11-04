using Task_Tracking_Console_App.Backend.Storage;
using Habit_Tracking_Console_App.Frontend;
using Habit_Tracking_Console_App.Backend.Logic.Object_Manager;
using Habit_Tracking_Console_App.Backend.Logic;

namespace Task_Tracking_Console_App.Backend.Logic.Commander
{
    /// <summary>
    /// Handles the main commands input by the user.
    /// </summary>
    class CommandHandler
    {
        private DynamicStorageManager dynamicStorage;
        private TaskManager taskManager;
        private Action topText;

        public CommandHandler(DynamicStorageManager dynamicStorageManger)
        {
            this.dynamicStorage = dynamicStorageManger;
            this.taskManager = new TaskManager(this.dynamicStorage);
            this.topText = () => Console.Write("");
        }

        public void Run()
        {
            string userInput;

            do
            {
                this.topText.Invoke();
                userInput = IO.PromptForNotEmptyInput("Enter \"help\" to print a list of commands.");
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

                IO.Clear();
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
                            InvalidCommand(command);
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
            IO.Msg("Available Commands:");
            IO.MsgForWindow("", "", "", '-', int.MaxValue);
            IO.Msg("- exit: saves and exits the program");
            IO.Msg("- show <item>: displays all items in category(eg. task, habit)");
            IO.Msg("- create <item>: creates an item(eg. task, habit)");
            IO.Msg("- delete <item>: deletes an item(eg. task, habit)");
            IO.Msg("- edit <item>: creates an item(eg. task, habit)");
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
                        this.taskManager.PromptAndCreateTask();
                        break;
                    default:
                        InvalidArgument(command, inputArgs, 1);
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
                        this.taskManager.PromptAndDeleteTask();
                        break;
                    default:
                        InvalidArgument(command, inputArgs, 1);
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
                        topText = () => TaskIO.DisplayAllTasks(this.dynamicStorage.getTasks());
                        break;
                    default:
                        InvalidArgument(command, inputArgs, 1);
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
                        this.taskManager.PromptAndEditTask();
                        break;
                    default:
                        InvalidArgument(command, inputArgs, 1);
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
                        this.taskManager.PromptAndDoTask();
                        break;
                    default:
                        InvalidArgument(command, inputArgs, 1);
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
                        this.taskManager.PromptAndUndoTask();
                        break;
                    default:
                        InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Saves all tasks and closes the program.
        /// </summary>
        private void ExitInvoked()
        {
            this.dynamicStorage.Save();
        }

        /// <summary>
        /// Prints an error notification regarding an invalid command.
        /// Text should be given and executed in View.
        /// </summary>
        /// <param name="command">The user inputted command.</param>
        public void InvalidCommand(string command)
        {
            IO.Error($"The command \"{command}\" is not valid, try again!");
        }

        /// <summary>
        /// Prints an error notification regarding an invalid argument.
        /// Text should be given and executed in View.
        /// </summary>
        /// <param name="command">The user inputted command.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        public void InvalidArgument(string command, string[] inputArgs, int index)
        {
            IO.Error($"The argument \"{inputArgs[index]}\" in \"{command}\" is not valid, try again!");
        }
    }
}
