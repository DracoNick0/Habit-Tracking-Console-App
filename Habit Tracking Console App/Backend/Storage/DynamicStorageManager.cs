using Habit_Tracking_Console_App.Backend.Objects;
using Habit_Tracking_Console_App.Frontend.PrintHelpers;
using Task_Tracking_Console_App.Backend.Objects;
using Task_Tracking_Console_App.Frontend.PrintHelpers;

namespace Task_Tracking_Console_App.Backend.Storage
{
    /// <summary>
    /// Manages tasks in a dynamic storage(dictionary).
    /// </summary>
    class DynamicStorageManager
    {
        private PersistentStorageManager persistentStorageManager;
        private Dictionary<string, TaskObject> tasks;

        public DynamicStorageManager()
        {
            persistentStorageManager = new PersistentStorageManager();
            tasks = persistentStorageManager.RetrieveTasks().ToDictionary(task => task.Name);
        }

        /// <summary>
        /// Takes all details of a task and creates a task with those details.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="importance">The importance of the task.</param>
        /// <param name="isGood">If the task is positive.</param>
        /// <param name="description">The description of the task.</param>
        /// <param name="recurrence">The recurrence of the task.</param>
        /// <param name="occurrence">The occurrences within the recurrence interval of the task.</param>
        /// <returns></returns>
        public bool CreateTask(string name, int importance, bool isGood, string description, RecurrenceEnum recurrence, int occurrence, DateTime dueDate)
        {
            if (!tasks.ContainsKey(name))
            {
                TaskObject newTask = new TaskObject(name, importance, isGood, description, recurrence, occurrence, dueDate);
                Add(newTask);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds a task object to the dynamic storage.
        /// </summary>
        /// <param name="task">The task object.</param>
        /// <returns>True if successfully added, otherwise false.</returns>
        public bool Add(TaskObject? task)
        {
            if (task != null)
            {
                if (!tasks.ContainsKey(task.Name))
                {
                    tasks.Add(task.Name, task);
                    return true;
                }
                else
                {
                    CLIHelper.Info("A task with the same name already exists, please try again.");
                }
            }

            return false;
        }

        /// <summary>
        /// Removes a task object from the dynamic storage.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns>True if successfully removed, otherwise false.</returns>
        public bool RemoveTask(string taskName)
        {
            if (tasks.ContainsKey(taskName))
            {
                tasks.Remove(taskName);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Calls the persistent storage manager to save all tasks.
        /// </summary>
        public void Save()
        {
            persistentStorageManager.SaveTasks(new List<TaskObject>(tasks.Values));
        }

        /// <summary>
        /// Marks the task with the specified name as complete.
        /// </summary>
        /// <param name="taskName">The tasks name.</param>
        /// <returns>If the task was successfully marked.</returns>
        public bool DoTask(string taskName)
        {
            if (tasks.ContainsKey(taskName))
            {
                ++tasks[taskName].Completions;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Marks the task with the specified name as incomplete.
        /// </summary>
        /// <param name="taskName">The tasks name.</param>
        /// <returns>If the task was successfully marked.</returns>
        public bool UndoTask(string taskName)
        {
            if (tasks.ContainsKey(taskName) && tasks[taskName].Completions > 0)
            {
                --tasks[taskName].Completions;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns if the task exists in the dictionary.
        /// </summary>
        /// <param name="taskName">The name of the task.</param>
        /// <returns>True if task exists, otherwise false.</returns>
        public bool TaskExists(string taskName)
        {
            return tasks.ContainsKey(taskName);
        }

        /// <summary>
        /// Returns the task object with the name specified.
        /// </summary>
        /// <param name="userInput">User input.</param>
        /// <returns>The task object, otherwise null.</returns>
        public TaskObject? GetTaskObject(string userInput)
        {
            if (tasks.ContainsKey(userInput))
            {
                return tasks[userInput];
            }

            return null;
        }

        /// <summary>
        /// Returns a list of all task objects.
        /// </summary>
        /// <returns>List of all task objects.</returns>
        public List<TaskObject> getTasks()
        {
            return new List<TaskObject>(tasks.Values);
        }
    }
}
