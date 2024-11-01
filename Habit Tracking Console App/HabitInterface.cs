using System.Diagnostics.Metrics;

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
            //string name, description;
            //DateTime endDate;
            //bool isGood;
            //int importance;

            Console.WriteLine("You can make changes to the habit after answering the following prompts.");
            Console.Write("Enter the name of the habit: ");
            //name = InterfaceHelper.PromptForNotEmptyInput("Enter the name of the habit: ");
            //description = InterfaceHelper.PromptForNotEmptyInput("Enter the description of the habit: ");

            return true;
        }
    }
}
