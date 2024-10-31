namespace Habit_Tracking_Console_App
{
    class HabitInterface
    {
        HabitManager habitManager;

        public HabitInterface()
        {
            this.habitManager = new HabitManager();
        }

        public bool HabitCreator()
        {
            Console.WriteLine("You can make changes to the habit after the following prompts.");
            Console.Write("Enter the name of the habit: ");

            return true;
        }
    }
}
