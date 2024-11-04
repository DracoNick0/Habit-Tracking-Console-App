using Task_Tracking_Console_App.Backend.Objects;
using Task_Tracking_Console_App.Backend.Storage;
using Habit_Tracking_Console_App.Frontend;
using Habit_Tracking_Console_App.Backend.Objects;
using Habit_Tracking_Console_App.Backend.Logic.Commander;
using Habit_Tracking_Console_App.Backend.Logic;

namespace Task_Tracking_Console_App.Backend.Logic.Commander
{
    class CommandExecutor
    {
        private DynamicStorageManager dynamicStorage;
        private TaskIO taskIO;

        public CommandExecutor(DynamicStorageManager dynamicStorage, TaskIO taskIO)
        {
            this.dynamicStorage = dynamicStorage;
            this.taskIO = taskIO;
        }

        /// <summary>
        /// Prompts the user for task details, then creates the task.
        /// </summary>
        /// <returns></returns>
        public bool PromptAndCreateTask()
        {
            IO.Info("You can make changes to the task after answering the following prompts.");

            // Get new task details.
            string name = taskIO.PromptAndGetNewTaskName();
            string description = taskIO.PromptAndGetDescription();
            bool isGood = taskIO.PromptAndGetIsGood();
            int importance = taskIO.PromptAndGetImportance();
            DateTime dueDate = taskIO.PromptAndGetDueDate();
            RecurrenceEnum recurrence = this.taskIO.PromptAndGetRecurrence();
            int occurrence = taskIO.PromptAndGetOccurrence();

            // Prompt user to correct any mistakes in task details.
            this.taskIO.PromptAndGetTaskCorrection(ref name, ref importance, ref isGood, ref description, ref recurrence, ref occurrence);

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
                taskIO.DisplayAllTasks(dynamicStorage.getTasks());

                userInput = IO.PromptForNotEmptyInput("Enter the task name: ");

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
                taskIO.DisplayAllTasks(dynamicStorage.getTasks());

                userInput = IO.PromptForNotEmptyInput("Enter the task name: ");
                if (dynamicStorage.TaskExists(userInput))
                {
                    task = dynamicStorage.GetTaskObject(userInput);
                }
            }

            // Get new task details.
            string name = taskIO.PromptAndGetNewTaskName();
            string description = taskIO.PromptAndGetDescription();
            bool isGood = taskIO.PromptAndGetIsGood();
            int importance = taskIO.PromptAndGetImportance();
            DateTime dueDate = taskIO.PromptAndGetDueDate();
            RecurrenceEnum recurrence = this.taskIO.PromptAndGetRecurrence();
            int occurrence = taskIO.PromptAndGetOccurrence();

            // Prompt user to correct any mistakes in task details.
            this.taskIO.PromptAndGetTaskCorrection(ref name, ref importance, ref isGood, ref description, ref recurrence, ref occurrence);

            task.Edit(name, importance, isGood, description, recurrence, occurrence);
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
                taskIO.DisplayAllTasks(dynamicStorage.getTasks());
                userInput = IO.PromptForNotEmptyInput("Enter the task name: ");
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
                taskIO.DisplayAllTasks(dynamicStorage.getTasks());
                userInput = IO.PromptForNotEmptyInput("Enter the task name: ");
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
