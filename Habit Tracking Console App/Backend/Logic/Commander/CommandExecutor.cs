using Task_Tracking_Console_App.Backend.Objects;
using Task_Tracking_Console_App.Backend.Storage;
using Habit_Tracking_Console_App.Frontend;
using Habit_Tracking_Console_App.Backend.Objects;
using Habit_Tracking_Console_App.Backend.Logic.Commander;

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
            string name = this.taskIO.PromptAndGetNewTaskName();
            string description = this.taskIO.PromptAndGetDescription();
            bool isGood = this.taskIO.PromptAndGetIsGood();
            int importance = this.taskIO.PromptAndGetImportance();
            DateTime dueDate = this.taskIO.PromptAndGetDueDate();
            RecurrenceEnum recurrence = this.taskIO.PromptAndGetRecurrence();
            int occurrence = this.taskIO.PromptAndGetOccurrence();

            // Prompt user to correct any mistakes in task details.
            this.taskIO.PromptAndGetTaskCorrection(ref name, ref importance, ref isGood, ref description, ref recurrence, ref occurrence);

            // Create task.
            return this.dynamicStorage.CreateTask(name, importance, isGood, description, recurrence, occurrence, dueDate);
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
            List<string> taskNames = this.dynamicStorage.getTasks().Select(task => task.Name).ToList();
            string userInput;

            while (true)
            {
                this.taskIO.DisplayAllTasks(this.dynamicStorage.getTasks());

                userInput = IO.PromptForNotEmptyInput("Enter the task name: ");

                if (taskNames.Contains(userInput))
                {
                    return this.dynamicStorage.RemoveTask(userInput);
                }
            }
        }

        /// <summary>
        /// Prompts the user for a task to edit, then prompts for changes, then edits the task.
        /// </summary>
        public void PromptAndEditTask()
        {
            List<string> taskNames = this.dynamicStorage.getTasks().Select(task => task.Name).ToList();
            TaskObject? task = null;
            string userInput;

            while (task == null)
            {
                this.taskIO.DisplayAllTasks(this.dynamicStorage.getTasks());

                userInput = IO.PromptForNotEmptyInput("Enter the task name: ");
                if (this.dynamicStorage.TaskExists(userInput))
                {
                    task = this.dynamicStorage.GetTaskObject(userInput);
                }
            }

            // Get new task details.
            string name = this.taskIO.PromptAndGetNewTaskName();
            string description = this.taskIO.PromptAndGetDescription();
            bool isGood = this.taskIO.PromptAndGetIsGood();
            int importance = this.taskIO.PromptAndGetImportance();
            DateTime dueDate = this.taskIO.PromptAndGetDueDate();
            RecurrenceEnum recurrence = this.taskIO.PromptAndGetRecurrence();
            int occurrence = this.taskIO.PromptAndGetOccurrence();

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
            List<string> taskNames = this.dynamicStorage.getTasks().Select(task => task.Name).ToList();
            string userInput = "";

            while (!this.dynamicStorage.DoTask(userInput))
            {
                this.taskIO.DisplayAllTasks(this.dynamicStorage.getTasks());
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
            List<string> taskNames = this.dynamicStorage.getTasks().Select(task => task.Name).ToList();
            string userInput = "";

            while (!this.dynamicStorage.UndoTask(userInput))
            {
                this.taskIO.DisplayAllTasks(this.dynamicStorage.getTasks());
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
