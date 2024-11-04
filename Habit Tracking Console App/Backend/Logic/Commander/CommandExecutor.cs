using Task_Tracking_Console_App.Backend.Objects;
using Task_Tracking_Console_App.Backend.Storage;
using Habit_Tracking_Console_App.Frontend.PrintHelpers;
using Habit_Tracking_Console_App.Backend.Objects;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using Habit_Tracking_Console_App.Backend.Logic.Commander;

namespace Task_Tracking_Console_App.Backend.Logic.Commander
{
    class CommandExecutor
    {
        private DynamicStorageManager dynamicStorage;
        private TaskManager taskInterface;

        public CommandExecutor(DynamicStorageManager dynamicStorage, TaskManager taskInterface)
        {
            this.dynamicStorage = dynamicStorage;
            this.taskInterface = taskInterface;
        }

        /// <summary>
        /// Prompts the user for task details, then creates the task.
        /// </summary>
        /// <returns></returns>
        public bool PromptAndCreateTask()
        {
            CLIHelper.Info("You can make changes to the task after answering the following prompts.");

            // Get new task details.
            string name = taskInterface.PromptForName();
            string description = taskInterface.PromptForDescription();
            bool isGood = taskInterface.PromptForIsGood();
            int importance = taskInterface.PromptForImportance();

            DateTime dueDate = CLIHelper.PromptForDateInput();

            string recurrenceAsString = this.taskInterface.PromptForRecurrence();
            int occurrence = taskInterface.PromptForOccurrence();

            // Prompt user to correct any mistakes in task details.
            this.taskInterface.PromptForTaskCorrection(ref name, ref importance, ref isGood, ref description, ref recurrenceAsString, ref occurrence);

            RecurrenceEnum recurrence = this.StringToRecurrence(recurrenceAsString);

            // Create task.
            return dynamicStorage.CreateTask(name, importance, isGood, description, recurrence, occurrence, dueDate);
        }

        private RecurrenceEnum StringToRecurrence(string recurrenceAsString)
        {
            return (RecurrenceEnum)Enum.Parse(typeof(RecurrenceEnum), recurrenceAsString);
        }

        /// <summary>
        /// Prompts the user for a task, then deletes the task.
        /// </summary>
        /// <returns>If task was successfully deleted.</returns>
        public bool PromptAndDeleteTask()
        {
            List<string> taskNames = dynamicStorage.getTasks().Select(task => task.Name).ToList();
            string userInput;

            while (true)
            {
                taskInterface.DisplayAllTasks(dynamicStorage.getTasks());

                userInput = CLIHelper.PromptForNotEmptyInput("Enter the task name: ");

                if (taskNames.Contains(userInput))
                {
                    return dynamicStorage.RemoveTask(userInput);
                }
            }
        }

        /// <summary>
        /// Prompts the user for a task to edit, then prompts for changes, then edits the task.
        /// </summary>
        public void PromptAndEditTask()
        {
            List<string> taskNames = dynamicStorage.getTasks().Select(task => task.Name).ToList();
            TaskObject? task = null;
            string userInput;

            while (task == null)
            {
                taskInterface.DisplayAllTasks(dynamicStorage.getTasks());

                userInput = CLIHelper.PromptForNotEmptyInput("Enter the task name: ");
                if (dynamicStorage.TaskExists(userInput))
                {
                    task = dynamicStorage.GetTaskObject(userInput);
                }
            }

            string name = task.Name;
            int importance = task.Importance;
            bool isGood = task.IsGood;
            string description = task.Description;
            RecurrenceEnum recurrence = task.Recurrence;
            string recurrenceAsString = recurrence.ToString();

            int occurrences = task.Occurrence;

            taskInterface.PromptForTaskCorrection(ref name, ref importance, ref isGood, ref description, ref recurrenceAsString, ref occurrences);

            recurrence = StringToRecurrence(recurrenceAsString);

            task.Edit(name, importance, isGood, description, recurrence, occurrences);
        }

        /// <summary>
        /// Prompts the user for a task to mark as complete, then marks the task as complete.
        /// </summary>
        /// <returns>If task was marked successfully.</returns>
        public bool PromptAndDoTask()
        {
            List<string> taskNames = dynamicStorage.getTasks().Select(task => task.Name).ToList();
            string userInput = "";

            while (!dynamicStorage.DoTask(userInput))
            {
                taskInterface.DisplayAllTasks(dynamicStorage.getTasks());
                userInput = CLIHelper.PromptForNotEmptyInput("Enter the task name: ");
            }

            return false;
        }

        /// <summary>
        /// Prompts the user for a task to mark as incomplete, then marks the task as incomplete.
        /// </summary>
        /// <returns>If task was marked successfully.</returns>
        public bool PromptAndUndoTask()
        {
            List<string> taskNames = dynamicStorage.getTasks().Select(task => task.Name).ToList();
            string userInput = "";

            while (!dynamicStorage.UndoTask(userInput))
            {
                taskInterface.DisplayAllTasks(dynamicStorage.getTasks());
                userInput = CLIHelper.PromptForNotEmptyInput("Enter the task name: ");
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
