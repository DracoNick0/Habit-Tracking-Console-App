using Habit_Tracking_Console_App.Model.Storage;
using Habit_Tracking_Console_App.View;

namespace Habit_Tracking_Console_App.Model
{
    class HabitManager
    {
        private StorageManager storageManager;
        private Dictionary<string, HabitObject> habits;

        public HabitManager()
        {
            storageManager = new StorageManager();
            habits = storageManager.RetrieveHabits().ToDictionary(habit => habit.Name);
        }

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

        public void SaveHabits()
        {
            storageManager.SaveHabits(new List<HabitObject>(habits.Values));
        }

        public List<HabitObject> getHabits()
        {
            return new List<HabitObject>(habits.Values);
        }
    }
}
