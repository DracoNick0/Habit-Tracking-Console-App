using Habit_Tracking_Console_App.Backend.Objects;
using Habit_Tracking_Console_App.Frontend.PrintHelpers;
using Habit_Tracking_Console_App.Backend.Storage;

namespace Habit_Tracking_Console_App.Backend.Logic.Commander
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
            string name = habitInterface.PromptForName();
            string description = habitInterface.PromptForDescription();
            bool isGood = habitInterface.PromptForIsGood();
            int importance = habitInterface.PromptForImportance();
            string recurrenceAsString = this.habitInterface.PromptForRecurrence();
            int occurrence = habitInterface.PromptForOccurrence();

            // Prompt user to correct any mistakes in habit details.
            this.habitInterface.PromptForHabitCorrection(ref name, ref importance, ref isGood, ref description, ref recurrenceAsString, ref occurrence);

            RecurrenceEnum recurrence = this.StringToRecurrence(recurrenceAsString);

            // Create habit.
            return dynamicStorage.CreateHabit(name, importance, isGood, description, recurrence, occurrence);
        }

        private RecurrenceEnum StringToRecurrence(string recurrenceAsString)
        {
            return (RecurrenceEnum)Enum.Parse(typeof(RecurrenceEnum), recurrenceAsString);
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
                habitInterface.DisplayAllHabits(dynamicStorage.getHabits());

                userInput = CLIHelper.PromptForNotEmptyInput("Enter the habit name: ");

                if (habitNames.Contains(userInput))
                {
                    return dynamicStorage.RemoveHabit(habitInterface.PromptForHabit());
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
                habitInterface.DisplayAllHabits(dynamicStorage.getHabits());

                userInput = CLIHelper.PromptForNotEmptyInput("Enter the habit name: ");
                if (dynamicStorage.HabitExists(userInput))
                {
                    habit = dynamicStorage.GetHabitObject(userInput);
                }
            }

            string name = habit.Name;
            int importance = habit.Importance;
            bool isGood = habit.IsGood;
            string description = habit.Description;
            RecurrenceEnum recurrence = habit.Recurrence;
            string recurrenceAsString = recurrence.ToString();

            int occurrences = habit.Occurrence;

            habitInterface.PromptForHabitCorrection(ref name, ref importance, ref isGood, ref description, ref recurrenceAsString, ref occurrences);

            recurrence = StringToRecurrence(recurrenceAsString);

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

            while (!dynamicStorage.DoHabit(userInput))
            {
                habitInterface.DisplayAllHabits(dynamicStorage.getHabits());
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

            while (!dynamicStorage.UndoHabit(userInput))
            {
                habitInterface.DisplayAllHabits(dynamicStorage.getHabits());
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
