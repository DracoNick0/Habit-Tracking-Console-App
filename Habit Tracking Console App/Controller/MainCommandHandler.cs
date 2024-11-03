using Habit_Tracking_Console_App.Model;
using Habit_Tracking_Console_App.Model.Storage;
using Habit_Tracking_Console_App.View;

namespace Habit_Tracking_Console_App.ViewModel
{
    /// <summary>
    /// Handles the main commands input by the user.
    /// </summary>
    class MainCommandHandler
    {
        private DynamicStorageManager dynamicStorage;
        private HabitInterface habitInterface;

        public MainCommandHandler()
        {
            this.dynamicStorage = new DynamicStorageManager();
            this.habitInterface = new HabitInterface();
        }

        public void Run()
        {
            string userInput;

            do
            {
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
                        case "show":
                            this.ShowInvoked(command, inputArgs);
                            break;
                        case "edit":
                            this.EditCommand(command, inputArgs);
                            break;
                        default:
                            this.InvalidCommand(command);
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
                        this.PromptAndCreateHabit();
                        break;
                    default:
                        this.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        private bool PromptAndCreateHabit()
        {
            CLIHelper.Info("You can make changes to the habit after answering the following prompts.");

            // Get new habit details.
            string name = this.habitInterface.PromptForName();
            string description = this.habitInterface.PromptForDescription();
            bool isGood = this.habitInterface.PromptForIsGood();
            int importance = this.habitInterface.PromptForImportance();

            this.habitInterface.PromptForHabitCorrection(ref name, ref importance, ref isGood, ref description);

            return this.dynamicStorage.CreateHabit(name, importance, isGood, description);
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
                        List<string> habitNames = dynamicStorage.getHabits().Select(habit => habit.Name).ToList();
                        string userInput;

                        while (true)
                        {
                            this.habitInterface.DisplayAllHabits(dynamicStorage.getHabits());

                            userInput = CLIHelper.PromptForNotEmptyInput("Enter the habit name: ");

                            if (habitNames.Contains(userInput))
                            {
                                this.dynamicStorage.RemoveHabit(this.habitInterface.PromptForHabit());
                                break;
                            }
                        }

                        break;
                    default:
                        this.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Calls functions that are under the category of 'show'.
        /// </summary>
        /// <param name="command">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void ShowInvoked(string command, params string[] inputArgs)
        {
            if (inputArgs.Length > 1)
            {
                switch (inputArgs[1])
                {
                    case "habit":
                        this.habitInterface.DisplayAllHabits(this.dynamicStorage.getHabits());
                        break;
                    default:
                        this.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Calls functions that are under the category of 'edit'.
        /// </summary>
        /// <param name="command">The user input.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void EditCommand(string command, params string[] inputArgs)
        {
            if (inputArgs.Length > 1)
            {
                switch (inputArgs[1])
                {
                    case "habit":
                        List<string> habitNames = dynamicStorage.getHabits().Select(habit => habit.Name).ToList();
                        HabitObject? habit;
                        string userInput;

                        while (true)
                        {
                            this.habitInterface.DisplayAllHabits(dynamicStorage.getHabits());

                            userInput = CLIHelper.PromptForNotEmptyInput("Enter the habit name: ");

                            if ((habit = this.dynamicStorage.GetHabitObject(this.habitInterface.PromptForHabit())) != null)
                            {
                                break;
                            }
                        }

                        string name = habit.Name;
                        int importance = habit.Importance;
                        bool isGood = habit.IsGood;
                        string description = habit.Description;

                        this.habitInterface.PromptForHabitCorrection(ref name, ref importance, ref isGood, ref description);

                        habit.Edit(name, importance, isGood, description);
                        break;
                    default:
                        this.InvalidArgument(command, inputArgs, 1);
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

        /// <summary>
        /// Prints an error notification regarding an invalid command.
        /// Text should be given and executed in View.
        /// </summary>
        /// <param name="command">The user inputted command.</param>
        private void InvalidCommand(string command)
        {
            CLIHelper.Error($"The command \"{command}\" is not valid, try again!");
        }

        /// <summary>
        /// Prints an error notification regarding an invalid argument.
        /// Text should be given and executed in View.
        /// </summary>
        /// <param name="command">The user inputted command.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        private void InvalidArgument(string command, string[] inputArgs, int index)
        {
            CLIHelper.Error($"The argument \"{inputArgs[index]}\" in \"{command}\" is not valid, try again!");
        }
    }
}
