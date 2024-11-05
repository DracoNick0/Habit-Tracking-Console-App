using Habit_Tracking_Console_App.Backend.Objects;
using Habit_Tracking_Console_App.Frontend;
using Task_Tracking_Console_App.Backend.Objects;
using Task_Tracking_Console_App.Backend.Storage;

namespace Habit_Tracking_Console_App.Backend.Logic.Object_Manager
{
    class TaskManager
    {
        DynamicStorageManager dynamicStorage;
        public TaskManager(DynamicStorageManager dynamicStorageManager)
        {
            this.dynamicStorage = dynamicStorageManager;
        }

        /// <summary>
        /// Prompts the user for task details, then creates the task.
        /// </summary>
        /// <returns></returns>
        public bool PromptAndCreateTask()
        {
            IO.Info("You can make changes to the task after answering the following prompts.");

            // Get new task details.
            string name = TaskIO.PromptAndGetNewTaskName();
            string description = TaskIO.PromptAndGetDescription();
            bool isGood = TaskIO.PromptAndGetIsGood();
            int difficulty = TaskIO.PromptAndGetDifficulty();
            DateTime dueDate = TaskIO.PromptAndGetDueDate();
            RecurrenceEnum recurrence = TaskIO.PromptAndGetRecurrence();
            int occurrence = TaskIO.PromptAndGetOccurrence();

            // Prompt user to correct any mistakes in task details.
            TaskIO.PromptAndGetTaskCorrection(ref name, ref difficulty, ref isGood, ref description, ref recurrence, ref occurrence);

            // Create task.
            return this.dynamicStorage.CreateTask(name, difficulty, isGood, description, recurrence, occurrence, dueDate);
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
                TaskIO.DisplayAllTasks(this.dynamicStorage.getTasks());

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
                TaskIO.DisplayAllTasks(this.dynamicStorage.getTasks());

                userInput = IO.PromptForNotEmptyInput("Enter the task name: ");
                if (this.dynamicStorage.TaskExists(userInput))
                {
                    task = this.dynamicStorage.GetTaskObject(userInput);
                }
            }

            // Get new task details.
            string name = TaskIO.PromptAndGetNewTaskName();
            string description = TaskIO.PromptAndGetDescription();
            bool isGood = TaskIO.PromptAndGetIsGood();
            int difficulty = TaskIO.PromptAndGetDifficulty();
            DateTime dueDate = TaskIO.PromptAndGetDueDate();
            RecurrenceEnum recurrence = TaskIO.PromptAndGetRecurrence();
            int occurrence = TaskIO.PromptAndGetOccurrence();

            // Prompt user to correct any mistakes in task details.
            TaskIO.PromptAndGetTaskCorrection(ref name, ref difficulty, ref isGood, ref description, ref recurrence, ref occurrence);

            task.Edit(name, difficulty, isGood, description, recurrence, occurrence);
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
                TaskIO.DisplayAllTasks(this.dynamicStorage.getTasks());
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
                TaskIO.DisplayAllTasks(this.dynamicStorage.getTasks());
                userInput = IO.PromptForNotEmptyInput("Enter the task name: ");
            }

            return false;
        }
    }
}
