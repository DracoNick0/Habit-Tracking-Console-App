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

        public bool CreateHabit()
        {
            HabitObject newHabit = habitInterface.PromptForHabitCreation();
            CLIHelper.Info($"Habit {newHabit.Name} was created successfully.");
            return AddHabit(newHabit);
        }

        private bool AddHabit(HabitObject habit)
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
    }
}
