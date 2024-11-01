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
            string name, description;
            bool isGood;
            int? importance = null;

            Console.WriteLine("You can make changes to the habit after answering the following prompts.");
            Console.Write("Enter the name of the habit: ");

            string namePrompt = "Enter the name of the habit: ";
            name = InterfaceHelper.PromptForNotEmptyInput(namePrompt);

            string descriptionPrompt = "Enter the description of the habit: ";
            description = InterfaceHelper.PromptForNotEmptyInput(descriptionPrompt);

            string isGoodPrompt = "Is the habit a good habit? ";
            isGood = InterfaceHelper.PromptForTrueFalseInput(isGoodPrompt);

            string importancePrompt = "If 1 is trivial and 5 is of utmost importance, enter the digit that represents the importance of this habit: ";
            while ((importance = InterfaceHelper.PromptForIntInput(importancePrompt)) == null || 1 > importance || importance > 5)
            {
                Console.Clear();
                Console.WriteLine("Input was not valid, try again!");
            }

            this.habitManager.AddHabit(new HabitObject(name, isGood, description, (int)importance));

            return true;
        }
    }
}
