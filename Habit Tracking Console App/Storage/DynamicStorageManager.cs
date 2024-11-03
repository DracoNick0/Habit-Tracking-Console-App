using Habit_Tracking_Console_App.Objects;
using Habit_Tracking_Console_App.PrintHelpers;

namespace Habit_Tracking_Console_App.Storage
{
    /// <summary>
    /// Manages habits in a dynamic storage(dictionary).
    /// </summary>
    class DynamicStorageManager
    {
        private PersistentStorageManager persistentStorageManager;
        private Dictionary<string, HabitObject> habits;

        public DynamicStorageManager()
        {
            persistentStorageManager = new PersistentStorageManager();
            habits = persistentStorageManager.RetrieveHabits().ToDictionary(habit => habit.Name);
        }

        public bool CreateHabit(string name, int importance, bool isGood, string description, RecurrenceEnum recurrence, int occurrence)
        {
            if (!habits.ContainsKey(name))
            {
                HabitObject newHabit = new HabitObject(name, importance, isGood, description, recurrence, occurrence);
                Add(newHabit);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds a habit object to the dynamic storage.
        /// </summary>
        /// <param name="habit">The habit object.</param>
        /// <returns>True if successfully added, otherwise false.</returns>
        public bool Add(HabitObject? habit)
        {
            if (habit != null)
            {
                if (!habits.ContainsKey(habit.Name))
                {
                    habits.Add(habit.Name, habit);
                    return true;
                }
                else
                {
                    CLIHelper.Info("A habit with the same name already exists, please try again.");
                }
            }

            return false;
        }

        /// <summary>
        /// Removes a habit object from the dynamic storage.
        /// </summary>
        /// <param name="habitName">Name of the habit.</param>
        /// <returns>True if successfully removed, otherwise false.</returns>
        public bool RemoveHabit(string habitName)
        {
            if (habits.ContainsKey(habitName))
            {
                habits.Remove(habitName);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Calls the persistent storage manager to save all habits.
        /// </summary>
        public void Save()
        {
            persistentStorageManager.SaveHabits(new List<HabitObject>(habits.Values));
        }

        public bool DoHabit(string habitName)
        {
            if (habits.ContainsKey(habitName))
            {
                habits[habitName].Completed = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UndoHabit(string habitName)
        {
            if (habits.ContainsKey(habitName))
            {
                habits[habitName].Completed = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HabitExists(string habitName)
        {
            return habits.ContainsKey(habitName);
        }

        public HabitObject? GetHabitObject(string userInput)
        {
            if (habits.ContainsKey(userInput))
            {
                return habits[userInput];
            }

            return null;
        }

        /// <summary>
        /// Returns a list of all habit objects.
        /// </summary>
        /// <returns>List of all habit objects.</returns>
        public List<HabitObject> getHabits()
        {
            return new List<HabitObject>(habits.Values);
        }
    }
}
