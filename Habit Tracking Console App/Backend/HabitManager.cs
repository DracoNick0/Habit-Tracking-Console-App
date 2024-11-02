using Habit_Tracking_Console_App.Backend.Storage;
using Habit_Tracking_Console_App.Interface;
using Habit_Tracking_Console_App.Objects;

namespace Habit_Tracking_Console_App.Backend
{
    class HabitManager
    {
        private StorageManager storageManager;
        private Dictionary<string, HabitObject> habits;

        public HabitManager()
        {
            this.storageManager = new StorageManager();
            this.habits = this.storageManager.RetrieveHabits().ToDictionary(habit => habit.Name);
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
                this.habits.Remove(habitName);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SaveHabits()
        {
            this.storageManager.SaveHabits(new List<HabitObject>(habits.Values));
        }

        public List<HabitObject> getHabits()
        {
            return new List<HabitObject>(this.habits.Values);
        }
    }
}
