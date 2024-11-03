﻿using Habit_Tracking_Console_App.Model;
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
        private Action topText;

        public MainCommandHandler()
        {
            this.dynamicStorage = new DynamicStorageManager();
            this.habitInterface = new HabitInterface();
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

        /// <summary>
        /// Prompts the user for habit details, then creates the habit.
        /// </summary>
        /// <returns></returns>
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
                        this.PromptAndDeleteHabit();
                        break;
                    default:
                        this.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Prompts the user for a habit, then deletes the habit.
        /// </summary>
        /// <returns>If habit was successfully deleted.</returns>
        private bool PromptAndDeleteHabit()
        {
            List<string> habitNames = dynamicStorage.getHabits().Select(habit => habit.Name).ToList();
            string userInput;

            while (true)
            {
                this.habitInterface.DisplayAllHabits(dynamicStorage.getHabits());

                userInput = CLIHelper.PromptForNotEmptyInput("Enter the habit name: ");

                if (habitNames.Contains(userInput))
                {
                    return this.dynamicStorage.RemoveHabit(this.habitInterface.PromptForHabit());
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
        private void EditInvoked(string command, params string[] inputArgs)
        {
            if (inputArgs.Length > 1)
            {
                switch (inputArgs[1])
                {
                    case "habit":
                        this.PromptAndEditHabit();
                        break;
                    default:
                        this.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Prompts the user for a habit to edit, then prompts for changes, then edits the habit.
        /// </summary>
        private void PromptAndEditHabit()
        {
            List<string> habitNames = dynamicStorage.getHabits().Select(habit => habit.Name).ToList();
            HabitObject? habit = null;
            string userInput;

            while (habit == null)
            {
                this.habitInterface.DisplayAllHabits(dynamicStorage.getHabits());

                userInput = CLIHelper.PromptForNotEmptyInput("Enter the habit name: ");
                if (this.dynamicStorage.HabitExists(userInput))
                {
                    habit = this.dynamicStorage.GetHabitObject(userInput);
                }
            }

            string name = habit.Name;
            int importance = habit.Importance;
            bool isGood = habit.IsGood;
            string description = habit.Description;

            this.habitInterface.PromptForHabitCorrection(ref name, ref importance, ref isGood, ref description);

            habit.Edit(name, importance, isGood, description);
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
                        this.PromptAndDoHabit();
                        break;
                    default:
                        this.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        private bool PromptAndDoHabit()
        {
            List<string> habitNames = dynamicStorage.getHabits().Select(habit => habit.Name).ToList();
            string userInput = "";

            while (!this.dynamicStorage.DoHabit(userInput))
            {
                this.habitInterface.DisplayAllHabits(dynamicStorage.getHabits());
                userInput = CLIHelper.PromptForNotEmptyInput("Enter the habit name: ");
            }

            return false;
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
                        this.PromptAndUndoHabit();
                        break;
                    default:
                        this.InvalidArgument(command, inputArgs, 1);
                        break;
                }
            }
        }

        private bool PromptAndUndoHabit()
        {
            List<string> habitNames = dynamicStorage.getHabits().Select(habit => habit.Name).ToList();
            string userInput = "";

            while (!this.dynamicStorage.UndoHabit(userInput))
            {
                this.habitInterface.DisplayAllHabits(dynamicStorage.getHabits());
                userInput = CLIHelper.PromptForNotEmptyInput("Enter the habit name: ");
            }

            return false;
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
