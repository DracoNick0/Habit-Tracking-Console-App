﻿using Habit_Tracking_Console_App.Storage;
using Habit_Tracking_Console_App.PrintHelpers;
using Habit_Tracking_Console_App.Objects;

namespace Habit_Tracking_Console_App.Commander
{
    class CommandExecutor
    {
        private DynamicStorageManager dynamicStorage;
        private HabitInterface habitInterface;

        public CommandExecutor(DynamicStorageManager dynamicStorage, HabitInterface habitInterface)
        {
            this.dynamicStorage = dynamicStorage;
            this.habitInterface = habitInterface;
        }

        /// <summary>
        /// Prompts the user for habit details, then creates the habit.
        /// </summary>
        /// <returns></returns>
        public bool PromptAndCreateHabit()
        {
            CLIHelper.Info("You can make changes to the habit after answering the following prompts.");

            // Get new habit details.
            string name = this.habitInterface.PromptForName();
            string description = this.habitInterface.PromptForDescription();
            bool isGood = this.habitInterface.PromptForIsGood();
            int importance = this.habitInterface.PromptForImportance();

            RecurrenceEnum recurrence = this.habitInterface.PromptForRecurrence();
            int occurrence = this.habitInterface.PromptForOccurrence();

            this.habitInterface.PromptForHabitCorrection(ref name, ref importance, ref isGood, ref description, ref recurrence, ref occurrence);

            return this.dynamicStorage.CreateHabit(name, importance, isGood, description, recurrence, occurrence);
        }

        /// <summary>
        /// Prompts the user for a habit, then deletes the habit.
        /// </summary>
        /// <returns>If habit was successfully deleted.</returns>
        public bool PromptAndDeleteHabit()
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
        /// Prompts the user for a habit to edit, then prompts for changes, then edits the habit.
        /// </summary>
        public void PromptAndEditHabit()
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
            RecurrenceEnum recurrence = habit.Recurrence;
            int occurrences = habit.Occurrence;

            this.habitInterface.PromptForHabitCorrection(ref name, ref importance, ref isGood, ref description, ref recurrence, ref occurrences);

            habit.Edit(name, importance, isGood, description, recurrence, occurrences);
        }

        /// <summary>
        /// Prompts the user for a habit to mark as complete, then marks the habit as complete.
        /// </summary>
        /// <returns>If habit was marked successfully.</returns>
        public bool PromptAndDoHabit()
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
        /// Prompts the user for a habit to mark as incomplete, then marks the habit as incomplete.
        /// </summary>
        /// <returns>If habit was marked successfully.</returns>
        public bool PromptAndUndoHabit()
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
        /// Prints an error notification regarding an invalid command.
        /// Text should be given and executed in View.
        /// </summary>
        /// <param name="command">The user inputted command.</param>
        public void InvalidCommand(string command)
        {
            CLIHelper.Error($"The command \"{command}\" is not valid, try again!");
        }

        /// <summary>
        /// Prints an error notification regarding an invalid argument.
        /// Text should be given and executed in View.
        /// </summary>
        /// <param name="command">The user inputted command.</param>
        /// <param name="inputArgs">User input split by the char ' '.</param>
        public void InvalidArgument(string command, string[] inputArgs, int index)
        {
            CLIHelper.Error($"The argument \"{inputArgs[index]}\" in \"{command}\" is not valid, try again!");
        }
    }
}
