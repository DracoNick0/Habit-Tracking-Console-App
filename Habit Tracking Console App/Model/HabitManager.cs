using Habit_Tracking_Console_App.Model.Storage;
using Habit_Tracking_Console_App.View;

namespace Habit_Tracking_Console_App.Model
{
    /// <summary>
    /// Manages habits in a dynamic storage(dictionary).
    /// </summary>
    class HabitManager
    {
        private PersistentStorageManager storageManager;
        private Dictionary<string, HabitObject> habits;

        public HabitManager()
        {
            storageManager = new PersistentStorageManager();
            habits = storageManager.RetrieveHabits().ToDictionary(habit => habit.Name);
        }

        /// <summary>
        /// Adds a habit object to the dynamic storage.
        /// </summary>
        /// <param name="habit">The habit object.</param>
        /// <returns>True if successfully added, otherwise false.</returns>
        public bool AddHabit(HabitObject habit)
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
        public void SaveHabits()
        {
            storageManager.SaveHabits(new List<HabitObject>(habits.Values));
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
