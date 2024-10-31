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
            if (!this.habits.ContainsKey(habit.Habit))
            {
                this.habits.Add(habit.Habit, habit);
                Console.WriteLine("Habit \"" + habit.Habit + "\" was added.");
            }
            else
            {
                Console.Error.WriteLine("A habit with the same name already exists, please try again.");
            }


            return false;
        }
    }
}
