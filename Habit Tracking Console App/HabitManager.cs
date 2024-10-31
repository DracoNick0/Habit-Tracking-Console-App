namespace Habit_Tracking_Console_App
{
    class HabitManager
    {
        Dictionary<string, HabitObject> habits;

        public HabitManager()
        {
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

        public bool CreateHabit(string name, bool isGood, string description, int importance, DateTime endDate, bool completed)
        {
            return AddHabit(new HabitObject(name, isGood, description, importance, endDate, completed));
        }
    }
}
