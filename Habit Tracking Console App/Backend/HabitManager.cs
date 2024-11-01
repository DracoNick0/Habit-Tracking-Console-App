using Habit_Tracking_Console_App.Interface;
using Habit_Tracking_Console_App.Objects;

namespace Habit_Tracking_Console_App.Backend
{
    class HabitManager
    {
        private HabitInterface habitInterface;
        Dictionary<string, HabitObject> habits;

        public HabitManager()
        {
            habitInterface = new HabitInterface();
            habits = new Dictionary<string, HabitObject>();
        }

        private bool AddHabit(HabitObject habit)
        {
            if (!habits.ContainsKey(habit.Name))
            {
                habits.Add(habit.Name, habit);
                Console.WriteLine("Habit \"" + habit.Name + "\" was added.");
                return true;
            }
            else
            {
                Console.Error.WriteLine("A habit with the same name already exists, please try again.");
            }


            return false;
        }

        public bool CreateHabit()
        {
            HabitObject newHabit = habitInterface.HabitCreator();
            return AddHabit(newHabit);
        }
    }
}
