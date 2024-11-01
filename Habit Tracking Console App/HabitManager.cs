namespace Habit_Tracking_Console_App
{
    class HabitManager
    {
        private HabitInterface habitInterface;
        Dictionary<string, HabitObject> habits;

        public HabitManager()
        {
            this.habitInterface = new HabitInterface();
            this.habits = new Dictionary<string, HabitObject>();
        }

        public bool AddHabit(HabitObject habit)
        {
            if (!this.habits.ContainsKey(habit.Name))
            {
                this.habits.Add(habit.Name, habit);
                Console.WriteLine("Habit \"" + habit.Name + "\" was added.");
                return true;
            }
            else
            {
                Console.Error.WriteLine("A habit with the same name already exists, please try again.");
            }


            return false;
        }

        public bool CreateHabit(string name, bool isGood, string description, int importance)
        {
            HabitObject newHabit = habitInterface.HabitCreator();
            return AddHabit(newHabit);
        }
    }
}
